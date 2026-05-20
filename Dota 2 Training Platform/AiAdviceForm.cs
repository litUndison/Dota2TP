using Dota_2_Training_Platform.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dota_2_Training_Platform
{
    public partial class AiAdviceForm : Form
    {
        public const int PositionModeAuto = 0;

        private readonly string _heroName;
        private readonly MatchPlayerModel _player;
        private readonly DotaMatchDetailsModel _match;
        private readonly Func<string, Task<string>> _getAdviceAsync;
        private readonly string _autoPosition;

        public AiAdviceForm(
            string heroName,
            MatchPlayerModel player,
            DotaMatchDetailsModel match,
            Func<string, Task<string>> getAdviceAsync)
        {
            _heroName = heroName;
            _player = player;
            _match = match;
            _getAdviceAsync = getAdviceAsync;
            _autoPosition = GuessPosition(player);

            InitializeComponent();
        }

        private void AiAdviceForm_Load(object sender, EventArgs e)
        {
            cmbPosition.Items.Clear();
            cmbPosition.Items.Add("Автоопределение");
            cmbPosition.Items.Add("Позиция 1 — Керри");
            cmbPosition.Items.Add("Позиция 2 — Мид");
            cmbPosition.Items.Add("Позиция 3 — Оффлейн");
            cmbPosition.Items.Add("Позиция 4 — Саппорт");
            cmbPosition.Items.Add("Позиция 5 — Жёсткий саппорт");
            cmbPosition.SelectedIndex = 0;

            lblTitle.Text = $"AI-анализ — {_heroName}";
            UpdateAutoHint();
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAutoHint();
        }

        private void UpdateAutoHint()
        {
            if (cmbPosition.SelectedIndex == PositionModeAuto)
            {
                lblAutoHint.Visible = true;
                lblAutoHint.Text = $"Автоопределение: {_autoPosition}";
            }
            else
            {
                lblAutoHint.Visible = true;
                lblAutoHint.Text = "Позиция задана вручную — в запрос уйдёт выбранная роль.";
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (_getAdviceAsync == null)
                return;

            string position = ResolvePositionForPrompt();
            string prompt = BuildPrompt(_heroName, _player, _match, position, cmbPosition.SelectedIndex == PositionModeAuto);

            btnSend.Enabled = false;
            cmbPosition.Enabled = false;
            txtResponse.Text = "Запрос отправляется, подождите...";

            try
            {
                string advice = await _getAdviceAsync(prompt);
                txtResponse.Text = advice ?? string.Empty;
            }
            catch (Exception ex)
            {
                txtResponse.Text = $"Ошибка: {ex.Message}";
            }
            finally
            {
                btnSend.Enabled = true;
                cmbPosition.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string ResolvePositionForPrompt()
        {
            switch (cmbPosition.SelectedIndex)
            {
                case 1: return "Позиция 1 (Керри)";
                case 2: return "Позиция 2 (Мид)";
                case 3: return "Позиция 3 (Оффлейн)";
                case 4: return "Позиция 4 (Саппорт)";
                case 5: return "Позиция 5 (Жёсткий саппорт)";
                default: return _autoPosition;
            }
        }

        public static string GuessPosition(MatchPlayerModel player)
        {
            if (player == null)
                return "Позиция не определена";

            if (player.gold_per_min >= 600 && player.last_hits >= 180)
                return "Керри (позиция 1)";
            if (player.gold_per_min >= 500 && player.assists >= 8)
                return "Мид/Оффлейн (позиция 2-3)";
            if (player.assists >= 12 && player.last_hits < 90)
                return "Саппорт (позиция 4-5)";
            return "Позиция не определена";
        }

        public static string BuildPrompt(
            string heroName,
            MatchPlayerModel player,
            DotaMatchDetailsModel match,
            string position,
            bool isAutoPosition)
        {
            string winner = match.radiant_win ? "Radiant" : "Dire";
            string playerSide = player.isRadiant ? "Radiant" : "Dire";
            string result = winner == playerSide ? "Победа" : "Поражение";

            string positionLine = isAutoPosition
                ? $"Позиция (автоопределение, может быть неточной): {position}"
                : $"Позиция (указана игроком): {position}";

            return
                "Ты тренер по Dota 2. Дай ровно 4-5 коротких предложения на русском языке. Не обращайся к игроку напрямую. Говори как третье, независимое лицо.\n" +
                "Формат: 1-2 сильная сторона, 1-2 точки роста, 1-2 практический совет к следующей игре.\n" +
                "Без воды и без общих фраз. Учитывай указанную позицию.\n\n" +
                $"Герой: {heroName}\n" +
                $"{positionLine}\n" +
                $"Результат матча: {result}\n" +
                $"K/D/A: {player.kills}/{player.deaths}/{player.assists}\n" +
                $"GPM/XPM: {player.gold_per_min}/{player.xp_per_min}\n" +
                $"LH/DN: {player.last_hits}/{player.denies}\n" +
                $"NetWorth: {player.net_worth}\n" +
                $"Hero/Tower damage: {player.hero_damage}/{player.tower_damage}\n" +
                $"Длительность матча в секундах: {match.duration}\n";
        }
    }
}
