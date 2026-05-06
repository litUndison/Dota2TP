using System;
using System.Windows.Forms;

namespace Dota_2_Training_Platform
{
    public partial class RecordingOverlayForm : Form
    {
        private readonly Timer _closeTimer;

        public RecordingOverlayForm()
        {
            InitializeComponent();
            _closeTimer = new Timer { Interval = 1400 };
            _closeTimer.Tick += (s, e) =>
            {
                _closeTimer.Stop();
                Close();
            };
        }

        public void ShowToast(string message)
        {
            toastMessageLabel.Text = message ?? "";
            PositionInCorner();
            Show();
            BringToFront();
            _closeTimer.Stop();
            _closeTimer.Start();
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
                var cp = base.CreateParams;
                cp.ExStyle |= WS_EX_NOACTIVATE;
                return cp;
            }
        }
    }
}
