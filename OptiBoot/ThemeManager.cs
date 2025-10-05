using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBoot
{
    public class ThemeManager
    {     
            public static bool IsDarkMode
            {
                get => Properties.Settings.Default.Theme == "Dark";
                set => Properties.Settings.Default.Theme = value ? "Dark" : "Light";
            }

            private static Color BackColorLight = Color.White;
            private static Color ForeColorLight = Color.Black;

            private static Color BackColorDark = Color.FromArgb(30, 30, 30);
            private static Color ForeColorDark = Color.White;

            public static void ApplyTheme(Form form)
            {
                Color backColor = IsDarkMode ? BackColorDark : BackColorLight;
                Color foreColor = IsDarkMode ? ForeColorDark : ForeColorLight;

                form.BackColor = backColor;
                form.ForeColor = foreColor;

                foreach (Control control in form.Controls)
                {
                    ApplyThemeToControl(control, backColor, foreColor);
                }
            }

            private static void ApplyThemeToControl(Control control, Color backColor, Color foreColor)
            {
                control.BackColor = backColor;
                control.ForeColor = foreColor;

                foreach (Control child in control.Controls)
                {
                    ApplyThemeToControl(child, backColor, foreColor);
                }
            }

            public static void SaveTheme()
            {
                Properties.Settings.Default.Save();
            }
        }

    }