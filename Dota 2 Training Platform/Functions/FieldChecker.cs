using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota_2_Training_Platform.Functions
{
    static public class FieldChecker
    {
        public static string correctPasswordSymbols = "1234567890abcdefghijklmnopqrstuvwxyz-_";
        public static string correctTeamSymbols = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя 1234567890abcdefghijklmnopqrstuvwxyz-_";
        public static string correctSteamIDSymbols = "1234567890";

        public enum CheckType
        {
            Password,
            Team,
            SteamID
        };

        public static void FieldCheck(Guna2TextBox currentTextBox, CheckType type)
        {
            string validationString = string.Empty;
            switch(type)
            {
                case CheckType.Password:
                    {
                        validationString = correctPasswordSymbols;
                        break;
                    }
                case CheckType.Team:
                    {
                        validationString = correctTeamSymbols;
                        break;
                    }
                case CheckType.SteamID:
                    {
                        validationString = correctSteamIDSymbols;
                        break;
                    }
            }
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
