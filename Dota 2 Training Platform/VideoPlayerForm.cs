using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dota_2_Training_Platform
{
    public partial class VideoPlayerForm : Form
    {
        private readonly string _videoPath;
        private readonly string _momentsPath;
        private Timer _stateTimer;
        private bool _isSeeking;
        private bool _isPlaying = true;
        private double _durationSec;
        private double _currentSec;
        private double _lastRenderedDurationSec = -1;
        private List<VideoMoment> _moments = new List<VideoMoment>();
        private ToolTip _markersToolTip;

        public VideoPlayerForm(string videoPath)
        {
            _videoPath = videoPath;
            _momentsPath = Path.ChangeExtension(videoPath, ".moments.json");
            InitializeComponent();
            WireEvents();
            _stateTimer = new Timer { Interval = 80 };
            _stateTimer.Tick += async (s, e) => await RefreshPlayerStateAsync();
            _markersToolTip = new ToolTip
            {
                ShowAlways = true,
                InitialDelay = 150,
                ReshowDelay = 100,
                AutoPopDelay = 3000
            };
        }

        private void WireEvents()
        {
            _playPauseButton.Click += async (s, e) => await TogglePlayPauseAsync();
            _addMomentButton.Click += AddMomentButton_Click;
            _editMomentButton.Click += EditMomentButton_Click;
            _deleteMomentButton.Click += DeleteMomentButton_Click;
            _timeline.MouseDown += (s, e) => _isSeeking = true;
            _timeline.MouseUp += async (s, e) =>
            {
                await SeekToTrackbarAsync();
                _isSeeking = false;
            };
            _momentsList.DoubleClick += async (s, e) => await SeekToSelectedMomentAsync();
            _momentsList.SelectedIndexChanged += (s, e) => UpdateMomentActionButtonsState();
            _momentsList.Leave += (s, e) =>
            {
                if (_editMomentButton != null && _editMomentButton.Focused)
                {
                    return;
                }

                if (_deleteMomentButton != null && _deleteMomentButton.Focused)
                {
                    return;
                }

                _momentsList.SelectedItems.Clear();
                UpdateMomentActionButtonsState();
            };
            Load += VideoPlayerForm_Load;
            FormClosing += (s, e) => _stateTimer.Stop();
            Resize += (s, e) => RenderMomentMarkers();

            UpdateMomentActionButtonsState();
        }

        private async void VideoPlayerForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists(_videoPath))
            {
                MessageBox.Show("Файл видео не найден.", "Просмотр", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            try
            {
                await _webView.EnsureCoreWebView2Async();

                string videoDirectory = Path.GetDirectoryName(_videoPath);
                string videoFileName = Path.GetFileName(_videoPath);
                string hostName = "dota2tp.video";
                _webView.CoreWebView2.SetVirtualHostNameToFolderMapping(
                    hostName,
                    videoDirectory,
                    CoreWebView2HostResourceAccessKind.Allow);
                string videoUrl = $"https://{hostName}/{Uri.EscapeDataString(videoFileName)}";
                _webView.Source = new Uri(videoUrl);
                LoadMoments();
                RenderMomentsList();
                RenderMomentMarkers();
                _stateTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось открыть видео во встроенном плеере.\n\n" + ex.Message,
                    "Просмотр",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private async Task RefreshPlayerStateAsync()
        {
            if (_webView?.CoreWebView2 == null) return;

            try
            {
                await _webView.ExecuteScriptAsync(
                    "(function(){var v=document.querySelector('video'); if(v){v.controls=false;}})()");

                string raw = await _webView.ExecuteScriptAsync(
                    "(function(){var v=document.querySelector('video'); if(!v){return null;} return {currentTime:v.currentTime||0,duration:v.duration||0,paused:!!v.paused};})()");
                if (string.IsNullOrWhiteSpace(raw))
                    return;

                using (var doc = JsonDocument.Parse(raw))
                {
                    if (doc.RootElement.ValueKind == JsonValueKind.Null)
                        return;
                    _currentSec = doc.RootElement.GetProperty("currentTime").GetDouble();
                    _durationSec = doc.RootElement.GetProperty("duration").GetDouble();
                    _isPlaying = !doc.RootElement.GetProperty("paused").GetBoolean();
                }

                _playPauseButton.Text = _isPlaying ? "Пауза" : "Пуск";
                _timeLabel.Text = $"{FormatTime(_currentSec)} / {FormatTime(_durationSec)}";

                if (!_isSeeking && _durationSec > 0)
                {
                    int max = Math.Max(1, _timeline.Maximum);
                    int value = (int)Math.Max(0, Math.Min(max, (_currentSec / _durationSec) * max));
                    _timeline.Value = value;
                }

                // Когда длительность появляется не сразу, перерисовываем маркеры после её получения.
                if (_durationSec > 0 && Math.Abs(_durationSec - _lastRenderedDurationSec) > 0.01)
                {
                    _lastRenderedDurationSec = _durationSec;
                    RenderMomentMarkers();
                }
            }
            catch
            {
            }
        }

        private async Task TogglePlayPauseAsync()
        {
            if (_webView?.CoreWebView2 == null) return;
            try
            {
                await _webView.ExecuteScriptAsync(
                    "(function(){var v=document.querySelector('video'); if(!v){return false;} if(v.paused){v.play();} else {v.pause();} return !v.paused;})()");
            }
            catch
            {
            }
        }

        private async Task SeekToTrackbarAsync()
        {
            if (_webView?.CoreWebView2 == null || _durationSec <= 0) return;
            double sec = (_timeline.Value / (double)Math.Max(1, _timeline.Maximum)) * _durationSec;
            await SeekToSecondAsync(sec);
        }

        private async Task SeekToSecondAsync(double sec)
        {
            if (_webView?.CoreWebView2 == null) return;
            string value = sec.ToString("0.###", CultureInfo.InvariantCulture);
            await _webView.ExecuteScriptAsync(
                $"(function(){{var v=document.querySelector('video'); if(v){{v.currentTime={value};}} }})()");
        }

        private async void AddMomentButton_Click(object sender, EventArgs e)
        {
            var moment = ShowMomentEditor(null, _currentSec);
            if (moment == null) return;

            _moments.Add(moment);
            _moments = _moments.OrderBy(m => m.Second).ToList();
            SaveMoments();
            RenderMomentsList();
            RenderMomentMarkers();
            await SeekToSecondAsync(moment.Second);
        }

        private void EditMomentButton_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMoment();
            if (selected == null) return;

            var edited = ShowMomentEditor(selected, selected.Second);
            if (edited == null) return;

            selected.Title = edited.Title;
            selected.Description = edited.Description;
            selected.ColorHex = edited.ColorHex;
            selected.Second = edited.Second;
            _moments = _moments.OrderBy(m => m.Second).ToList();
            SaveMoments();
            RenderMomentsList();
            RenderMomentMarkers();
        }

        private void DeleteMomentButton_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMoment();
            if (selected == null) return;

            if (MessageBox.Show("Удалить заметку?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            _moments.Remove(selected);
            SaveMoments();
            RenderMomentsList();
            RenderMomentMarkers();
        }

        private async Task SeekToSelectedMomentAsync()
        {
            var selected = GetSelectedMoment();
            if (selected == null) return;
            await SeekToSecondAsync(selected.Second);
        }

        private VideoMoment GetSelectedMoment()
        {
            if (_momentsList.SelectedItems.Count == 0) return null;
            return _momentsList.SelectedItems[0].Tag as VideoMoment;
        }

        private void RenderMomentsList()
        {
            _momentsList.Items.Clear();
            foreach (var moment in _moments.OrderBy(m => m.Second))
            {
                Color markerColor = ParseColor(moment.ColorHex, Color.OrangeRed);
                var item = new ListViewItem(FormatTime(moment.Second));
                item.UseItemStyleForSubItems = false;
                var colorSubItem = new ListViewItem.ListViewSubItem(item, "      ")
                {
                    BackColor = markerColor,
                    ForeColor = markerColor
                };
                item.SubItems.Add(colorSubItem);
                item.SubItems.Add(moment.Title ?? "");
                item.SubItems.Add(moment.Description ?? "");
                item.Tag = moment;
                _momentsList.Items.Add(item);
            }
            UpdateMomentActionButtonsState();
        }

        private void RenderMomentMarkers()
        {
            _markersPanel.Controls.Clear();
            if (_durationSec <= 0 || _markersPanel.Width <= 0) return;

            foreach (var moment in _moments)
            {
                Color markerColor = ParseColor(moment.ColorHex, Color.OrangeRed);
                var marker = new Button
                {
                    Width = 4,
                    Height = _markersPanel.Height,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = markerColor,
                    Text = ""
                };
                marker.FlatAppearance.BorderSize = 0;
                marker.Cursor = Cursors.Hand;
                int x = (int)Math.Round((moment.Second / _durationSec) * Math.Max(0, _markersPanel.Width - marker.Width));
                marker.Left = x;
                marker.Top = 0;
                marker.BringToFront();
                marker.Tag = moment;
                _markersToolTip.SetToolTip(marker, moment.Title ?? "");
                marker.Click += async (s, e) =>
                {
                    var m = ((Button)s).Tag as VideoMoment;
                    if (m != null)
                    {
                        SelectMomentInList(m);
                        await SeekToSecondAsync(m.Second);
                    }
                };
                _markersPanel.Controls.Add(marker);
            }
        }

        private void SelectMomentInList(VideoMoment moment)
        {
            if (moment == null || _momentsList == null)
            {
                return;
            }

            foreach (ListViewItem item in _momentsList.Items)
            {
                if (ReferenceEquals(item.Tag, moment))
                {
                    _momentsList.SelectedItems.Clear();
                    item.Selected = true;
                    item.Focused = true;
                    item.EnsureVisible();
                    break;
                }
            }
        }

        private void LoadMoments()
        {
            try
            {
                if (!File.Exists(_momentsPath))
                {
                    _moments = new List<VideoMoment>();
                    return;
                }

                var json = File.ReadAllText(_momentsPath);
                _moments = JsonSerializer.Deserialize<List<VideoMoment>>(json) ?? new List<VideoMoment>();
            }
            catch
            {
                _moments = new List<VideoMoment>();
            }
        }

        private void SaveMoments()
        {
            try
            {
                var json = JsonSerializer.Serialize(_moments, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_momentsPath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить заметки:\n" + ex.Message, "Заметки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private VideoMoment ShowMomentEditor(VideoMoment source, double defaultSecond)
        {
            using (var form = new MarkerEditorForm())
            {
                form.Text = source == null ? "Добавить маркер" : "Изменить маркер";
                form.SetMaxSecond(_durationSec > 0 ? _durationSec : 999999);
                form.SetData(source?.Title, source?.Description, source?.Second ?? defaultSecond, source?.ColorHex);
                if (form.ShowDialog(this) != DialogResult.OK) return null;

                double sec = form.MarkerSecond;
                if (sec < 0) sec = 0;
                if (_durationSec > 0 && sec > _durationSec) sec = _durationSec;

                return new VideoMoment
                {
                    Id = source?.Id ?? Guid.NewGuid().ToString("N"),
                    Title = form.MarkerTitle,
                    Description = form.MarkerDescription,
                    ColorHex = form.MarkerColorHex,
                    Second = sec
                };
            }
        }

        private Color ParseColor(string colorHex, Color fallback)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(colorHex))
                {
                    return fallback;
                }

                return ColorTranslator.FromHtml(colorHex);
            }
            catch
            {
                return fallback;
            }
        }

        private string FormatTime(double sec)
        {
            if (sec < 0) sec = 0;
            var ts = TimeSpan.FromSeconds(sec);
            return ts.TotalHours >= 1
                ? ts.ToString(@"hh\:mm\:ss")
                : ts.ToString(@"mm\:ss");
        }

        private void UpdateMomentActionButtonsState()
        {
            bool hasSelection = _momentsList != null && _momentsList.SelectedItems.Count > 0;
            if (_editMomentButton != null)
            {
                _editMomentButton.Enabled = hasSelection;
            }

            if (_deleteMomentButton != null)
            {
                _deleteMomentButton.Enabled = hasSelection;
            }
        }

        private class VideoMoment
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string ColorHex { get; set; }
            public double Second { get; set; }
        }
    }
}
