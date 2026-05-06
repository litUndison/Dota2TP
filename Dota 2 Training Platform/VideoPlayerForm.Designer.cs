namespace Dota_2_Training_Platform
{
    partial class VideoPlayerForm
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
            this._rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this._webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this._controlsPanel = new System.Windows.Forms.Panel();
            this._deleteMomentButton = new System.Windows.Forms.Button();
            this._editMomentButton = new System.Windows.Forms.Button();
            this._addMomentButton = new System.Windows.Forms.Button();
            this._timeLabel = new System.Windows.Forms.Label();
            this._playPauseButton = new System.Windows.Forms.Button();
            this._timelineHost = new System.Windows.Forms.Panel();
            this._timeline = new System.Windows.Forms.TrackBar();
            this._markersPanel = new System.Windows.Forms.Panel();
            this._momentsList = new System.Windows.Forms.ListView();
            this._colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._colColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._rootLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._webView)).BeginInit();
            this._controlsPanel.SuspendLayout();
            this._timelineHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._timeline)).BeginInit();
            this.SuspendLayout();
            // 
            // _rootLayout
            // 
            this._rootLayout.ColumnCount = 1;
            this._rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.Controls.Add(this._webView, 0, 0);
            this._rootLayout.Controls.Add(this._controlsPanel, 0, 1);
            this._rootLayout.Controls.Add(this._timelineHost, 0, 2);
            this._rootLayout.Controls.Add(this._momentsList, 0, 3);
            this._rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rootLayout.Location = new System.Drawing.Point(0, 0);
            this._rootLayout.Margin = new System.Windows.Forms.Padding(2);
            this._rootLayout.Name = "_rootLayout";
            this._rootLayout.RowCount = 4;
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this._rootLayout.Size = new System.Drawing.Size(900, 618);
            this._rootLayout.TabIndex = 0;
            // 
            // _webView
            // 
            this._webView.AllowExternalDrop = true;
            this._webView.CreationProperties = null;
            this._webView.DefaultBackgroundColor = System.Drawing.Color.White;
            this._webView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._webView.Location = new System.Drawing.Point(2, 2);
            this._webView.Margin = new System.Windows.Forms.Padding(2);
            this._webView.Name = "_webView";
            this._webView.Size = new System.Drawing.Size(896, 371);
            this._webView.TabIndex = 0;
            this._webView.ZoomFactor = 1D;
            // 
            // _controlsPanel
            // 
            this._controlsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this._controlsPanel.Controls.Add(this._deleteMomentButton);
            this._controlsPanel.Controls.Add(this._editMomentButton);
            this._controlsPanel.Controls.Add(this._addMomentButton);
            this._controlsPanel.Controls.Add(this._timeLabel);
            this._controlsPanel.Controls.Add(this._playPauseButton);
            this._controlsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._controlsPanel.Location = new System.Drawing.Point(2, 377);
            this._controlsPanel.Margin = new System.Windows.Forms.Padding(2);
            this._controlsPanel.Name = "_controlsPanel";
            this._controlsPanel.Size = new System.Drawing.Size(896, 42);
            this._controlsPanel.TabIndex = 1;
            // 
            // _deleteMomentButton
            // 
            this._deleteMomentButton.Location = new System.Drawing.Point(448, 9);
            this._deleteMomentButton.Margin = new System.Windows.Forms.Padding(2);
            this._deleteMomentButton.Name = "_deleteMomentButton";
            this._deleteMomentButton.Size = new System.Drawing.Size(68, 23);
            this._deleteMomentButton.TabIndex = 4;
            this._deleteMomentButton.Text = "Удалить";
            this._deleteMomentButton.UseVisualStyleBackColor = true;
            // 
            // _editMomentButton
            // 
            this._editMomentButton.Location = new System.Drawing.Point(354, 9);
            this._editMomentButton.Margin = new System.Windows.Forms.Padding(2);
            this._editMomentButton.Name = "_editMomentButton";
            this._editMomentButton.Size = new System.Drawing.Size(90, 23);
            this._editMomentButton.TabIndex = 3;
            this._editMomentButton.Text = "Изменить";
            this._editMomentButton.UseVisualStyleBackColor = true;
            // 
            // _addMomentButton
            // 
            this._addMomentButton.Location = new System.Drawing.Point(248, 9);
            this._addMomentButton.Margin = new System.Windows.Forms.Padding(2);
            this._addMomentButton.Name = "_addMomentButton";
            this._addMomentButton.Size = new System.Drawing.Size(102, 23);
            this._addMomentButton.TabIndex = 2;
            this._addMomentButton.Text = "Добавить заметку";
            this._addMomentButton.UseVisualStyleBackColor = true;
            // 
            // _timeLabel
            // 
            this._timeLabel.AutoSize = true;
            this._timeLabel.ForeColor = System.Drawing.Color.White;
            this._timeLabel.Location = new System.Drawing.Point(82, 14);
            this._timeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._timeLabel.Name = "_timeLabel";
            this._timeLabel.Size = new System.Drawing.Size(72, 13);
            this._timeLabel.TabIndex = 1;
            this._timeLabel.Text = "00:00 / 00:00";
            // 
            // _playPauseButton
            // 
            this._playPauseButton.Location = new System.Drawing.Point(8, 9);
            this._playPauseButton.Margin = new System.Windows.Forms.Padding(2);
            this._playPauseButton.Name = "_playPauseButton";
            this._playPauseButton.Size = new System.Drawing.Size(68, 23);
            this._playPauseButton.TabIndex = 0;
            this._playPauseButton.Text = "Пауза";
            this._playPauseButton.UseVisualStyleBackColor = true;
            // 
            // _timelineHost
            // 
            this._timelineHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this._timelineHost.Controls.Add(this._markersPanel);
            this._timelineHost.Controls.Add(this._timeline);
            this._timelineHost.Location = new System.Drawing.Point(2, 423);
            this._timelineHost.Margin = new System.Windows.Forms.Padding(2);
            this._timelineHost.Name = "_timelineHost";
            this._timelineHost.Size = new System.Drawing.Size(896, 32);
            this._timelineHost.TabIndex = 2;
            // 
            // _timeline
            // 
            this._timeline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._timeline.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this._timeline.Location = new System.Drawing.Point(-2, 2);
            this._timeline.Margin = new System.Windows.Forms.Padding(2);
            this._timeline.LargeChange = 50;
            this._timeline.Maximum = 10000;
            this._timeline.Name = "_timeline";
            this._timeline.SmallChange = 5;
            this._timeline.Size = new System.Drawing.Size(900, 45);
            this._timeline.TabIndex = 0;
            this._timeline.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // _markersPanel
            // 
            this._markersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._markersPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this._markersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._markersPanel.Location = new System.Drawing.Point(8, 22);
            this._markersPanel.Margin = new System.Windows.Forms.Padding(2);
            this._markersPanel.Name = "_markersPanel";
            this._markersPanel.Size = new System.Drawing.Size(881, 10);
            this._markersPanel.TabIndex = 1;
            // 
            // _momentsList
            // 
            this._momentsList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this._momentsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._colTime,
            this._colColor,
            this._colTitle,
            this._colDescription});
            this._momentsList.FullRowSelect = true;
            this._momentsList.GridLines = true;
            this._momentsList.HideSelection = false;
            this._momentsList.Location = new System.Drawing.Point(2, 459);
            this._momentsList.Margin = new System.Windows.Forms.Padding(2);
            this._momentsList.Name = "_momentsList";
            this._momentsList.Size = new System.Drawing.Size(896, 157);
            this._momentsList.TabIndex = 3;
            this._momentsList.UseCompatibleStateImageBehavior = false;
            this._momentsList.View = System.Windows.Forms.View.Details;
            // 
            // _colTime
            // 
            this._colTime.Text = "Время";
            this._colTime.Width = 90;
            // 
            // _colColor
            // 
            this._colColor.Text = "Цвет";
            this._colColor.Width = 80;
            // 
            // _colTitle
            // 
            this._colTitle.Text = "Название";
            this._colTitle.Width = 220;
            // 
            // _colDescription
            // 
            this._colDescription.Text = "Описание";
            this._colDescription.Width = 700;
            // 
            // VideoPlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(900, 618);
            this.Controls.Add(this._rootLayout);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "VideoPlayerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Просмотр записи матча";
            this._rootLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._webView)).EndInit();
            this._controlsPanel.ResumeLayout(false);
            this._controlsPanel.PerformLayout();
            this._timelineHost.ResumeLayout(false);
            this._timelineHost.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._timeline)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel _rootLayout;
        private Microsoft.Web.WebView2.WinForms.WebView2 _webView;
        private System.Windows.Forms.Panel _controlsPanel;
        private System.Windows.Forms.Button _deleteMomentButton;
        private System.Windows.Forms.Button _editMomentButton;
        private System.Windows.Forms.Button _addMomentButton;
        private System.Windows.Forms.Label _timeLabel;
        private System.Windows.Forms.Button _playPauseButton;
        private System.Windows.Forms.Panel _timelineHost;
        private System.Windows.Forms.TrackBar _timeline;
        private System.Windows.Forms.ListView _momentsList;
        private System.Windows.Forms.ColumnHeader _colTime;
        private System.Windows.Forms.ColumnHeader _colColor;
        private System.Windows.Forms.ColumnHeader _colTitle;
        private System.Windows.Forms.ColumnHeader _colDescription;
        private System.Windows.Forms.Panel _markersPanel;
    }
}
