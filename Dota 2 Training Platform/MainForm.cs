using DataBaseManager;
using Dota_2_Training_Platform.Functions;
using Dota_2_Training_Platform.Models;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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


        private Panel tooltipPanel;
        private bool isMouseOverTooltip = false;
        private Timer tooltipTimer = new Timer { Interval = 150 };
        private Control tooltipOwner = null;
        private int tooltipRequestId = 0;

        private DotaMatchDetailsModel match;

        Color EditSwitchButtonColor;

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


            EditSwitchButtonColor = EditSwitcher.FillColor;


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
            SelectPlayerComboBox.Items.Clear();
            PanelWithTeams.Visible = false;
            OpenInMatchDetailsForm.Visible = false;
            MatchInfoPanel.Visible = false;
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
                button.FillColor = EditSwitchButtonColor;
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

        private async void SelectPlayerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedPlayerMatches.Controls.Clear();
            if (SelectPlayerComboBox.SelectedIndex == -1)
            {
                return;
            }
            SelectedPlayerPicture.Image = pictureBoxes[SelectPlayerComboBox.SelectedIndex].Image;
            PanelWithTeams.Visible = false;
            OpenInMatchDetailsForm.Visible = false;
            MatchInfoPanel.Visible = false;
            await LoadPlayerMatches(currentTeam.Players[SelectPlayerComboBox.SelectedIndex].AccountID);
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

        private async Task LoadPlayerMatches(string steamId)
        {
            SelectedPlayerMatches.Controls.Clear();

            var result = await ApiCourier.TryGetPlayerMatches(steamId, 10);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.ErrorMessage);
                return;
            }

            var matches = result.Data;

            // если профиль скрыт (OpenDota возвращает [])
            if (matches == null || matches.Count == 0)
            {
                Label label = new Label();
                label.Text = "Профиль игрока скрыт";
                label.ForeColor = Color.Black;
                label.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                label.AutoSize = false;
                label.Width = 360;
                label.Top = 20;
                label.Left = 2;
                label.TextAlign = ContentAlignment.MiddleCenter;

                SelectedPlayerMatches.Controls.Add(label);
                return;
            }

            foreach (var match in matches)
            {
                bool isRadiant = match.PlayerSlot < 128;
                bool playerWon = (isRadiant && match.RadiantWin) || (!isRadiant && !match.RadiantWin);
                string winnerSide = match.RadiantWin ? "Radiant" : "Dire";

                DateTime matchDate =
                    DateTimeOffset.FromUnixTimeSeconds(match.StartTime).LocalDateTime;

                TimeSpan duration = TimeSpan.FromSeconds(match.Duration);

                string text =
                    //$"Победитель: {winnerSide}\n" +
                    $"Время старта: {matchDate:dd.MM.yyyy HH:mm}\nДлительность: {duration:mm\\:ss}\nID:{match.MatchId}";

                Guna2Button button = new Guna2Button();
                button.Animated = true;
                button.BorderRadius = 0;
                button.Font = new Font(PlayerName1.Font, FontStyle.Regular); 

                button.Text = text;
                button.Width = SelectedPlayerMatches.Width - 20;
                button.Height = 70;

                button.Top = SelectedPlayerMatches.Controls.Count == 0
                    ? 2
                    : SelectedPlayerMatches.Controls[SelectedPlayerMatches.Controls.Count - 1].Bottom + 2;
                button.Left = 2;

                button.Tag = match.MatchId;

                button.FillColor = playerWon
                    ? Color.FromArgb(40, 120, 60)   // победа
                    : Color.FromArgb(115, 30, 30);  // поражение

                button.Click += LoadMatchInfo;

                SelectedPlayerMatches.Controls.Add(button);
            }
        }

        private async void LoadMatchInfo(object sender, EventArgs e)
        {
            var button = sender as Guna2Button;
            long matchId = (long)button.Tag;

            var result = await ApiCourier.TryGetMatch(matchId);

            tooltipOwner = null;
            if(tooltipPanel != null)
            {
                tooltipPanel.Visible = false;
                tooltipTimer.Stop();
            }
            

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.ErrorMessage);
                return;
            }

            match = result.Data;

            //MatchDetailsForm form = new MatchDetailsForm(match);

            PanelWithTeams.Visible = true;
            OpenInMatchDetailsForm.Visible = true;
            MatchInfoPanel.Visible = true;
            InitCustomTooltip();
            LoadMatch(match);

         

            tooltipTimer.Tick += Checker;

            //form.Show();
        }
        void Checker(object sender, EventArgs e)
        {
            if (tooltipOwner == null)
            {
                tooltipPanel.Visible = false;
                tooltipTimer.Stop();
            }
        }
        #region PlayerMatchInfo

        private void LoadMatch(DotaMatchDetailsModel match)
        {
            MatchID.Text = $"Матч: {match.match_id}";
            RadiantScore.Text = match.radiant_score.ToString();
            DireScore.Text = match.dire_score.ToString();
            WinnerLabel.Text = match.radiant_win ? "Победа сил Света" : "Победа сил Тьмы"; //Radiant / dire
            WinnerLabel.ForeColor = match.radiant_win ? Color.FromArgb(255, 105, 136, 34) : Color.FromArgb(255, 172, 60, 42); //Radiant / dire\

            GameMode.Text = LobbyAndGameModes.GameModes.ContainsKey(match.game_mode) ? LobbyAndGameModes.GameModes[match.game_mode] : "Неизвестно";

            LobbyType.Text = LobbyAndGameModes.LobbyTypes.ContainsKey(match.lobby_type) ? LobbyAndGameModes.LobbyTypes[match.lobby_type] : "Неизвестно";

            TimeSpan duration = TimeSpan.FromSeconds(match.duration);
            DurationLabel.Text = duration.ToString(@"mm\:ss");

            DateTime matchDate = DateTimeOffset.FromUnixTimeSeconds(match.start_time).LocalDateTime;
            StartTime.Text = $"{matchDate:dd.MM.yyyy HH:mm}";


            PopulateMatchPanels(match);
            //foreach (var player in match.players)
            //{
            //    string team = player.isRadiant ? "силы Света" : "силы Тьмы";

            //    string kda = $"{player.kills}/{player.deaths}/{player.assists}";

            //    MatchGrid.Rows.Add(
            //        player.personaname,
            //        team,
            //        ApiCourier.Heroes[player.hero_id],
            //        kda,
            //        player.net_worth
            //    );
            //}
        }
        private void PopulateMatchPanels(DotaMatchDetailsModel match)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 2000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.SetToolTip(KillsTip, "Убийств");
            toolTip1.SetToolTip(KillsTip2, "Убийств");
            toolTip1.SetToolTip(DeathsTip, "Смертей");
            toolTip1.SetToolTip(DeathsTip2, "Смертей");
            toolTip1.SetToolTip(AssistsTip, "Помощи");
            toolTip1.SetToolTip(AssistsTip2, "Помощи");
            toolTip1.SetToolTip(NetworthTip, "Общая ценность");
            toolTip1.SetToolTip(NetworthTip2, "Общая ценность");
            toolTip1.SetToolTip(LastHit_deniesTip, "Добитых крипов/Не отданных");
            toolTip1.SetToolTip(LastHit_deniesTip2, "Добитых крипов/Не отданных");
            toolTip1.SetToolTip(gpmTip, "Золото в минуту");
            toolTip1.SetToolTip(gpmTip2, "Золото в минуту");
            toolTip1.SetToolTip(DamageTip, "Урон по героям/Урон по постройкам");
            toolTip1.SetToolTip(DamageTip2, "Урон по героям/Урон по постройкам");


            //Label damageTip = new Label();
            //ToolTip toolTip = new ToolTip();
            //toolTip.
            //DamageTip.Text = "Урон по героям/Урон по постройкам";
            //hdLabel.
            radiantPanel.Controls.Clear();
            direPanel.Controls.Clear();

            int blockHeight = 60;
            int spacing = 5;

            foreach (var player in match.players)
            {
                // Создаём основной блок игрока
                Panel playerBlock = new Panel
                {
                    Width = radiantPanel.Width, // можно растянуть на всю ширину панели
                    Height = blockHeight,
                    BackColor = Color.FromArgb(240, 240, 240) // светло-серый фон
                };

                // Позиция внутри родительской панели зависит от уже добавленных блоков
                int yOffset = (player.isRadiant ? radiantPanel.Controls.Count : direPanel.Controls.Count) * (blockHeight + spacing);
                playerBlock.Location = new Point(0, yOffset);

                int x = 0;

                // 1 Иконка героя
                Guna2PictureBox heroPic = new Guna2PictureBox
                {
                    BackColor = Color.FromArgb(255, 120, 120, 120),
                    Width = 128,
                    Height = 60,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = Properties.Resources.Loading
                };
                LoadHeroImage(heroPic, player.hero_id);
                heroPic.Location = new Point(x, 0);
                x += heroPic.Width;

                heroPic.MouseEnter += (s, e) =>
                {
                    tooltipPanel.Visible = false;
                    tooltipOwner = heroPic;
                    ShowItemsTooltip(player, heroPic);
                };

                heroPic.MouseLeave += (s, e) =>
                {
                    tooltipOwner = null;
                    tooltipTimer.Start();
                };

                playerBlock.Controls.Add(heroPic);

                // 2 Имя игрока
                Label nameLabel = new Label
                {
                    BackColor = Color.FromArgb(255, 130, 130, 130),
                    Text = string.IsNullOrEmpty(player.personaname) ? "Аноним" : player.personaname,
                    Width = 135,
                    Height = 60,
                    Location = new Point(x, 0),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                x += nameLabel.Width;
                playerBlock.Controls.Add(nameLabel);

                // 3 Kills
                Label killsLabel = new Label
                {
                    BackColor = Color.FromArgb(255, 120, 120, 120),
                    Text = player.kills.ToString(),
                    Width = 41,
                    Height = 60,
                    Location = new Point(x, 0),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                x += killsLabel.Width;
                playerBlock.Controls.Add(killsLabel);

                // 4 Deaths
                Label deathsLabel = new Label
                {
                    BackColor = Color.FromArgb(255, 130, 130, 130),
                    Text = player.deaths.ToString(),
                    Width = 41,
                    Height = 60,
                    Location = new Point(x, 0),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                x += deathsLabel.Width;
                playerBlock.Controls.Add(deathsLabel);

                // 5 Assists
                Label assistsLabel = new Label
                {
                    BackColor = Color.FromArgb(255, 120, 120, 120),
                    Text = player.assists.ToString(),
                    Width = 41,
                    Height = 60,
                    Location = new Point(x, 0),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                x += assistsLabel.Width;
                playerBlock.Controls.Add(assistsLabel);

                // 6 Net Worth
                Label netWorthLabel = new Label
                {
                    BackColor = Color.FromArgb(255, 130, 130, 130),
                    Text = player.net_worth.ToString(),
                    Width = 71,
                    Height = 60,
                    Location = new Point(x, 0),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                x += netWorthLabel.Width;
                playerBlock.Controls.Add(netWorthLabel);

                // 7 Last Hits
                Label lhLabel = new Label
                {
                    BackColor = Color.FromArgb(255, 120, 120, 120),
                    Text = $"{player.last_hits}/{player.denies}",
                    Width = 76,
                    Height = 60,
                    Location = new Point(x, 0),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                x += lhLabel.Width;
                playerBlock.Controls.Add(lhLabel);

                // 8 Gold per Minute
                Label gpmLabel = new Label
                {
                    BackColor = Color.FromArgb(255, 130, 130, 130),
                    Text = player.gold_per_min.ToString(),
                    Width = 72,
                    Height = 60,
                    Location = new Point(x, 0),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                x += gpmLabel.Width;
                playerBlock.Controls.Add(gpmLabel);

                // 9 Damage
                Label hdLabel = new Label
                {
                    BackColor = Color.FromArgb(255, 120, 120, 120),
                    Text = $"{player.hero_damage}/{player.tower_damage}",
                    Width = 80,
                    Height = 60,
                    Location = new Point(x, 0),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                playerBlock.Controls.Add(hdLabel);

                // Добавляем блок в нужную панель
                if (player.isRadiant)
                    radiantPanel.Controls.Add(playerBlock);
                else
                    direPanel.Controls.Add(playerBlock);
            }
        }

        private async void LoadHeroImage(PictureBox pic, int heroId)
        {
            try
            {
                string url = ApiCourier.GetHeroImage(heroId);

                using (HttpClient client = new HttpClient())
                {
                    var stream = await client.GetStreamAsync(url);
                    Image img = Image.FromStream(stream);

                    pic.Invoke(new Action(() =>
                    {
                        pic.Image = img;
                        pic.SizeMode = PictureBoxSizeMode.StretchImage; // теперь растягиваем
                    }));
                }
            }
            catch
            {
                // если ошибка — можно оставить gif или поставить fallback
            }
        }
        private void InitCustomTooltip()
        {
            tooltipPanel = new Guna2Panel
            {
                Size = new Size(289, 164),
                BorderRadius = 5,
                BackColor = Color.FromArgb(40, 40, 40),
                Visible = false
            };
            tooltipPanel.MouseEnter += (s, e) =>
            {
                tooltipTimer.Stop();
            };

            tooltipPanel.MouseLeave += (s, e) =>
            {
                tooltipOwner = null;
                tooltipTimer.Start();
            };

            this.Controls.Add(tooltipPanel);
            tooltipPanel.BringToFront();
        }
        private void ShowItemsTooltip(MatchPlayerModel player, Control heroControl)
        {
            int currentId = ++tooltipRequestId;

            tooltipPanel.Controls.Clear();

            int spacing = 5;
            Size itemSize = new Size(66, 48);

            // --- первый ряд ---
            int x = 5, y = 5;
            int[] firstRow = { player.item_0, player.item_1, player.item_2 };
            foreach (var itemId in firstRow)
            {
                AddItemSlot(itemId, new Point(x, y));
                x += itemSize.Width + spacing;
            }

            // Нейтральный справа от первого ряда
            AddNeutralItemSlot(player.item_neutral, new Point(x, y));

            // --- второй ряд  ---
            x = 5;
            y += itemSize.Height + spacing;
            int[] secondRow = { player.item_3, player.item_4, player.item_5 };
            foreach (var itemId in secondRow)
            {
                AddItemSlot(itemId, new Point(x, y));
                x += itemSize.Width + spacing;
            }

            // --- третий ряд ---
            x = 5;
            y += itemSize.Height + spacing;
            int[] backpack = { player.backpack_0, player.backpack_1, player.backpack_2 };
            foreach (var itemId in backpack)
            {
                AddItemSlot(itemId, new Point(x, y), true);
                x += itemSize.Width + spacing;
            }

            Point pos = heroControl.PointToScreen(Point.Empty);
            pos = this.PointToClient(pos);
            if (currentId != tooltipRequestId) return;

            tooltipPanel.Location = new Point(pos.X + heroControl.Width, pos.Y - tooltipPanel.Height / 2 + itemSize.Height / 2);
            tooltipPanel.Visible = true;

            async Task AddItemSlot(int itemId, Point location, bool isBackpack = false)
            {
                Guna2PictureBox slot = new Guna2PictureBox
                {
                    Size = itemSize,
                    Location = location,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    FillColor = Color.FromArgb(100, 80, 80, 80),
                    ErrorImage = Properties.Resources.Loading
                };


                if (itemId != 0)
                {
                    LoadItemIntoSlot(slot, itemId, isBackpack);

                    ToolTip tip = new ToolTip();
                    tip.SetToolTip(slot, ApiCourier.ItemsById[itemId].dname);

                    slot.MouseEnter += (s, e) => isMouseOverTooltip = true;
                    slot.MouseLeave += (s, e) =>
                    {
                        isMouseOverTooltip = false;
                        tip.Hide(slot);
                    };
                    //if (isBackpack)
                    //{
                    //    slot.Image = MakeItemGray(slot.Image);
                    //}
                }

                tooltipPanel.Controls.Add(slot);
            }

            async Task AddNeutralItemSlot(int itemId, Point location, bool isBackpack = false)
            {
                Guna2CirclePictureBox slot = new Guna2CirclePictureBox
                {
                    Size = itemSize,
                    Location = location,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    FillColor = Color.FromArgb(100, 80, 80, 80),
                    ErrorImage = Properties.Resources.Loading
                };


                if (itemId != 0)
                {
                    LoadItemIntoSlot(slot, itemId, isBackpack);

                    ToolTip tip = new ToolTip();
                    tip.SetToolTip(slot, ApiCourier.ItemsById[itemId].dname);

                    slot.MouseEnter += (s, e) => isMouseOverTooltip = true;
                    slot.MouseLeave += (s, e) =>
                    {
                        isMouseOverTooltip = false;
                        tip.Hide(slot);
                    };
                    //if (isBackpack)
                    //{
                    //    slot.Image = MakeItemGray(slot.Image);
                    //}
                }

                tooltipPanel.Controls.Add(slot);
            }

        }
        private async Task LoadItemIntoSlot(PictureBox slot, int itemId, bool isBackpack)
        {
            slot.Image = Properties.Resources.Loading;

            var img = await ApiCourier.GetItemImageAsync(itemId);

            if (img == null) return;

            if (isBackpack)
                img = MakeItemGray(img);

            //slot.Invoke(new Action(() =>
            //{
            //    slot.Image = img;
            //}));
            slot.Image = img;
        }
        private Image MakeItemGray(Image original)
        {
            Bitmap grayBitmap = new Bitmap(original.Width, original.Height);

            using (Graphics g = Graphics.FromImage(grayBitmap))
            {
                // создаём серый цветовой матрикс
                var colorMatrix = new System.Drawing.Imaging.ColorMatrix(
                    new float[][]
                    {
                    new float[]{0.3f,0.3f,0.3f,0,0},
                    new float[]{0.3f,0.3f,0.3f,0,0},
                    new float[]{0.3f,0.3f,0.3f,0,0},
                    new float[]{0,0,0,1,0},
                    new float[]{0,0,0,0,1}
                    });

                var attributes = new System.Drawing.Imaging.ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);

                g.DrawImage(original,
                    new Rectangle(0, 0, original.Width, original.Height),
                    0, 0, original.Width, original.Height,
                    GraphicsUnit.Pixel, attributes);
            }

            return grayBitmap;
        }

        #endregion

        private void guna2Button1_Click(object sender, EventArgs e) //OpenInMatchDetailsForm
        {
            MatchDetailsForm form = new MatchDetailsForm(match);
            form.Show();
        }

        #region Trainings
        #endregion

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            CreateTrainingTask taskForm = new CreateTrainingTask();
            taskForm.Show();
        }
    }
}
