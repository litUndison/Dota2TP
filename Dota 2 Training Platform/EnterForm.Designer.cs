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
            this.RegistrationButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.SteamIDTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.AuthorizationButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.PasswordTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.TrainerButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.PlayerButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.SuspendLayout();
            // 
            // RegistrationButton
            // 
            this.RegistrationButton.Animated = true;
            this.RegistrationButton.BackColor = System.Drawing.Color.Transparent;
            this.RegistrationButton.BorderRadius = 5;
            this.RegistrationButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.RegistrationButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.RegistrationButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.RegistrationButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.RegistrationButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.RegistrationButton.FillColor = System.Drawing.Color.Silver;
            this.RegistrationButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.RegistrationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.RegistrationButton.ForeColor = System.Drawing.Color.White;
            this.RegistrationButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.RegistrationButton.IndicateFocus = true;
            this.RegistrationButton.Location = new System.Drawing.Point(89, 297);
            this.RegistrationButton.Name = "RegistrationButton";
            this.RegistrationButton.Size = new System.Drawing.Size(160, 43);
            this.RegistrationButton.TabIndex = 6;
            this.RegistrationButton.Text = "Регистрация";
            this.RegistrationButton.UseTransparentBackground = true;
            this.RegistrationButton.Click += new System.EventHandler(this.RegistrationButton_Click);
            // 
            // SteamIDTextBox
            // 
            this.SteamIDTextBox.Animated = true;
            this.SteamIDTextBox.BackColor = System.Drawing.Color.Transparent;
            this.SteamIDTextBox.BorderColor = System.Drawing.Color.Gray;
            this.SteamIDTextBox.BorderRadius = 5;
            this.SteamIDTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SteamIDTextBox.DefaultText = "";
            this.SteamIDTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.SteamIDTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.SteamIDTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.SteamIDTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.SteamIDTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.SteamIDTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.SteamIDTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.SteamIDTextBox.Location = new System.Drawing.Point(69, 76);
            this.SteamIDTextBox.Name = "SteamIDTextBox";
            this.SteamIDTextBox.PlaceholderText = "SteamID";
            this.SteamIDTextBox.SelectedText = "";
            this.SteamIDTextBox.Size = new System.Drawing.Size(200, 38);
            this.SteamIDTextBox.TabIndex = 1;
            this.SteamIDTextBox.TextChanged += new System.EventHandler(this.SteamIDTextBox_TextChanged);
            // 
            // AuthorizationButton
            // 
            this.AuthorizationButton.Animated = true;
            this.AuthorizationButton.BackColor = System.Drawing.Color.Transparent;
            this.AuthorizationButton.BorderRadius = 5;
            this.AuthorizationButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.AuthorizationButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.AuthorizationButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.AuthorizationButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.AuthorizationButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.AuthorizationButton.FillColor = System.Drawing.Color.Silver;
            this.AuthorizationButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.AuthorizationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.AuthorizationButton.ForeColor = System.Drawing.Color.White;
            this.AuthorizationButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.AuthorizationButton.IndicateFocus = true;
            this.AuthorizationButton.Location = new System.Drawing.Point(89, 237);
            this.AuthorizationButton.Name = "AuthorizationButton";
            this.AuthorizationButton.Size = new System.Drawing.Size(160, 43);
            this.AuthorizationButton.TabIndex = 5;
            this.AuthorizationButton.Text = "Вход";
            this.AuthorizationButton.UseTransparentBackground = true;
            this.AuthorizationButton.Click += new System.EventHandler(this.AuthorizationButton_Click);
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Animated = true;
            this.PasswordTextBox.BackColor = System.Drawing.Color.Transparent;
            this.PasswordTextBox.BorderColor = System.Drawing.Color.Gray;
            this.PasswordTextBox.BorderRadius = 5;
            this.PasswordTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PasswordTextBox.DefaultText = "";
            this.PasswordTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.PasswordTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PasswordTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PasswordTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PasswordTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PasswordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.PasswordTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PasswordTextBox.Location = new System.Drawing.Point(69, 120);
            this.PasswordTextBox.Name = "PasswordTextBox";
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
            this.TrainerButton.BorderRadius = 1;
            this.TrainerButton.CheckedState.FillColor = System.Drawing.Color.Gray;
            this.TrainerButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.TrainerButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.TrainerButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.TrainerButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.TrainerButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.TrainerButton.FillColor = System.Drawing.Color.Silver;
            this.TrainerButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TrainerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.TrainerButton.ForeColor = System.Drawing.Color.White;
            this.TrainerButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.TrainerButton.IndicateFocus = true;
            this.TrainerButton.Location = new System.Drawing.Point(69, 164);
            this.TrainerButton.Name = "TrainerButton";
            this.TrainerButton.Size = new System.Drawing.Size(101, 24);
            this.TrainerButton.TabIndex = 3;
            this.TrainerButton.Text = "Тренер";
            this.TrainerButton.UseTransparentBackground = true;
            this.TrainerButton.Click += new System.EventHandler(this.TrainerButton_Click);
            // 
            // PlayerButton
            // 
            this.PlayerButton.Animated = true;
            this.PlayerButton.BackColor = System.Drawing.Color.Transparent;
            this.PlayerButton.BorderRadius = 1;
            this.PlayerButton.CheckedState.FillColor = System.Drawing.Color.Gray;
            this.PlayerButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.PlayerButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.PlayerButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.PlayerButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.PlayerButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.PlayerButton.FillColor = System.Drawing.Color.Silver;
            this.PlayerButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PlayerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.PlayerButton.ForeColor = System.Drawing.Color.White;
            this.PlayerButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.PlayerButton.IndicateFocus = true;
            this.PlayerButton.Location = new System.Drawing.Point(168, 164);
            this.PlayerButton.Name = "PlayerButton";
            this.PlayerButton.Size = new System.Drawing.Size(101, 24);
            this.PlayerButton.TabIndex = 4;
            this.PlayerButton.Text = "Игрок";
            this.PlayerButton.UseTransparentBackground = true;
            this.PlayerButton.Click += new System.EventHandler(this.PlayerButton_Click);
            // 
            // EnterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(334, 361);
            this.Controls.Add(this.PlayerButton);
            this.Controls.Add(this.TrainerButton);
            this.Controls.Add(this.RegistrationButton);
            this.Controls.Add(this.SteamIDTextBox);
            this.Controls.Add(this.AuthorizationButton);
            this.Controls.Add(this.PasswordTextBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EnterForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2GradientButton RegistrationButton;
        private Guna.UI2.WinForms.Guna2TextBox SteamIDTextBox;
        private Guna.UI2.WinForms.Guna2GradientButton AuthorizationButton;
        private Guna.UI2.WinForms.Guna2TextBox PasswordTextBox;
        private Guna.UI2.WinForms.Guna2GradientButton TrainerButton;
        private Guna.UI2.WinForms.Guna2GradientButton PlayerButton;
    }
}

