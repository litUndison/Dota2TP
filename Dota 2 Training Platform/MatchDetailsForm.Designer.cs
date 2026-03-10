namespace Dota_2_Training_Platform
{
    partial class MatchDetailsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.WinnerLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.DurationLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.MatchGrid = new Guna.UI2.WinForms.Guna2DataGridView();
            this.PlayerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlayerHero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlayerNetworth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatchID = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.RadiantTeam = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.DireTeam = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.RadiantScore = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.DireScore = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.HeroTip = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.StartTime = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.PlayerTip = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.KillsTip = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.DeathsTip = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.AssistsTip = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.NetworthTip = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.LastHit_deniesTip = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.gpmTip = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.DamageTip = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.radiantPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.direPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel6 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel7 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel8 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel9 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel10 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)(this.MatchGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // WinnerLabel
            // 
            this.WinnerLabel.AutoSize = false;
            this.WinnerLabel.BackColor = System.Drawing.Color.Transparent;
            this.WinnerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WinnerLabel.Location = new System.Drawing.Point(500, 12);
            this.WinnerLabel.Name = "WinnerLabel";
            this.WinnerLabel.Size = new System.Drawing.Size(200, 20);
            this.WinnerLabel.TabIndex = 0;
            this.WinnerLabel.Text = "Winner";
            this.WinnerLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DurationLabel
            // 
            this.DurationLabel.AutoSize = false;
            this.DurationLabel.BackColor = System.Drawing.Color.Transparent;
            this.DurationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DurationLabel.Location = new System.Drawing.Point(500, 50);
            this.DurationLabel.Name = "DurationLabel";
            this.DurationLabel.Size = new System.Drawing.Size(200, 15);
            this.DurationLabel.TabIndex = 1;
            this.DurationLabel.Text = "Duration";
            this.DurationLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MatchGrid
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.MatchGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MatchGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.MatchGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MatchGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PlayerName,
            this.Column2,
            this.PlayerHero,
            this.kda,
            this.PlayerNetworth});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MatchGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.MatchGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.MatchGrid.Location = new System.Drawing.Point(379, 547);
            this.MatchGrid.Name = "MatchGrid";
            this.MatchGrid.RowHeadersVisible = false;
            this.MatchGrid.Size = new System.Drawing.Size(436, 237);
            this.MatchGrid.TabIndex = 2;
            this.MatchGrid.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.MatchGrid.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.MatchGrid.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.MatchGrid.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.MatchGrid.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.MatchGrid.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.MatchGrid.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.MatchGrid.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.MatchGrid.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.MatchGrid.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MatchGrid.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.MatchGrid.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MatchGrid.ThemeStyle.HeaderStyle.Height = 15;
            this.MatchGrid.ThemeStyle.ReadOnly = false;
            this.MatchGrid.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.MatchGrid.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.MatchGrid.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MatchGrid.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.MatchGrid.ThemeStyle.RowsStyle.Height = 22;
            this.MatchGrid.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.MatchGrid.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // PlayerName
            // 
            this.PlayerName.HeaderText = "Игрок";
            this.PlayerName.Name = "PlayerName";
            this.PlayerName.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Команда";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // PlayerHero
            // 
            this.PlayerHero.HeaderText = "Герой";
            this.PlayerHero.Name = "PlayerHero";
            this.PlayerHero.ReadOnly = true;
            // 
            // kda
            // 
            this.kda.HeaderText = "KDA";
            this.kda.Name = "kda";
            this.kda.ReadOnly = true;
            // 
            // PlayerNetworth
            // 
            this.PlayerNetworth.HeaderText = "Ценность";
            this.PlayerNetworth.Name = "PlayerNetworth";
            this.PlayerNetworth.ReadOnly = true;
            // 
            // MatchID
            // 
            this.MatchID.AutoSize = false;
            this.MatchID.BackColor = System.Drawing.Color.Transparent;
            this.MatchID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MatchID.Location = new System.Drawing.Point(12, 12);
            this.MatchID.Name = "MatchID";
            this.MatchID.Size = new System.Drawing.Size(200, 15);
            this.MatchID.TabIndex = 3;
            this.MatchID.Text = "MatchID";
            this.MatchID.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RadiantTeam
            // 
            this.RadiantTeam.AutoSize = false;
            this.RadiantTeam.BackColor = System.Drawing.Color.Transparent;
            this.RadiantTeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadiantTeam.Location = new System.Drawing.Point(250, 80);
            this.RadiantTeam.Name = "RadiantTeam";
            this.RadiantTeam.Size = new System.Drawing.Size(200, 15);
            this.RadiantTeam.TabIndex = 5;
            this.RadiantTeam.Text = "Силы Света";
            this.RadiantTeam.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DireTeam
            // 
            this.DireTeam.AutoSize = false;
            this.DireTeam.BackColor = System.Drawing.Color.Transparent;
            this.DireTeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DireTeam.Location = new System.Drawing.Point(750, 80);
            this.DireTeam.Name = "DireTeam";
            this.DireTeam.Size = new System.Drawing.Size(200, 15);
            this.DireTeam.TabIndex = 6;
            this.DireTeam.Text = "Силы Тьмы";
            this.DireTeam.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RadiantScore
            // 
            this.RadiantScore.AutoSize = false;
            this.RadiantScore.BackColor = System.Drawing.Color.Transparent;
            this.RadiantScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadiantScore.Location = new System.Drawing.Point(294, 50);
            this.RadiantScore.Name = "RadiantScore";
            this.RadiantScore.Size = new System.Drawing.Size(200, 15);
            this.RadiantScore.TabIndex = 7;
            this.RadiantScore.Text = "10";
            this.RadiantScore.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DireScore
            // 
            this.DireScore.AutoSize = false;
            this.DireScore.BackColor = System.Drawing.Color.Transparent;
            this.DireScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DireScore.Location = new System.Drawing.Point(706, 50);
            this.DireScore.Name = "DireScore";
            this.DireScore.Size = new System.Drawing.Size(200, 15);
            this.DireScore.TabIndex = 8;
            this.DireScore.Text = "10";
            this.DireScore.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HeroTip
            // 
            this.HeroTip.AutoSize = false;
            this.HeroTip.BackColor = System.Drawing.Color.Transparent;
            this.HeroTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeroTip.Location = new System.Drawing.Point(9, 107);
            this.HeroTip.Name = "HeroTip";
            this.HeroTip.Size = new System.Drawing.Size(60, 15);
            this.HeroTip.TabIndex = 11;
            this.HeroTip.Text = "Герой";
            this.HeroTip.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartTime
            // 
            this.StartTime.AutoSize = false;
            this.StartTime.BackColor = System.Drawing.Color.Transparent;
            this.StartTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartTime.Location = new System.Drawing.Point(972, 12);
            this.StartTime.Name = "StartTime";
            this.StartTime.Size = new System.Drawing.Size(200, 15);
            this.StartTime.TabIndex = 12;
            this.StartTime.Text = "Время проведения";
            this.StartTime.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PlayerTip
            // 
            this.PlayerTip.AutoSize = false;
            this.PlayerTip.BackColor = System.Drawing.Color.Transparent;
            this.PlayerTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerTip.Location = new System.Drawing.Point(75, 107);
            this.PlayerTip.Name = "PlayerTip";
            this.PlayerTip.Size = new System.Drawing.Size(117, 15);
            this.PlayerTip.TabIndex = 13;
            this.PlayerTip.Text = "Игрок";
            this.PlayerTip.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KillsTip
            // 
            this.KillsTip.AutoSize = false;
            this.KillsTip.BackColor = System.Drawing.Color.Transparent;
            this.KillsTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KillsTip.Location = new System.Drawing.Point(198, 107);
            this.KillsTip.Name = "KillsTip";
            this.KillsTip.Size = new System.Drawing.Size(43, 15);
            this.KillsTip.TabIndex = 14;
            this.KillsTip.Text = "У";
            this.KillsTip.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeathsTip
            // 
            this.DeathsTip.AutoSize = false;
            this.DeathsTip.BackColor = System.Drawing.Color.Transparent;
            this.DeathsTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeathsTip.Location = new System.Drawing.Point(247, 107);
            this.DeathsTip.Name = "DeathsTip";
            this.DeathsTip.Size = new System.Drawing.Size(43, 15);
            this.DeathsTip.TabIndex = 15;
            this.DeathsTip.Text = "С";
            this.DeathsTip.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AssistsTip
            // 
            this.AssistsTip.AutoSize = false;
            this.AssistsTip.BackColor = System.Drawing.Color.Transparent;
            this.AssistsTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AssistsTip.Location = new System.Drawing.Point(296, 107);
            this.AssistsTip.Name = "AssistsTip";
            this.AssistsTip.Size = new System.Drawing.Size(43, 15);
            this.AssistsTip.TabIndex = 16;
            this.AssistsTip.Text = "П";
            this.AssistsTip.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NetworthTip
            // 
            this.NetworthTip.AutoSize = false;
            this.NetworthTip.BackColor = System.Drawing.Color.Transparent;
            this.NetworthTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NetworthTip.Location = new System.Drawing.Point(345, 107);
            this.NetworthTip.Name = "NetworthTip";
            this.NetworthTip.Size = new System.Drawing.Size(46, 15);
            this.NetworthTip.TabIndex = 17;
            this.NetworthTip.Text = "ОЦ";
            this.NetworthTip.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LastHit_deniesTip
            // 
            this.LastHit_deniesTip.AutoSize = false;
            this.LastHit_deniesTip.BackColor = System.Drawing.Color.Transparent;
            this.LastHit_deniesTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastHit_deniesTip.Location = new System.Drawing.Point(397, 107);
            this.LastHit_deniesTip.Name = "LastHit_deniesTip";
            this.LastHit_deniesTip.Size = new System.Drawing.Size(61, 15);
            this.LastHit_deniesTip.TabIndex = 18;
            this.LastHit_deniesTip.Text = "ДК/НО";
            this.LastHit_deniesTip.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gpmTip
            // 
            this.gpmTip.AutoSize = false;
            this.gpmTip.BackColor = System.Drawing.Color.Transparent;
            this.gpmTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpmTip.Location = new System.Drawing.Point(464, 107);
            this.gpmTip.Name = "gpmTip";
            this.gpmTip.Size = new System.Drawing.Size(53, 15);
            this.gpmTip.TabIndex = 19;
            this.gpmTip.Text = "З / М";
            this.gpmTip.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DamageTip
            // 
            this.DamageTip.AutoSize = false;
            this.DamageTip.BackColor = System.Drawing.Color.Transparent;
            this.DamageTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DamageTip.Location = new System.Drawing.Point(523, 107);
            this.DamageTip.Name = "DamageTip";
            this.DamageTip.Size = new System.Drawing.Size(65, 15);
            this.DamageTip.TabIndex = 20;
            this.DamageTip.Text = "Урон";
            this.DamageTip.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radiantPanel
            // 
            this.radiantPanel.Location = new System.Drawing.Point(6, 128);
            this.radiantPanel.Name = "radiantPanel";
            this.radiantPanel.Size = new System.Drawing.Size(585, 399);
            this.radiantPanel.TabIndex = 21;
            // 
            // direPanel
            // 
            this.direPanel.Location = new System.Drawing.Point(597, 128);
            this.direPanel.Name = "direPanel";
            this.direPanel.Size = new System.Drawing.Size(585, 399);
            this.direPanel.TabIndex = 22;
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.AutoSize = false;
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(597, 107);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(60, 15);
            this.guna2HtmlLabel2.TabIndex = 11;
            this.guna2HtmlLabel2.Text = "Герой";
            this.guna2HtmlLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.AutoSize = false;
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(1111, 107);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(71, 15);
            this.guna2HtmlLabel3.TabIndex = 20;
            this.guna2HtmlLabel3.Text = "Урон";
            this.guna2HtmlLabel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.AutoSize = false;
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(663, 107);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(117, 15);
            this.guna2HtmlLabel4.TabIndex = 13;
            this.guna2HtmlLabel4.Text = "Игрок";
            this.guna2HtmlLabel4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2HtmlLabel5
            // 
            this.guna2HtmlLabel5.AutoSize = false;
            this.guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel5.Location = new System.Drawing.Point(1052, 107);
            this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            this.guna2HtmlLabel5.Size = new System.Drawing.Size(53, 15);
            this.guna2HtmlLabel5.TabIndex = 19;
            this.guna2HtmlLabel5.Text = "З / М";
            this.guna2HtmlLabel5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2HtmlLabel6
            // 
            this.guna2HtmlLabel6.AutoSize = false;
            this.guna2HtmlLabel6.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel6.Location = new System.Drawing.Point(786, 107);
            this.guna2HtmlLabel6.Name = "guna2HtmlLabel6";
            this.guna2HtmlLabel6.Size = new System.Drawing.Size(43, 15);
            this.guna2HtmlLabel6.TabIndex = 14;
            this.guna2HtmlLabel6.Text = "У";
            this.guna2HtmlLabel6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2HtmlLabel7
            // 
            this.guna2HtmlLabel7.AutoSize = false;
            this.guna2HtmlLabel7.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel7.Location = new System.Drawing.Point(985, 107);
            this.guna2HtmlLabel7.Name = "guna2HtmlLabel7";
            this.guna2HtmlLabel7.Size = new System.Drawing.Size(61, 15);
            this.guna2HtmlLabel7.TabIndex = 18;
            this.guna2HtmlLabel7.Text = "ДК/НО";
            this.guna2HtmlLabel7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2HtmlLabel8
            // 
            this.guna2HtmlLabel8.AutoSize = false;
            this.guna2HtmlLabel8.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel8.Location = new System.Drawing.Point(835, 107);
            this.guna2HtmlLabel8.Name = "guna2HtmlLabel8";
            this.guna2HtmlLabel8.Size = new System.Drawing.Size(43, 15);
            this.guna2HtmlLabel8.TabIndex = 15;
            this.guna2HtmlLabel8.Text = "С";
            this.guna2HtmlLabel8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2HtmlLabel9
            // 
            this.guna2HtmlLabel9.AutoSize = false;
            this.guna2HtmlLabel9.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel9.Location = new System.Drawing.Point(933, 107);
            this.guna2HtmlLabel9.Name = "guna2HtmlLabel9";
            this.guna2HtmlLabel9.Size = new System.Drawing.Size(46, 15);
            this.guna2HtmlLabel9.TabIndex = 17;
            this.guna2HtmlLabel9.Text = "ОЦ";
            this.guna2HtmlLabel9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2HtmlLabel10
            // 
            this.guna2HtmlLabel10.AutoSize = false;
            this.guna2HtmlLabel10.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel10.Location = new System.Drawing.Point(884, 107);
            this.guna2HtmlLabel10.Name = "guna2HtmlLabel10";
            this.guna2HtmlLabel10.Size = new System.Drawing.Size(43, 15);
            this.guna2HtmlLabel10.TabIndex = 16;
            this.guna2HtmlLabel10.Text = "П";
            this.guna2HtmlLabel10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MatchDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.guna2HtmlLabel2);
            this.Controls.Add(this.HeroTip);
            this.Controls.Add(this.DamageTip);
            this.Controls.Add(this.guna2HtmlLabel3);
            this.Controls.Add(this.PlayerTip);
            this.Controls.Add(this.direPanel);
            this.Controls.Add(this.gpmTip);
            this.Controls.Add(this.guna2HtmlLabel4);
            this.Controls.Add(this.radiantPanel);
            this.Controls.Add(this.KillsTip);
            this.Controls.Add(this.guna2HtmlLabel5);
            this.Controls.Add(this.LastHit_deniesTip);
            this.Controls.Add(this.StartTime);
            this.Controls.Add(this.DeathsTip);
            this.Controls.Add(this.guna2HtmlLabel6);
            this.Controls.Add(this.NetworthTip);
            this.Controls.Add(this.DireScore);
            this.Controls.Add(this.guna2HtmlLabel7);
            this.Controls.Add(this.AssistsTip);
            this.Controls.Add(this.RadiantScore);
            this.Controls.Add(this.guna2HtmlLabel8);
            this.Controls.Add(this.DireTeam);
            this.Controls.Add(this.guna2HtmlLabel9);
            this.Controls.Add(this.RadiantTeam);
            this.Controls.Add(this.guna2HtmlLabel10);
            this.Controls.Add(this.MatchID);
            this.Controls.Add(this.MatchGrid);
            this.Controls.Add(this.DurationLabel);
            this.Controls.Add(this.WinnerLabel);
            this.Name = "MatchDetailsForm";
            this.Text = "MatchDetailsForm";
            ((System.ComponentModel.ISupportInitialize)(this.MatchGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel WinnerLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel DurationLabel;
        private Guna.UI2.WinForms.Guna2DataGridView MatchGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlayerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlayerHero;
        private System.Windows.Forms.DataGridViewTextBoxColumn kda;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlayerNetworth;
        private Guna.UI2.WinForms.Guna2HtmlLabel MatchID;
        private Guna.UI2.WinForms.Guna2HtmlLabel RadiantTeam;
        private Guna.UI2.WinForms.Guna2HtmlLabel DireTeam;
        private Guna.UI2.WinForms.Guna2HtmlLabel RadiantScore;
        private Guna.UI2.WinForms.Guna2HtmlLabel DireScore;
        private Guna.UI2.WinForms.Guna2HtmlLabel HeroTip;
        private Guna.UI2.WinForms.Guna2HtmlLabel StartTime;
        private Guna.UI2.WinForms.Guna2HtmlLabel PlayerTip;
        private Guna.UI2.WinForms.Guna2HtmlLabel KillsTip;
        private Guna.UI2.WinForms.Guna2HtmlLabel DeathsTip;
        private Guna.UI2.WinForms.Guna2HtmlLabel AssistsTip;
        private Guna.UI2.WinForms.Guna2HtmlLabel NetworthTip;
        private Guna.UI2.WinForms.Guna2HtmlLabel LastHit_deniesTip;
        private Guna.UI2.WinForms.Guna2HtmlLabel gpmTip;
        private Guna.UI2.WinForms.Guna2HtmlLabel DamageTip;
        private Guna.UI2.WinForms.Guna2Panel radiantPanel;
        private Guna.UI2.WinForms.Guna2Panel direPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel6;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel7;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel8;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel9;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel10;
    }
}