using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBaseManager;
using Dota_2_Training_Platform.Functions;
using Dota_2_Training_Platform.Models;

namespace Dota_2_Training_Platform
{
    public partial class EnterForm : Form
    {
        public enum TypeOfEntering
        {
            Player,
            Trainer
        }

        private enum EnterWizardStep
        {
            Role,
            Mode,
            SignIn,
            Register
        }

        private EnterWizardStep currentStep = EnterWizardStep.Role;
        private TypeOfEntering? selectedRole;
        private UserModel currentUser;
        private Form form2;
        private bool isNavigatingBack;

        public EnterForm()
        {
            InitializeComponent();
            ShowWizardStep(EnterWizardStep.Role);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var screenBounds = Screen.FromControl(this).WorkingArea;
            Location = new Point(
                screenBounds.Left + (screenBounds.Width - Width) / 2,
                screenBounds.Top + (screenBounds.Height - Height) / 2);

            if (!await EnsureInternetConnectionAsync())
            {
                Application.Exit();
                return;
            }

            dbManager.InitializeDatabase();

            try
            {
                SetStartupLoadingState(true);
                await ApiCourier.LoadHeroes();
                await ApiCourier.LoadItems();
            }
            finally
            {
                SetStartupLoadingState(false);
            }
        }

        private async Task<bool> EnsureInternetConnectionAsync()
        {
            while (true)
            {
                SetStartupLoadingState(true);
                stepTitleLabel.Text = "Проверка подключения к интернету...";

                bool isOnline = await NetworkConnectivity.IsInternetAvailableAsync();

                SetStartupLoadingState(false);

                if (isOnline)
                {
                    ShowWizardStep(currentStep);
                    return true;
                }

                var answer = MessageBox.Show(
                    "Нет подключения к интернету.\n\n" +
                    "Программа не может работать без сети: нужен доступ к OpenDota, Steam-профилям и другим онлайн-сервисам.\n\n" +
                    "Повторить проверку?",
                    "Нет сети",
                    MessageBoxButtons.RetryCancel,
                    MessageBoxIcon.Warning);

                if (answer != DialogResult.Retry)
                    return false;
            }
        }

        private void SetStartupLoadingState(bool isLoading)
        {
            UseWaitCursor = isLoading;
            enterContentPanel.Enabled = !isLoading;
            BackButton.Enabled = !isLoading && currentStep != EnterWizardStep.Role;
            guna2Panel1.Enabled = true;
        }

        private void ShowWizardStep(EnterWizardStep step, bool resetRoleSelection = false)
        {
            if (resetRoleSelection)
                selectedRole = null;

            currentStep = step;

            SetStepPanelState(panelRoleStep, step == EnterWizardStep.Role);
            SetStepPanelState(panelModeStep, step == EnterWizardStep.Mode);
            SetStepPanelState(panelSignInStep, step == EnterWizardStep.SignIn);
            SetStepPanelState(panelRegisterStep, step == EnterWizardStep.Register);

            BackButton.Enabled = step != EnterWizardStep.Role;
            BackButton.Visible = step != EnterWizardStep.Role;

            switch (step)
            {
                case EnterWizardStep.Role:
                    stepTitleLabel.Text = "Кто вы?";
                    selectedRole = null;
                    break;
                case EnterWizardStep.Mode:
                    stepTitleLabel.Text = selectedRole == TypeOfEntering.Trainer
                        ? "Тренер"
                        : "Игрок";
                    RegisterModeButton.FillColor = selectedRole == TypeOfEntering.Trainer ? Color.FromArgb(255, 0, 0, 192) : Color.FromArgb(255, 192, 0, 0);
                    SignInModeButton.FillColor = selectedRole == TypeOfEntering.Trainer ? Color.FromArgb(255, 0, 0, 192) : Color.FromArgb(255, 192, 0, 0);
                    break;
                case EnterWizardStep.SignIn:
                    stepTitleLabel.Text = "Вход в аккаунт";
                    LoginSignInTextBox.Focus();
                    ConfirmSignInButton.FillColor = selectedRole == TypeOfEntering.Trainer ? Color.FromArgb(255, 0, 0, 192) : Color.FromArgb(255, 192, 0, 0);
                    break;
                case EnterWizardStep.Register:
                    stepTitleLabel.Text = "Регистрация";
                    LoginRegisterTextBox.Focus();
                    ConfirmRegisterButton.FillColor = selectedRole == TypeOfEntering.Trainer ? Color.FromArgb(255, 0, 0, 192) : Color.FromArgb(255, 192, 0, 0);
                    break;
            }
        }

        private static void SetStepPanelState(Guna2Panel panel, bool isActive)
        {
            panel.Visible = isActive;
            panel.Enabled = isActive;
            if (isActive)
                panel.BringToFront();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (isNavigatingBack)
                return;

            isNavigatingBack = true;
            try
            {
                switch (currentStep)
                {
                    case EnterWizardStep.Mode:
                        ShowWizardStep(EnterWizardStep.Role, resetRoleSelection: true);
                        break;
                    case EnterWizardStep.SignIn:
                    case EnterWizardStep.Register:
                        ClearAuthFields();
                        ShowWizardStep(EnterWizardStep.Mode);
                        break;
                }
            }
            finally
            {
                isNavigatingBack = false;
            }
        }

        private void ClearAuthFields()
        {
            LoginSignInTextBox.Clear();
            PasswordSignInTextBox.Clear();
            ShowPasswordSignInCheckBox.Checked = false;

            LoginRegisterTextBox.Clear();
            AccountIdRegisterTextBox.Clear();
            PasswordRegisterTextBox.Clear();
            ShowPasswordRegisterCheckBox.Checked = false;
        }

        private void PlayerRoleButton_Click(object sender, EventArgs e)
        {
            selectedRole = TypeOfEntering.Player;
            ShowWizardStep(EnterWizardStep.Mode);
        }

        private void TrainerRoleButton_Click(object sender, EventArgs e)
        {
            selectedRole = TypeOfEntering.Trainer;
            ShowWizardStep(EnterWizardStep.Mode);
        }

        private void SignInModeButton_Click(object sender, EventArgs e)
        {
            ShowWizardStep(EnterWizardStep.SignIn);
        }

        private void RegisterModeButton_Click(object sender, EventArgs e)
        {
            ShowWizardStep(EnterWizardStep.Register);
        }

        private void ConfirmSignInButton_Click(object sender, EventArgs e)
        {
            if (selectedRole == null)
            {
                MessageBox.Show("Выберите роль.");
                ShowWizardStep(EnterWizardStep.Role, resetRoleSelection: true);
                return;
            }

            if (!ValidateSignInFields())
                return;

            bool success = selectedRole == TypeOfEntering.Player
                ? PlayerSignIn(LoginSignInTextBox.Text, PasswordSignInTextBox.Text)
                : TrainerSignIn(LoginSignInTextBox.Text, PasswordSignInTextBox.Text);

            if (success)
                OpenForm(selectedRole.Value);
        }

        private async void ConfirmRegisterButton_Click(object sender, EventArgs e)
        {
            if (selectedRole == null)
            {
                MessageBox.Show("Выберите роль.");
                ShowWizardStep(EnterWizardStep.Role, resetRoleSelection: true);
                return;
            }

            if (!ValidateRegisterFields())
                return;

            ConfirmRegisterButton.Enabled = false;
            try
            {
                bool success = selectedRole == TypeOfEntering.Player
                    ? await PlayerSignUp(
                        LoginRegisterTextBox.Text,
                        AccountIdRegisterTextBox.Text,
                        PasswordRegisterTextBox.Text)
                    : await TrainerSignUp(
                        LoginRegisterTextBox.Text,
                        AccountIdRegisterTextBox.Text,
                        PasswordRegisterTextBox.Text);

                if (success)
                {
                    MessageBox.Show("Регистрация успешна! Теперь можно войти.");
                    ClearAuthFields();
                    ShowWizardStep(EnterWizardStep.SignIn);
                }
                //else
                //{
                //    MessageBox.Show("Регистрация не удалась.");
                //}
            }
            finally
            {
                ConfirmRegisterButton.Enabled = true;
            }
        }

        private bool ValidateSignInFields()
        {
            if (LoginSignInTextBox.Text.Trim().Length < 3)
            {
                HighlightInvalid(LoginSignInTextBox);
                return false;
            }

            if (PasswordSignInTextBox.Text.Length < 5)
            {
                HighlightInvalid(PasswordSignInTextBox);
                return false;
            }

            ResetBorder(LoginSignInTextBox);
            ResetBorder(PasswordSignInTextBox);
            return true;
        }

        private bool ValidateRegisterFields()
        {
            if (LoginRegisterTextBox.Text.Trim().Length < 3)
            {
                HighlightInvalid(LoginRegisterTextBox);
                return false;
            }

            if (AccountIdRegisterTextBox.Text.Trim().Length < 5)
            {
                HighlightInvalid(AccountIdRegisterTextBox);
                return false;
            }

            if (PasswordRegisterTextBox.Text.Length < 5)
            {
                HighlightInvalid(PasswordRegisterTextBox);
                return false;
            }

            ResetBorder(LoginRegisterTextBox);
            ResetBorder(AccountIdRegisterTextBox);
            ResetBorder(PasswordRegisterTextBox);
            return true;
        }

        private static void HighlightInvalid(Guna2TextBox textBox)
        {
            textBox.BorderColor = Color.Red;
        }

        private static void ResetBorder(Guna2TextBox textBox)
        {
            textBox.BorderColor = Color.Gray;
        }

        private void OpenForm(TypeOfEntering typeOfEntering)
        {
            var role = typeOfEntering == TypeOfEntering.Trainer ? UserRole.Trainer : UserRole.Player;
            form2 = new SelectTeamForm(currentUser, this, role);
            form2.StartPosition = FormStartPosition.Manual;

            int x = DesktopLocation.X + (Width - form2.Width) / 2;
            int y = DesktopLocation.Y + (Height - form2.Height) / 2;

            form2.Location = new Point(x, y);
            form2.Show();
            Hide();
        }

        public async Task<bool> PlayerSignUp(string login, string accountId, string password)
        {
            if (string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(accountId) ||
                string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var player = await dbManager.AddPlayer(login, accountId, password);
            return player != null;
        }

        public bool PlayerSignIn(string login, string password)
        {
            var user = dbManager.GetPlayer(login, password);
            if (user != null)
            {
                currentUser = user;
                return true;
            }

            MessageBox.Show("Неверный логин или пароль");
            return false;
        }

        public async Task<bool> TrainerSignUp(string login, string accountId, string password)
        {
            if (string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(accountId) ||
                string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var user = await dbManager.AddTrainer(login, accountId, password);
            return user != null;
        }

        public bool TrainerSignIn(string login, string password)
        {
            var user = dbManager.GetTrainer(login, password);
            if (user != null)
            {
                currentUser = user;
                return true;
            }

            MessageBox.Show("Неверный логин или пароль");
            return false;
        }

        private void LoginSignInTextBox_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(LoginSignInTextBox, FieldChecker.CheckType.Password);
        }

        private void PasswordSignInTextBox_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PasswordSignInTextBox, FieldChecker.CheckType.Password);
        }

        private void LoginRegisterTextBox_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(LoginRegisterTextBox, FieldChecker.CheckType.Password);
        }

        private void AccountIdRegisterTextBox_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(AccountIdRegisterTextBox, FieldChecker.CheckType.Numbers);
        }

        private void PasswordRegisterTextBox_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PasswordRegisterTextBox, FieldChecker.CheckType.Password);
        }

        private void ShowPasswordSignInCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PasswordSignInTextBox.PasswordChar = ShowPasswordSignInCheckBox.Checked ? '\0' : '*';
        }

        private void ShowPasswordRegisterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PasswordRegisterTextBox.PasswordChar = ShowPasswordRegisterCheckBox.Checked ? '\0' : '*';
        }

        private void EnterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
