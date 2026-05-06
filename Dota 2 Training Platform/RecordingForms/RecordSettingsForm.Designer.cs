namespace Dota_2_Training_Platform
{
    partial class RecordSettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFps = new System.Windows.Forms.Label();
            this.cmbFps = new System.Windows.Forms.ComboBox();
            this.lblResolution = new System.Windows.Forms.Label();
            this.cmbResolution = new System.Windows.Forms.ComboBox();
            this.chkRecordAudio = new System.Windows.Forms.CheckBox();
            this.lblHotKey = new System.Windows.Forms.Label();
            this.cmbHotKey = new System.Windows.Forms.ComboBox();
            this.lblMarkerHotKey = new System.Windows.Forms.Label();
            this.cmbMarkerHotKey = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(22, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(163, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Настройки записи";
            // 
            // lblFps
            // 
            this.lblFps.AutoSize = true;
            this.lblFps.Location = new System.Drawing.Point(24, 65);
            this.lblFps.Name = "lblFps";
            this.lblFps.Size = new System.Drawing.Size(27, 13);
            this.lblFps.TabIndex = 1;
            this.lblFps.Text = "FPS";
            // 
            // cmbFps
            // 
            this.cmbFps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFps.FormattingEnabled = true;
            this.cmbFps.Items.AddRange(new object[] {
            "30",
            "60"});
            this.cmbFps.Location = new System.Drawing.Point(27, 84);
            this.cmbFps.Name = "cmbFps";
            this.cmbFps.Size = new System.Drawing.Size(382, 21);
            this.cmbFps.TabIndex = 2;
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Location = new System.Drawing.Point(24, 122);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(70, 13);
            this.lblResolution.TabIndex = 3;
            this.lblResolution.Text = "Разрешение";
            // 
            // cmbResolution
            // 
            this.cmbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResolution.FormattingEnabled = true;
            this.cmbResolution.Items.AddRange(new object[] {
            "Исходное",
            "1920x1080",
            "1280x720"});
            this.cmbResolution.Location = new System.Drawing.Point(27, 141);
            this.cmbResolution.Name = "cmbResolution";
            this.cmbResolution.Size = new System.Drawing.Size(382, 21);
            this.cmbResolution.TabIndex = 4;
            // 
            // chkRecordAudio
            // 
            this.chkRecordAudio.AutoSize = true;
            this.chkRecordAudio.Enabled = false;
            this.chkRecordAudio.Location = new System.Drawing.Point(27, 181);
            this.chkRecordAudio.Name = "chkRecordAudio";
            this.chkRecordAudio.Size = new System.Drawing.Size(213, 17);
            this.chkRecordAudio.TabIndex = 5;
            this.chkRecordAudio.Text = "Записывать системный звук (скоро)";
            this.chkRecordAudio.UseVisualStyleBackColor = true;
            this.chkRecordAudio.Visible = false;
            // 
            // lblHotKey
            // 
            this.lblHotKey.AutoSize = true;
            this.lblHotKey.Location = new System.Drawing.Point(24, 212);
            this.lblHotKey.Name = "lblHotKey";
            this.lblHotKey.Size = new System.Drawing.Size(134, 13);
            this.lblHotKey.TabIndex = 6;
            this.lblHotKey.Text = "Горячая клавиша записи";
            // 
            // cmbHotKey
            // 
            this.cmbHotKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHotKey.FormattingEnabled = true;
            this.cmbHotKey.Location = new System.Drawing.Point(27, 231);
            this.cmbHotKey.Name = "cmbHotKey";
            this.cmbHotKey.Size = new System.Drawing.Size(182, 21);
            this.cmbHotKey.TabIndex = 7;
            // 
            // lblMarkerHotKey
            // 
            this.lblMarkerHotKey.AutoSize = true;
            this.lblMarkerHotKey.Location = new System.Drawing.Point(227, 212);
            this.lblMarkerHotKey.Name = "lblMarkerHotKey";
            this.lblMarkerHotKey.Size = new System.Drawing.Size(142, 13);
            this.lblMarkerHotKey.TabIndex = 8;
            this.lblMarkerHotKey.Text = "Горячая клавиша маркера";
            // 
            // cmbMarkerHotKey
            // 
            this.cmbMarkerHotKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarkerHotKey.FormattingEnabled = true;
            this.cmbMarkerHotKey.Location = new System.Drawing.Point(230, 231);
            this.cmbMarkerHotKey.Name = "cmbMarkerHotKey";
            this.cmbMarkerHotKey.Size = new System.Drawing.Size(179, 21);
            this.cmbMarkerHotKey.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(230, 277);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(179, 34);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(27, 277);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(182, 34);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // RecordSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 329);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbMarkerHotKey);
            this.Controls.Add(this.lblMarkerHotKey);
            this.Controls.Add(this.cmbHotKey);
            this.Controls.Add(this.lblHotKey);
            this.Controls.Add(this.chkRecordAudio);
            this.Controls.Add(this.cmbResolution);
            this.Controls.Add(this.lblResolution);
            this.Controls.Add(this.cmbFps);
            this.Controls.Add(this.lblFps);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecordSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки записи";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFps;
        private System.Windows.Forms.ComboBox cmbFps;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.ComboBox cmbResolution;
        private System.Windows.Forms.CheckBox chkRecordAudio;
        private System.Windows.Forms.Label lblHotKey;
        private System.Windows.Forms.ComboBox cmbHotKey;
        private System.Windows.Forms.Label lblMarkerHotKey;
        private System.Windows.Forms.ComboBox cmbMarkerHotKey;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}