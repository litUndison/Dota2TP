using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Dota_2_Training_Platform.Models;

namespace Dota_2_Training_Platform
{
    public class RecordSettingsForm : Form
    {
        private Guna2HtmlLabel lblTitle;
        private Guna2HtmlLabel lblFps;
        private Guna2HtmlLabel lblResolution;
        private Guna2HtmlLabel lblHotKey;

        private Guna2ComboBox cmbFps;
        private Guna2ComboBox cmbResolution;
        private Guna2ComboBox cmbHotKey;

        private Guna2CheckBox chkRecordAudio;
        private Guna2CheckBox chkUseSameHotkeyForStop;

        private Guna2Button btnSave;
        private Guna2Button btnCancel;

        public RecordSettingsForm()
        {
            InitializeUi();
            LoadDefaultValues();
        }

        private void InitializeUi()
        {
            this.Text = "Настройки записи";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ClientSize = new Size(430, 360);
            this.BackColor = Color.White;

            lblTitle = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Text = "Настройки записи матча",
                Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold),
                Location = new Point(24, 20),
                Size = new Size(300, 30)
            };

            lblFps = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Text = "FPS",
                Font = new Font("Microsoft Sans Serif", 10F),
                Location = new Point(24, 75),
                Size = new Size(120, 24)
            };

            cmbFps = new Guna2ComboBox
            {
                Location = new Point(24, 102),
                Size = new Size(380, 36),
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BorderRadius = 4
            };
            cmbFps.Items.AddRange(new object[] { "30", "60" });

            lblResolution = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Text = "Разрешение",
                Font = new Font("Microsoft Sans Serif", 10F),
                Location = new Point(24, 148),
                Size = new Size(120, 24)
            };

            cmbResolution = new Guna2ComboBox
            {
                Location = new Point(24, 175),
                Size = new Size(380, 36),
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BorderRadius = 4
            };
            cmbResolution.Items.AddRange(new object[]
            {
                "Исходное",
                "1920x1080",
                "1280x720"
            });

            chkRecordAudio = new Guna2CheckBox
            {
                Text = "Записывать системный звук",
                Location = new Point(24, 225),
                Size = new Size(250, 28),
                Checked = true,
                BackColor = Color.Transparent
            };

            lblHotKey = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Text = "Горячая клавиша старта/стопа",
                Font = new Font("Microsoft Sans Serif", 10F),
                Location = new Point(24, 258),
                Size = new Size(240, 24)
            };

            cmbHotKey = new Guna2ComboBox
            {
                Location = new Point(24, 285),
                Size = new Size(180, 36),
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BorderRadius = 4
            };

            for (Keys key = Keys.F1; key <= Keys.F12; key++)
                cmbHotKey.Items.Add(key);

            chkUseSameHotkeyForStop = new Guna2CheckBox
            {
                Text = "Эта же клавиша останавливает запись",
                Location = new Point(215, 290),
                Size = new Size(220, 28),
                Checked = true,
                Enabled = false,
                BackColor = Color.Transparent
            };

            btnSave = new Guna2Button
            {
                Text = "Сохранить",
                Location = new Point(214, 325),
                Size = new Size(190, 35),
                FillColor = Color.FromArgb(33, 42, 57),
                ForeColor = Color.White,
                BorderRadius = 4,
                DialogResult = DialogResult.None
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Guna2Button
            {
                Text = "Отмена",
                Location = new Point(24, 325),
                Size = new Size(180, 35),
                FillColor = Color.Gray,
                ForeColor = Color.White,
                BorderRadius = 4,
                DialogResult = DialogResult.Cancel
            };

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblFps);
            this.Controls.Add(cmbFps);
            this.Controls.Add(lblResolution);
            this.Controls.Add(cmbResolution);
            this.Controls.Add(chkRecordAudio);
            this.Controls.Add(lblHotKey);
            this.Controls.Add(cmbHotKey);
            this.Controls.Add(chkUseSameHotkeyForStop);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void LoadDefaultValues()
        {
            cmbFps.SelectedIndex = 0;
            cmbResolution.SelectedItem = "1920x1080";
            cmbHotKey.SelectedItem = Keys.F9;
            chkRecordAudio.Checked = true;
            chkUseSameHotkeyForStop.Checked = true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cmbFps.SelectedItem == null)
            {
                MessageBox.Show("Выбери FPS.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbResolution.SelectedItem == null)
            {
                MessageBox.Show("Выбери разрешение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbHotKey.SelectedItem == null)
            {
                MessageBox.Show("Выбери горячую клавишу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public RecordSettingsModel GetSettings()
        {
            return new RecordSettingsModel
            {
                Fps = int.Parse(cmbFps.SelectedItem.ToString()),
                Resolution = cmbResolution.SelectedItem.ToString(),
                RecordAudio = chkRecordAudio.Checked,
                HotKey = (Keys)cmbHotKey.SelectedItem,
                UseSameHotkeyForStop = chkUseSameHotkeyForStop.Checked
            };
        }

        public void SetSettings(RecordSettingsModel settings)
        {
            if (settings == null)
                return;

            cmbFps.SelectedItem = settings.Fps.ToString();
            cmbResolution.SelectedItem = settings.Resolution;
            chkRecordAudio.Checked = settings.RecordAudio;
            cmbHotKey.SelectedItem = settings.HotKey;
            chkUseSameHotkeyForStop.Checked = settings.UseSameHotkeyForStop;
        }
    }
}