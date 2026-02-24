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

        List<string> oldNames = new List<string>();
        List<string> newNames = new List<string>();

        #endregion

        public MainForm(TeamModel currentTeam, UserModel currentUser)
        {
            InitializeComponent();
            this.currentTeam = currentTeam;
            this.currentUser = currentUser;
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
            Application.Exit();
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
        }

        private void PlayerBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void PlayerPicture1_Click(object sender, EventArgs e)
        {

        }


        private void LoadTeam()
        {
            TeamName.Text = currentTeam.Name;
            oldNames.Add(TeamName.Text);
            for (int i = 0; i < currentTeam.Players.Count; i++)
            {
                if (!string.IsNullOrEmpty(currentTeam.Players[i].Avatarfull))
                {
                    pictureBoxes[i].LoadAsync(currentTeam.Players[i].Avatarfull);
                }
                htmlLabels[i].Text = currentTeam.Players[i].Name;
                textBoxes[i].Text = currentTeam.Players[i].AccountID;
                oldNames.Add(textBoxes[i].Text);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e) // переключатель редактирования
        {
            if(SwitchTeamEdit())
            { // Edit enabled
                EditConfirm.Visible = true;
                EditButton.Text = "Выключить редактирование";
                EditButton.FillColor = Color.FromArgb(255, 128, 128);
            }
            else
            { // Edit disabled
                EditConfirm.Visible = false;
                EditButton.Text = "Включить редактирование";
                EditButton.FillColor = Color.FromArgb(94, 148,255);
            }
        }

        private bool SwitchTeamEdit()
        {
            canEdit = !canEdit;
            if (canEdit)
            {
                for (int i = 0; i < textBoxes.Length; i++)
                {
                    textBoxes[i].ReadOnly = false;
                }
            }
            else
            {
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

                List<string> newIds = textBoxes
                    .Select(tb => tb.Text.Trim())
                    .Where(x => !string.IsNullOrEmpty(x))
                    .ToList();

                await dbManager.UpdateTeamFullAsync(currentTeam, newTeamName, newIds);


                currentTeam = dbManager.GetTeam(currentTeam.Id);
                LoadTeam();
                MessageBox.Show("Команда успешно обновлена");
                
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
            FieldChecker.FieldCheck(PlayerID1, FieldChecker.CheckType.SteamID);
        }

        private void PlayerID3_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerID1, FieldChecker.CheckType.SteamID);
        }

        private void PlayerID4_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerID1, FieldChecker.CheckType.SteamID);
        }

        private void PlayerID5_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerID1, FieldChecker.CheckType.SteamID);
        }
    }
}
