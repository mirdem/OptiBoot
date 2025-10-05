using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptiBoot
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        [DllImport("kernel32.dll")]
        static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

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

        private void linkLabelXLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = "https://x.com/xfDev1",
                UseShellExecute = true,
                Verb = "open"
            });
        }

        private void linkLabelAllProjects_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = "https://sourceforge.net/u/xfdev/profile/",
                UseShellExecute = true,
                Verb = "open"
            });
        }

        private void linkLabelFeedback_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (Properties.Settings.Default.language == "Turkish")
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = "https://forms.gle/JqRr58QkR3otfons8",
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            else
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = "https://forms.gle/jMpehVzJxEG9VUr58",
                    UseShellExecute = true,
                    Verb = "open"
                });
            }

        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);

            if (Properties.Settings.Default.AlwaysTop == true) 
            {
                this.TopMost = true;
            }
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            this.Text = ReadIniValue("FormAbout", "caption", iniPath);
            labelDesc.Text = ReadIniValue("FormAbout", "labelDescription", iniPath);
            linkLabelXLink.Text = ReadIniValue("FormAbout", "labelLinkX", iniPath);
            linkLabelAllProjects.Text = ReadIniValue("FormAbout", "labelLinkAllProjects", iniPath);
            linkLabelFeedback.Text = ReadIniValue("FormAbout", "labelLinkFeedback", iniPath);

        }
    }
}
