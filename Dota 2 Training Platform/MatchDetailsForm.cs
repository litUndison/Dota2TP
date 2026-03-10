using Dota_2_Training_Platform.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dota_2_Training_Platform
{
    public partial class MatchDetailsForm : Form
    {
        private DotaMatchDetailsModel _match;

        public MatchDetailsForm(DotaMatchDetailsModel match)
        {
            InitializeComponent();
            LoadMatch(match);
        }

        private async void MatchDetailsForm_Load(object sender, EventArgs e)
        {

        }

        private void LoadMatch(DotaMatchDetailsModel match)
        {
            MatchID.Text = "ID: " + match.match_id.ToString();
            RadiantScore.Text = match.radiant_score.ToString();
            DireScore.Text = match.dire_score.ToString();
            WinnerLabel.Text = match.radiant_win ? "Победа сил Света" : "Победа сил Тьмы"; //Radiant / dire

            TimeSpan duration = TimeSpan.FromSeconds(match.duration);
            DurationLabel.Text = duration.ToString(@"mm\:ss");

            DateTime matchDate = DateTimeOffset.FromUnixTimeSeconds(match.start_time).LocalDateTime;
            StartTime.Text = $"Матч проведён {matchDate:dd.MM.yyyy HH:mm}";


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
                PictureBox heroPic = new PictureBox
                {
                    Width = 60,
                    Height = 60,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    //ImageLocation = ApiCourier.GetHeroImage(player.hero_id) // метод получения картинки героя
                };
                //MessageBox.Show(player.hero_id.ToString());
                //MessageBox.Show(ApiCourier.Heroes[player.hero_id].img);
                //MessageBox.Show(ApiCourier.Heroes[player.hero_id].id.ToString());
                //MessageBox.Show(ApiCourier.GetHeroImage(player.hero_id));
                //heroPic.LoadAsync(ApiCourier.GetHeroImage(player.hero_id));
                heroPic.Location = new Point(x, 0);
                x += heroPic.Width;

                // Tooltip для предметов
                //ToolTip tooltip = new ToolTip();
                //string itemsText = GetItemsText(player); // метод возвращает строку с предметами
                //tooltip.SetToolTip(heroPic, itemsText);

                playerBlock.Controls.Add(heroPic);

                // 2 Имя игрока
                Label nameLabel = new Label
                {
                    Text = player.personaname,
                    Width = 117,
                    Height = 60,
                    Location = new Point(x, 0),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                x += nameLabel.Width;
                playerBlock.Controls.Add(nameLabel);

                // 3 Kills
                Label killsLabel = new Label
                {
                    Text = player.kills.ToString(),
                    Width = 43,
                    Height = 60,
                    Location = new Point(x, 0),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                x += killsLabel.Width;
                playerBlock.Controls.Add(killsLabel);

                // 4 Deaths
                Label deathsLabel = new Label
                {
                    Text = player.deaths.ToString(),
                    Width = 43,
                    Height = 60,
                    Location = new Point(x, 0),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                x += deathsLabel.Width;
                playerBlock.Controls.Add(deathsLabel);

                // 5 Assists
                Label assistsLabel = new Label
                {
                    Text = player.assists.ToString(),
                    Width = 43,
                    Height = 60,
                    Location = new Point(x, 0),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                x += assistsLabel.Width;
                playerBlock.Controls.Add(assistsLabel);

                // 6 Net Worth
                Label netWorthLabel = new Label
                {
                    Text = player.net_worth.ToString(),
                    Width = 46,
                    Height = 60,
                    Location = new Point(x, 0),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                x += netWorthLabel.Width;
                playerBlock.Controls.Add(netWorthLabel);

                // 7 Last Hits
                Label lhLabel = new Label
                {
                    Text = player.last_hits.ToString(),
                    Width = 61,
                    Height = 60,
                    Location = new Point(x, 0),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                x += lhLabel.Width;
                playerBlock.Controls.Add(lhLabel);

                // 8 Gold per Minute
                Label gpmLabel = new Label
                {
                    Text = player.gold_per_min.ToString(),
                    Width = 53,
                    Height = 60,
                    Location = new Point(x, 0),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                x += gpmLabel.Width;
                playerBlock.Controls.Add(gpmLabel);

                // 9 Hero Damage
                Label hdLabel = new Label
                {
                    Text = player.hero_damage.ToString(),
                    Width = 65,
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

        // Метод для получения текста предметов игрока
        private string GetItemsText(MatchPlayerModel player)
        {
            List<int> items = new List<int>
        {
            player.item_0, player.item_1, player.item_2,
            player.item_3, player.item_4, player.item_5,
            player.backpack_0, player.backpack_1, player.backpack_2,
            player.item_neutral, player.item_neutral2
        };

                StringBuilder sb = new StringBuilder();
                foreach (var itemId in items)
                {
                    if (itemId != 0)
                        sb.AppendLine(ApiCourier.ItemsById[itemId].id.ToString()); // возвращает название предмета по ID
                }
                return sb.ToString();
            }
        }
}
