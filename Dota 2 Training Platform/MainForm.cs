using DataBaseManager;
using Dota_2_Training_Platform.Functions;
using Dota_2_Training_Platform.Models;
using Dota_2_Training_Platform.Models.Trainings;
using Dota_2_Training_Platform.Trainings;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Configuration;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TheArtOfDevHtmlRenderer.Adapters;
using Microsoft.Web.WebView2.Core;

namespace Dota_2_Training_Platform
{
    public partial class MainForm : Form
    {

        #region Initializings // все инициализации переменных

        UserModel currentUser;
        TeamModel currentTeam;
        List<TrainingTask> tasks = new List<TrainingTask>();
        bool isTasksLoading = false;
        bool isTaskAnalysisRunning = false;
        bool progressDataDirty = true;
        bool isTaskDeleteMode = false;
        Guna2HtmlLabel tasksLoadingLabel;
        bool isProgressLoading = false;
        bool pendingProgressMetricRefresh = false;
        bool pendingProgressPlayerRefresh = false;

        private TabPage progressTabPage;
        private Label progressStatusLabel;
        private Label kpiActiveLabel;
        private Label kpiOverdueLabel;
        private Label kpiCompletedWeekLabel;
        private Label kpiCompletionRateLabel;
        private FlowLayoutPanel progressDeadlinesPanel;
        private FlowLayoutPanel progressTaskProgressPanel;
        private ComboBox progressPlayerComboBox;
        private ComboBox progressMetricComboBox;
        private Chart playerTrendChart;
        private Chart teamCompareChart;
        private const string DotaTwitchCategoryUrl = "https://www.twitch.tv/directory/category/dota-2";
        private bool isTwitchForcedRedirect = false;
        private static readonly HttpClient _aiHttpClient = new HttpClient();
        private static DateTime _aiRateLimitUntilUtc = DateTime.MinValue;

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

            guna2TabControl1.SelectedIndexChanged += Guna2TabControl1_SelectedIndexChanged;

            //добавление всех игроков в выпадающий список (в анализе статистики)
            PlayersComboBoxFill();
            InitializeProgressTab();
            InitializeTasksLoadingLabel();
            InitializeTwitchRestrictions();
            _ = RefreshTasksAsync();
        }
        private async void Guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExitTaskDeleteMode();
            if (guna2TabControl1.SelectedTab == tabPage5 && progressDataDirty)
            {
                await RefreshProgressTabAsync();
                progressDataDirty = false;
            }
        }
        private void InitializeTasksLoadingLabel()
        {
            tasksLoadingLabel = new Guna2HtmlLabel();
            tasksLoadingLabel.Text = "Загрузка тренировок...";
            tasksLoadingLabel.AutoSize = false;
            tasksLoadingLabel.Width = guna2HtmlLabel2.Width;
            tasksLoadingLabel.Height = guna2HtmlLabel2.Height;
            tasksLoadingLabel.TextAlignment = ContentAlignment.MiddleCenter;
            tasksLoadingLabel.ForeColor = Color.DimGray;
            tasksLoadingLabel.BackColor = Color.Transparent;
            tasksLoadingLabel.Location = guna2HtmlLabel2.Location;
            tasksLoadingLabel.Visible = false;
            tabPage4.Controls.Add(tasksLoadingLabel);
            tasksLoadingLabel.BringToFront();
        }

        private void InitializeProgressTab()
        {
            progressTabPage = tabPage5;
            progressTabPage.BackColor = Color.White;
            progressTabPage.Padding = new Padding(8);
            progressTabPage.Controls.Clear();

            kpiActiveLabel = new Label { Left = 10, Top = 10, Width = 220, Height = 28, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            kpiOverdueLabel = new Label { Left = 240, Top = 10, Width = 220, Height = 28, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            kpiCompletedWeekLabel = new Label { Left = 470, Top = 10, Width = 260, Height = 28, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            kpiCompletionRateLabel = new Label { Left = 740, Top = 10, Width = 240, Height = 28, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };

            var deadlinesTitle = new Label { Text = "Ближайшие дедлайны", Left = 10, Top = 45, Width = 300, Height = 22, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            progressDeadlinesPanel = new FlowLayoutPanel
            {
                Left = 10,
                Top = 70,
                Width = 470,
                Height = 140,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
                WrapContents = false
            };

            var progressTitle = new Label { Text = "Прогресс активных задач", Left = 500, Top = 45, Width = 300, Height = 22, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            progressTaskProgressPanel = new FlowLayoutPanel
            {
                Left = 500,
                Top = 70,
                Width = 480,
                Height = 140,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
                WrapContents = false
            };

            progressPlayerComboBox = new ComboBox { Left = 10, Top = 220, Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
            progressMetricComboBox = new ComboBox { Left = 240, Top = 220, Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
            progressMetricComboBox.Items.AddRange(new object[]
            {
                "Убийства",
                "Помощи",
                "Смерти",
                "Сыграть матчей"
            });
            progressMetricComboBox.SelectedIndex = 0;
            progressPlayerComboBox.SelectedIndexChanged += ProgressPlayerComboBox_SelectedIndexChanged;
            progressMetricComboBox.SelectedIndexChanged += ProgressMetricComboBox_SelectedIndexChanged;

            var rangeLabel = new Label
            {
                Left = 10,
                Top = 245,
                Width = 970,
                Height = 18,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.DimGray,
                Text = "Статистика за последние 30 дней"
            };

            playerTrendChart = BuildChart("Динамика игрока");
            playerTrendChart.Left = 10;
            playerTrendChart.Top = 255;
            playerTrendChart.Width = 480;
            playerTrendChart.Height = 450;

            teamCompareChart = BuildChart("Сравнение игроков");
            teamCompareChart.Left = 500;
            teamCompareChart.Top = 255;
            teamCompareChart.Width = 480;
            teamCompareChart.Height = 450;

            progressStatusLabel = new Label
            {
                Left = 500,
                Top = 220,
                Width = 480,
                Height = 22,
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.DimGray,
                Text = ""
            };

            progressTabPage.Controls.Add(kpiActiveLabel);
            progressTabPage.Controls.Add(kpiOverdueLabel);
            progressTabPage.Controls.Add(kpiCompletedWeekLabel);
            progressTabPage.Controls.Add(kpiCompletionRateLabel);
            progressTabPage.Controls.Add(deadlinesTitle);
            progressTabPage.Controls.Add(progressDeadlinesPanel);
            progressTabPage.Controls.Add(progressTitle);
            progressTabPage.Controls.Add(progressTaskProgressPanel);
            progressTabPage.Controls.Add(progressPlayerComboBox);
            progressTabPage.Controls.Add(progressMetricComboBox);
            progressTabPage.Controls.Add(rangeLabel);
            progressTabPage.Controls.Add(playerTrendChart);
            progressTabPage.Controls.Add(teamCompareChart);
            progressTabPage.Controls.Add(progressStatusLabel);
        }

        private Chart BuildChart(string title)
        {
            var chart = new Chart();
            chart.ChartAreas.Add(new ChartArea("main"));
            chart.Titles.Add(title);
            chart.Legends.Add(new Legend("legend"));
            chart.BackColor = Color.WhiteSmoke;
            return chart;
        }

        private void InitializeTwitchRestrictions()
        {
            webView21.CoreWebView2InitializationCompleted += WebView21_CoreWebView2InitializationCompleted;
            if (webView21.CoreWebView2 != null)
            {
                AttachTwitchRestrictions();
            }
            else
            {
                _ = webView21.EnsureCoreWebView2Async();
            }

            if (webView21.Source == null || !webView21.Source.AbsoluteUri.Contains("/directory/category/dota-2"))
            {
                webView21.Source = new Uri(DotaTwitchCategoryUrl);
            }
        }

        private void WebView21_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                AttachTwitchRestrictions();
            }
        }

        private void AttachTwitchRestrictions()
        {
            if (webView21.CoreWebView2 == null)
            {
                return;
            }
            webView21.CoreWebView2.NavigationStarting -= CoreWebView2_NavigationStarting;
            webView21.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;
        }

        private void CoreWebView2_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            if (isTwitchForcedRedirect)
            {
                isTwitchForcedRedirect = false;
                return;
            }

            if (!Uri.TryCreate(e.Uri, UriKind.Absolute, out var targetUri) || !IsAllowedTwitchUri(targetUri))
            {
                e.Cancel = true;
                ForceTwitchDotaPage();
            }
        }

        private bool IsAllowedTwitchUri(Uri uri)
        {
            if (uri == null)
            {
                return false;
            }

            var host = uri.Host.ToLowerInvariant();
            if (host != "www.twitch.tv" && host != "twitch.tv")
            {
                return false;
            }

            var path = uri.AbsolutePath.TrimEnd('/');
            if (path.Equals("/directory/category/dota-2", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // Разрешаем только страницы каналов вида /channel_name.
            return Regex.IsMatch(path, "^/[A-Za-z0-9_]+$");
        }

        private void ForceTwitchDotaPage()
        {
            if (webView21 == null)
            {
                return;
            }

            isTwitchForcedRedirect = true;
            webView21.Source = new Uri(DotaTwitchCategoryUrl);
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
            FieldChecker.FieldCheck(PlayerID1, FieldChecker.CheckType.Names);
        }

        private void PlayerID1_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerID1, FieldChecker.CheckType.Numbers);
        }

        private void PlayerID2_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerID2, FieldChecker.CheckType.Numbers);
        }

        private void PlayerID3_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerID3, FieldChecker.CheckType.Numbers);
        }

        private void PlayerID4_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerID4, FieldChecker.CheckType.Numbers);
        }

        private void PlayerID5_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PlayerID5, FieldChecker.CheckType.Numbers);
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
            AiAdviceButton.Visible = false;
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
                label.Width = SelectedPlayerMatches.Width - 20;
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
                    $"Время старта: {matchDate:dd.MM.yyyy HH:mm}\nДлительность: {(int)duration.TotalMinutes}:{duration.Seconds:D2}\nID:{match.MatchId}";

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
            AiAdviceButton.Visible = true;
            InitCustomTooltip();
            LoadMatch(match);

         

            tooltipTimer.Tick += Checker;

            //form.Show();
        }

        private async void AiAdviceButton_Click(object sender, EventArgs e)
        {
            if (match == null || SelectPlayerComboBox.SelectedIndex < 0 || SelectPlayerComboBox.SelectedIndex >= currentTeam.Players.Count)
            {
                MessageBox.Show("Сначала выберите игрока и матч.");
                return;
            }

            AiAdviceButton.Enabled = false;
            try
            {
                var playerAccountId = currentTeam.Players[SelectPlayerComboBox.SelectedIndex].AccountID;
                long accountId;
                if (!long.TryParse(playerAccountId, out accountId))
                {
                    ShowAiAdviceWindow("Ошибка AI", "Не удалось определить игрока.");
                    return;
                }

                var player = match.players?.FirstOrDefault(p => p.account_id == accountId);
                if (player == null)
                {
                    ShowAiAdviceWindow("Ошибка AI", "Игрок не найден в выбранном матче.");
                    return;
                }

                string heroName = ApiCourier.Heroes.ContainsKey(player.hero_id)
                    ? ApiCourier.Heroes[player.hero_id].localized_name
                    : $"hero_id={player.hero_id}";

                string position = GuessPosition(player);
                string winner = match.radiant_win ? "Radiant" : "Dire";
                string playerSide = player.isRadiant ? "Radiant" : "Dire";
                string result = winner == playerSide ? "Победа" : "Поражение";

                string prompt =
                    "Ты тренер по Dota 2. Дай ровно 4-5 коротких предложения на русском языке. Не обращайся к игроку на прямую. Говори как третье, независимое лицо.\n" +
                    "Формат: 1-2 сильная сторона, 1-2 точки роста, 1-2 практический совет к следующей игре.\n" +
                    "Без воды и без общих фраз. Не зацикливайся на роли игрока, роль не всегда корректно вычисляется\n\n" +
                    $"Герой: {heroName}\n" +
                    $"Позиция (приблизительное решение): {position}\n" +
                    $"Результат матча: {result}\n" +
                    $"K/D/A: {player.kills}/{player.deaths}/{player.assists}\n" +
                    $"GPM/XPM: {player.gold_per_min}/{player.xp_per_min}\n" +
                    $"LH/DN: {player.last_hits}/{player.denies}\n" +
                    $"NetWorth: {player.net_worth}\n" +
                    $"Hero/Tower damage: {player.hero_damage}/{player.tower_damage}\n" +
                    $"Длительность матча в секундах: {match.duration}\n";

                var advice = await GetGeminiAdviceAsync(prompt);
                ShowAiAdviceWindow("AI-рекомендация", advice);
            }
            catch (Exception ex)
            {
                ShowAiAdviceWindow("Ошибка AI", $"Ошибка AI: {ex.Message}");
            }
            finally
            {
                AiAdviceButton.Enabled = true;
            }
        }

        private void ShowAiAdviceWindow(string title, string text)
        {
            using (var form = new Form())
            using (var box = new TextBox())
            {
                form.Text = title;
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                form.Width = 640;
                form.Height = 360;

                box.Multiline = true;
                box.ReadOnly = true;
                box.ScrollBars = ScrollBars.Vertical;
                box.Dock = DockStyle.Fill;
                box.Font = new Font("Segoe UI", 10F);
                box.Text = text ?? "";

                form.Controls.Add(box);
                form.ShowDialog(this);
            }
        }

        private string GuessPosition(MatchPlayerModel player)
        {
            if (player.gold_per_min >= 600 && player.last_hits >= 180) return "Керри (позиция 1)";
            if (player.gold_per_min >= 500 && player.assists >= 8) return "Мид/Оффлейн (позиция 2-3)";
            if (player.assists >= 12 && player.last_hits < 90) return "Саппорт (позиция 4-5)";
            return "Позиция не определена";
        }

        private async Task<string> GetGeminiAdviceAsync(string prompt)
        {
            if (DateTime.UtcNow < _aiRateLimitUntilUtc)
            {
                int waitSeconds = (int)Math.Ceiling((_aiRateLimitUntilUtc - DateTime.UtcNow).TotalSeconds);
                return $"Слишком много запросов к AI. Подождите примерно {Math.Max(1, waitSeconds)} сек. и попробуйте снова.";
            }

            string apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                apiKey = System.Configuration.ConfigurationManager.AppSettings["GeminiApiKey"];
            }

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return "Не найден Gemini API ключ. Добавьте GEMINI_API_KEY или App.config key GeminiApiKey.";
            }

            string preferredModel = System.Configuration.ConfigurationManager.AppSettings["GeminiModel"];
            if (string.IsNullOrWhiteSpace(preferredModel))
            {
                preferredModel = "gemini-2.0-flash";
            }

            var modelsToTry = new List<string>();
            modelsToTry.Add(preferredModel);
            foreach (var model in new[] { "gemini-2.0-flash", "gemini-1.5-flash-latest", "gemini-1.5-flash" })
            {
                if (!modelsToTry.Contains(model))
                {
                    modelsToTry.Add(model);
                }
            }

            string lastError = "AI не вернул текст рекомендации.";

            foreach (var model in modelsToTry)
            {
                string endpoint = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";
                var payload = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = prompt }
                            }
                        }
                    }
                    //generationConfig = new
                    //{
                    //    temperature = 0.5,
                    //    maxOutputTokens = 180
                    //}
                };

                var json = JsonSerializer.Serialize(payload);
                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    var response = await _aiHttpClient.PostAsync(endpoint, content);
                    var body = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        using (var doc = JsonDocument.Parse(body))
                        {
                            var root = doc.RootElement;
                            if (root.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
                            {
                                var text = candidates[0]
                                    .GetProperty("content")
                                    .GetProperty("parts")[0]
                                    .GetProperty("text")
                                    .GetString();
                                if (!string.IsNullOrWhiteSpace(text))
                                {
                                    return text.Trim();
                                }
                            }
                        }

                        lastError = "AI вернул пустой ответ.";
                        continue;
                    }

                    // Если модель не найдена - пробуем следующую.
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        lastError = $"Модель {model} недоступна (404).";
                        continue;
                    }

                    if ((int)response.StatusCode == 429)
                    {
                        // Короткий backoff + одна повторная попытка для текущей модели.
                        await Task.Delay(2000);
                        using (var retryContent = new StringContent(json, Encoding.UTF8, "application/json"))
                        {
                            var retryResponse = await _aiHttpClient.PostAsync(endpoint, retryContent);
                            if (retryResponse.IsSuccessStatusCode)
                            {
                                var retryBody = await retryResponse.Content.ReadAsStringAsync();
                                using (var retryDoc = JsonDocument.Parse(retryBody))
                                {
                                    var retryRoot = retryDoc.RootElement;
                                    if (retryRoot.TryGetProperty("candidates", out var retryCandidates) && retryCandidates.GetArrayLength() > 0)
                                    {
                                        var retryText = retryCandidates[0]
                                            .GetProperty("content")
                                            .GetProperty("parts")[0]
                                            .GetProperty("text")
                                            .GetString();
                                        if (!string.IsNullOrWhiteSpace(retryText))
                                        {
                                            return retryText.Trim();
                                        }
                                    }
                                }
                            }
                        }

                        _aiRateLimitUntilUtc = DateTime.UtcNow.AddSeconds(35);
                        lastError = "Ошибка 429: превышен лимит Gemini. Подождите ~30-40 сек. и попробуйте снова.";
                        break;
                    }

                    lastError = $"Ошибка Gemini API: {response.StatusCode}\n{body}";
                    break;
                }
            }

            return lastError;
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
            DurationLabel.Text = $"{(int)duration.TotalMinutes}:{duration.Seconds:D2}";
            //DurationLabel.Text = duration.ToString(@"mm\:ss");

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

        private async Task RefreshTasksAsync()
        {
            if (isTasksLoading)
            {
                return;
            }

            try
            {
                SetTasksLoadingState(true, "Загрузка тренировок...");
                tasks.Clear();
                tasks = await dbManager.GetTrainingTasksAsync(currentTeam.Id);
                RenderTasks();
                progressDataDirty = true;
                _ = AnalyzeAndRefreshTasksAsync();
            }
            finally
            {
                SetTasksLoadingState(false);
            }
        }

        private async Task AnalyzeAndRefreshTasksAsync()
        {
            if (isTaskAnalysisRunning)
            {
                return;
            }

            isTaskAnalysisRunning = true;
            try
            {
                await AnalyzeTrainingTasksProgressAsync();
                RenderTasks();
                progressDataDirty = true;
                if (guna2TabControl1.SelectedTab == tabPage5)
                {
                    await RefreshProgressTabAsync();
                    progressDataDirty = false;
                }
            }
            finally
            {
                isTaskAnalysisRunning = false;
            }
        }

        private void RenderTasks()
        {
            TrainingTasksPanel.Controls.Clear();
            ArchiveTasksPanel.Controls.Clear();
            var activeTasks = tasks.Where(t => t.Deadline >= DateTime.Now).OrderBy(t => t.Deadline).ToList();
            var archiveTasks = tasks.Where(t => t.Deadline < DateTime.Now).OrderByDescending(t => t.Deadline).ToList();
            RenderTaskList(TrainingTasksPanel, activeTasks, true);
            RenderTaskList(ArchiveTasksPanel, archiveTasks, false);
            RenderTeamPageActiveTasksPreview(activeTasks);
        }

        private void RenderTeamPageActiveTasksPreview(List<TrainingTask> activeTasks)
        {
            TeamPageTasksPanel.Controls.Clear();

            if (activeTasks.Count == 0)
            {
                TeamPageTasksPanel.Controls.Add(new Label
                {
                    Text = "Нет актуальных тренировок",
                    AutoSize = false,
                    Width = TeamPageTasksPanel.Width - 20,
                    Height = 30,
                    Left = 10,
                    Top = 12,
                    TextAlign = ContentAlignment.MiddleLeft
                });
                return;
            }

            RenderTaskList(TeamPageTasksPanel, activeTasks, false);
        }

        private async Task RefreshProgressTabAsync()
        {
            if (progressTabPage == null || isProgressLoading)
            {
                return;
            }

            isProgressLoading = true;
            progressStatusLabel.Text = "Обновление...";

            try
            {
                var activeTasks = tasks.Where(t => t.Deadline >= DateTime.Now).ToList();
                var overdueTasks = tasks.Where(t => t.Deadline < DateTime.Now && !t.IsCompleted).ToList();
                var completedWeekTasks = tasks.Where(t => t.IsCompleted && t.Deadline >= DateTime.Now.AddDays(-7)).ToList();

                kpiActiveLabel.Text = $"Активные: {activeTasks.Count}";
                kpiOverdueLabel.Text = $"Просроченные: {overdueTasks.Count}";
                kpiCompletedWeekLabel.Text = $"Выполнено (7 дн.): {completedWeekTasks.Count}";

                double averageCompletion = activeTasks.Count == 0
                    ? 0
                    : activeTasks.Average(GetTaskCompletionRatio) * 100.0;
                kpiCompletionRateLabel.Text = $"Средний прогресс: {averageCompletion:0}%";

                RenderProgressDeadlines(activeTasks);
                RenderProgressTaskBars(activeTasks);
                RefreshProgressPlayerFilter();
                await RenderAllChartsAsync();
                progressStatusLabel.Text = $"Обновлено: {DateTime.Now:HH:mm:ss}";
            }
            catch (Exception ex)
            {
                progressStatusLabel.Text = $"Ошибка: {ex.Message}";
            }
            finally
            {
                isProgressLoading = false;
                _ = ExecutePendingProgressRefreshAsync();
            }
        }

        private async Task ExecutePendingProgressRefreshAsync()
        {
            if (isProgressLoading || guna2TabControl1.SelectedTab != tabPage5)
            {
                return;
            }

            bool needMetricRefresh = pendingProgressMetricRefresh;
            bool needPlayerRefresh = pendingProgressPlayerRefresh;
            pendingProgressMetricRefresh = false;
            pendingProgressPlayerRefresh = false;

            if (needMetricRefresh)
            {
                progressStatusLabel.Text = "Обновление графиков...";
                await RenderAllChartsAsync();
                progressStatusLabel.Text = $"Обновлено: {DateTime.Now:HH:mm:ss}";
                return;
            }

            if (needPlayerRefresh)
            {
                progressStatusLabel.Text = "Обновление графика игрока...";
                await RenderPlayerTrendChartAsync();
                progressStatusLabel.Text = $"Обновлено: {DateTime.Now:HH:mm:ss}";
            }
        }

        private async Task RenderAllChartsAsync()
        {
            await RenderPlayerTrendChartAsync();
            await RenderTeamCompareChartAsync();
        }

        private async void ProgressPlayerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2TabControl1.SelectedTab != tabPage5)
            {
                return;
            }

            if (isProgressLoading)
            {
                pendingProgressPlayerRefresh = true;
                return;
            }

            progressStatusLabel.Text = "Обновление графика игрока...";
            await RenderPlayerTrendChartAsync();
            progressStatusLabel.Text = $"Обновлено: {DateTime.Now:HH:mm:ss}";
        }

        private async void ProgressMetricComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2TabControl1.SelectedTab != tabPage5)
            {
                return;
            }

            if (isProgressLoading)
            {
                pendingProgressMetricRefresh = true;
                return;
            }

            progressStatusLabel.Text = "Обновление графиков...";
            await RenderAllChartsAsync();
            progressStatusLabel.Text = $"Обновлено: {DateTime.Now:HH:mm:ss}";
        }

        private void RenderProgressDeadlines(List<TrainingTask> activeTasks)
        {
            progressDeadlinesPanel.Controls.Clear();
            foreach (var task in activeTasks.OrderBy(t => t.Deadline).Take(5))
            {
                progressDeadlinesPanel.Controls.Add(new Label
                {
                    AutoSize = false,
                    Width = progressDeadlinesPanel.Width - 30,
                    Height = 24,
                    Text = $"{task.Title} - до {task.Deadline:dd.MM HH:mm}",
                    TextAlign = ContentAlignment.MiddleLeft
                });
            }
        }

        private void RenderProgressTaskBars(List<TrainingTask> activeTasks)
        {
            progressTaskProgressPanel.Controls.Clear();
            foreach (var task in activeTasks.OrderBy(t => t.Deadline).Take(7))
            {
                double ratio = GetTaskCompletionRatio(task);
                var container = new Panel { Width = progressTaskProgressPanel.Width - 30, Height = 44 };
                var label = new Label
                {
                    Left = 0,
                    Top = 0,
                    Width = container.Width,
                    Height = 18,
                    Text = $"{task.Title} ({ratio * 100:0}%)"
                };
                var track = new Panel
                {
                    Left = 0,
                    Top = 20,
                    Width = container.Width,
                    Height = 18,
                    BackColor = Color.FromArgb(220, 220, 220)
                };
                int fillWidth = Math.Max(0, Math.Min(track.Width, (int)(track.Width * ratio)));
                var fill = new Panel
                {
                    Left = 0,
                    Top = 0,
                    Width = fillWidth,
                    Height = track.Height,
                    BackColor = Color.FromArgb(80, 170, 80)
                };
                track.Controls.Add(fill);
                container.Controls.Add(label);
                container.Controls.Add(track);
                progressTaskProgressPanel.Controls.Add(container);
            }
        }

        private void RefreshProgressPlayerFilter()
        {
            string selected = progressPlayerComboBox.SelectedItem as string;
            progressPlayerComboBox.Items.Clear();
            foreach (var player in currentTeam.Players)
            {
                progressPlayerComboBox.Items.Add(player.Name);
            }

            if (progressPlayerComboBox.Items.Count == 0)
            {
                return;
            }

            int oldIdx = selected == null ? -1 : progressPlayerComboBox.Items.IndexOf(selected);
            progressPlayerComboBox.SelectedIndex = oldIdx >= 0 ? oldIdx : 0;
        }

        private async Task RenderPlayerTrendChartAsync()
        {
            playerTrendChart.Series.Clear();
            var series = new Series("Игрок")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2
            };
            playerTrendChart.Series.Add(series);

            if (progressPlayerComboBox.SelectedIndex < 0 || progressPlayerComboBox.SelectedIndex >= currentTeam.Players.Count)
            {
                return;
            }

            var player = currentTeam.Players[progressPlayerComboBox.SelectedIndex];
            var matchesResult = await ApiCourier.TryGetPlayerMatchesInPeriod(player.AccountID, DateTime.Now.AddDays(-30), DateTime.Now, 60, 600);
            if (!matchesResult.IsSuccess || matchesResult.Data == null)
            {
                return;
            }

            if (GetSelectedProgressMetric() == TrainingMetric.MatchesPlayed)
            {
                int cumulativeMatches = 0;
                var groupedByDay = matchesResult.Data
                    .OrderBy(m => m.StartTime)
                    .GroupBy(m => DateTimeOffset.FromUnixTimeSeconds(m.StartTime).Date);

                foreach (var dayGroup in groupedByDay)
                {
                    cumulativeMatches += dayGroup.Count();
                    int idx = series.Points.AddXY(dayGroup.Key.ToString("dd.MM"), cumulativeMatches);
                    series.Points[idx].ToolTip = $"Дата: {dayGroup.Key:dd.MM.yyyy}\nЗначение: {cumulativeMatches}";
                }
                return;
            }

            var detailsCache = new Dictionary<long, DotaMatchDetailsModel>();
            foreach (var match in matchesResult.Data.OrderBy(m => m.StartTime))
            {
                var date = DateTimeOffset.FromUnixTimeSeconds(match.StartTime).DateTime;
                double? value = await GetMatchMetricValueAsync(match, player.AccountID, detailsCache);
                if (!value.HasValue)
                {
                    continue;
                }
                int idx = series.Points.AddXY(date.ToString("dd.MM"), value.Value);
                series.Points[idx].ToolTip = $"Дата: {date:dd.MM.yyyy}\nЗначение: {value.Value:0.##}";
            }
        }

        private async Task RenderTeamCompareChartAsync()
        {
            teamCompareChart.Series.Clear();
            var series = new Series("Среднее")
            {
                ChartType = SeriesChartType.Column,
            };
            teamCompareChart.Series.Add(series);

            var detailsCache = new Dictionary<long, DotaMatchDetailsModel>();
            foreach (var player in currentTeam.Players)
            {
                var result = await ApiCourier.TryGetPlayerMatchesInPeriod(player.AccountID, DateTime.Now.AddDays(-30), DateTime.Now, 60, 600);
                if (!result.IsSuccess || result.Data == null || result.Data.Count == 0)
                {
                    continue;
                }

                if (GetSelectedProgressMetric() == TrainingMetric.MatchesPlayed)
                {
                    int idx = series.Points.AddXY(player.Name, result.Data.Count);
                    series.Points[idx].ToolTip = $"Игрок: {player.Name}\nЗначение: {result.Data.Count}";
                    continue;
                }

                var values = new List<double>();
                foreach (var match in result.Data)
                {
                    double? value = await GetMatchMetricValueAsync(match, player.AccountID, detailsCache);
                    if (value.HasValue)
                    {
                        values.Add(value.Value);
                    }
                }

                if (values.Count == 0)
                {
                    int noDataIdx = series.Points.AddXY(player.Name, 0);
                    series.Points[noDataIdx].ToolTip = $"Игрок: {player.Name}\nНет данных за 30 дней";
                    series.Points[noDataIdx].Label = "н/д";
                    continue;
                }

                double avg = values.Average();
                int pointIndex = series.Points.AddXY(player.Name, avg);
                series.Points[pointIndex].ToolTip = $"Игрок: {player.Name}\nЗначение: {avg:0.##}";
            }
        }

        private double GetTaskCompletionRatio(TrainingTask task)
        {
            if (task.PlayerIds == null || task.PlayerIds.Count == 0)
            {
                return 0;
            }

            int done = task.PlayerIds.Count(pid =>
                task.CompletedPlayers != null &&
                task.CompletedPlayers.ContainsKey(pid) &&
                task.CompletedPlayers[pid]);
            return (double)done / task.PlayerIds.Count;
        }

        private TrainingMetric GetSelectedProgressMetric()
        {
            if (progressMetricComboBox == null || progressMetricComboBox.SelectedItem == null)
            {
                return TrainingMetric.Kills;
            }

            switch (progressMetricComboBox.SelectedItem.ToString())
            {
                case "Помощи": return TrainingMetric.Assists;
                case "Смерти": return TrainingMetric.Deaths;
                // case "Золото в минуту": return TrainingMetric.GPM;
                // case "Добито крипов": return TrainingMetric.LastHits;
                case "Сыграть матчей": return TrainingMetric.MatchesPlayed;
                default: return TrainingMetric.Kills;
            }
        }

        private async Task<double?> GetMatchMetricValueAsync(
            DotaMatchModel match,
            string playerAccountId,
            Dictionary<long, DotaMatchDetailsModel> detailsCache)
        {
            var selected = GetSelectedProgressMetric();
            if (selected == TrainingMetric.Kills) return match.Kills;
            if (selected == TrainingMetric.Assists) return match.Assists;
            if (selected == TrainingMetric.Deaths) return match.Deaths;
            if (selected == TrainingMetric.MatchesPlayed) return 1;
            // if (selected == TrainingMetric.GPM && match.GoldPerMin.HasValue) return match.GoldPerMin.Value;
            // if (selected == TrainingMetric.LastHits && match.LastHits.HasValue) return match.LastHits.Value;

            if (!detailsCache.ContainsKey(match.MatchId))
            {
                var detailsResult = await ApiCourier.TryGetMatch(match.MatchId);
                if (!detailsResult.IsSuccess || detailsResult.Data == null)
                {
                    return null;
                }
                detailsCache[match.MatchId] = detailsResult.Data;
            }

            var details = detailsCache[match.MatchId];
            long parsedId;
            if (!long.TryParse(playerAccountId, out parsedId))
            {
                return null;
            }

            var player = details.players?.FirstOrDefault(p => p.account_id == parsedId);
            if (player == null)
            {
                return null;
            }

            switch (selected)
            {
                // case TrainingMetric.GPM: return player.gold_per_min;
                // case TrainingMetric.LastHits: return player.last_hits;
                default: return null;
            }
        }

        private void RenderTaskList(Guna2Panel hostPanel, List<TrainingTask> tasksToRender, bool canDeleteFromThisPanel)
        {
            foreach (var task in tasksToRender)
            {
                Guna2Panel taskPanel = new Guna2Panel();
                Font font = new Font(guna2HtmlLabel1.Font, FontStyle.Regular);

                taskPanel.Font = font;
                taskPanel.Width = hostPanel.Width - 17;
                taskPanel.Height = 190;
                taskPanel.FillColor = Color.Gray;
                taskPanel.BorderThickness = 0;

                taskPanel.Top = hostPanel.Controls.Count == 0 ? 10 : hostPanel.Controls[hostPanel.Controls.Count - 1].Bottom + 2;

                taskPanel.Controls.Add(new Guna2HtmlLabel()
                {
                    Font = font,
                    AutoSize = false,
                    Location = new Point(13, 5),
                    Size = new Size(274, 22),
                    Text = task.Title
                });
                taskPanel.Controls.Add(new Guna2HtmlLabel()
                {
                    Font = font,
                    AutoSize = true,
                    Location = new Point(13, 31),
                    Text = "Начало:" + task.StartDate
                });
                taskPanel.Controls.Add(new Guna2HtmlLabel()
                {
                    Font = font,
                    AutoSize = true,
                    Location = new Point(13, 54),
                    Text = "Конец:" + task.Deadline
                });
                string compare = string.Empty;
                switch(task.Comparison)
                {
                    case ComparisonType.GreaterOrEqual: // >=
                        compare = "больше или равно";
                        break;
                    case ComparisonType.LessOrEqual:     // <=
                        compare = "меньше или равно";
                        break;
                    case ComparisonType.Greater:       // >
                        compare = "больше";
                        break;
                    case ComparisonType.Less:             // <
                        compare = "меньше";
                        break;
                    case ComparisonType.Equal:             // ==
                        compare = "равно";
                        break;
                }
                string taskText = $"набрать {compare} чем";
                string metricText = GetMetricText(task.Metric);
                taskPanel.Controls.Add(new Guna2HtmlLabel()
                {
                    Font = font,
                    AutoSize = true,
                    Location = new Point(13, 77),
                    Text = $"Цель: {taskText} {task.TargetValue} ({metricText})"
                });
                if(task.PeriodValue != -1)
                {
                    taskPanel.Controls.Add(new Guna2HtmlLabel()
                    {
                        Font = font,
                        AutoSize = true,
                        Location = new Point(13, 100),
                        Text = $"Кол-во матчей для выполнения: {task.PeriodValue}"
                    });
                }
                Point Loc = new Point(13, 123);
                List <UserModel> players = new List<UserModel>();
                foreach(var playerId in task.PlayerIds)
                {
                    players.InsertRange(players.Count, currentTeam.Players.Where(p => p.AccountID == playerId).ToList());
                }
                for (int i = 0; i < players.Count; i++)
                {
                    Guna2PictureBox playerImg = new Guna2PictureBox();
                    playerImg.FillColor = Color.DarkGray;
                    playerImg.Width = 50;
                    playerImg.Height = 50;
                    if (i == 0)
                    {
                        playerImg.Location = Loc;
                    }
                    else
                    {
                        Loc.X += playerImg.Width + 3;
                        playerImg.Location = Loc;
                    }
                    playerImg.SizeMode = PictureBoxSizeMode.StretchImage;
                    playerImg.Load(players[i].Avatarfull);

                    if (playerImg.Image != null)
                    {
                        bool isTaskActive = DateTime.Now <= task.Deadline;
                        bool isCompleted = task.CompletedPlayers.ContainsKey(players[i].AccountID)
                            && task.CompletedPlayers[players[i].AccountID];
                        if (isTaskActive)
                        {
                            playerImg.Image = isCompleted
                                ? MakePlayerGreen(playerImg.Image)
                                : MakeItemGray(playerImg.Image);
                        }
                        else
                        {
                            playerImg.Image = isCompleted
                                ? MakePlayerGreen(playerImg.Image)
                                : MakePlayerRed(playerImg.Image);
                        }
                    }

                    taskPanel.Controls.Add(playerImg);

                }

                taskPanel.Tag = task;
                taskPanel.Click += OpenTask;
                if (canDeleteFromThisPanel)
                {
                    taskPanel.MouseEnter += TaskPanel_MouseEnter;
                    taskPanel.MouseLeave += TaskPanel_MouseLeave;
                }
                foreach (Control child in taskPanel.Controls)
                {
                    child.Click += (s, e) => OpenTask(taskPanel, e);
                    if (canDeleteFromThisPanel)
                    {
                        child.MouseEnter += (s, e) => TaskPanel_MouseEnter(taskPanel, e);
                        child.MouseLeave += (s, e) => TaskPanel_MouseLeave(taskPanel, e);
                    }
                }

                hostPanel.Controls.Add(taskPanel);
            }
        }
        private void TaskPanel_MouseEnter(object sender, EventArgs e)
        {
            if (!isTaskDeleteMode) return;
            var panel = sender as Guna2Panel;
            if (panel == null) return;
            panel.BorderColor = Color.FromArgb(76, 132, 255);
            panel.BorderThickness = 2;
        }
        private void TaskPanel_MouseLeave(object sender, EventArgs e)
        {
            if (!isTaskDeleteMode) return;
            var panel = sender as Guna2Panel;
            if (panel == null) return;
            panel.BorderThickness = 0;
        }
        private void OpenTask(object sender, EventArgs e)
        {
            TrainingTask task = (sender as Guna2Panel).Tag as TrainingTask;
            if (task == null)
            {
                return;
            }

            if (!isTaskDeleteMode)
            {
                return;
            }

            if (task.Deadline < DateTime.Now)
            {
                MessageBox.Show("Можно удалять только актуальные задачи");
                return;
            }

            var confirm = MessageBox.Show(
                $"Удалить задачу \"{task.Title}\"?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            _ = DeleteTaskAndReloadAsync(task.Id);
        }
        private async Task DeleteTaskAndReloadAsync(int taskId)
        {
            ExitTaskDeleteMode();
            await dbManager.RemoveTrainingTaskAsync(taskId);
            await RefreshTasksAsync();
        }

        private async Task AnalyzeTrainingTasksProgressAsync()
        {
            var detailsCache = new Dictionary<long, DotaMatchDetailsModel>();

            foreach (var task in tasks)
            {
                if (task.Deadline < DateTime.Now)
                {
                    continue;
                }

                if (task.CompletedPlayers == null)
                {
                    task.CompletedPlayers = new Dictionary<string, bool>();
                }

                foreach (var playerId in task.PlayerIds)
                {
                    var matchesResult = await ApiCourier.TryGetPlayerMatchesInPeriod(playerId, task.StartDate, task.Deadline);
                    if (!matchesResult.IsSuccess)
                    {
                        continue;
                    }

                    var details = new List<DotaMatchDetailsModel>();

                    foreach (var match in matchesResult.Data)
                    {
                        if (!detailsCache.ContainsKey(match.MatchId))
                        {
                            var matchResult = await ApiCourier.TryGetMatch(match.MatchId);
                            if (!matchResult.IsSuccess || matchResult.Data == null)
                            {
                                continue;
                            }

                            detailsCache[match.MatchId] = matchResult.Data;
                        }

                        details.Add(detailsCache[match.MatchId]);
                    }

                    bool isCompleted = await TrainingTasksAnalyzer.CheckTrainingAsync(task, playerId, details);
                    task.CompletedPlayers[playerId] = isCompleted;
                }
            }
        }

        private string GetMetricText(TrainingMetric metric)
        {
            switch (metric)
            {
                case TrainingMetric.Kills: return "убийства";
                case TrainingMetric.Assists: return "помощь";
                case TrainingMetric.Deaths: return "смерти";
                case TrainingMetric.GPM: return "золото в минуту";
                case TrainingMetric.LastHits: return "добитые крипы";
                case TrainingMetric.MatchesPlayed: return "сыгранные матчи";
                default: return "метрика";
            }
        }

        // Делает изображение красноватым
        private Image MakePlayerRed(Image original)
        {
            Bitmap redBitmap = new Bitmap(original.Width, original.Height);

            using (Graphics g = Graphics.FromImage(redBitmap))
            {
                var colorMatrix = new System.Drawing.Imaging.ColorMatrix(
                    new float[][]
                    {
                new float[]{1.0f, 0f, 0f, 0, 0},  // Красный канал усиливаем
                new float[]{0f, 0.3f, 0f, 0, 0},  // Зеленый ослабляем
                new float[]{0f, 0f, 0.3f, 0, 0},  // Синий ослабляем
                new float[]{0, 0, 0, 1, 0},
                new float[]{0, 0, 0, 0, 1}
                    });

                var attributes = new System.Drawing.Imaging.ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);

                g.DrawImage(original,
                    new Rectangle(0, 0, original.Width, original.Height),
                    0, 0, original.Width, original.Height,
                    GraphicsUnit.Pixel, attributes);
            }

            return redBitmap;
        }

        // Делает изображение зеленоватым
        private Image MakePlayerGreen(Image original)
        {
            Bitmap greenBitmap = new Bitmap(original.Width, original.Height);

            using (Graphics g = Graphics.FromImage(greenBitmap))
            {
                var colorMatrix = new System.Drawing.Imaging.ColorMatrix(
                    new float[][]
                    {
                new float[]{0.3f, 0f, 0f, 0, 0},  // Красный ослабляем
                new float[]{0f, 1.0f, 0f, 0, 0},  // Зеленый усиливаем
                new float[]{0f, 0f, 0.3f, 0, 0},  // Синий ослабляем
                new float[]{0, 0, 0, 1, 0},
                new float[]{0, 0, 0, 0, 1}
                    });

                var attributes = new System.Drawing.Imaging.ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);

                g.DrawImage(original,
                    new Rectangle(0, 0, original.Width, original.Height),
                    0, 0, original.Width, original.Height,
                    GraphicsUnit.Pixel, attributes);
            }

            return greenBitmap;
        }

        #endregion

        private async void guna2Button1_Click_1(object sender, EventArgs e)
        {
            ExitTaskDeleteMode();
            CreateTrainingTask taskForm = new CreateTrainingTask(currentTeam, this);
            taskForm.ShowDialog();
        }
        public async void CreateTask(CreateTrainingTask form)
        {
            await dbManager.AddTrainingTaskAsync(form.currentTask, currentTeam.Id);
            form.Dispose();
            await RefreshTasksAsync();
        }

        private async void ClearArchiveButton_Click(object sender, EventArgs e)
        {
            if (isTasksLoading)
            {
                return;
            }

            var result = MessageBox.Show(
                "Очистить архив завершившихся задач?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            ExitTaskDeleteMode();
            await dbManager.ClearTrainingArchiveAsync(currentTeam.Id, DateTime.Now);
            await RefreshTasksAsync();
        }

        private async void DeleteTaskButton_Click(object sender, EventArgs e)
        {
            if (isTasksLoading)
            {
                return;
            }

            if (isTaskDeleteMode)
            {
                ExitTaskDeleteMode();
                MessageBox.Show("Выбор удаления задач отменён");
                return;
            }

            var hasActiveTasks = tasks.Any(t => t.Deadline >= DateTime.Now);
            if (!hasActiveTasks)
            {
                MessageBox.Show("Нет актуальных задач для удаления");
                return;
            }

            isTaskDeleteMode = true;
            DeleteTaskButton.Text = "Отменить удаление";
            MessageBox.Show("Выберите тренировку");
        }

        private void ExitTaskDeleteMode()
        {
            if (!isTaskDeleteMode)
            {
                return;
            }

            isTaskDeleteMode = false;
            DeleteTaskButton.Text = "Удалить тренировку";
            foreach (Control control in TrainingTasksPanel.Controls)
            {
                if (control is Guna2Panel panel)
                {
                    panel.BorderThickness = 0;
                }
            }
        }

        private void SetTasksLoadingState(bool isLoading, string text = "Загрузка тренировок...")
        {
            isTasksLoading = isLoading;
            ClearArchiveButton.Enabled = !isLoading;
            DeleteTaskButton.Enabled = !isLoading;
            guna2Button1.Enabled = !isLoading;
            TrainingTasksPanel.Enabled = !isLoading;
            ArchiveTasksPanel.Enabled = !isLoading;
            TeamPageTasksPanel.Enabled = !isLoading;
            Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;

            if (tasksLoadingLabel != null)
            {
                tasksLoadingLabel.Text = text;
                tasksLoadingLabel.Visible = isLoading;
            }
        }

    }
}
