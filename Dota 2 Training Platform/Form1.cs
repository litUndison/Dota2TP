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

namespace Dota_2_Training_Platform
{
    public partial class Form1 : Form
    {
        Form2 form2;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dbManager.Start();
            //dbManager.ReadAllPlayers();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e) // Тренер
        {
            //OpenForm(TypeOfEntering.Trainer);
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e) // Игрок
        {
            //OpenForm(TypeOfEntering.Player);
        }

        private void OpenForm(TypeOfEntering typeOfEntering)
        {
            form2 = new Form2(typeOfEntering);
            form2.StartPosition = FormStartPosition.Manual;
            form2.Location = this.DesktopLocation;
            form2.Show();
            this.Hide();
        }
        #region PlayerSignInUp
        private void guna2GradientButton4_Click(object sender, EventArgs e) // Вход игрока
        {
            if(SteamIDTextBoxPlayer.Text.Length < 5)
            {
                SteamIDTextBoxPlayer.BorderColor = Color.Red;
                return;
            }
            if(PasswordTextBoxPlayer.Text.Length < 5)
            {
                PasswordTextBoxPlayer.BorderColor = Color.Red;
                return;
            }
            if (PlayerSignIn(SteamIDTextBoxPlayer.Text, PasswordTextBoxPlayer.Text))
            {
                MessageBox.Show("Вход успешен");
            }
            else
            {
                MessageBox.Show("Вход не успешен");
            }
        }

        private async void guna2GradientButton3_Click(object sender, EventArgs e) // регистрация игрока
        {
            if (await PlayerSignUp(SteamIDTextBoxPlayer.Text, PasswordTextBoxPlayer.Text))
            {
                MessageBox.Show("Регистрация успешна!");
            }
            else
            {
                MessageBox.Show("Регистрация не успешна(");
            }
        }
        #endregion

        #region TrainerSignInUp
        private void guna2GradientButton6_Click(object sender, EventArgs e) // вход тренера
        {
            if (SteamIDTextBoxTrainer.Text.Length < 5)
            {
                SteamIDTextBoxTrainer.BorderColor = Color.Red;
                return;
            }
            if (PasswordTextBoxTrainer.Text.Length < 5)
            {
                PasswordTextBoxTrainer.BorderColor = Color.Red;
                return;
            }
            if (TrainerSignIn(SteamIDTextBoxTrainer.Text, PasswordTextBoxTrainer.Text))
            {
                MessageBox.Show("Вход успешен");
            }
            else
            {
                MessageBox.Show("Вход не успешен");
            }
        }

        private async void guna2GradientButton5_Click(object sender, EventArgs e) // регистрация тренера
        {
            if (await TrainerSignUp(SteamIDTextBoxTrainer.Text, PasswordTextBoxTrainer.Text))
            {
                MessageBox.Show("Регистрация успешна!");
            }
            else
            {
                MessageBox.Show("Регистрация не успешна(");
            }
        }
        #endregion
        



        public async Task<bool> PlayerSignUp(string SteamID, string Password)
        {
            if (SteamIDTextBoxPlayer.Text.Length != 0 && PasswordTextBoxPlayer.Text.Length != 0)
            {
                DotaPlayerProfileModel player = await dbManager.AddPlayer(SteamID, Password);
                if (player != null)
                {
                    return true;
                }
            }
            return false;
        }
        public bool PlayerSignIn(string SteamID, string Password)
        {
            foreach (var user in dbManager.Players)
            {
                if ((user.AccountID == SteamID || user.SteamID == SteamID) && user.Password == Password)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> TrainerSignUp(string SteamID, string Password)
        {
            if (SteamIDTextBoxTrainer.Text.Length != 0 && PasswordTextBoxTrainer.Text.Length != 0)
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
            foreach (var user in dbManager.Trainers)
            {
                if ((user.AccountID == SteamID || user.SteamID == SteamID) && user.Password == Password)
                {
                    return true;
                }
            }
            return false;
        }

        private void SteamIDTextBoxPlayer_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
