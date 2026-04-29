using System;

namespace Dota_2_Training_Platform.Models
{
    public class MatchRecordInfo
    {
        public string FileName { get; set; }
        public string VideoPath { get; set; }
        public string PreviewPath { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Fps { get; set; }
        public string Resolution { get; set; }
        public bool RecordAudio { get; set; }
        public string Hotkey { get; set; }
        public long FileSizeBytes { get; set; }
    }
}
