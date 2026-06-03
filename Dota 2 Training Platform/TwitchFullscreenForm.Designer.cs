namespace Dota_2_Training_Platform
{
    partial class TwitchFullscreenForm
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
            this.twitchFullscreenWebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.twitchFullscreenTopPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.btnCloseFullscreen = new Guna.UI2.WinForms.Guna2Button();
            this.lblFullscreenHint = new System.Windows.Forms.Label();
            this.twitchFullscreenTopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.twitchFullscreenWebView)).BeginInit();
            this.SuspendLayout();
            // 
            // twitchFullscreenWebView
            // 
            this.twitchFullscreenWebView.AllowExternalDrop = true;
            this.twitchFullscreenWebView.CreationProperties = null;
            this.twitchFullscreenWebView.DefaultBackgroundColor = System.Drawing.Color.Black;
            this.twitchFullscreenWebView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.twitchFullscreenWebView.Location = new System.Drawing.Point(0, 36);
            this.twitchFullscreenWebView.Name = "twitchFullscreenWebView";
            this.twitchFullscreenWebView.Size = new System.Drawing.Size(984, 525);
            this.twitchFullscreenWebView.TabIndex = 0;
            this.twitchFullscreenWebView.ZoomFactor = 1D;
            // 
            // twitchFullscreenTopPanel
            // 
            this.twitchFullscreenTopPanel.Controls.Add(this.btnCloseFullscreen);
            this.twitchFullscreenTopPanel.Controls.Add(this.lblFullscreenHint);
            this.twitchFullscreenTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.twitchFullscreenTopPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(27)))));
            this.twitchFullscreenTopPanel.Location = new System.Drawing.Point(0, 0);
            this.twitchFullscreenTopPanel.Name = "twitchFullscreenTopPanel";
            this.twitchFullscreenTopPanel.Size = new System.Drawing.Size(984, 36);
            this.twitchFullscreenTopPanel.TabIndex = 1;
            // 
            // btnCloseFullscreen
            // 
            this.btnCloseFullscreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseFullscreen.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(70)))), ((int)(((byte)(255)))));
            this.btnCloseFullscreen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCloseFullscreen.ForeColor = System.Drawing.Color.White;
            this.btnCloseFullscreen.Location = new System.Drawing.Point(852, 4);
            this.btnCloseFullscreen.Name = "btnCloseFullscreen";
            this.btnCloseFullscreen.Size = new System.Drawing.Size(120, 28);
            this.btnCloseFullscreen.TabIndex = 1;
            this.btnCloseFullscreen.Text = "Закрыть (Esc)";
            this.btnCloseFullscreen.Click += new System.EventHandler(this.BtnCloseFullscreen_Click);
            // 
            // lblFullscreenHint
            // 
            this.lblFullscreenHint.AutoSize = true;
            this.lblFullscreenHint.BackColor = System.Drawing.Color.Transparent;
            this.lblFullscreenHint.ForeColor = System.Drawing.Color.White;
            this.lblFullscreenHint.Location = new System.Drawing.Point(12, 11);
            this.lblFullscreenHint.Name = "lblFullscreenHint";
            this.lblFullscreenHint.Size = new System.Drawing.Size(145, 13);
            this.lblFullscreenHint.TabIndex = 0;
            this.lblFullscreenHint.Text = "Полноэкранный просмотр";
            // 
            // TwitchFullscreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.twitchFullscreenWebView);
            this.Controls.Add(this.twitchFullscreenTopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TwitchFullscreenForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Twitch — полный экран";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.twitchFullscreenTopPanel.ResumeLayout(false);
            this.twitchFullscreenTopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.twitchFullscreenWebView)).EndInit();
            this.ResumeLayout(false);
        }

        private Microsoft.Web.WebView2.WinForms.WebView2 twitchFullscreenWebView;
        private Guna.UI2.WinForms.Guna2Panel twitchFullscreenTopPanel;
        private Guna.UI2.WinForms.Guna2Button btnCloseFullscreen;
        private System.Windows.Forms.Label lblFullscreenHint;
    }
}
