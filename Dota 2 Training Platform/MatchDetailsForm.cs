using Dota_2_Training_Platform.Models;
using Guna.UI2.WinForms;
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
        private Panel tooltipPanel;
        private bool isMouseOverTooltip = false;
        private Timer tooltipTimer = new Timer { Interval = 150 };
        private Control tooltipOwner = null;
        private int tooltipRequestId = 0;

        public MatchDetailsForm(DotaMatchDetailsModel match)
        {
            InitializeComponent();
            InitCustomTooltip();
            LoadMatch(match);

            tooltipTimer.Tick += (s, e) =>
            {
                if (tooltipOwner == null)
                {
                    tooltipPanel.Visible = false;
                    tooltipTimer.Stop();
                }
            };
        }

        private async void MatchDetailsForm_Load(object sender, EventArgs e)
        {

        }

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
    }
}
