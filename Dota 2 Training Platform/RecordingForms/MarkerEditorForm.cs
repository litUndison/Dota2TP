using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Dota_2_Training_Platform
{
    public partial class MarkerEditorForm : Form
    {
        public string MarkerTitle => markerTitleTextBox.Text?.Trim() ?? "";
        public string MarkerDescription => markerDescriptionTextBox.Text?.Trim() ?? "";
        public string MarkerColorHex => ColorTranslator.ToHtml(markerColorPreviewPanel.BackColor);
        public double MarkerSecond => (double)markerSecondNumeric.Value;

        public MarkerEditorForm()
        {
            InitializeComponent();
            pickColorButton.Click += PickColorButton_Click;
            saveMarkerButton.Click += SaveMarkerButton_Click;
            cancelMarkerButton.Click += (s, e) => DialogResult = DialogResult.Cancel;
        }

        public void SetData(string title, string description, double second, string colorHex)
        {
            markerTitleTextBox.Text = title ?? "";
            markerDescriptionTextBox.Text = description ?? "";

            if (second < 0) second = 0;
            if (second > (double)markerSecondNumeric.Maximum) second = (double)markerSecondNumeric.Maximum;
            markerSecondNumeric.Value = (decimal)second;

            markerColorPreviewPanel.BackColor = ParseColor(colorHex, Color.OrangeRed);
        }

        public void SetMaxSecond(double maxSecond)
        {
            if (maxSecond <= 0)
            {
                markerSecondNumeric.Maximum = 999999;
                return;
            }

            if (maxSecond > (double)decimal.MaxValue)
            {
                maxSecond = (double)decimal.MaxValue;
            }

            markerSecondNumeric.Maximum = (decimal)maxSecond;
            if (markerSecondNumeric.Value > markerSecondNumeric.Maximum)
            {
                markerSecondNumeric.Value = markerSecondNumeric.Maximum;
            }
        }

        private void PickColorButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new ColorDialog())
            {
                dialog.Color = markerColorPreviewPanel.BackColor;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    markerColorPreviewPanel.BackColor = dialog.Color;
                }
            }
        }

        private void SaveMarkerButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
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
    }
}
