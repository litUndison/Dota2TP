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
using Dota_2_Training_Platform.Functions;
using Guna.UI2.WinForms;
using System.Web.WebSockets;

namespace Dota_2_Training_Platform
{
    public partial class TrainerTeamsForm : Form
    {
        UserModel currentUser;
        TeamModel currentTeam;
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
            PrintAllTeams();
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

        private void CreateTeamButton_Click(object sender, EventArgs e) // добавление команды
        {
            Guna2TextBox[] playerBoxes = new Guna2TextBox[] { PlayerBox1, PlayerBox2, PlayerBox3, PlayerBox4, PlayerBox5 };
            Guna2HtmlLabel[] nameboxes = { guna2HtmlLabel1, guna2HtmlLabel2, guna2HtmlLabel3, guna2HtmlLabel4, guna2HtmlLabel5 };
            Guna2PictureBox[] imageboxes = { PlayerPicture1, PlayerPicture2, PlayerPicture3, PlayerPicture4, PlayerPicture5 };
            for (int i = 0; i < playerBoxes.Length; i++)
            {
                playerBoxes[i].ReadOnly = false;
                playerBoxes[i].Text = "";
            }
            TeamNameBox.ReadOnly = false;
            for (int i = 0; i < nameboxes.Length; i++)
            {
                nameboxes[i].Text = $"Игрок {i+1}";
            }
            TeamConfirm.Visible = true;
            for (int i = 0; i < imageboxes.Length; i++)
            {
                imageboxes[i].Image = null;
            }
            MessageBox.Show("Поля доступны для ввода");
        }
        private async void TeamConfirm_Click(object sender, EventArgs e)
        {
            Guna2TextBox[] playerBoxes = { PlayerBox1, PlayerBox2, PlayerBox3, PlayerBox4, PlayerBox5 };

            if (string.IsNullOrWhiteSpace(TeamNameBox.Text) || TeamNameBox.Text.Length < 5)
            {
                MessageBox.Show("Название команды не введено или менее 5 символов");
                return;
            }

            // Список задач API
            List<Task<ApiCourier.ApiResult<DotaPlayerProfileModel>>> apiTasks = new List<Task<ApiCourier.ApiResult<DotaPlayerProfileModel>>>();

            for (int i = 0; i < playerBoxes.Length; i++)
            {
                string input = playerBoxes[i].Text.Trim();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    apiTasks.Add(ApiCourier.TryGetUserInfo(input));
                }
            }

            // Ждем все API запросы параллельно
            var results = await Task.WhenAll(apiTasks);

            List<DotaPlayerProfileModel> players = new List<DotaPlayerProfileModel>();

            foreach (var res in results)
            {
                if (!res.IsSuccess)
                {
                    MessageBox.Show(res.ErrorMessage);
                    return;
                }

                players.Add(res.Data);
            }

            if (players.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы одного игрока");
                return;
            }

            await Task.Run(() => dbManager.CreateTeam(TeamNameBox.Text, currentUser.SteamID, players));

            TeamConfirm.Visible = false;

            PrintAllTeams();
        }





        private void PrintAllTeams()
        {
            guna2Panel1.Controls.Clear(); // ВОТ ЭТО ОБЯЗАТЕЛЬНО

            currentTeams = dbManager.GetTrainerTeams(currentUser.SteamID);

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
        }

        private void TeamNameBox_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerBox1, correctTeamSymbols);
            //string.IsNullOrWhiteSpace(TeamNameBox.Text)
        }

        private void PlayerBox1_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerBox1, correctSteamIDSymbols);
        }

        private void PlayerBox2_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerBox2, correctSteamIDSymbols);
        }

        private void PlayerBox3_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerBox3, correctSteamIDSymbols);
        }

        private void PlayerBox4_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerBox4, correctSteamIDSymbols);
        }

        private void PlayerBox5_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerBox5, correctSteamIDSymbols);
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e) // ContinueButton
        {
            // создать новую форму в которую буду передавать currentTeam и currentUser
        }



        //private void DoSmth(object sender, EventArgs e)
        //{
        //    MessageBox.Show($"1223 {guna2Panel1.Controls.GetChildIndex((Button)sender)}"); // чтобы узнать на какой позиции стоит кнопка 
        //}
    }
}
