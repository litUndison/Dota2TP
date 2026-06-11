namespace Dota_2_Training_Platform
{
    partial class EnterForm
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
            this.components = new System.ComponentModel.Container();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.BackButton = new Guna.UI2.WinForms.Guna2Button();
            this.stepTitleLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.enterContentPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.panelRoleStep = new Guna.UI2.WinForms.Guna2Panel();
            this.TrainerRoleButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.PlayerRoleButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.panelModeStep = new Guna.UI2.WinForms.Guna2Panel();
            this.RegisterModeButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.SignInModeButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.panelSignInStep = new Guna.UI2.WinForms.Guna2Panel();
            this.ConfirmSignInButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.ShowPasswordSignInCheckBox = new Guna.UI2.WinForms.Guna2CheckBox();
            this.PasswordSignInTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.LoginSignInTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelRegisterStep = new Guna.UI2.WinForms.Guna2Panel();
            this.ConfirmRegisterButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.ShowPasswordRegisterCheckBox = new Guna.UI2.WinForms.Guna2CheckBox();
            this.PasswordRegisterTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.AccountIdRegisterTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.LoginRegisterTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.enterContentPanel.SuspendLayout();
            this.panelRoleStep.SuspendLayout();
            this.panelModeStep.SuspendLayout();
            this.panelSignInStep.SuspendLayout();
            this.panelRegisterStep.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 1D;
            this.guna2BorderlessForm1.DragStartTransparencyValue = 1D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(238, 0);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(42, 24);
            this.guna2ControlBox1.TabIndex = 0;
            // 
            // guna2ControlBox2
            // 
            this.guna2ControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox2.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox2.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox2.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox2.Location = new System.Drawing.Point(196, 0);
            this.guna2ControlBox2.Name = "guna2ControlBox2";
            this.guna2ControlBox2.Size = new System.Drawing.Size(42, 24);
            this.guna2ControlBox2.TabIndex = 1;
            // 
            // BackButton
            // 
            this.BackButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BackButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BackButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BackButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BackButton.FillColor = System.Drawing.Color.Transparent;
            this.BackButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BackButton.ForeColor = System.Drawing.Color.White;
            this.BackButton.Location = new System.Drawing.Point(0, 0);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(72, 24);
            this.BackButton.TabIndex = 2;
            this.BackButton.TabStop = false;
            this.BackButton.Text = "← Назад";
            this.BackButton.Visible = false;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // stepTitleLabel
            // 
            this.stepTitleLabel.AutoSize = false;
            this.stepTitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.stepTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.stepTitleLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.stepTitleLabel.Location = new System.Drawing.Point(0, 58);
            this.stepTitleLabel.Name = "stepTitleLabel";
            this.stepTitleLabel.Size = new System.Drawing.Size(280, 18);
            this.stepTitleLabel.TabIndex = 3;
            this.stepTitleLabel.Text = "Как вы хотите продолжить?";
            this.stepTitleLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // enterContentPanel
            // 
            this.enterContentPanel.Controls.Add(this.panelRoleStep);
            this.enterContentPanel.Controls.Add(this.panelModeStep);
            this.enterContentPanel.Controls.Add(this.panelSignInStep);
            this.enterContentPanel.Controls.Add(this.panelRegisterStep);
            this.enterContentPanel.FillColor = System.Drawing.Color.Transparent;
            this.enterContentPanel.Location = new System.Drawing.Point(12, 104);
            this.enterContentPanel.Name = "enterContentPanel";
            this.enterContentPanel.Size = new System.Drawing.Size(256, 280);
            this.enterContentPanel.TabIndex = 4;
            // 
            // panelRoleStep
            // 
            this.panelRoleStep.Controls.Add(this.TrainerRoleButton);
            this.panelRoleStep.Controls.Add(this.PlayerRoleButton);
            this.panelRoleStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRoleStep.FillColor = System.Drawing.Color.Transparent;
            this.panelRoleStep.Location = new System.Drawing.Point(0, 0);
            this.panelRoleStep.Name = "panelRoleStep";
            this.panelRoleStep.Size = new System.Drawing.Size(256, 280);
            this.panelRoleStep.TabIndex = 0;
            // 
            // TrainerRoleButton
            // 
            this.TrainerRoleButton.Animated = true;
            this.TrainerRoleButton.BackColor = System.Drawing.Color.Transparent;
            this.TrainerRoleButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.TrainerRoleButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.TrainerRoleButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.TrainerRoleButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.TrainerRoleButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.TrainerRoleButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TrainerRoleButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.TrainerRoleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TrainerRoleButton.ForeColor = System.Drawing.Color.White;
            this.TrainerRoleButton.Location = new System.Drawing.Point(28, 132);
            this.TrainerRoleButton.Name = "TrainerRoleButton";
            this.TrainerRoleButton.Size = new System.Drawing.Size(200, 48);
            this.TrainerRoleButton.TabIndex = 1;
            this.TrainerRoleButton.Text = "Тренер";
            this.TrainerRoleButton.UseTransparentBackground = true;
            this.TrainerRoleButton.Click += new System.EventHandler(this.TrainerRoleButton_Click);
            // 
            // PlayerRoleButton
            // 
            this.PlayerRoleButton.Animated = true;
            this.PlayerRoleButton.BackColor = System.Drawing.Color.Transparent;
            this.PlayerRoleButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.PlayerRoleButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.PlayerRoleButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.PlayerRoleButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.PlayerRoleButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.PlayerRoleButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PlayerRoleButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.PlayerRoleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.PlayerRoleButton.ForeColor = System.Drawing.Color.White;
            this.PlayerRoleButton.Location = new System.Drawing.Point(28, 56);
            this.PlayerRoleButton.Name = "PlayerRoleButton";
            this.PlayerRoleButton.Size = new System.Drawing.Size(200, 48);
            this.PlayerRoleButton.TabIndex = 0;
            this.PlayerRoleButton.Text = "Игрок";
            this.PlayerRoleButton.UseTransparentBackground = true;
            this.PlayerRoleButton.Click += new System.EventHandler(this.PlayerRoleButton_Click);
            // 
            // panelModeStep
            // 
            this.panelModeStep.Controls.Add(this.RegisterModeButton);
            this.panelModeStep.Controls.Add(this.SignInModeButton);
            this.panelModeStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelModeStep.FillColor = System.Drawing.Color.Transparent;
            this.panelModeStep.Location = new System.Drawing.Point(0, 0);
            this.panelModeStep.Name = "panelModeStep";
            this.panelModeStep.Size = new System.Drawing.Size(256, 280);
            this.panelModeStep.TabIndex = 1;
            this.panelModeStep.Visible = false;
            // 
            // RegisterModeButton
            // 
            this.RegisterModeButton.Animated = true;
            this.RegisterModeButton.BackColor = System.Drawing.Color.Transparent;
            this.RegisterModeButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.RegisterModeButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.RegisterModeButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.RegisterModeButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.RegisterModeButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.RegisterModeButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.RegisterModeButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.RegisterModeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.RegisterModeButton.ForeColor = System.Drawing.Color.White;
            this.RegisterModeButton.Location = new System.Drawing.Point(28, 132);
            this.RegisterModeButton.Name = "RegisterModeButton";
            this.RegisterModeButton.Size = new System.Drawing.Size(200, 48);
            this.RegisterModeButton.TabIndex = 1;
            this.RegisterModeButton.Text = "Регистрация";
            this.RegisterModeButton.UseTransparentBackground = true;
            this.RegisterModeButton.Click += new System.EventHandler(this.RegisterModeButton_Click);
            // 
            // SignInModeButton
            // 
            this.SignInModeButton.Animated = true;
            this.SignInModeButton.BackColor = System.Drawing.Color.Transparent;
            this.SignInModeButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.SignInModeButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.SignInModeButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.SignInModeButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.SignInModeButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.SignInModeButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.SignInModeButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SignInModeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.SignInModeButton.ForeColor = System.Drawing.Color.White;
            this.SignInModeButton.Location = new System.Drawing.Point(28, 56);
            this.SignInModeButton.Name = "SignInModeButton";
            this.SignInModeButton.Size = new System.Drawing.Size(200, 48);
            this.SignInModeButton.TabIndex = 0;
            this.SignInModeButton.Text = "Вход";
            this.SignInModeButton.UseTransparentBackground = true;
            this.SignInModeButton.Click += new System.EventHandler(this.SignInModeButton_Click);
            // 
            // panelSignInStep
            // 
            this.panelSignInStep.Controls.Add(this.ConfirmSignInButton);
            this.panelSignInStep.Controls.Add(this.ShowPasswordSignInCheckBox);
            this.panelSignInStep.Controls.Add(this.PasswordSignInTextBox);
            this.panelSignInStep.Controls.Add(this.LoginSignInTextBox);
            this.panelSignInStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSignInStep.FillColor = System.Drawing.Color.Transparent;
            this.panelSignInStep.Location = new System.Drawing.Point(0, 0);
            this.panelSignInStep.Name = "panelSignInStep";
            this.panelSignInStep.Size = new System.Drawing.Size(256, 280);
            this.panelSignInStep.TabIndex = 2;
            this.panelSignInStep.Visible = false;
            // 
            // ConfirmSignInButton
            // 
            this.ConfirmSignInButton.Animated = true;
            this.ConfirmSignInButton.BackColor = System.Drawing.Color.Transparent;
            this.ConfirmSignInButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.ConfirmSignInButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.ConfirmSignInButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ConfirmSignInButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ConfirmSignInButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.ConfirmSignInButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.ConfirmSignInButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ConfirmSignInButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ConfirmSignInButton.ForeColor = System.Drawing.Color.White;
            this.ConfirmSignInButton.Location = new System.Drawing.Point(28, 196);
            this.ConfirmSignInButton.Name = "ConfirmSignInButton";
            this.ConfirmSignInButton.Size = new System.Drawing.Size(200, 44);
            this.ConfirmSignInButton.TabIndex = 3;
            this.ConfirmSignInButton.Text = "Войти";
            this.ConfirmSignInButton.UseTransparentBackground = true;
            this.ConfirmSignInButton.Click += new System.EventHandler(this.ConfirmSignInButton_Click);
            // 
            // ShowPasswordSignInCheckBox
            // 
            this.ShowPasswordSignInCheckBox.AutoSize = true;
            this.ShowPasswordSignInCheckBox.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ShowPasswordSignInCheckBox.CheckedState.BorderRadius = 0;
            this.ShowPasswordSignInCheckBox.CheckedState.BorderThickness = 0;
            this.ShowPasswordSignInCheckBox.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ShowPasswordSignInCheckBox.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.ShowPasswordSignInCheckBox.Location = new System.Drawing.Point(28, 128);
            this.ShowPasswordSignInCheckBox.Name = "ShowPasswordSignInCheckBox";
            this.ShowPasswordSignInCheckBox.Size = new System.Drawing.Size(127, 17);
            this.ShowPasswordSignInCheckBox.TabIndex = 2;
            this.ShowPasswordSignInCheckBox.Text = "Отображать пароль";
            this.ShowPasswordSignInCheckBox.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.ShowPasswordSignInCheckBox.UncheckedState.BorderRadius = 0;
            this.ShowPasswordSignInCheckBox.UncheckedState.BorderThickness = 0;
            this.ShowPasswordSignInCheckBox.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.ShowPasswordSignInCheckBox.CheckedChanged += new System.EventHandler(this.ShowPasswordSignInCheckBox_CheckedChanged);
            // 
            // PasswordSignInTextBox
            // 
            this.PasswordSignInTextBox.Animated = true;
            this.PasswordSignInTextBox.BackColor = System.Drawing.Color.Transparent;
            this.PasswordSignInTextBox.BorderColor = System.Drawing.Color.Gray;
            this.PasswordSignInTextBox.BorderRadius = 1;
            this.PasswordSignInTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PasswordSignInTextBox.DefaultText = "";
            this.PasswordSignInTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.PasswordSignInTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PasswordSignInTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PasswordSignInTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PasswordSignInTextBox.FillColor = System.Drawing.Color.DarkGray;
            this.PasswordSignInTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PasswordSignInTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.PasswordSignInTextBox.ForeColor = System.Drawing.Color.Black;
            this.PasswordSignInTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PasswordSignInTextBox.Location = new System.Drawing.Point(28, 72);
            this.PasswordSignInTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.PasswordSignInTextBox.Name = "PasswordSignInTextBox";
            this.PasswordSignInTextBox.PasswordChar = '*';
            this.PasswordSignInTextBox.PlaceholderText = "Пароль";
            this.PasswordSignInTextBox.SelectedText = "";
            this.PasswordSignInTextBox.Size = new System.Drawing.Size(200, 38);
            this.PasswordSignInTextBox.TabIndex = 1;
            this.PasswordSignInTextBox.TextChanged += new System.EventHandler(this.PasswordSignInTextBox_TextChanged);
            // 
            // LoginSignInTextBox
            // 
            this.LoginSignInTextBox.Animated = true;
            this.LoginSignInTextBox.BackColor = System.Drawing.Color.Transparent;
            this.LoginSignInTextBox.BorderColor = System.Drawing.Color.Gray;
            this.LoginSignInTextBox.BorderRadius = 1;
            this.LoginSignInTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.LoginSignInTextBox.DefaultText = "";
            this.LoginSignInTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.LoginSignInTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.LoginSignInTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.LoginSignInTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.LoginSignInTextBox.FillColor = System.Drawing.Color.DarkGray;
            this.LoginSignInTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.LoginSignInTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.LoginSignInTextBox.ForeColor = System.Drawing.Color.Black;
            this.LoginSignInTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.LoginSignInTextBox.Location = new System.Drawing.Point(28, 16);
            this.LoginSignInTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.LoginSignInTextBox.Name = "LoginSignInTextBox";
            this.LoginSignInTextBox.PlaceholderText = "Логин";
            this.LoginSignInTextBox.SelectedText = "";
            this.LoginSignInTextBox.Size = new System.Drawing.Size(200, 38);
            this.LoginSignInTextBox.TabIndex = 0;
            this.LoginSignInTextBox.TextChanged += new System.EventHandler(this.LoginSignInTextBox_TextChanged);
            // 
            // panelRegisterStep
            // 
            this.panelRegisterStep.Controls.Add(this.ConfirmRegisterButton);
            this.panelRegisterStep.Controls.Add(this.ShowPasswordRegisterCheckBox);
            this.panelRegisterStep.Controls.Add(this.PasswordRegisterTextBox);
            this.panelRegisterStep.Controls.Add(this.AccountIdRegisterTextBox);
            this.panelRegisterStep.Controls.Add(this.LoginRegisterTextBox);
            this.panelRegisterStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRegisterStep.FillColor = System.Drawing.Color.Transparent;
            this.panelRegisterStep.Location = new System.Drawing.Point(0, 0);
            this.panelRegisterStep.Name = "panelRegisterStep";
            this.panelRegisterStep.Size = new System.Drawing.Size(256, 280);
            this.panelRegisterStep.TabIndex = 3;
            this.panelRegisterStep.Visible = false;
            // 
            // ConfirmRegisterButton
            // 
            this.ConfirmRegisterButton.Animated = true;
            this.ConfirmRegisterButton.BackColor = System.Drawing.Color.Transparent;
            this.ConfirmRegisterButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.ConfirmRegisterButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.ConfirmRegisterButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ConfirmRegisterButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ConfirmRegisterButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.ConfirmRegisterButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.ConfirmRegisterButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ConfirmRegisterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ConfirmRegisterButton.ForeColor = System.Drawing.Color.White;
            this.ConfirmRegisterButton.Location = new System.Drawing.Point(28, 228);
            this.ConfirmRegisterButton.Name = "ConfirmRegisterButton";
            this.ConfirmRegisterButton.Size = new System.Drawing.Size(200, 44);
            this.ConfirmRegisterButton.TabIndex = 4;
            this.ConfirmRegisterButton.Text = "Зарегистрироваться";
            this.ConfirmRegisterButton.UseTransparentBackground = true;
            this.ConfirmRegisterButton.Click += new System.EventHandler(this.ConfirmRegisterButton_Click);
            // 
            // ShowPasswordRegisterCheckBox
            // 
            this.ShowPasswordRegisterCheckBox.AutoSize = true;
            this.ShowPasswordRegisterCheckBox.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ShowPasswordRegisterCheckBox.CheckedState.BorderRadius = 0;
            this.ShowPasswordRegisterCheckBox.CheckedState.BorderThickness = 0;
            this.ShowPasswordRegisterCheckBox.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ShowPasswordRegisterCheckBox.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.ShowPasswordRegisterCheckBox.Location = new System.Drawing.Point(28, 196);
            this.ShowPasswordRegisterCheckBox.Name = "ShowPasswordRegisterCheckBox";
            this.ShowPasswordRegisterCheckBox.Size = new System.Drawing.Size(127, 17);
            this.ShowPasswordRegisterCheckBox.TabIndex = 3;
            this.ShowPasswordRegisterCheckBox.Text = "Отображать пароль";
            this.ShowPasswordRegisterCheckBox.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.ShowPasswordRegisterCheckBox.UncheckedState.BorderRadius = 0;
            this.ShowPasswordRegisterCheckBox.UncheckedState.BorderThickness = 0;
            this.ShowPasswordRegisterCheckBox.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.ShowPasswordRegisterCheckBox.CheckedChanged += new System.EventHandler(this.ShowPasswordRegisterCheckBox_CheckedChanged);
            // 
            // PasswordRegisterTextBox
            // 
            this.PasswordRegisterTextBox.Animated = true;
            this.PasswordRegisterTextBox.BackColor = System.Drawing.Color.Transparent;
            this.PasswordRegisterTextBox.BorderColor = System.Drawing.Color.Gray;
            this.PasswordRegisterTextBox.BorderRadius = 1;
            this.PasswordRegisterTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PasswordRegisterTextBox.DefaultText = "";
            this.PasswordRegisterTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.PasswordRegisterTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PasswordRegisterTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PasswordRegisterTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PasswordRegisterTextBox.FillColor = System.Drawing.Color.DarkGray;
            this.PasswordRegisterTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PasswordRegisterTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.PasswordRegisterTextBox.ForeColor = System.Drawing.Color.Black;
            this.PasswordRegisterTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PasswordRegisterTextBox.Location = new System.Drawing.Point(28, 144);
            this.PasswordRegisterTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.PasswordRegisterTextBox.Name = "PasswordRegisterTextBox";
            this.PasswordRegisterTextBox.PasswordChar = '*';
            this.PasswordRegisterTextBox.PlaceholderText = "Пароль";
            this.PasswordRegisterTextBox.SelectedText = "";
            this.PasswordRegisterTextBox.Size = new System.Drawing.Size(200, 38);
            this.PasswordRegisterTextBox.TabIndex = 2;
            this.PasswordRegisterTextBox.TextChanged += new System.EventHandler(this.PasswordRegisterTextBox_TextChanged);
            // 
            // AccountIdRegisterTextBox
            // 
            this.AccountIdRegisterTextBox.Animated = true;
            this.AccountIdRegisterTextBox.BackColor = System.Drawing.Color.Transparent;
            this.AccountIdRegisterTextBox.BorderColor = System.Drawing.Color.Gray;
            this.AccountIdRegisterTextBox.BorderRadius = 1;
            this.AccountIdRegisterTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.AccountIdRegisterTextBox.DefaultText = "";
            this.AccountIdRegisterTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.AccountIdRegisterTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.AccountIdRegisterTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.AccountIdRegisterTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.AccountIdRegisterTextBox.FillColor = System.Drawing.Color.DarkGray;
            this.AccountIdRegisterTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.AccountIdRegisterTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.AccountIdRegisterTextBox.ForeColor = System.Drawing.Color.Black;
            this.AccountIdRegisterTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.AccountIdRegisterTextBox.Location = new System.Drawing.Point(28, 80);
            this.AccountIdRegisterTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.AccountIdRegisterTextBox.Name = "AccountIdRegisterTextBox";
            this.AccountIdRegisterTextBox.PlaceholderText = "Account ID / SteamID64";
            this.AccountIdRegisterTextBox.SelectedText = "";
            this.AccountIdRegisterTextBox.Size = new System.Drawing.Size(200, 38);
            this.AccountIdRegisterTextBox.TabIndex = 1;
            this.AccountIdRegisterTextBox.TextChanged += new System.EventHandler(this.AccountIdRegisterTextBox_TextChanged);
            // 
            // LoginRegisterTextBox
            // 
            this.LoginRegisterTextBox.Animated = true;
            this.LoginRegisterTextBox.BackColor = System.Drawing.Color.Transparent;
            this.LoginRegisterTextBox.BorderColor = System.Drawing.Color.Gray;
            this.LoginRegisterTextBox.BorderRadius = 1;
            this.LoginRegisterTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.LoginRegisterTextBox.DefaultText = "";
            this.LoginRegisterTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.LoginRegisterTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.LoginRegisterTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.LoginRegisterTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.LoginRegisterTextBox.FillColor = System.Drawing.Color.DarkGray;
            this.LoginRegisterTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.LoginRegisterTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.LoginRegisterTextBox.ForeColor = System.Drawing.Color.Black;
            this.LoginRegisterTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.LoginRegisterTextBox.Location = new System.Drawing.Point(28, 16);
            this.LoginRegisterTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.LoginRegisterTextBox.Name = "LoginRegisterTextBox";
            this.LoginRegisterTextBox.PlaceholderText = "Логин";
            this.LoginRegisterTextBox.SelectedText = "";
            this.LoginRegisterTextBox.Size = new System.Drawing.Size(200, 38);
            this.LoginRegisterTextBox.TabIndex = 0;
            this.LoginRegisterTextBox.TextChanged += new System.EventHandler(this.LoginRegisterTextBox_TextChanged);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(125)))), ((int)(((byte)(156)))));
            this.guna2Panel1.Controls.Add(this.BackButton);
            this.guna2Panel1.Controls.Add(this.guna2ControlBox1);
            this.guna2Panel1.Controls.Add(this.guna2ControlBox2);
            this.guna2Panel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(280, 24);
            this.guna2Panel1.TabIndex = 5;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.guna2Panel1;
            this.guna2DragControl1.TransparentWhileDrag = false;
            // 
            // EnterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(84)))), ((int)(((byte)(110)))));
            this.ClientSize = new System.Drawing.Size(280, 400);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.enterContentPanel);
            this.Controls.Add(this.stepTitleLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EnterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dota 2 Training Platform";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EnterForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.enterContentPanel.ResumeLayout(false);
            this.panelRoleStep.ResumeLayout(false);
            this.panelModeStep.ResumeLayout(false);
            this.panelSignInStep.ResumeLayout(false);
            this.panelSignInStep.PerformLayout();
            this.panelRegisterStep.ResumeLayout(false);
            this.panelRegisterStep.PerformLayout();
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2;
        private Guna.UI2.WinForms.Guna2Button BackButton;
        private Guna.UI2.WinForms.Guna2HtmlLabel stepTitleLabel;
        private Guna.UI2.WinForms.Guna2Panel enterContentPanel;
        private Guna.UI2.WinForms.Guna2Panel panelRoleStep;
        private Guna.UI2.WinForms.Guna2GradientButton TrainerRoleButton;
        private Guna.UI2.WinForms.Guna2GradientButton PlayerRoleButton;
        private Guna.UI2.WinForms.Guna2Panel panelModeStep;
        private Guna.UI2.WinForms.Guna2GradientButton RegisterModeButton;
        private Guna.UI2.WinForms.Guna2GradientButton SignInModeButton;
        private Guna.UI2.WinForms.Guna2Panel panelSignInStep;
        private Guna.UI2.WinForms.Guna2GradientButton ConfirmSignInButton;
        private Guna.UI2.WinForms.Guna2CheckBox ShowPasswordSignInCheckBox;
        private Guna.UI2.WinForms.Guna2TextBox PasswordSignInTextBox;
        private Guna.UI2.WinForms.Guna2TextBox LoginSignInTextBox;
        private Guna.UI2.WinForms.Guna2Panel panelRegisterStep;
        private Guna.UI2.WinForms.Guna2GradientButton ConfirmRegisterButton;
        private Guna.UI2.WinForms.Guna2CheckBox ShowPasswordRegisterCheckBox;
        private Guna.UI2.WinForms.Guna2TextBox PasswordRegisterTextBox;
        private Guna.UI2.WinForms.Guna2TextBox AccountIdRegisterTextBox;
        private Guna.UI2.WinForms.Guna2TextBox LoginRegisterTextBox;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
    }
}
