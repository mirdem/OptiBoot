using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OptiBoot.FormUpdates;

namespace OptiBoot
{
    public partial class FormSelectScreen : Form
    {

        public FormSelectScreen()
        {
            InitializeComponent();
            labelVersion.Text = "v " + this.ProductVersion;
            this.BackColor = Color.FromArgb(245, 247, 250);
            toolTip1.ShowAlways = true;
            InitializeSystemTray();

        }
        public event Func<Task> OnCheckForUpdates;
        private const string VersionFileUrl = "https://raw.githubusercontent.com/mirdem/OptiBoot/refs/heads/main/version.json";
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenu;

        public class VersionInfo
        {
            public string Version { get; set; }
            public string DownloadUrl { get; set; }
            public string ReleaseNotes { get; set; }
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

        private void InitializeSystemTray()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Icon(Application.StartupPath + @"\data\img\OptiBootSS.ico");
            notifyIcon.Text = "OptiBoot";
            notifyIcon.Visible = false;

            contextMenu = new ContextMenuStrip();

            ToolStripMenuItem showItem = new ToolStripMenuItem("Show Optiboot");
            showItem.Click += (s, e) => ShowApplication();

            ToolStripMenuItem exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += (s, e) => ExitApplication();

            contextMenu.Items.Add(showItem);
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add(exitItem);

            notifyIcon.ContextMenuStrip = contextMenu;
            notifyIcon.DoubleClick += (s, e) => ShowApplication();
        }
        private void HideToSystemTray()
        {
            this.Hide();
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(1000, "Info", "The application is minimized to the system tray", ToolTipIcon.Info);
        }

        private void ShowApplication()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            notifyIcon.Visible = false;
        }

        private void ExitApplication()
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.EnableSysTray == true)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true; // Formun kapanmasını engelle
                    HideToSystemTray();
                    return;
                }
                base.OnFormClosing(e);
            }

        }

        public async Task CheckForUpdates()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            try
            {

                // Mevcut sürüm bilgisini al
                var localVersion = Assembly.GetExecutingAssembly().GetName().Version;

                // Uzak sürüm bilgisini al
                var remoteVersionInfo = await GetRemoteVersionInfo();

                if (remoteVersionInfo != null)
                {
                    var remoteVersion = new Version(remoteVersionInfo.Version);
                    if (remoteVersion > localVersion)
                    {

                        var result = MessageBox.Show(
                            $"{ReadIniValue("FormUpdates", "msgDownloadUpdateYesNo", iniPath)} ({remoteVersion}) {ReadIniValue("FormUpdates", "msgDownloadUpdateYesNo2", iniPath)} ",
                            ReadIniValue("FormUpdates", "msgDownloadUpdateYesNoHeader", iniPath),
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information
                        );

                        if (result == DialogResult.Yes)
                        {
                            // Tarayıcıda indirme sayfasını aç
                            OpenDownloadPage(remoteVersionInfo.DownloadUrl);
                        }
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {
            }
            finally
            {

            }
        }
        private void OpenDownloadPage(string url)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            try
            {
                // URL'yi tarayıcıda aç
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ReadIniValue("FormUpdates", "msgErrorOpenURL", iniPath)}  {ex.Message}\n\n" +
                                $"{ReadIniValue("FormUpdates", "msgErrorOpenURL2", iniPath)} \n{url}",
                                ReadIniValue("FormServices", "msgError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task<VersionInfo> GetRemoteVersionInfo()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            using (var httpClient = new HttpClient())
            {
                try
                {
                    // GitHub'dan sürüm bilgisini al
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "MyAppUpdateChecker");
                    var response = await httpClient.GetAsync(VersionFileUrl);

                    // HTTP durum kodunu kontrol et
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"HTTP hatası: {response.StatusCode}");
                    }

                    var json = await response.Content.ReadAsStringAsync();

                    // Gelen içeriğin JSON olup olmadığını kontrol et
                    if (json.Trim().StartsWith("<"))
                    {
                        throw new Exception("Sunucudan HTML yanıtı alındı. URL'yi kontrol edin.");
                    }

                    return JsonConvert.DeserializeObject<VersionInfo>(json);
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ReadIniValue("FormUpdates", "msgErrorGetVersion", iniPath)}  {ex.Message}");
                }
            }
        }

        public void LoadLanguage()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            buttonStartupApps.Text = ReadIniValue("FormSelectScreen", "buttonStartupApps", iniPath);
            buttonServices.Text = ReadIniValue("FormSelectScreen", "buttonServices", iniPath);
            buttonPerformance.Text = ReadIniValue("FormSelectScreen", "buttonCPUMon", iniPath);
            buttonRAM.Text = ReadIniValue("FormSelectScreen", "buttonRAMMon", iniPath);
            buttonGPU.Text = ReadIniValue("FormSelectScreen", "buttonGPUSpecs", iniPath);
            buttonSoundAdapter.Text = ReadIniValue("FormSelectScreen", "buttonSoundDevices", iniPath);
            buttonDiskManager.Text = ReadIniValue("FormSelectScreen", "buttonDiskManagement", iniPath);
            buttonSensors.Text = ReadIniValue("FormSelectScreen", "buttonSensors", iniPath);
            buttonAbout.Text = ReadIniValue("FormSelectScreen", "buttonAbout", iniPath);
            buttonUpdates.Text = ReadIniValue("FormSelectScreen", "buttonUpdates", iniPath);
            buttonSettings.Text = ReadIniValue("FormSelectScreen", "buttonSettings", iniPath);
            buttonTasks.Text = ReadIniValue("FormSelectScreen", "buttonTasks", iniPath);
            buttonWinProcess.Text = ReadIniValue("FormSelectScreen", "buttonWinProcess", iniPath);
            toolTip1.SetToolTip(buttonStartupApps, ReadIniValue("FormSelectScreen", "toolTipStartupApps", iniPath));
            toolTip1.SetToolTip(buttonServices, ReadIniValue("FormSelectScreen", "toolTipServices", iniPath));
            toolTip1.SetToolTip(buttonPerformance, ReadIniValue("FormSelectScreen", "toolTipCPU", iniPath));
            toolTip1.SetToolTip(buttonRAM, ReadIniValue("FormSelectScreen", "toolTipRAM", iniPath));
            toolTip1.SetToolTip(buttonGPU, ReadIniValue("FormSelectScreen", "toolTipGPU", iniPath));
            toolTip1.SetToolTip(buttonSoundAdapter, ReadIniValue("FormSelectScreen", "toolTipSoundDevices", iniPath));
            toolTip1.SetToolTip(buttonDiskManager, ReadIniValue("FormSelectScreen", "toolTipDiskManagement", iniPath));
            toolTip1.SetToolTip(buttonSensors, ReadIniValue("FormSelectScreen", "toolTipSensors", iniPath));
            toolTip1.SetToolTip(buttonTasks, ReadIniValue("FormSelectScreen", "toolTipTasks", iniPath));
        }


        private void buttonStartupApps_Click(object sender, EventArgs e)
        {
            FormStartupApps formStartupApps = new FormStartupApps();
            formStartupApps.Show();
            this.Hide();
            
        }

        private void buttonServices_Click(object sender, EventArgs e)
        {
            FormServices frmServices = new FormServices();
            frmServices.Show();
            this.Hide();
        }

        private async void FormSelectScreen_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);

            if (Properties.Settings.Default.AlwaysTop == true)
            {
                this.TopMost = true;
            }
            if (Properties.Settings.Default.CheckUpdates == true)
            {
                if (OnCheckForUpdates != null)
                {
                    await OnCheckForUpdates.Invoke();
                }

                await CheckForUpdates();
            }
            LoadLanguage();
        }

        private void buttonPerformance_Click(object sender, EventArgs e)
        {
            FormPerformance frmCpu = new FormPerformance();
            frmCpu.Show();
            this.Hide();
        }

        private void buttonRAM_Click(object sender, EventArgs e)
        {
            FormRAMInfo frmRamInfo = new FormRAMInfo();
            frmRamInfo.Show();
            this.Hide();
        }

        private void buttonGPU_Click(object sender, EventArgs e)
        {
            FormGPUInfo frmGPUInfo = new FormGPUInfo();
            frmGPUInfo.Show();
            this.Hide();
        }

        private void buttonSoundAdapter_Click(object sender, EventArgs e)
        {
            FormSoundAdapters frmSoundAdapters = new FormSoundAdapters();
            frmSoundAdapters.Show();
            this.Hide();
        }

        private void buttonDiskManager_Click(object sender, EventArgs e)
        {
            FormDiskManager frmDiskManager = new FormDiskManager();
            frmDiskManager.Show();
            this.Hide();
        }

        private void buttonSensors_Click(object sender, EventArgs e)
        {
            FormSensors frmSensors = new FormSensors();
            frmSensors.Show();
            this.Hide();
        }

        private void FormSelectScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            FormAbout frmAbout = new FormAbout();
            frmAbout.Show();
        }

        private void buttonUpdates_Click(object sender, EventArgs e)
        {
            FormUpdates frmUpdates = new FormUpdates();
            frmUpdates.Show();
            this.Hide();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            FormSettings frmSettings = new FormSettings();
            frmSettings.Show();
            this.Hide();
        }

        private void buttonTasks_Click(object sender, EventArgs e)
        {
            FormTaskSchuelder formTaskSchuelder = new FormTaskSchuelder();
            formTaskSchuelder.Show();
            this.Hide();
        }

        private void buttonWinProcess_Click(object sender, EventArgs e)
        {
            FormWindowsProcesses formWindowsProcesses = new FormWindowsProcesses();
            formWindowsProcesses.Show();
            this.Hide();
        }
    }
}
