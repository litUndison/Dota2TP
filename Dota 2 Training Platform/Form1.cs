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
using JsonManager;

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
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e) // Тренер
        {
            OpenForm(TypeOfEntering.Trainer);
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e) // Игрок
        {
            OpenForm(TypeOfEntering.Player);
        }

        private void OpenForm(TypeOfEntering typeOfEntering)
        {
            form2 = new Form2(typeOfEntering);
            form2.StartPosition = FormStartPosition.Manual;
            form2.Location = this.DesktopLocation;
            form2.Show();
            this.Hide();
        }
    }
}
