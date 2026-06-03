namespace Dota_2_Training_Platform
{
    partial class TwitchSettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.cmbStreamCount = new System.Windows.Forms.ComboBox();
            this.lblStreamCount = new System.Windows.Forms.Label();
            this.cmbRefreshInterval = new System.Windows.Forms.ComboBox();
            this.lblRefreshInterval = new System.Windows.Forms.Label();
            this.chkAutoRefresh = new System.Windows.Forms.CheckBox();
            this.grpInfoPanel = new System.Windows.Forms.GroupBox();
            this.chkShowTags = new System.Windows.Forms.CheckBox();
            this.chkShowLanguage = new System.Windows.Forms.CheckBox();
            this.chkShowStartedAt = new System.Windows.Forms.CheckBox();
            this.chkShowViewers = new System.Windows.Forms.CheckBox();
            this.chkShowStreamer = new System.Windows.Forms.CheckBox();
            this.chkShowAvatar = new System.Windows.Forms.CheckBox();
            this.chkShowTitle = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpList.SuspendLayout();
            this.grpInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(16, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(145, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Настройки Twitch";
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.cmbStreamCount);
            this.grpList.Controls.Add(this.lblStreamCount);
            this.grpList.Controls.Add(this.cmbRefreshInterval);
            this.grpList.Controls.Add(this.lblRefreshInterval);
            this.grpList.Controls.Add(this.chkAutoRefresh);
            this.grpList.Location = new System.Drawing.Point(16, 46);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(416, 132);
            this.grpList.TabIndex = 1;
            this.grpList.TabStop = false;
            this.grpList.Text = "Список стримов";
            // 
            // cmbStreamCount
            // 
            this.cmbStreamCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStreamCount.FormattingEnabled = true;
            this.cmbStreamCount.Items.AddRange(new object[] {
            "5",
            "10",
            "20"});
            this.cmbStreamCount.Location = new System.Drawing.Point(16, 96);
            this.cmbStreamCount.Name = "cmbStreamCount";
            this.cmbStreamCount.Size = new System.Drawing.Size(384, 21);
            this.cmbStreamCount.TabIndex = 4;
            // 
            // lblStreamCount
            // 
            this.lblStreamCount.AutoSize = true;
            this.lblStreamCount.Location = new System.Drawing.Point(13, 80);
            this.lblStreamCount.Name = "lblStreamCount";
            this.lblStreamCount.Size = new System.Drawing.Size(177, 13);
            this.lblStreamCount.TabIndex = 3;
            this.lblStreamCount.Text = "Количество стримов в списке";
            // 
            // cmbRefreshInterval
            // 
            this.cmbRefreshInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRefreshInterval.FormattingEnabled = true;
            this.cmbRefreshInterval.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "30",
            "60",
            "120"});
            this.cmbRefreshInterval.Location = new System.Drawing.Point(16, 52);
            this.cmbRefreshInterval.Name = "cmbRefreshInterval";
            this.cmbRefreshInterval.Size = new System.Drawing.Size(384, 21);
            this.cmbRefreshInterval.TabIndex = 2;
            // 
            // lblRefreshInterval
            // 
            this.lblRefreshInterval.AutoSize = true;
            this.lblRefreshInterval.Location = new System.Drawing.Point(13, 36);
            this.lblRefreshInterval.Name = "lblRefreshInterval";
            this.lblRefreshInterval.Size = new System.Drawing.Size(198, 13);
            this.lblRefreshInterval.TabIndex = 1;
            this.lblRefreshInterval.Text = "Интервал автообновления (секунды)";
            // 
            // chkAutoRefresh
            // 
            this.chkAutoRefresh.AutoSize = true;
            this.chkAutoRefresh.Location = new System.Drawing.Point(16, 19);
            this.chkAutoRefresh.Name = "chkAutoRefresh";
            this.chkAutoRefresh.Size = new System.Drawing.Size(219, 17);
            this.chkAutoRefresh.TabIndex = 0;
            this.chkAutoRefresh.Text = "Автоматически обновлять список";
            this.chkAutoRefresh.UseVisualStyleBackColor = true;
            this.chkAutoRefresh.CheckedChanged += new System.EventHandler(this.ChkAutoRefresh_CheckedChanged);
            // 
            // grpInfoPanel
            // 
            this.grpInfoPanel.Controls.Add(this.chkShowTags);
            this.grpInfoPanel.Controls.Add(this.chkShowLanguage);
            this.grpInfoPanel.Controls.Add(this.chkShowStartedAt);
            this.grpInfoPanel.Controls.Add(this.chkShowViewers);
            this.grpInfoPanel.Controls.Add(this.chkShowStreamer);
            this.grpInfoPanel.Controls.Add(this.chkShowAvatar);
            this.grpInfoPanel.Controls.Add(this.chkShowTitle);
            this.grpInfoPanel.Location = new System.Drawing.Point(16, 190);
            this.grpInfoPanel.Name = "grpInfoPanel";
            this.grpInfoPanel.Size = new System.Drawing.Size(416, 196);
            this.grpInfoPanel.TabIndex = 2;
            this.grpInfoPanel.TabStop = false;
            this.grpInfoPanel.Text = "Панель информации о стриме (снизу)";
            // 
            // chkShowTags
            // 
            this.chkShowTags.AutoSize = true;
            this.chkShowTags.Location = new System.Drawing.Point(16, 166);
            this.chkShowTags.Name = "chkShowTags";
            this.chkShowTags.Size = new System.Drawing.Size(50, 17);
            this.chkShowTags.TabIndex = 6;
            this.chkShowTags.Text = "Теги";
            this.chkShowTags.UseVisualStyleBackColor = true;
            // 
            // chkShowLanguage
            // 
            this.chkShowLanguage.AutoSize = true;
            this.chkShowLanguage.Location = new System.Drawing.Point(16, 143);
            this.chkShowLanguage.Name = "chkShowLanguage";
            this.chkShowLanguage.Size = new System.Drawing.Size(57, 17);
            this.chkShowLanguage.TabIndex = 5;
            this.chkShowLanguage.Text = "Язык";
            this.chkShowLanguage.UseVisualStyleBackColor = true;
            // 
            // chkShowStartedAt
            // 
            this.chkShowStartedAt.AutoSize = true;
            this.chkShowStartedAt.Location = new System.Drawing.Point(16, 120);
            this.chkShowStartedAt.Name = "chkShowStartedAt";
            this.chkShowStartedAt.Size = new System.Drawing.Size(104, 17);
            this.chkShowStartedAt.TabIndex = 4;
            this.chkShowStartedAt.Text = "Время начала";
            this.chkShowStartedAt.UseVisualStyleBackColor = true;
            // 
            // chkShowViewers
            // 
            this.chkShowViewers.AutoSize = true;
            this.chkShowViewers.Location = new System.Drawing.Point(16, 97);
            this.chkShowViewers.Name = "chkShowViewers";
            this.chkShowViewers.Size = new System.Drawing.Size(74, 17);
            this.chkShowViewers.TabIndex = 3;
            this.chkShowViewers.Text = "Зрители";
            this.chkShowViewers.UseVisualStyleBackColor = true;
            // 
            // chkShowStreamer
            // 
            this.chkShowStreamer.AutoSize = true;
            this.chkShowStreamer.Location = new System.Drawing.Point(16, 74);
            this.chkShowStreamer.Name = "chkShowStreamer";
            this.chkShowStreamer.Size = new System.Drawing.Size(82, 17);
            this.chkShowStreamer.TabIndex = 2;
            this.chkShowStreamer.Text = "Стример";
            this.chkShowStreamer.UseVisualStyleBackColor = true;
            // 
            // chkShowAvatar
            // 
            this.chkShowAvatar.AutoSize = true;
            this.chkShowAvatar.Location = new System.Drawing.Point(16, 51);
            this.chkShowAvatar.Name = "chkShowAvatar";
            this.chkShowAvatar.Size = new System.Drawing.Size(68, 17);
            this.chkShowAvatar.TabIndex = 1;
            this.chkShowAvatar.Text = "Аватар";
            this.chkShowAvatar.UseVisualStyleBackColor = true;
            // 
            // chkShowTitle
            // 
            this.chkShowTitle.AutoSize = true;
            this.chkShowTitle.Location = new System.Drawing.Point(16, 28);
            this.chkShowTitle.Name = "chkShowTitle";
            this.chkShowTitle.Size = new System.Drawing.Size(136, 17);
            this.chkShowTitle.TabIndex = 0;
            this.chkShowTitle.Text = "Название трансляции";
            this.chkShowTitle.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(253, 400);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(179, 34);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(16, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(179, 34);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // TwitchSettingsForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(448, 448);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpInfoPanel);
            this.Controls.Add(this.grpList);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TwitchSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки Twitch";
            this.grpList.ResumeLayout(false);
            this.grpList.PerformLayout();
            this.grpInfoPanel.ResumeLayout(false);
            this.grpInfoPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.ComboBox cmbStreamCount;
        private System.Windows.Forms.Label lblStreamCount;
        private System.Windows.Forms.ComboBox cmbRefreshInterval;
        private System.Windows.Forms.Label lblRefreshInterval;
        private System.Windows.Forms.CheckBox chkAutoRefresh;
        private System.Windows.Forms.GroupBox grpInfoPanel;
        private System.Windows.Forms.CheckBox chkShowTags;
        private System.Windows.Forms.CheckBox chkShowLanguage;
        private System.Windows.Forms.CheckBox chkShowStartedAt;
        private System.Windows.Forms.CheckBox chkShowViewers;
        private System.Windows.Forms.CheckBox chkShowStreamer;
        private System.Windows.Forms.CheckBox chkShowAvatar;
        private System.Windows.Forms.CheckBox chkShowTitle;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
