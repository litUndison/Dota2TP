namespace Dota_2_Training_Platform
{
    partial class RecordingOverlayForm
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
            this.toastMessageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // toastMessageLabel
            // 
            this.toastMessageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toastMessageLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toastMessageLabel.ForeColor = System.Drawing.Color.White;
            this.toastMessageLabel.Location = new System.Drawing.Point(0, 0);
            this.toastMessageLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toastMessageLabel.Name = "toastMessageLabel";
            this.toastMessageLabel.Padding = new System.Windows.Forms.Padding(9, 6, 9, 6);
            this.toastMessageLabel.Size = new System.Drawing.Size(225, 39);
            this.toastMessageLabel.TabIndex = 0;
            this.toastMessageLabel.Text = "Уведомление";
            this.toastMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RecordingOverlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(225, 39);
            this.ControlBox = false;
            this.Controls.Add(this.toastMessageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "RecordingOverlayForm";
            this.Opacity = 0.82D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label toastMessageLabel;
    }
}
