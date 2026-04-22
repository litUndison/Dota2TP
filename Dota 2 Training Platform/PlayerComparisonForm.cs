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
    public partial class PlayerComparisonForm : Form
    {
        private TeamModel _team;
        private UserModel _player1;
        private UserModel _player2;
        private List<DotaMatchModel> _player1Matches;
        private List<DotaMatchModel> _player2Matches;

        public PlayerComparisonForm(TeamModel team)
        {
            _team = team;
            _player1Matches = new List<DotaMatchModel>();
            _player2Matches = new List<DotaMatchModel>();
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Сравнение игроков";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            CreateControls();
        }

        private void CreateControls()
        {
            Guna2Panel mainPanel = guna2Panel1;

            Guna2Panel selectionPanel = new Guna2Panel()
            {
                Dock = DockStyle.Top,
                Height = 80,
                Padding = new Padding(0, 10, 0, 10)
            };
            mainPanel.Controls.Add(selectionPanel);

            Guna2HtmlLabel player1Label = Player1Label;
            selectionPanel.Controls.Add(player1Label);

            Guna2ComboBox player1ComboBox = Player1ComboBox;
            player1ComboBox.Items.AddRange(_team.Players.Select(p => p.Name).ToArray());
            player1ComboBox.SelectedIndexChanged += (s, e) => LoadPlayer1Data(player1ComboBox.SelectedItem?.ToString());
            selectionPanel.Controls.Add(player1ComboBox);

            Guna2HtmlLabel player2Label = Player2Label;
            selectionPanel.Controls.Add(player2Label);

            Guna2ComboBox player2ComboBox = Player2ComboBox;
            player2ComboBox.Items.AddRange(_team.Players.Select(p => p.Name).ToArray());
            player2ComboBox.SelectedIndexChanged += (s, e) => LoadPlayer2Data(player2ComboBox.SelectedItem?.ToString());
            selectionPanel.Controls.Add(player2ComboBox);

            Guna2Button compareButton = CompareBtn;
            compareButton.Click += async (s, e) =>
            {
                compareButton.Enabled = false;
                await ComparePlayers(player1ComboBox.SelectedItem?.ToString(), player2ComboBox.SelectedItem?.ToString());
                // compareButton.Enabled = true;
            };
            selectionPanel.Controls.Add(compareButton);

            Panel resultsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };
            mainPanel.Controls.Add(resultsPanel);

            CreateComparisonTable(resultsPanel);
        }

        private void CreateComparisonTable(Panel parent)
        {
            // Initialize all comparison labels with default values
            InitializeComparisonLabels();
        }

        private void InitializeComparisonLabels()
        {
            // Set statistic labels text
            MatchesPlayedLabel.Text = "Сыграно матчей";
            WinsLabel.Text = "Победы";
            LossesLabel.Text = "Поражения";
            WinRateLabel.Text = "Win Rate, %";
            KDALabel.Text = "KDA, среднее";
            GPMLabel.Text = "GPM, среднее";
            LastHitsLabel.Text = "Добитые крипы, среднее";
            KillsLabel.Text = "Убийства, среднее";
            AssistsLabel.Text = "Помощи, среднее";
            DeathsLabel.Text = "Смертей,среднее";

            // Set initial values for player stats
            Player1MatchesPlayed.Text = "-";
            Player1Wins.Text = "-";
            Player1Losses.Text = "-";
            Player1WinRate.Text = "-";
            Player1KDA.Text = "-";
            Player1GPM.Text = "-";
            Player1LastHits.Text = "-";
            Player1Kills.Text = "-";
            Player1Assists.Text = "-";
            Player1Deaths.Text = "-";

            Player2MatchesPlayed.Text = "-";
            Player2Wins.Text = "-";
            Player2Losses.Text = "-";
            Player2WinRate.Text = "-";
            Player2KDA.Text = "-";
            Player2GPM.Text = "-";
            Player2LastHits.Text = "-";
            Player2Kills.Text = "-";
            Player2Assists.Text = "-";
            Player2Deaths.Text = "-";
        }

        private async Task LoadPlayer1Data(string playerName)
        {
            if (string.IsNullOrEmpty(playerName)) return;

            _player1 = _team.Players.FirstOrDefault(p => p.Name == playerName);
            if (_player1 != null)
            {
                await LoadPlayerMatches(_player1, 1);
            }
        }

        private async Task LoadPlayer2Data(string playerName)
        {
            if (string.IsNullOrEmpty(playerName)) return;

            _player2 = _team.Players.FirstOrDefault(p => p.Name == playerName);
            if (_player2 != null)
            {
                await LoadPlayerMatches(_player2, 2);
            }
        }

        private async Task LoadPlayerMatches(UserModel player, int playerNumber)
        {
            try
            {
                // Load player matches from API
                var matchesResult = await ApiCourier.TryGetPlayerMatches(player.AccountID, 25);

                if (matchesResult.IsSuccess && matchesResult.Data != null)
                {
                    // Filter matches for last 30 days
                    var DaysAgo = DateTime.Now.AddDays(-90);
                    var recentMatches = matchesResult.Data
                        .Where(m => DateTimeOffset.FromUnixTimeSeconds(m.StartTime).DateTime >= DaysAgo)
                        .ToList();

                    if (playerNumber == 1)
                        _player1Matches = recentMatches;
                    else
                        _player2Matches = recentMatches;
                }
                else
                {
                    // If API fails, create empty list
                    if (playerNumber == 1)
                        _player1Matches = new List<DotaMatchModel>();
                    else
                        _player2Matches = new List<DotaMatchModel>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных игрока {player.Name}: {ex.Message}");

                // Set empty lists on error
                if (playerNumber == 1)
                    _player1Matches = new List<DotaMatchModel>();
                else
                    _player2Matches = new List<DotaMatchModel>();
            }
        }

        private async Task ComparePlayers(string player1Name, string player2Name)
        {
            if (string.IsNullOrEmpty(player1Name) || string.IsNullOrEmpty(player2Name))
            {
                MessageBox.Show("Пожалуйста, выберите обоих игроков");
                CompareBtn.Enabled = true;
                return;
            }

            if (player1Name == player2Name)
            {
                MessageBox.Show("Выберите разных игроков");
                CompareBtn.Enabled = true;
                return;
            }

            try
            {
                await Task.WhenAll(
                    LoadPlayer1Data(player1Name),
                    LoadPlayer2Data(player2Name)
                );

                DisplayComparisonResults();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сравнении игроков: {ex.Message}");
                CompareBtn.Enabled = true;
            }
            
        }

        private async void DisplayComparisonResults()
        {
            // Calcular estadísticas para ambos jugadores
            var stats1Task = CalculatePlayerStats(_player1Matches, _player1);
            var stats2Task = CalculatePlayerStats(_player2Matches, _player2);

            await Task.WhenAll(stats1Task, stats2Task);
            
            var stats1 = await stats1Task;
            var stats2 = await stats2Task;

            // Actualizar la interfaz con los resultados
            UpdateComparisonDisplay(stats1, stats2);
        }

        private async Task<PlayerStats> CalculatePlayerStats(List<DotaMatchModel> matches, UserModel player)
        {
            int playerInMatches = 0;
            if (matches == null || matches.Count == 0)
                return new PlayerStats();

            var playerAccountId = long.Parse(player?.AccountID ?? "0");
            
            // Filter matches where player actually participated
            var playerMatches = matches.Where(m => m.MatchId > 0).ToList();
            
            var wins = 0;
            var totalDeaths = 0;
            var totalAssists = 0;
            var totalGPM = 0;
            var totalLastHits = 0;
            var totalKills = 0;
            var validGPMCount = 0;
            var validLastHitsCount = 0;

            // Load detailed match data for each match to determine wins
            foreach (var match in playerMatches.Take(25)) // Limit to 20 matches to avoid API rate limits
            {
                try
                {
                    var matchDetailsResult = await ApiCourier.TryGetMatch(match.MatchId);
                    if (matchDetailsResult.IsSuccess && matchDetailsResult.Data != null)
                    {
                        var matchDetails = matchDetailsResult.Data;
                        var playerInMatch = matchDetails.players.FirstOrDefault(p => p.account_id == playerAccountId);
                        
                        if (playerInMatch != null)
                        {
                            playerInMatches++;

                            bool playerWon = (playerInMatch.isRadiant && matchDetails.radiant_win) || (!playerInMatch.isRadiant && !matchDetails.radiant_win);
                            if (playerWon) wins++;

                            totalDeaths += playerInMatch.deaths;
                            totalAssists += playerInMatch.assists;
                            totalKills += playerInMatch.kills;
                            

                            if (playerInMatch.gold_per_min > 0)
                            {
                                totalGPM += playerInMatch.gold_per_min;
                                validGPMCount++;
                            }
                            

                            if (playerInMatch.last_hits > 0)
                            {
                                totalLastHits += playerInMatch.last_hits;
                                validLastHitsCount++;
                            }
                        }
                    }
                }
                catch
                {
                    // Skip matches that can't be loaded
                    continue;
                }
            }

            var stats = new PlayerStats
            {
                MatchesPlayed = playerMatches.Count,
                Wins = wins,
                Kills = Math.Round(totalKills / (float)playerInMatches, 2),
                Deaths = Math.Round(totalDeaths / (float)playerInMatches, 2),
                Assists = Math.Round(totalAssists / (float)playerInMatches, 2),
                GPM = validGPMCount > 0 ? totalGPM / validGPMCount : 0,
                LastHits = validLastHitsCount > 0 ? totalLastHits / validLastHitsCount : 0
            };

            stats.Losses = stats.MatchesPlayed - stats.Wins;
            stats.WinRate = stats.MatchesPlayed > 0 ? (double)stats.Wins / stats.MatchesPlayed * 100 : 0;
            stats.KDA = stats.Deaths > 0 ? (double)(totalKills + stats.Assists) / stats.Deaths : totalKills + stats.Assists;

            return stats;
        }

        private void UpdateComparisonDisplay(PlayerStats stats1, PlayerStats stats2)
        {
            // Update matches played
            Player1MatchesPlayed.Text = stats1.MatchesPlayed.ToString();
            Player2MatchesPlayed.Text = stats2.MatchesPlayed.ToString();
            CompareAndColorElements(Player1MatchesPlayed, Player2MatchesPlayed, stats1.MatchesPlayed, stats2.MatchesPlayed);

            // Update wins
            Player1Wins.Text = stats1.Wins.ToString();
            Player2Wins.Text = stats2.Wins.ToString();
            CompareAndColorElements(Player1Wins, Player2Wins, stats1.Wins, stats2.Wins);

            // Update losses
            Player1Losses.Text = stats1.Losses.ToString();
            Player2Losses.Text = stats2.Losses.ToString();
            CompareAndColorElements(Player1Losses, Player2Losses, stats1.Losses, stats2.Losses, true); // Lower is better

            // Update win rate
            Player1WinRate.Text = $"{stats1.WinRate:F1}%";
            Player2WinRate.Text = $"{stats2.WinRate:F1}%";
            CompareAndColorElements(Player1WinRate, Player2WinRate, stats1.WinRate, stats2.WinRate);

            // Update KDA
            Player1KDA.Text = stats1.KDA.ToString("F2");
            Player2KDA.Text = stats2.KDA.ToString("F2");
            CompareAndColorElements(Player1KDA, Player2KDA, stats1.KDA, stats2.KDA);

            // Update GPM
            Player1GPM.Text = stats1.GPM.ToString();
            Player2GPM.Text = stats2.GPM.ToString();
            CompareAndColorElements(Player1GPM, Player2GPM, stats1.GPM, stats2.GPM);

            // Update Last Hits
            Player1LastHits.Text = stats1.LastHits.ToString();
            Player2LastHits.Text = stats2.LastHits.ToString();
            CompareAndColorElements(Player1LastHits, Player2LastHits, stats1.LastHits, stats2.LastHits);

            // Update kills
            Player1Kills.Text = stats1.Kills.ToString();
            Player2Kills.Text = stats2.Kills.ToString();
            CompareAndColorElements(Player1Kills, Player2Kills, stats1.Kills, stats2.Kills);

            // Update assists
            Player1Assists.Text = stats1.Assists.ToString();
            Player2Assists.Text = stats2.Assists.ToString();
            CompareAndColorElements(Player1Assists, Player2Assists, stats1.Assists, stats2.Assists);

            // Update deaths
            Player1Deaths.Text = stats1.Deaths.ToString();
            Player2Deaths.Text = stats2.Deaths.ToString();
            CompareAndColorElements(Player1Deaths, Player2Deaths, stats1.Deaths, stats2.Deaths, true); // Lower is better

            CompareBtn.Enabled = true;
        }

        private void CompareAndColorElements(Control control1, Control control2, double value1, double value2, bool lowerIsBetter = false)
        {
            if (control1 != null && control2 != null)
            {
                if (Math.Abs(value1 - value2) < 0.001) // Equal
                {
                    control1.BackColor = Color.White;
                    control2.BackColor = Color.White;
                }
                else if (lowerIsBetter)
                {
                    // Lower is better (deaths, losses)
                    control1.BackColor = value1 < value2 ? Color.LightGreen : Color.LightCoral;
                    control2.BackColor = value2 < value1 ? Color.LightGreen : Color.LightCoral;
                }
                else
                {
                    // Higher is better (wins, gpm, etc.)
                    control1.BackColor = value1 > value2 ? Color.LightGreen : Color.LightCoral;
                    control2.BackColor = value2 > value1 ? Color.LightGreen : Color.LightCoral;
                }
            }
        }

    }

    public class PlayerStats
    {
        public int MatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public double WinRate { get; set; }
        public double KDA { get; set; }
        public int GPM { get; set; }
        public double LastHits { get; set; }
        public double Kills { get; set; }
        public double Assists { get; set; }
        public double Deaths { get; set; }
    }
}
