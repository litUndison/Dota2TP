using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Dota_2_Training_Platform.Models;

namespace Dota_2_Training_Platform.Services
{
    public class ScreenRecorderService
    {
        private Process _ffmpegProcess;
        private string _currentVideoPath;
        private string _currentPreviewPath;
        private string _currentMetadataPath;
        private MatchRecordInfo _currentInfo;
        private readonly object _lock = new object();
        private readonly StringBuilder _stderrBuffer = new StringBuilder();

        public bool IsRecording => _ffmpegProcess != null && !_ffmpegProcess.HasExited;

        public string FfmpegPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg", "ffmpeg.exe");

        public string GetLastFfmpegLog(int maxChars = 4000)
        {
            lock (_lock)
            {
                if (_stderrBuffer.Length <= maxChars)
                    return _stderrBuffer.ToString();
                return _stderrBuffer.ToString(_stderrBuffer.Length - maxChars, maxChars);
            }
        }

        public RecordingStartResult StartRecording(string teamFolderPath, RecordSettingsModel settings)
        {
            try
            {
                if (IsRecording)
                {
                    return new RecordingStartResult
                    {
                        Success = false,
                        ErrorMessage = "Запись уже идёт."
                    };
                }

                if (!File.Exists(FfmpegPath))
                {
                    return new RecordingStartResult
                    {
                        Success = false,
                        ErrorMessage = $"Не найден ffmpeg: {FfmpegPath}"
                    };
                }

                Directory.CreateDirectory(teamFolderPath);

                string fileNameWithoutExtension = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                _currentVideoPath = Path.Combine(teamFolderPath, fileNameWithoutExtension + ".mp4");
                _currentPreviewPath = Path.Combine(teamFolderPath, fileNameWithoutExtension + ".jpg");
                _currentMetadataPath = Path.Combine(teamFolderPath, fileNameWithoutExtension + ".json");

                // По требованиям: без звука, фиксированное разрешение 1920x1080.
                int fps = settings != null && settings.Fps == 60 ? 60 : 30;
                string inputArgs = $"-video_size 1920x1080 -f gdigrab -framerate {fps} -i desktop";

                string outputArgs =
                    "-c:v libx264 " +
                    "-preset ultrafast " +
                    "-pix_fmt yuv420p " +
                    "-movflags +faststart " +
                    "-an ";

                string fullArguments = $"{inputArgs} {outputArgs} -y \"{_currentVideoPath}\"";

                _currentInfo = new MatchRecordInfo
                {
                    FileName = Path.GetFileName(_currentVideoPath),
                    VideoPath = _currentVideoPath,
                    PreviewPath = _currentPreviewPath,
                    CreatedAt = DateTime.Now,
                    Fps = fps,
                    Resolution = "1920x1080",
                    RecordAudio = false,
                    Hotkey = settings?.HotKey.ToString() ?? "",
                    FileSizeBytes = 0
                };

                lock (_lock)
                {
                    _stderrBuffer.Clear();
                }

                _ffmpegProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = FfmpegPath,
                        Arguments = fullArguments,
                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        RedirectStandardError = true,
                        RedirectStandardOutput = false,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    },
                    EnableRaisingEvents = true
                };

                _ffmpegProcess.Start();
                _ffmpegProcess.ErrorDataReceived += (s, e) =>
                {
                    if (e.Data == null) return;
                    lock (_lock)
                    {
                        _stderrBuffer.AppendLine(e.Data);
                        if (_stderrBuffer.Length > 200_000)
                        {
                            _stderrBuffer.Remove(0, 50_000);
                        }
                    }
                };
                _ffmpegProcess.BeginErrorReadLine();

                // Быстрая проверка: если ffmpeg упал сразу, вернём лог.
                Thread.Sleep(500);
                if (_ffmpegProcess.HasExited)
                {
                    string log = GetLastFfmpegLog();
                    _ffmpegProcess.Dispose();
                    _ffmpegProcess = null;
                    return new RecordingStartResult
                    {
                        Success = false,
                        ErrorMessage = "FFmpeg завершился сразу после старта.\n\n" + log
                    };
                }

                return new RecordingStartResult
                {
                    Success = true,
                    VideoPath = _currentVideoPath,
                    PreviewPath = _currentPreviewPath,
                    MetadataPath = _currentMetadataPath,
                    FileNameWithoutExtension = fileNameWithoutExtension
                };
            }
            catch (Exception ex)
            {
                return new RecordingStartResult
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<bool> StopRecordingAsync()
        {
            try
            {
                if (_ffmpegProcess == null || _ffmpegProcess.HasExited)
                    return false;

                await _ffmpegProcess.StandardInput.WriteLineAsync("q");
                await _ffmpegProcess.StandardInput.FlushAsync();

                await Task.Run(() => _ffmpegProcess.WaitForExit(20000));

                if (!_ffmpegProcess.HasExited)
                    _ffmpegProcess.Kill();

                _ffmpegProcess.Dispose();
                _ffmpegProcess = null;

                if (_currentInfo != null && File.Exists(_currentVideoPath))
                {
                    var fileInfo = new FileInfo(_currentVideoPath);
                    _currentInfo.FileSizeBytes = fileInfo.Length;

                    await GeneratePreviewAsync(_currentVideoPath, _currentPreviewPath);
                    await SaveMetadataAsync(_currentMetadataPath, _currentInfo);
                }

                return true;
            }
            catch
            {
                try
                {
                    if (_ffmpegProcess != null && !_ffmpegProcess.HasExited)
                        _ffmpegProcess.Kill();
                }
                catch { }

                return false;
            }
        }

        private async Task GeneratePreviewAsync(string videoPath, string previewPath)
        {
            if (!File.Exists(FfmpegPath) || !File.Exists(videoPath))
                return;

            async Task TryGenerate(string seek)
            {
                string args = $"-ss {seek} -i \"{videoPath}\" -frames:v 1 -q:v 2 -y \"{previewPath}\"";
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = FfmpegPath,
                        Arguments = args,
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };

                    process.Start();
                    await Task.Run(() => process.WaitForExit());
                }
            }

            await TryGenerate("00:00:03");
            if (!File.Exists(previewPath))
            {
                await TryGenerate("00:00:00.5");
            }
        }

        private async Task SaveMetadataAsync(string metadataPath, MatchRecordInfo info)
        {
            var json = JsonSerializer.Serialize(info, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await Task.Run(() => File.WriteAllText(metadataPath, json, Encoding.UTF8));
        }
    }
}