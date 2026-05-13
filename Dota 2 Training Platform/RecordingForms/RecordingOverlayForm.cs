using System;
using System.Windows.Forms;

namespace Dota_2_Training_Platform
{
    public partial class RecordingOverlayForm : Form
    {
        private readonly Timer _messageTimer;
        private const double IdleOpacity = 0;
        private const double ActiveOpacity = 0.60;

        public RecordingOverlayForm()
        {
            InitializeComponent();
            _messageTimer = new Timer { Interval = 1400 };
            _messageTimer.Tick += (s, e) =>
            {
                _messageTimer.Stop();
                toastMessageLabel.Text = string.Empty;
                Opacity = IdleOpacity;
            };

            Opacity = IdleOpacity;
        }

        public void EnsureVisible()
        {
            PositionInCorner();
            if (!Visible)
            {
                Show();
            }
            BringToFront();
        }

        public void ShowToast(string message)
        {
            toastMessageLabel.Text = message ?? "";
            EnsureVisible();
            Opacity = ActiveOpacity;
            _messageTimer.Stop();
            _messageTimer.Start();
        }

        private void PositionInCorner()
        {
            var screen = Screen.PrimaryScreen?.WorkingArea;
            if (screen == null)
            {
                return;
            }

            Left = screen.Value.Right - Width - 14;
            Top = screen.Value.Top + 14;
        }

        protected override bool ShowWithoutActivation => true;

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_NOACTIVATE = 0x08000000;
                const int WS_EX_LAYERED = 0x00080000;
                const int WS_EX_TRANSPARENT = 0x00000020;
                var cp = base.CreateParams;
                // Клики проходят сквозь окно (видно уведомление, но не перекрываются кнопки под ним).
                cp.ExStyle |= WS_EX_NOACTIVATE | WS_EX_LAYERED | WS_EX_TRANSPARENT;
                return cp;
            }
        }
    }
}
