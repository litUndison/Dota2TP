using Dota_2_Training_Platform.Models;
using DataBaseManager;
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
using Dota_2_Training_Platform.Functions;

namespace Dota_2_Training_Platform
{
    public partial class MainForm : Form
    {

        #region Initializings // все инициализации переменных

        UserModel currentUser;
        TeamModel currentTeam;

        Guna2PictureBox[] pictureBoxes;
        Guna2HtmlLabel[] htmlLabels;
        Guna2TextBox[] textBoxes;

        bool canEdit = false;

        List<string> newNames = new List<string>();
        bool selfclose = false;
        Form StartForm;
        Form TeamsForm;

        #endregion

        public MainForm(TeamModel currentTeam, UserModel currentUser, Form StartForm, Form TeamsForm)
        {
            InitializeComponent();
            this.currentTeam = currentTeam;
            this.currentUser = currentUser;
            this.TeamsForm = TeamsForm;
            this.StartForm = StartForm;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            //DialogResult result = MessageBox.Show("Вы хотите вернутся в меню входа?", "Подтверждение", 
            //    MessageBoxButtons.YesNo,
            //    MessageBoxIcon.Question
            //);

            //if (result == DialogResult.Yes)
            //{
            //    // Пользователь нажал "Да"
            //    Application.Exit();
            //}
            //else
            //{
            //    // Пользователь нажал "Нет"
            //    //MessageBox.Show("Операция отменена");

            //}
            if (!selfclose)
            {
                Application.Exit();
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            #region trash // лучше это не видеть

            Guna2PictureBox[] buffer1 = { PlayerPicture1, PlayerPicture2, PlayerPicture3, PlayerPicture4, PlayerPicture5 };
            pictureBoxes = buffer1;
            Guna2HtmlLabel[] buffer2 = { PlayerName1, PlayerName2, PlayerName3, PlayerName4, PlayerName5 };
            htmlLabels = buffer2;
            Guna2TextBox[] buffer3 = { PlayerID1, PlayerID2, PlayerID3, PlayerID4, PlayerID5 };
            textBoxes = buffer3;

            #endregion


            LoadTeam();

            TrainerPicture.LoadAsync(currentUser.Avatarfull);
            TrainerName.Text = currentUser.Name;
            TrainerID.Text = currentUser.AccountID;



            //размещение кнопок меню над gunaPage

            PanelWithButtons.Parent = this; // форма
            PanelWithButtons.BringToFront();

            PanelWithButtons.Location = new Point(0, PanelWithButtons.Location.Y);
            PanelWithButtons.Anchor = AnchorStyles.Top | AnchorStyles.Left;


            //добавление всех игроков в выпадающий список (в анализе статистики)

            PlayersComboBoxFill();
        }
        private void PlayersComboBoxFill()
        {
            for (int i = 0; i < currentTeam.Players.Count; i++)
            {
                SelectPlayerComboBox.Items.Add(currentTeam.Players[i].Name);
            }
        }
        private void PlayerBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void PlayerPicture1_Click(object sender, EventArgs e)
        {

        }


        private void LoadTeam()
        {
            //очистка полей

            for (int i = 0; i < 5; i++)
            {
                pictureBoxes[i].Image = null;
                htmlLabels[i].Text = "Игрок";
                textBoxes[i].Text = "";
            }

            //загрузка новых игроков
            TeamName.Text = currentTeam.Name;
            for (int i = 0; i < currentTeam.Players.Count; i++)
            {
                if (!string.IsNullOrEmpty(currentTeam.Players[i].Avatarfull))
                {
                    pictureBoxes[i].LoadAsync(currentTeam.Players[i].Avatarfull);
                }
                htmlLabels[i].Text = currentTeam.Players[i].Name;
                textBoxes[i].Text = currentTeam.Players[i].AccountID;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e) // переключатель редактирования
        {
            SwitchTeamEdit(EditSwitcher);
        }

        private bool SwitchTeamEdit(Guna2Button button)
        {
            canEdit = !canEdit;
            if (canEdit)
            {
                EditConfirm.Visible = true;
                button.Text = "Выключить редактирование";
                button.FillColor = Color.FromArgb(255, 128, 128);
                TeamName.ReadOnly = false;
                for (int i = 0; i < textBoxes.Length; i++)
                {
                    textBoxes[i].ReadOnly = false;
                }
            }
            else
            {
                EditConfirm.Visible = false;
                button.Text = "Включить редактирование";
                button.FillColor = Color.FromArgb(94, 148, 255);
                TeamName.ReadOnly = true;
                for (int i = 0; i < textBoxes.Length; i++)
                {
                    textBoxes[i].ReadOnly = true;
                }

            }
            return canEdit;
        }

        private async void EditConfirm_Click(object sender, EventArgs e)
        {
            EditConfirm.Enabled = false;

            try
            {
                string newTeamName = TeamName.Text.Trim();

                var team = await dbManager.UpdateTeamFullAsync(currentTeam, newTeamName, PlayerID1.Text, PlayerID2.Text, PlayerID3.Text, PlayerID4.Text, PlayerID5.Text);

                if (team != null)
                {
                    currentTeam = team;
                    LoadTeam();
                    MessageBox.Show("Команда успешно обновлена");
                    SwitchTeamEdit(EditSwitcher);
                    PlayersComboBoxFill();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            EditConfirm.Enabled = true;
        }

        private void TeamName_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerID1, FieldChecker.CheckType.Team);
        }

        private void PlayerID1_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerID1, FieldChecker.CheckType.SteamID);
        }

        private void PlayerID2_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerID2, FieldChecker.CheckType.SteamID);
        }

        private void PlayerID3_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerID3, FieldChecker.CheckType.SteamID);
        }

        private void PlayerID4_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerID4, FieldChecker.CheckType.SteamID);
        }

        private void PlayerID5_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerID5, FieldChecker.CheckType.SteamID);
        }

        private void ToEnterFormButton_Click(object sender, EventArgs e)
        {
            selfclose = true;
            EnterForm form = StartForm as EnterForm;

            form.StartPosition = FormStartPosition.Manual;

            int x = this.DesktopLocation.X + (this.Width - form.Width) / 2;
            int y = this.DesktopLocation.Y + (this.Height - form.Height) / 2;

            form.Location = new Point(x, y);
            this.Close();
            form.Show();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ChangeTeamButton_Click(object sender, EventArgs e)
        {
            selfclose = true;
            TrainerTeamsForm form = TeamsForm as TrainerTeamsForm;

            form.StartPosition = FormStartPosition.Manual;

            int x = this.DesktopLocation.X + (this.Width - form.Width) / 2;
            int y = this.DesktopLocation.Y + (this.Height - form.Height) / 2;

            form.Location = new Point(x, y);
            this.Close();
            form.Show();
            form.PrintAllTeams();
        }

        private void SelectPlayerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedPlayerMatches.Controls.Clear();
            if (SelectPlayerComboBox.SelectedIndex != -1)
            {
                SelectedPlayerPicture.Image = pictureBoxes[SelectPlayerComboBox.SelectedIndex].Image;
            }

            //должна происходить загрузка игр выбранного игрока



            //currentTeams = dbManager.GetTrainerTeams(currentUser.SteamID);

            //foreach (TeamModel team in currentTeams)
            //{
            //    Guna2Button button = new Guna2Button();
            //    button.Animated = true;
            //    button.BorderRadius = 10;
            //    Font font = new Font(PlayerName1.Font, FontStyle.Regular);
            //    button.Font = font;
            //    button.Text = team.Name;
            //    button.Width = 250;
            //    button.Height = 35;

            //    button.Top = guna2Panel1.Controls.Count == 0
            //        ? 10
            //        : guna2Panel1.Controls[guna2Panel1.Controls.Count - 1].Bottom + 2;

            //    button.Tag = team;
            //    button.Click += LoadMatch;

            //    guna2Panel1.Controls.Add(button);
            //}
        }

        private void LoadMatch(object sender, EventArgs e)
        {

        }
    }
}
