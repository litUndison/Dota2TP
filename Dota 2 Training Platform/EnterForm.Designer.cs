namespace Dota_2_Training_Platform
{
    partial class EnterForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.RegistrationButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.SteamIDTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.AuthorizationButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.PasswordTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.TrainerButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.PlayerButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2CheckBox1 = new Guna.UI2.WinForms.Guna2CheckBox();
            this.SuspendLayout();
            // 
            // RegistrationButton
            // 
            this.RegistrationButton.Animated = true;
            this.RegistrationButton.BackColor = System.Drawing.Color.Transparent;
            this.RegistrationButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.RegistrationButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.RegistrationButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.RegistrationButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.RegistrationButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.RegistrationButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.RegistrationButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.RegistrationButton.FocusedColor = System.Drawing.Color.Gray;
            this.RegistrationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.RegistrationButton.ForeColor = System.Drawing.Color.White;
            this.RegistrationButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.RegistrationButton.IndicateFocus = true;
            this.RegistrationButton.Location = new System.Drawing.Point(45, 302);
            this.RegistrationButton.Name = "RegistrationButton";
            this.RegistrationButton.Size = new System.Drawing.Size(160, 43);
            this.RegistrationButton.TabIndex = 7;
            this.RegistrationButton.Text = "Регистрация";
            this.RegistrationButton.UseTransparentBackground = true;
            this.RegistrationButton.Click += new System.EventHandler(this.RegistrationButton_Click);
            // 
            // SteamIDTextBox
            // 
            this.SteamIDTextBox.Animated = true;
            this.SteamIDTextBox.BackColor = System.Drawing.Color.Transparent;
            this.SteamIDTextBox.BorderColor = System.Drawing.Color.Gray;
            this.SteamIDTextBox.BorderRadius = 1;
            this.SteamIDTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SteamIDTextBox.DefaultText = "";
            this.SteamIDTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.SteamIDTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.SteamIDTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.SteamIDTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.SteamIDTextBox.FillColor = System.Drawing.Color.DarkGray;
            this.SteamIDTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.SteamIDTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.SteamIDTextBox.ForeColor = System.Drawing.Color.Black;
            this.SteamIDTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.SteamIDTextBox.Location = new System.Drawing.Point(25, 67);
            this.SteamIDTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.SteamIDTextBox.Name = "SteamIDTextBox";
            this.SteamIDTextBox.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.SteamIDTextBox.PlaceholderText = "AccountID";
            this.SteamIDTextBox.SelectedText = "";
            this.SteamIDTextBox.Size = new System.Drawing.Size(200, 38);
            this.SteamIDTextBox.TabIndex = 1;
            this.SteamIDTextBox.TextChanged += new System.EventHandler(this.SteamIDTextBox_TextChanged);
            // 
            // AuthorizationButton
            // 
            this.AuthorizationButton.Animated = true;
            this.AuthorizationButton.BackColor = System.Drawing.Color.Transparent;
            this.AuthorizationButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.AuthorizationButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.AuthorizationButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.AuthorizationButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.AuthorizationButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.AuthorizationButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.AuthorizationButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.AuthorizationButton.FocusedColor = System.Drawing.Color.Gray;
            this.AuthorizationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.AuthorizationButton.ForeColor = System.Drawing.Color.White;
            this.AuthorizationButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.AuthorizationButton.IndicateFocus = true;
            this.AuthorizationButton.Location = new System.Drawing.Point(45, 253);
            this.AuthorizationButton.Name = "AuthorizationButton";
            this.AuthorizationButton.Size = new System.Drawing.Size(160, 43);
            this.AuthorizationButton.TabIndex = 6;
            this.AuthorizationButton.Text = "Вход";
            this.AuthorizationButton.UseTransparentBackground = true;
            this.AuthorizationButton.Click += new System.EventHandler(this.AuthorizationButton_Click);
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Animated = true;
            this.PasswordTextBox.BackColor = System.Drawing.Color.Transparent;
            this.PasswordTextBox.BorderColor = System.Drawing.Color.Gray;
            this.PasswordTextBox.BorderRadius = 1;
            this.PasswordTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PasswordTextBox.DefaultText = "";
            this.PasswordTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.PasswordTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PasswordTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PasswordTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PasswordTextBox.FillColor = System.Drawing.Color.DarkGray;
            this.PasswordTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PasswordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.PasswordTextBox.ForeColor = System.Drawing.Color.Black;
            this.PasswordTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PasswordTextBox.Location = new System.Drawing.Point(25, 113);
            this.PasswordTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.PasswordTextBox.PlaceholderText = "Пароль";
            this.PasswordTextBox.SelectedText = "";
            this.PasswordTextBox.Size = new System.Drawing.Size(200, 38);
            this.PasswordTextBox.TabIndex = 2;
            this.PasswordTextBox.TextChanged += new System.EventHandler(this.PasswordTextBox_TextChanged);
            // 
            // TrainerButton
            // 
            this.TrainerButton.Animated = true;
            this.TrainerButton.BackColor = System.Drawing.Color.Transparent;
            this.TrainerButton.CheckedState.FillColor = System.Drawing.Color.Gray;
            this.TrainerButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.TrainerButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.TrainerButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.TrainerButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.TrainerButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.TrainerButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TrainerButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TrainerButton.FocusedColor = System.Drawing.Color.Gray;
            this.TrainerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.TrainerButton.ForeColor = System.Drawing.Color.White;
            this.TrainerButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.TrainerButton.IndicateFocus = true;
            this.TrainerButton.Location = new System.Drawing.Point(25, 214);
            this.TrainerButton.Name = "TrainerButton";
            this.TrainerButton.Size = new System.Drawing.Size(101, 24);
            this.TrainerButton.TabIndex = 4;
            this.TrainerButton.Text = "Тренер";
            this.TrainerButton.UseTransparentBackground = true;
            this.TrainerButton.Click += new System.EventHandler(this.TrainerButton_Click);
            // 
            // PlayerButton
            // 
            this.PlayerButton.Animated = true;
            this.PlayerButton.BackColor = System.Drawing.Color.Transparent;
            this.PlayerButton.CheckedState.FillColor = System.Drawing.Color.Gray;
            this.PlayerButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.PlayerButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.PlayerButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.PlayerButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.PlayerButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.PlayerButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PlayerButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PlayerButton.FocusedColor = System.Drawing.Color.Gray;
            this.PlayerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.PlayerButton.ForeColor = System.Drawing.Color.White;
            this.PlayerButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.PlayerButton.IndicateFocus = true;
            this.PlayerButton.Location = new System.Drawing.Point(124, 214);
            this.PlayerButton.Name = "PlayerButton";
            this.PlayerButton.Size = new System.Drawing.Size(101, 24);
            this.PlayerButton.TabIndex = 5;
            this.PlayerButton.Text = "Игрок";
            this.PlayerButton.UseTransparentBackground = true;
            this.PlayerButton.Click += new System.EventHandler(this.PlayerButton_Click);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.AutoSize = false;
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(25, 190);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(200, 18);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.TabStop = false;
            this.guna2HtmlLabel1.Text = "Выберите способ входа";
            this.guna2HtmlLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 1D;
            this.guna2BorderlessForm1.DragStartTransparencyValue = 1D;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(208, 0);
            this.guna2ControlBox1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(42, 24);
            this.guna2ControlBox1.TabIndex = 8;
            // 
            // guna2ControlBox2
            // 
            this.guna2ControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox2.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.guna2ControlBox2.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox2.Location = new System.Drawing.Point(166, 0);
            this.guna2ControlBox2.Margin = new System.Windows.Forms.Padding(2);
            this.guna2ControlBox2.Name = "guna2ControlBox2";
            this.guna2ControlBox2.Size = new System.Drawing.Size(42, 24);
            this.guna2ControlBox2.TabIndex = 9;
            // 
            // guna2CheckBox1
            // 
            this.guna2CheckBox1.AutoSize = true;
            this.guna2CheckBox1.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2CheckBox1.CheckedState.BorderRadius = 0;
            this.guna2CheckBox1.CheckedState.BorderThickness = 0;
            this.guna2CheckBox1.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2CheckBox1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.guna2CheckBox1.Location = new System.Drawing.Point(25, 158);
            this.guna2CheckBox1.Name = "guna2CheckBox1";
            this.guna2CheckBox1.Size = new System.Drawing.Size(127, 17);
            this.guna2CheckBox1.TabIndex = 10;
            this.guna2CheckBox1.Text = "Отображать пароль";
            this.guna2CheckBox1.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2CheckBox1.UncheckedState.BorderRadius = 0;
            this.guna2CheckBox1.UncheckedState.BorderThickness = 0;
            this.guna2CheckBox1.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2CheckBox1.CheckedChanged += new System.EventHandler(this.guna2CheckBox1_CheckedChanged);
            // 
            // EnterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(250, 361);
            this.Controls.Add(this.guna2CheckBox1);
            this.Controls.Add(this.guna2ControlBox2);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.PlayerButton);
            this.Controls.Add(this.TrainerButton);
            this.Controls.Add(this.RegistrationButton);
            this.Controls.Add(this.SteamIDTextBox);
            this.Controls.Add(this.AuthorizationButton);
            this.Controls.Add(this.PasswordTextBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EnterForm";
            this.Text = "Dota 2 Training Platrform";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2GradientButton RegistrationButton;
        private Guna.UI2.WinForms.Guna2TextBox SteamIDTextBox;
        private Guna.UI2.WinForms.Guna2GradientButton AuthorizationButton;
        private Guna.UI2.WinForms.Guna2TextBox PasswordTextBox;
        private Guna.UI2.WinForms.Guna2GradientButton TrainerButton;
        private Guna.UI2.WinForms.Guna2GradientButton PlayerButton;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2CheckBox guna2CheckBox1;
    }
}

