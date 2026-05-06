namespace Dota_2_Training_Platform
{
    partial class MarkerEditorForm
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
            this.markerTitleLabel = new System.Windows.Forms.Label();
            this.markerTitleTextBox = new System.Windows.Forms.TextBox();
            this.markerDescriptionLabel = new System.Windows.Forms.Label();
            this.markerDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.markerSecondLabel = new System.Windows.Forms.Label();
            this.markerSecondNumeric = new System.Windows.Forms.NumericUpDown();
            this.markerColorLabel = new System.Windows.Forms.Label();
            this.markerColorPreviewPanel = new System.Windows.Forms.Panel();
            this.pickColorButton = new System.Windows.Forms.Button();
            this.saveMarkerButton = new System.Windows.Forms.Button();
            this.cancelMarkerButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.markerSecondNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // markerTitleLabel
            // 
            this.markerTitleLabel.AutoSize = true;
            this.markerTitleLabel.Location = new System.Drawing.Point(9, 10);
            this.markerTitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.markerTitleLabel.Name = "markerTitleLabel";
            this.markerTitleLabel.Size = new System.Drawing.Size(57, 13);
            this.markerTitleLabel.TabIndex = 0;
            this.markerTitleLabel.Text = "Название";
            // 
            // markerTitleTextBox
            // 
            this.markerTitleTextBox.Location = new System.Drawing.Point(11, 25);
            this.markerTitleTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.markerTitleTextBox.Name = "markerTitleTextBox";
            this.markerTitleTextBox.Size = new System.Drawing.Size(299, 20);
            this.markerTitleTextBox.TabIndex = 1;
            // 
            // markerDescriptionLabel
            // 
            this.markerDescriptionLabel.AutoSize = true;
            this.markerDescriptionLabel.Location = new System.Drawing.Point(9, 52);
            this.markerDescriptionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.markerDescriptionLabel.Name = "markerDescriptionLabel";
            this.markerDescriptionLabel.Size = new System.Drawing.Size(57, 13);
            this.markerDescriptionLabel.TabIndex = 2;
            this.markerDescriptionLabel.Text = "Описание";
            // 
            // markerDescriptionTextBox
            // 
            this.markerDescriptionTextBox.Location = new System.Drawing.Point(11, 67);
            this.markerDescriptionTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.markerDescriptionTextBox.Multiline = true;
            this.markerDescriptionTextBox.Name = "markerDescriptionTextBox";
            this.markerDescriptionTextBox.Size = new System.Drawing.Size(299, 78);
            this.markerDescriptionTextBox.TabIndex = 3;
            // 
            // markerSecondLabel
            // 
            this.markerSecondLabel.AutoSize = true;
            this.markerSecondLabel.Location = new System.Drawing.Point(9, 147);
            this.markerSecondLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.markerSecondLabel.Name = "markerSecondLabel";
            this.markerSecondLabel.Size = new System.Drawing.Size(49, 13);
            this.markerSecondLabel.TabIndex = 4;
            this.markerSecondLabel.Text = "Секунда";
            // 
            // markerSecondNumeric
            // 
            this.markerSecondNumeric.DecimalPlaces = 2;
            this.markerSecondNumeric.Location = new System.Drawing.Point(11, 162);
            this.markerSecondNumeric.Margin = new System.Windows.Forms.Padding(2);
            this.markerSecondNumeric.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.markerSecondNumeric.Name = "markerSecondNumeric";
            this.markerSecondNumeric.Size = new System.Drawing.Size(90, 20);
            this.markerSecondNumeric.TabIndex = 5;
            // 
            // markerColorLabel
            // 
            this.markerColorLabel.Location = new System.Drawing.Point(164, 147);
            this.markerColorLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.markerColorLabel.Name = "markerColorLabel";
            this.markerColorLabel.Size = new System.Drawing.Size(42, 13);
            this.markerColorLabel.TabIndex = 6;
            this.markerColorLabel.Text = "Цвет";
            this.markerColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // markerColorPreviewPanel
            // 
            this.markerColorPreviewPanel.BackColor = System.Drawing.Color.OrangeRed;
            this.markerColorPreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.markerColorPreviewPanel.Location = new System.Drawing.Point(164, 162);
            this.markerColorPreviewPanel.Margin = new System.Windows.Forms.Padding(2);
            this.markerColorPreviewPanel.Name = "markerColorPreviewPanel";
            this.markerColorPreviewPanel.Size = new System.Drawing.Size(42, 20);
            this.markerColorPreviewPanel.TabIndex = 7;
            // 
            // pickColorButton
            // 
            this.pickColorButton.Location = new System.Drawing.Point(219, 162);
            this.pickColorButton.Margin = new System.Windows.Forms.Padding(2);
            this.pickColorButton.Name = "pickColorButton";
            this.pickColorButton.Size = new System.Drawing.Size(91, 20);
            this.pickColorButton.TabIndex = 8;
            this.pickColorButton.Text = "Выбрать цвет";
            this.pickColorButton.UseVisualStyleBackColor = true;
            // 
            // saveMarkerButton
            // 
            this.saveMarkerButton.Location = new System.Drawing.Point(11, 197);
            this.saveMarkerButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveMarkerButton.Name = "saveMarkerButton";
            this.saveMarkerButton.Size = new System.Drawing.Size(90, 24);
            this.saveMarkerButton.TabIndex = 9;
            this.saveMarkerButton.Text = "Сохранить";
            this.saveMarkerButton.UseVisualStyleBackColor = true;
            // 
            // cancelMarkerButton
            // 
            this.cancelMarkerButton.Location = new System.Drawing.Point(219, 197);
            this.cancelMarkerButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelMarkerButton.Name = "cancelMarkerButton";
            this.cancelMarkerButton.Size = new System.Drawing.Size(91, 24);
            this.cancelMarkerButton.TabIndex = 10;
            this.cancelMarkerButton.Text = "Отмена";
            this.cancelMarkerButton.UseVisualStyleBackColor = true;
            // 
            // MarkerEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 232);
            this.Controls.Add(this.cancelMarkerButton);
            this.Controls.Add(this.saveMarkerButton);
            this.Controls.Add(this.pickColorButton);
            this.Controls.Add(this.markerColorPreviewPanel);
            this.Controls.Add(this.markerColorLabel);
            this.Controls.Add(this.markerSecondNumeric);
            this.Controls.Add(this.markerSecondLabel);
            this.Controls.Add(this.markerDescriptionTextBox);
            this.Controls.Add(this.markerDescriptionLabel);
            this.Controls.Add(this.markerTitleTextBox);
            this.Controls.Add(this.markerTitleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MarkerEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Маркер";
            ((System.ComponentModel.ISupportInitialize)(this.markerSecondNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label markerTitleLabel;
        private System.Windows.Forms.TextBox markerTitleTextBox;
        private System.Windows.Forms.Label markerDescriptionLabel;
        private System.Windows.Forms.TextBox markerDescriptionTextBox;
        private System.Windows.Forms.Label markerSecondLabel;
        private System.Windows.Forms.NumericUpDown markerSecondNumeric;
        private System.Windows.Forms.Label markerColorLabel;
        private System.Windows.Forms.Panel markerColorPreviewPanel;
        private System.Windows.Forms.Button pickColorButton;
        private System.Windows.Forms.Button saveMarkerButton;
        private System.Windows.Forms.Button cancelMarkerButton;
    }
}
