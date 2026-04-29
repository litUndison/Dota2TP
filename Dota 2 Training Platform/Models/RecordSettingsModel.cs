using System.Windows.Forms;

namespace Dota_2_Training_Platform.Models
{
    public class RecordSettingsModel
    {
        public int Fps { get; set; } = 30;
        public string Resolution { get; set; } = "1920x1080";
        public bool RecordAudio { get; set; } = true;
        public Keys HotKey { get; set; } = Keys.F9;
        public bool UseSameHotkeyForStop { get; set; } = true;
    }
}