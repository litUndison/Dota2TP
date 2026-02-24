using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota_2_Training_Platform.Functions
{
    static public class FieldChecker
    {
        public static void FieldCheck(Guna2TextBox currentTextBox, string validationString)
        {
            if (currentTextBox.Text != null && currentTextBox.Text.Length != 0)
            {
                for (int i = 0; i < currentTextBox.Text.Length; i++)
                {
                    if (!validationString.Contains(currentTextBox.Text[i].ToString().ToLower()))
                    {
                        currentTextBox.Text = currentTextBox.Text.Remove(i, 1);
                        i--;
                        currentTextBox.SelectionStart = currentTextBox.Text.Length;
                        currentTextBox.BorderColor = Color.Red;
                        currentTextBox.FocusedState.BorderColor = Color.Red;
                        continue;
                    }
                    currentTextBox.BorderColor = Color.Gray;
                    currentTextBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
                }
            }
        }
    }
}
