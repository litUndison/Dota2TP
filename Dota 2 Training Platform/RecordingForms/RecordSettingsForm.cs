using Dota_2_Training_Platform.Models;
using System;
using System.Windows.Forms;

namespace Dota_2_Training_Platform
{
    public partial class RecordSettingsForm : Form
    {
        public RecordSettingsForm()
        {
            InitializeComponent();
            for (Keys key = Keys.F1; key <= Keys.F12; key++)
            {
                cmbHotKey.Items.Add(key);
                cmbMarkerHotKey.Items.Add(key);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cmbFps.SelectedItem == null)
            {
                MessageBox.Show("Выбери FPS.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbResolution.SelectedItem == null)
            {
                MessageBox.Show("Выбери разрешение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbHotKey.SelectedItem == null)
            {
                MessageBox.Show("Выбери горячую клавишу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbMarkerHotKey.SelectedItem == null)
            {
                MessageBox.Show("Выбери горячую клавишу маркера.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((Keys)cmbHotKey.SelectedItem == (Keys)cmbMarkerHotKey.SelectedItem)
            {
                MessageBox.Show("Горячая клавиша записи и маркера не должны совпадать.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public RecordSettingsModel GetSettings()
        {
            return new RecordSettingsModel
            {
                Fps = int.Parse(cmbFps.SelectedItem.ToString()),
                Resolution = cmbResolution.SelectedItem.ToString(),
                RecordAudio = chkRecordAudio.Checked,
                HotKey = (Keys)cmbHotKey.SelectedItem,
                MarkerHotKey = (Keys)cmbMarkerHotKey.SelectedItem
            };
        }

        public void SetSettings(RecordSettingsModel settings)
        {
            if (settings == null)
                return;

            cmbFps.SelectedItem = settings.Fps.ToString();
            cmbResolution.SelectedItem = settings.Resolution;
            chkRecordAudio.Checked = settings.RecordAudio;
            cmbHotKey.SelectedItem = settings.HotKey;
            cmbMarkerHotKey.SelectedItem = settings.MarkerHotKey;

            if (cmbFps.SelectedItem == null) cmbFps.SelectedIndex = 0;
            if (cmbResolution.SelectedItem == null) cmbResolution.SelectedItem = "1920x1080";
            if (cmbHotKey.SelectedItem == null) cmbHotKey.SelectedItem = Keys.F9;
            if (cmbMarkerHotKey.SelectedItem == null) cmbMarkerHotKey.SelectedItem = Keys.F8;
        }
    }
}