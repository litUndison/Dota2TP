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
        public CreateTrainingTask()
        {
            InitializeComponent();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Period_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Period.SelectedIndex)
            {
                case 0: // один матч
                    {
                        PeriodValue.Visible = false;
                        Deadline.Visible = false;
                        break;
                    }
                case 1: // несколько матчей
                    {
                        PeriodValue.Visible = true;
                        Deadline.Visible = true;
                        break;
                    }
                case 2: // до определённой даты
                    {
                        PeriodValue.Visible = false;
                        Deadline.Visible = true;
                        break;
                    }
                default:
                    {
                        PeriodValue.Visible = false;
                        Deadline.Visible = false;
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
        }
    }
}
