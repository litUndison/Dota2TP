using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DataBaseManager;
using Dota_2_Training_Platform.Models;
using Guna.UI2.WinForms;

namespace Dota_2_Training_Platform
{
    public partial class TrainerTeamsForm : Form
    {
        UserModel currentUser;
        List<TeamModel> currentTeams = new List<TeamModel>();
        Color color;
        string correctTeamSymbols = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя 1234567890abcdefghijklmnopqrstuvwxyz-_";
        string correctSteamIDSymbols = "1234567890";
        public TrainerTeamsForm(UserModel currentUser)
        {
            this.currentUser = currentUser;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            color = PlayerBox1.FocusedState.BorderColor;
        }


        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e) // Назад на Form1
        {
            EnterForm enterForm = new EnterForm();
            enterForm.StartPosition = FormStartPosition.Manual;
            enterForm.Location = this.DesktopLocation;
            enterForm.Show();
            this.Close();
        }

        private async void CreateTeamButton_Click(object sender, EventArgs e)
        {
            List<string> steamids = new List<string>();
            Guna2TextBox[] playerBoxes = new Guna2TextBox[] { PlayerBox1, PlayerBox2, PlayerBox3, PlayerBox4, PlayerBox5 };

            if (string.IsNullOrWhiteSpace(TeamNameBox.Text) || TeamNameBox.Text.Length < 5)
            {
                MessageBox.Show("Название команды не введено или название менее 5 символов");
                return;
            }

            for (int i = 0; i < playerBoxes.Length; i++)
            {
                string input = playerBoxes[i].Text.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue; // пропускаем пустые поля
                }

                var apiResult = await ApiCourier.TryGetUserInfo(input);

                if (!apiResult.IsSuccess)
                {
                    MessageBox.Show($"Ошибка у игрока {i + 1}: {apiResult.ErrorMessage}");
                    return;
                }

                var profileInfo = apiResult.Data;
                //Console.WriteLine("Adding player: " + playerSteamId);
                steamids.Add(profileInfo.profile.steamid.ToString());
            }

            if (steamids.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы одного игрока в команду");
                return;
            }

            dbManager.CreateTeam(TeamNameBox.Text, currentUser.SteamID, steamids);
            PrintAllTeams();
        }



        private void PrintAllTeams()
        {
            int top = 10;
            int left = 10;
            currentTeams = dbManager.GetTrainerTeams(currentUser.SteamID);

            foreach (TeamModel team in currentTeams)
            {
                MessageBox.Show($"Team {team.Name} has {team.Players.Count} players");
                Button button = new Button();
                button.Left = left;
                if (guna2Panel1.Controls.Count > 0)
                {
                    button.Top = guna2Panel1.Controls[guna2Panel1.Controls.Count - 1].Top + button.Height + 2;
                }
                else
                {
                    button.Top = top;
                }

                button.Text = $"{team.Name}";
                button.Click += LoadTeam;
                guna2Panel1.Controls.Add(button);
                label1.Text = guna2Panel1.Controls.Count.ToString();
            }
        }

        private void LoadTeam(object sender, EventArgs e)
        {
            ShowAllMembers(guna2Panel1.Controls.GetChildIndex((Button)sender));
        }

        private void ShowAllMembers(int index)
        {
            Guna2TextBox[] playerBoxes = new Guna2TextBox[] { PlayerBox1, PlayerBox2, PlayerBox3, PlayerBox4, PlayerBox5 };
            Guna2HtmlLabel[] nameboxes = new Guna2HtmlLabel[] { guna2HtmlLabel1, guna2HtmlLabel2, guna2HtmlLabel3, guna2HtmlLabel4, guna2HtmlLabel5 };

            if (index < 0 || index >= currentTeams.Count) return;

            TeamModel team = currentTeams[index];

            // Очищаем все TextBox
            foreach (var box in playerBoxes)
                box.Text = "";

            // Заполняем игроков в PlayerBox1-5
            for (int i = 0; i < team.Players.Count && i < playerBoxes.Length; i++)
            {
                playerBoxes[i].Text = team.Players[i].AccountID; // или любой другой идентификатор игрока
                nameboxes[i].Text = team.Players[i].Name;
            }
        }

        private void TeamNameBox_TextChanged(object sender, EventArgs e)
        {
            FieldCheck(PlayerBox1, correctTeamSymbols);
            //string.IsNullOrWhiteSpace(TeamNameBox.Text)
        }

        private void PlayerBox1_TextChanged(object sender, EventArgs e)
        {
            FieldCheck(PlayerBox1, correctSteamIDSymbols);
        }

        private void PlayerBox2_TextChanged(object sender, EventArgs e)
        {
            FieldCheck(PlayerBox2, correctSteamIDSymbols);
        }

        private void PlayerBox3_TextChanged(object sender, EventArgs e)
        {
            FieldCheck(PlayerBox3, correctSteamIDSymbols);
        }

        private void PlayerBox4_TextChanged(object sender, EventArgs e)
        {
            FieldCheck(PlayerBox4, correctSteamIDSymbols);
        }

        private void PlayerBox5_TextChanged(object sender, EventArgs e)
        {
            FieldCheck(PlayerBox5, correctSteamIDSymbols);
        }


        private void FieldCheck(Guna2TextBox currentTextBox, string validationString)
        {
            if (currentTextBox.Text != null && currentTextBox.Text.Length != 0)
            {
                for (int i = 0; i < currentTextBox.Text.Length; i++)
                {
                    if (!validationString.Contains(currentTextBox.Text[i].ToString().ToLower()))
                    {
                        currentTextBox.Text = currentTextBox.Text.Remove(i, 1);
                        i--;
                        currentTextBox.SelectionStart = currentTextBox.Text.Length;
                        currentTextBox.BorderColor = Color.Red;
                        currentTextBox.FocusedState.BorderColor = Color.Red;
                        continue;
                    }
                    currentTextBox.BorderColor = Color.Gray;
                    currentTextBox.FocusedState.BorderColor = color;
                }
            }
        }

        //private void DoSmth(object sender, EventArgs e)
        //{
        //    MessageBox.Show($"1223 {guna2Panel1.Controls.GetChildIndex((Button)sender)}"); // чтобы узнать на какой позиции стоит кнопка 
        //}
    }
}
