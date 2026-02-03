using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DataBaseManager;
using Dota_2_Training_Platform.Models;

namespace Dota_2_Training_Platform
{
    public enum TypeOfEntering
    {
        Player,
        Trainer
    }

    public partial class Form2 : Form
    {
        TypeOfEntering typeOfEntering;
        public Form2(TypeOfEntering typeOfEntering)
        {
            this.typeOfEntering = typeOfEntering;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private async void guna2GradientButton2_Click(object sender, EventArgs e) // Регистрация
        {
            switch(typeOfEntering)
            {
                case TypeOfEntering.Player:
                    {
                        if(await PlayerSignUp(SteamIDTextBox.Text, PasswordTextBox.Text))
                        {
                            // что-то сделать если зарегался
                        }
                        else
                        {

                        }
                        break;
                    }
                case TypeOfEntering.Trainer:
                    {
                        break;
                    }
            }
        }


        public async Task<bool> PlayerSignUp(string SteamID, string Password)
        {
            if (SteamIDTextBox.Text.Length != 0 && PasswordTextBox.Text.Length != 0)
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
            foreach(var user in dbManager.Players)
            {
                if(user.SteamID == SteamID && user.Password == Password)
                {
                    return true;
                }
            }
            return false;
        }
        private void guna2GradientButton1_Click(object sender, EventArgs e) // Вход
        {
            switch (typeOfEntering)
            {
                case TypeOfEntering.Player:
                    {
                        if (PlayerSignIn(SteamIDTextBox.Text, PasswordTextBox.Text))
                        {
                            // что-то сделать если зарегался
                        }
                        else
                        {

                        }
                        break;
                    }
                case TypeOfEntering.Trainer:
                    {
                        break;
                    }
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e) // Назад на Form1
        {
            
        }
    }
}
