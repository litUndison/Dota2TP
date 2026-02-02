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
using JsonManager;

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
            if (SteamIDTextBox.Text.Length != 0 && PasswordTextBox.Text.Length != 0)
            {
                try
                {
                    DotaPlayerProfileModel profileInfo = new DotaPlayerProfileModel();
                    profileInfo = await ApiCourier.TryGetUserInfo(SteamIDTextBox.Text);
                    if (profileInfo != null)
                    {
                        MessageBox.Show("Работаец " + profileInfo.profile.ToString());
                        jManager.AddUser(SteamIDTextBox.Text, PasswordTextBox.Text);
                    }
                }
                catch(Exception ex)
                {

                }
                

            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e) // Вход
        {
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

    }
}
