using Dota_2_Training_Platform.Models;
using System;
using System.Windows.Forms;

namespace Dota_2_Training_Platform
{
    public partial class TwitchSettingsForm : Form
    {
        public TwitchSettingsForm()
        {
            InitializeComponent();
        }

        private void ChkAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            cmbRefreshInterval.Enabled = chkAutoRefresh.Checked;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cmbRefreshInterval.SelectedItem == null)
            {
                MessageBox.Show("Выберите интервал автообновления.", "Twitch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbStreamCount.SelectedItem == null)
            {
                MessageBox.Show("Выберите количество стримов.", "Twitch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        public TwitchSettingsModel GetSettings()
        {
            return new TwitchSettingsModel
            {
                AutoRefreshEnabled = chkAutoRefresh.Checked,
                RefreshIntervalSeconds = int.Parse(cmbRefreshInterval.SelectedItem.ToString()),
                StreamCount = int.Parse(cmbStreamCount.SelectedItem.ToString()),
                ShowTitle = chkShowTitle.Checked,
                ShowAvatar = chkShowAvatar.Checked,
                ShowStreamer = chkShowStreamer.Checked,
                ShowViewers = chkShowViewers.Checked,
                ShowStartedAt = chkShowStartedAt.Checked,
                ShowLanguage = chkShowLanguage.Checked,
                ShowTags = chkShowTags.Checked
            };
        }

        public void SetSettings(TwitchSettingsModel settings)
        {
            if (settings == null)
                settings = new TwitchSettingsModel();

            chkAutoRefresh.Checked = settings.AutoRefreshEnabled;
            cmbRefreshInterval.SelectedItem = settings.RefreshIntervalSeconds.ToString();
            cmbStreamCount.SelectedItem = settings.StreamCount.ToString();
            chkShowTitle.Checked = settings.ShowTitle;
            chkShowAvatar.Checked = settings.ShowAvatar;
            chkShowStreamer.Checked = settings.ShowStreamer;
            chkShowViewers.Checked = settings.ShowViewers;
            chkShowStartedAt.Checked = settings.ShowStartedAt;
            chkShowLanguage.Checked = settings.ShowLanguage;
            chkShowTags.Checked = settings.ShowTags;

            if (cmbRefreshInterval.SelectedItem == null)
                cmbRefreshInterval.SelectedIndex = 2;
            if (cmbStreamCount.SelectedItem == null)
                cmbStreamCount.SelectedIndex = 0;

            cmbRefreshInterval.Enabled = chkAutoRefresh.Checked;
        }
    }
}
