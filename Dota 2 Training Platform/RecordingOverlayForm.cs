using System.Drawing;
using System.Windows.Forms;

namespace Dota_2_Training_Platform
{
    public class RecordingOverlayForm : Form
    {
        public RecordingOverlayForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            ShowInTaskbar = false;
            BackColor = Color.Black;
            Opacity = 0.72;
            Width = 220;
            Height = 56;

            var screen = Screen.PrimaryScreen?.WorkingArea ?? new Rectangle(0, 0, 1920, 1080);
            Location = new Point(screen.Right - Width - 16, 16);

            var dot = new Label
            {
                Left = 12,
                Top = 16,
                Width = 24,
                Height = 24,
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Text = "●"
            };

            var text = new Label
            {
                Left = 42,
                Top = 18,
                Width = 166,
                Height = 22,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Text = "Идёт запись"
            };

            Controls.Add(dot);
            Controls.Add(text);
        }
    }
}
