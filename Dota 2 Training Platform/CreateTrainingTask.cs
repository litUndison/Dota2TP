using Dota_2_Training_Platform.Functions;
using Dota_2_Training_Platform.Models;
using Dota_2_Training_Platform.Models.Trainings;
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
    public partial class CreateTrainingTask : Form
    {
        private TeamModel _team;
        public TrainingTask currentTask = new TrainingTask();
        private Guna2CheckBox[] _playerCheckBoxes = new Guna2CheckBox[5];
        private List<UserModel> _selectedPlayers = new List<UserModel>();
        private TrainingType _trainingType;
        public CreateTrainingTask(TeamModel team)
        {
            
            _team = team;
            InitializeComponent();
            Metric.Items.Clear();
            #region MetricFill // заполнение метрики полями
            Metric.Items.Add("Убийства");
            Metric.Items.Add("Помощи");
            Metric.Items.Add("Смерти");
            Metric.Items.Add("Золото в минуту");
            Metric.Items.Add("Добито крипов");
            Metric.Items.Add("Сыграть матчей");
            #endregion
            #region Comparison // заполнение сравнения полями
            comboBoxComparison.Items.Clear();
            comboBoxComparison.Items.Add("Больше или равно (>=)");
            comboBoxComparison.Items.Add("Меньше или равно (<=)");
            comboBoxComparison.Items.Add("Больше (>)");
            comboBoxComparison.Items.Add("Меньше (<)");
            comboBoxComparison.Items.Add("Равно (=)");
            #endregion

        }

        private void Period_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Period.SelectedIndex)
            {
                case 0: // один матч
                    {
                        PeriodValue.Visible = false;
                        Deadline.Visible = true;
                        DeadlineTimeHours.Visible = true;
                        DeadlineTimeMinutes.Visible = true;
                        break;
                    }
                case 1: // несколько матчей
                    {
                        PeriodValue.Visible = true;
                        Deadline.Visible = true;
                        DeadlineTimeHours.Visible = true;
                        DeadlineTimeMinutes.Visible = true;
                        break;
                    }
                case 2: // до определённой даты
                    {
                        PeriodValue.Visible = false;
                        Deadline.Visible = true;
                        DeadlineTimeHours.Visible = true;
                        DeadlineTimeMinutes.Visible = true;
                        break;
                    }
                default:
                    {
                        PeriodValue.Visible = false;
                        Deadline.Visible = false;
                        DeadlineTimeHours.Visible = false;
                        DeadlineTimeMinutes.Visible = true;
                        break;
                    }
            }
        }

        private void CreateTrainingTask_Load(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 2000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.SetToolTip(comboBoxComparison, "Выберите, как сравнивать фактическое значение с целевым");

            #region trash
            _playerCheckBoxes[0] = guna2CheckBox1;
            _playerCheckBoxes[1] = guna2CheckBox2;
            _playerCheckBoxes[2] = guna2CheckBox3;
            _playerCheckBoxes[3] = guna2CheckBox4;
            _playerCheckBoxes[4] = guna2CheckBox5;
            #endregion
            for (int i = 0; i < _team.Players.Count && i < _playerCheckBoxes.Length; i++)
            {
                _playerCheckBoxes[i].Text = _team.Players[i].Name;
                _playerCheckBoxes[i].Tag = _team.Players[i];
                _playerCheckBoxes[i].CheckedChanged += CreateTrainingTask_CheckedChanged;
                _playerCheckBoxes[i].Visible = true;
            }
           


            Deadline.Text = DateTime.Now.ToString();
            int hour = int.Parse(DateTime.Now.ToString("HH"));
            DeadlineTimeHours.Text = $"{hour}";
            DeadlineTimeMinutes.Text = $"{DateTime.Now.ToString("mm")}";
            if (hour < 23)
            {
                DeadlineTimeHours.Text = $"{hour + 1}";
            }

            Deadline.MinDate = DateTime.Now;
            Deadline.MaxDate = DateTime.Now.AddYears(1);
        }

        private void CreateTrainingTask_CheckedChanged(object sender, EventArgs e)
        {
            if(sender as Guna2CheckBox != null) 
            {
                if((sender as Guna2CheckBox).Checked) 
                {
                    //MessageBox.Show(((sender as Guna2CheckBox).Tag as UserModel).Name + " added");
                    _selectedPlayers.Add((sender as Guna2CheckBox).Tag as UserModel);
                }
                else 
                {
                    //MessageBox.Show(((sender as Guna2CheckBox).Tag as UserModel).Name + " deleted");
                    _selectedPlayers = _selectedPlayers.Where(p => p.AccountID != ((sender as Guna2CheckBox).Tag as UserModel).AccountID).ToList();
                }
            }
            if(_selectedPlayers.Count > 1)
            {
                _trainingType = TrainingType.Team;
            }
            else if(_selectedPlayers.Count == 1)
            {
                _trainingType = TrainingType.Individual;
            }
            //MessageBox.Show(_selectedPlayers.Count.ToString());
        }

        private void TrainingType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            
            TrainingTaskName.Text = TrainingTaskName.Text.Trim();
            if(string.IsNullOrEmpty(TrainingTaskName.Text))
            {
                MessageBox.Show("Имя тренировки не указано");
                return;
            }
            if (_selectedPlayers.Count == 0)
            {
                MessageBox.Show("Игроки не выбраны");
                return;
            }
            if(Metric.SelectedIndex == -1)
            {
                MessageBox.Show("Метрика не выбрана");
                return;
            }
            if (comboBoxComparison.SelectedIndex == -1)
            {
                MessageBox.Show("Знак сравнения не выбран");
                return;
            }
            if (string.IsNullOrEmpty(TargetValue.Text))
            {
                MessageBox.Show("Целевое значение не указано");
                return;
            }
            if (!int.TryParse(TargetValue.Text, out int value))
            {
                MessageBox.Show("Целевое значение указано неверно");
                return;
            }
            if (Period.SelectedIndex == -1)
            {
                MessageBox.Show("Период выполнения не выбран");
                return;
            }
            if (Period.SelectedIndex == 1 && string.IsNullOrEmpty(PeriodValue.Text))
            {
                MessageBox.Show("Количество матчей не указано");
                return;
            }
            if(Period.SelectedIndex == 1 && !int.TryParse(PeriodValue.Text, out int value2))
            {
                MessageBox.Show("Кол-во матчей указано неверно");
                return;
            }
            //if (DateTime.Parse(Deadline.Text).Date < DateTime.Parse(DateTime.Now.ToString()).Date)
            //{
            //    MessageBox.Show("Дата не может быть раньше сегодняшней");
            //    return;
            //}
            if (int.TryParse(DeadlineTimeHours.Text, out int hours) && int.TryParse(DeadlineTimeMinutes.Text, out int minutes))
            {
                if ((hours < 0 || hours > 23) || (minutes < 0 || minutes > 60))
                {
                    MessageBox.Show("Время указано неверно");
                    return;
                }
            }
            if (string.IsNullOrEmpty(DeadlineTimeHours.Text) || string.IsNullOrEmpty(DeadlineTimeMinutes.Text))
            {
                MessageBox.Show("Время не указано");
                return;
            }
            if (DateTime.Parse(Deadline.Text).Date == DateTime.Parse(DateTime.Now.ToString()).Date)
            {
                if(DateTime.Now >= Deadline.Value.Date.AddHours(int.Parse(DeadlineTimeHours.Text)).AddMinutes(int.Parse(DeadlineTimeMinutes.Text)))
                {
                    MessageBox.Show("Время указано в прошлом");
                    return;
                }
            }

            SubmitButton.DialogResult = DialogResult.OK;
            currentTask.Title = TrainingTaskName.Text;
            currentTask.Type = _trainingType;
            currentTask.PlayerIds = _selectedPlayers.Select(p => p.AccountID).ToList();
            currentTask.Metric = (TrainingMetric)Metric.SelectedIndex;
            currentTask.TargetValue = int.Parse(TargetValue.Text);
            currentTask.Comparison = (ComparisonType)comboBoxComparison.SelectedIndex;
            currentTask.Period = (TrainingPeriod)Period.SelectedIndex;
            switch(Period.SelectedIndex)
            {
                case 0:
                    {
                        currentTask.PeriodValue = 1;
                        break;
                    }
                case 1:
                    {
                        currentTask.PeriodValue = int.Parse(PeriodValue.Text);
                        break;
                    }
                case 2:
                    {
                        currentTask.PeriodValue = -1;
                        break;
                    }
            }
            currentTask.Deadline = DateTime.Parse($"{DateTime.Parse(Deadline.Text).ToShortDateString()} {DeadlineTimeHours.Text}:{DeadlineTimeMinutes.Text}");
        }

        private void Deadline_ValueChanged(object sender, EventArgs e)
        {
            if (DateTime.Parse(Deadline.Text).Date < DateTime.Parse(DateTime.Now.ToString()).Date)
            {
                MessageBox.Show("Дата не может быть раньше сегодняшней");
                return;
            }
        }

        private void TargetValue_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(TargetValue, FieldChecker.CheckType.Numbers);
        }

        private void PeriodValue_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(PeriodValue, FieldChecker.CheckType.Numbers);
        }

        private void TrainingTaskName_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(TrainingTaskName, FieldChecker.CheckType.Names);
        }

        private void DeadlineTimeHours_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(DeadlineTimeHours, FieldChecker.CheckType.Numbers);
        }

        private void DeadlineTimeMinutes_TextChanged(object sender, EventArgs e)
        {
            FieldChecker.FieldCheck(DeadlineTimeMinutes, FieldChecker.CheckType.Numbers);
        }
    }
}
