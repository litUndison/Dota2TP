using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBaseManager;
using Dota_2_Training_Platform.Models;
using Dota_2_Training_Platform.Functions;

namespace Dota_2_Training_Platform
{
    public partial class EnterForm : Form
    {

        public enum TypeOfEntering
        {
            Player,
            Trainer
        }

        TypeOfEntering? entering = null;
        Color color;
        Form form2;


        UserModel currentUser;
        public EnterForm()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            
            color = SteamIDTextBox.FocusedState.BorderColor;
            dbManager.InitializeDatabase();

            var screenBounds = Screen.FromControl(this).WorkingArea;

            this.Location = new Point(screenBounds.Left + (screenBounds.Width - this.Width) / 2, screenBounds.Top + (screenBounds.Height - this.Height) / 2);
            
            await ApiCourier.LoadHeroes();
            await ApiCourier.LoadItems();

            //foreach(var element in ApiCourier.Heroes)
            //{
            //    MessageBox.Show($"{element.Key} : {element.Value}");
            //}
        }

        private void OpenForm(TypeOfEntering typeOfEntering)
        {
            switch (typeOfEntering)
            {
                case TypeOfEntering.Player: // у игрока будет другая форма, и интерфейс тоже вроде
                    {
                        form2 = new PlayerTeamsForm(currentUser, this);

                        break;
                    }
                case TypeOfEntering.Trainer:
                    {
                        form2 = new TrainerTeamsForm(currentUser, this);

                        break;
                    }
            }
            form2.StartPosition = FormStartPosition.Manual;

            int x = this.DesktopLocation.X + (this.Width - form2.Width) / 2;
            int y = this.DesktopLocation.Y + (this.Height - form2.Height) / 2;

            form2.Location = new Point(x, y);
            form2.Show();
            this.Hide();
        }

        #region OtherFunctions


        #endregion

        #region SignInUp //Authorization and Registration of trainers and players

        public async Task<bool> PlayerSignUp(string SteamID, string Password)
        {
            if (!string.IsNullOrWhiteSpace(SteamID) &&
                !string.IsNullOrWhiteSpace(Password))
            {
                var player = await dbManager.AddPlayer(SteamID, Password);
                return player != null;
            }

            return false;
        }

        public bool PlayerSignIn(string SteamID, string Password)
        {
            var user = dbManager.GetPlayer(SteamID, Password);

            if (user != null)
            {
                currentUser = user;
                return true;
            }
            MessageBox.Show("Неверный SteamID или пароль");
            return false;

        }

        public async Task<bool> TrainerSignUp(string SteamID, string Password)
        {
            if (SteamIDTextBox.Text.Length != 0 && PasswordTextBox.Text.Length != 0)
            {
                DotaPlayerProfileModel user = await dbManager.AddTrainer(SteamID, Password);
                if (user != null)
                {
                    return true;
                }
            }
            return false;
        }
        public bool TrainerSignIn(string SteamID, string Password)
        {
            var user = dbManager.GetTrainer(SteamID, Password);

            if (user != null)
            {
                currentUser = user;
                return true;
            }
            MessageBox.Show("Неверный SteamID или пароль");
            return false;
        }

        #endregion

        private void SteamIDTextBox_TextChanged(object sender, EventArgs e) // SteamIDTextBox
        {
            FieldChecker.FieldCheck(SteamIDTextBox, FieldChecker.CheckType.SteamID);
        }
        private void PasswordTextBox_TextChanged(object sender, EventArgs e) //PasswordTextBox
        {
            FieldChecker.FieldCheck(PasswordTextBox, FieldChecker.CheckType.Password);
        }

        private void TrainerButton_Click(object sender, EventArgs e) // Trainer button
        {
            TrainerButton.Checked = true;
            PlayerButton.Checked = false;
            entering = TypeOfEntering.Trainer;
        }

        private void PlayerButton_Click(object sender, EventArgs e) // Player button
        {
            TrainerButton.Checked = false;
            PlayerButton.Checked = true;
            entering = TypeOfEntering.Player;
        }

        private void AuthorizationButton_Click(object sender, EventArgs e)
        {
            switch (entering)
            {
                case TypeOfEntering.Player:
                    {
                        if (SteamIDTextBox.Text.Length < 5)
                        {
                            SteamIDTextBox.BorderColor = Color.Red;
                            return;
                        }
                        if (PasswordTextBox.Text.Length < 5)
                        {
                            PasswordTextBox.BorderColor = Color.Red;
                            return;
                        }
                        if (PlayerSignIn(SteamIDTextBox.Text, PasswordTextBox.Text))
                        {
                            OpenForm(TypeOfEntering.Player);
                            //MessageBox.Show("Вход успешен");
                        }
                        else
                        {
                            MessageBox.Show("Вход не успешен");
                        }
                        break;
                    }
                case TypeOfEntering.Trainer:
                    {
                        if (SteamIDTextBox.Text.Length < 5)
                        {
                            SteamIDTextBox.BorderColor = Color.Red;
                            return;
                        }
                        if (PasswordTextBox.Text.Length < 5)
                        {
                            PasswordTextBox.BorderColor = Color.Red;
                            return;
                        }
                        if (TrainerSignIn(SteamIDTextBox.Text, PasswordTextBox.Text))
                        {
                            OpenForm(TypeOfEntering.Trainer);
                            //MessageBox.Show("Вход успешен");
                        }
                        else
                        {
                            MessageBox.Show("Вход не успешен");
                        }
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Выберите тип входа");
                        break;
                    }
            }
        }

        private async void RegistrationButton_Click(object sender, EventArgs e)
        {
            switch (entering)
            {
                case TypeOfEntering.Player:
                    {
                        if (await PlayerSignUp(SteamIDTextBox.Text, PasswordTextBox.Text))
                        {
                            MessageBox.Show("Регистрация успешна!");
                        }
                        else
                        {
                            MessageBox.Show("Регистрация не успешна(");
                        }
                        break;
                    }
                case TypeOfEntering.Trainer:
                    {
                        if (await TrainerSignUp(SteamIDTextBox.Text, PasswordTextBox.Text))
                        {
                            MessageBox.Show("Регистрация успешна!");
                        }
                        else
                        {
                            MessageBox.Show("Регистрация не успешна(");
                        }
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Выберите тип регистрации");
                        break;
                    }
            }
        }

        private void EnterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
