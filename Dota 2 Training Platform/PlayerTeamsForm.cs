using DataBaseManager;
using Dota_2_Training_Platform.Functions;
using Dota_2_Training_Platform.Models;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dota_2_Training_Platform
{
    public partial class PlayerTeamsForm : Form
    {
        UserModel currentUser;
        TeamModel currentTeam;
        List<TeamModel> currentTeams = new List<TeamModel>();
        Color color;
        string correctTeamSymbols = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя 1234567890abcdefghijklmnopqrstuvwxyz-_";
        string correctSteamIDSymbols = "1234567890";

        bool selfExit = false;
        public PlayerTeamsForm(UserModel currentUser)
        {
            this.currentUser = currentUser;
            InitializeComponent();
        }

        private void PlayerTeamsForm_Load(object sender, EventArgs e)
        {
            color = PlayerBox1.FocusedState.BorderColor;
            PrintAllTeams();
        }
        private void PlayerTeamsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!selfExit)
                Application.Exit();
        }


        private void guna2GradientButton3_Click(object sender, EventArgs e) // Назад на Form1
        {
            EnterForm enterForm = new EnterForm();
            enterForm.StartPosition = FormStartPosition.Manual;
            enterForm.Location = this.DesktopLocation;
            enterForm.Show();
            selfExit = true;
            this.Close();
        }




        private void PrintAllTeams()
        {
            guna2Panel1.Controls.Clear(); // ВОТ ЭТО ОБЯЗАТЕЛЬНО

            currentTeams = dbManager.GetPlayerTeams(currentUser.SteamID);

            foreach (TeamModel team in currentTeams)
            {
                Guna2Button button = new Guna2Button();
                button.Animated = true;
                button.BorderRadius = 10;
                Font font = new Font(guna2HtmlLabel1.Font, FontStyle.Regular);
                button.Font = font;
                button.Text = team.Name;
                button.Width = 250;
                button.Height = 35;

                button.Top = guna2Panel1.Controls.Count == 0
                    ? 10
                    : guna2Panel1.Controls[guna2Panel1.Controls.Count - 1].Bottom + 2;

                button.Tag = team; // ВАЖНО
                button.Click += LoadTeam;

                guna2Panel1.Controls.Add(button);
            }
        }

        private void LoadTeam(object sender, EventArgs e)
        {
            var button = (Guna2Button)sender;
            var team = (TeamModel)button.Tag;

            ShowAllMembers(team);
        }

        private void ShowAllMembers(TeamModel team)
        {
            Guna2TextBox[] playerBoxes = { PlayerBox1, PlayerBox2, PlayerBox3, PlayerBox4, PlayerBox5 };
            Guna2HtmlLabel[] nameboxes = { guna2HtmlLabel1, guna2HtmlLabel2, guna2HtmlLabel3, guna2HtmlLabel4, guna2HtmlLabel5 };
            Guna2PictureBox[] imageboxes = { PlayerPicture1, PlayerPicture2, PlayerPicture3, PlayerPicture4, PlayerPicture5 };
            Guna2HtmlLabel[] selectors = { PlayerSelector1, PlayerSelector2, PlayerSelector3, PlayerSelector4, PlayerSelector5 };

            // Очистка
            for (int i = 0; i < playerBoxes.Length; i++)
            {
                playerBoxes[i].Text = "";
                playerBoxes[i].ReadOnly = true;
                nameboxes[i].Text = $"Игрок {i + 1}";
                imageboxes[i].Image = null;
            }

            TeamNameBox.ReadOnly = true;

            TeamNameBox.Text = team.Name;
            // Заполнение
            for (int i = 0; i < team.Players.Count && i < playerBoxes.Length; i++)
            {
                playerBoxes[i].Text = team.Players[i].AccountID;
                nameboxes[i].Text = team.Players[i].Name;

                if (!string.IsNullOrEmpty(team.Players[i].Avatarfull))
                {
                    imageboxes[i].LoadAsync(team.Players[i].Avatarfull);
                }
            }

            currentTeam = team;
            ContinueButton.Visible = true;
            for (int i = 0; i < nameboxes.Length; i++)
            {
                if(nameboxes[i].Text == currentUser.Name)
                {
                    nameboxes[i].ForeColor = Color.Blue;
                    selectors[i].Visible = true;
                    selectors[i].ForeColor = Color.Blue;
                    break;
                }
            }
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            selfExit = true;
            // создать новую форму в которую буду передавать currentTeam и currentUser
        }



        //private void DoSmth(object sender, EventArgs e)
        //{
        //    MessageBox.Show($"1223 {guna2Panel1.Controls.GetChildIndex((Button)sender)}"); // чтобы узнать на какой позиции стоит кнопка 
        //}
    }
}
