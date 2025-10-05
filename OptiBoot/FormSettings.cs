using Microsoft.Win32;
using Mono.Unix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptiBoot
{
    public partial class FormSettings : Form
    {
        private string OptiBoot;
        public string theme;
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        [DllImport("kernel32.dll")]
        static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        public string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

        private string ReadIniValue(string section, string key, string filePath)
        {
            if (!File.Exists(filePath))
                return "";

            string[] lines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8);
            string currentSection = "";

            foreach (var line in lines)
            {
                string trimmed = line.Trim();

                // Yorum satırlarını atla
                if (trimmed.StartsWith(";") || trimmed.StartsWith("#") || string.IsNullOrEmpty(trimmed))
                    continue;

                // [Section] kontrolü
                if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
                {
                    currentSection = trimmed.Substring(1, trimmed.Length - 2);
                    continue;
                }

                // Eğer doğru section içindeysek key=value arama
                if (currentSection.Equals(section, StringComparison.OrdinalIgnoreCase))
                {
                    int eqIndex = trimmed.IndexOf('=');
                    if (eqIndex > 0)
                    {
                        string k = trimmed.Substring(0, eqIndex).Trim();
                        string v = trimmed.Substring(eqIndex + 1).Trim();
                        if (k.Equals(key, StringComparison.OrdinalIgnoreCase))
                            return v;
                    }
                }
            }

            return ""; // Bulunamadı
        }


        public FormSettings()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            InitializeComponent();
            labelAppVersion.Text = Application.ProductVersion;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(checkBoxSystemTray, ReadIniValue("FormSettings", "toolTipSysTray", iniPath));
            checkBoxAlwaysTop.Checked = Properties.Settings.Default.AlwaysTop;
            checkBoxCheckUpdates.Checked = Properties.Settings.Default.CheckUpdates;
            checkBoxRunStartsWin.Checked = Properties.Settings.Default.RunStartsWin;
            checkBoxSystemTray.Checked = Properties.Settings.Default.EnableSysTray;
            comboBoxLanguage.Text = Properties.Settings.Default.language;
        }
        private bool IsAppInStartup()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", false))
                {
                    return key?.GetValue(OptiBoot) != null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void FormSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormSelectScreen frmSelect = new FormSelectScreen();
            frmSelect.Show();
            this.Hide();
        }

        private void buttonTabGeneral_Click(object sender, EventArgs e)
        {
            materialTabControl1.SelectedIndex = 0;
        }

        private void buttonTabOther_Click(object sender, EventArgs e)
        {
            materialTabControl1.SelectedIndex = 1;
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);

            if (ThemeManager.IsDarkMode)
            {
                radioButtonThemeDark.Checked = true;
            }
            else
            {
                radioButtonThemeLight.Checked = true;
            }

            if (Properties.Settings.Default.AlwaysTop == true)
            {
                this.TopMost = true;
            }
            LoadLanguage();
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            Properties.Settings.Default.CheckUpdates = checkBoxCheckUpdates.Checked;
            Properties.Settings.Default.AlwaysTop = checkBoxAlwaysTop.Checked;
            Properties.Settings.Default.EnableSysTray = checkBoxSystemTray.Checked;
            Properties.Settings.Default.RunStartsWin = checkBoxRunStartsWin.Checked;
            Properties.Settings.Default.language = comboBoxLanguage.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show(ReadIniValue("FormSettings", "msgSaveSettings", iniPath), ReadIniValue("FormSettings", "caption", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadLanguage();
        }

        private void buttonShowLogs_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Application.StartupPath + @"log.txt");
        }

        private void buttonDefaultSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            checkBoxAlwaysTop.Checked = Properties.Settings.Default.AlwaysTop;
            checkBoxCheckUpdates.Checked = Properties.Settings.Default.CheckUpdates;
            checkBoxRunStartsWin.Checked = Properties.Settings.Default.RunStartsWin;
            checkBoxSystemTray.Checked = Properties.Settings.Default.EnableSysTray;
            comboBoxLanguage.Text = Properties.Settings.Default.language;
        }

        private void buttonClearLogs_Click(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");


            MessageBox.Show(ReadIniValue("FormSettings", "msgboxClearedLogs", iniPath), ReadIniValue("Others", "msgHeaderInfo", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Information);
            File.WriteAllText(Application.StartupPath + @"log.txt", String.Empty);
        }

        public void LoadLanguage()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            this.Text = ReadIniValue("FormSettings", "caption", iniPath);
            buttonTabGeneral.Text = ReadIniValue("FormSettings", "buttonTabGeneral", iniPath);
            buttonTabOther.Text = ReadIniValue("FormSettings", "buttonTabOther", iniPath);
            labelLanguage.Text = ReadIniValue("FormSettings", "lblLanguage", iniPath);
            checkBoxAlwaysTop.Text = ReadIniValue("FormSettings", "chkAlwaysTop", iniPath);
            checkBoxSystemTray.Text = ReadIniValue("FormSettings", "chkMinimize", iniPath);
            checkBoxCheckUpdates.Text = ReadIniValue("FormSettings", "chkUpdates", iniPath);
            checkBoxRunStartsWin.Text = ReadIniValue("FormSettings", "chkRunStartsWin", iniPath);
            labelSysLogs.Text = ReadIniValue("FormSettings", "lblSysLogs", iniPath);
            buttonShowLogs.Text = ReadIniValue("FormSettings", "buttonShowLogs", iniPath);
            buttonClearLogs.Text = ReadIniValue("FormSettings", "buttonClearLogs", iniPath);
            buttonSaveSettings.Text = ReadIniValue("FormSettings", "buttonSave", iniPath);
            buttonDefaultSettings.Text = ReadIniValue("FormSettings", "buttonDefaults", iniPath);
            groupBoxTheme.Text = ReadIniValue("FormSettings", "grpTheme", iniPath);
            radioButtonThemeLight.Text = ReadIniValue("FormSettings", "radioBtnLight", iniPath);
            radioButtonThemeDark.Text = ReadIniValue("FormSettings", "radioBtnDark", iniPath);

        }

        private void checkBoxRunStartsWin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsAppInStartup())
                {
                    RemoveFromStartup();
                }
                else
                {
                    AddToStartup();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ReadIniValue("FormStartupApps", "msgFailDelete", iniPath)} {ex.Message}", ReadIniValue("Others", "msgHeaderError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddToStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                key?.SetValue(OptiBoot, $"\"{Application.ExecutablePath}\"");
            }
        }

        private void RemoveFromStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                key?.DeleteValue(OptiBoot, false);
            }
        }

        private void radioButtonThemeDark_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonThemeDark.Checked) // Sadece seçildiğinde uygula
            {
                ThemeManager.IsDarkMode = true;
                ThemeManager.ApplyTheme(this);     // 👈 Owner yerine kendi formunu uygula
                ThemeManager.SaveTheme();
            }
        }

        private void radioButtonThemeLight_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonThemeLight.Checked)
            {
                ThemeManager.IsDarkMode = false;
                ThemeManager.ApplyTheme(this);     // 👈 Owner yerine kendi formunu uygula
                ThemeManager.SaveTheme();
            }
        }
    }
}
