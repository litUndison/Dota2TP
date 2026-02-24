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

        string correctPasswordSymbols = "1234567890abcdefghijklmnopqrstuvwxyz-_";
        string correctSteamIDSymbols = "1234567890";

        TypeOfEntering entering;
        Color color;
        Form form2;


        UserModel currentUser;
        public EnterForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            color = SteamIDTextBox.FocusedState.BorderColor;
            dbManager.Start();
            //dbManager.ReadAllPlayers();
        }

        private void OpenForm(TypeOfEntering typeOfEntering)
        {
            switch(typeOfEntering)
            {
                case TypeOfEntering.Player: // у игрока будет другая форма, и интерфейс тоже вроде
                    {
                        form2 = new PlayerTeamsForm(currentUser);
                        form2.StartPosition = FormStartPosition.Manual;
                        form2.Location = this.DesktopLocation;
                        form2.Show();
                        this.Hide();
                        break;
                    }
                case TypeOfEntering.Trainer:
                    {
                        form2 = new TrainerTeamsForm(currentUser);
                        form2.StartPosition = FormStartPosition.Manual;
                        form2.Location = this.DesktopLocation;
                        form2.Show();
                        this.Hide();
                        break;
                    }
            }
            
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
            FieldChecker.FieldCheck(SteamIDTextBox, correctSteamIDSymbols);
        }
        private void PasswordTextBox_TextChanged(object sender, EventArgs e) //PasswordTextBox
        {
            FieldChecker.FieldCheck(PasswordTextBox, correctPasswordSymbols);
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
            switch(entering)
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

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            OpenForm(TypeOfEntering.Trainer);
        }
    }
}
