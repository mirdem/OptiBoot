using Mono.Unix;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptiBoot
{
    public partial class FormUpdates : Form
    {
        private const string VersionFileUrl = "https://raw.githubusercontent.com/mirdem/OptiBoot/refs/heads/main/version.json";

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

        public FormUpdates()
        {
            InitializeComponent();
        }
        public event Func<Task> OnCheckForUpdates;

        public async Task CheckForUpdates()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            try
            {
                lblUpdateStatus.Text = ReadIniValue("FormUpdates", "labelStatusChecking", iniPath);
                progressBar1.Style = ProgressBarStyle.Marquee;
                buttonCheck.Enabled = false;

                // Mevcut sürüm bilgisini al
                var localVersion = Assembly.GetExecutingAssembly().GetName().Version;
                label1.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();

                // Uzak sürüm bilgisini al
                var remoteVersionInfo = await GetRemoteVersionInfo();

                if (remoteVersionInfo != null)
                {
                    var remoteVersion = new Version(remoteVersionInfo.Version);
                    if (remoteVersion > localVersion)
                    {
                        lblUpdateStatus.Text = $"{ReadIniValue("FormUpdates", "labelStatusNewVersion", iniPath)} {remoteVersion}";

                        var result = MessageBox.Show(
                            $"{ReadIniValue("FormUpdates", "msgDownloadUpdateYesNo", iniPath)} ({remoteVersion}) {ReadIniValue("FormUpdates", "msgDownloadUpdateYesNo2", iniPath)} ",
                            ReadIniValue("FormUpdates", "msgDownloadUpdateYesNoHeader", iniPath),
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information
                        );

                        if (result == DialogResult.Yes)
                        {
                            pictureBoxLoad.Visible = false;
                            lblUpdateStatus.Visible = false;
                            // Tarayıcıda indirme sayfasını aç
                            OpenDownloadPage(remoteVersionInfo.DownloadUrl);
                        }
                    }
                    else
                    {
                        lblUpdateStatus.Text = ReadIniValue("FormUpdates", "labelStatusAppUpdated", iniPath);
                        pictureBoxLoad.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                lblUpdateStatus.Text = $"{ReadIniValue("FormUpdates", "msgErrorCheckingUpdate", iniPath)}  {ex.Message}";
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = 100;
                buttonCheck.Enabled = true;
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

        private void FormUpdates_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormSelectScreen frmSelect = new FormSelectScreen();
            frmSelect.Show();
            this.Hide();
        }


        private async void buttonCheck_Click(object sender, EventArgs e)
        {
            pictureBoxLoad.Visible = true;
            await CheckForUpdates();
        }

        private async void FormUpdates_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);

            if (Properties.Settings.Default.AlwaysTop == true)
            {
                this.TopMost = true;
            }
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            this.Text = ReadIniValue("FormUpdates", "caption", iniPath);
            buttonCheck.Text = ReadIniValue("FormUpdates", "buttonCheckUpdates", iniPath);
            buttonDownload.Text = ReadIniValue("FormUpdates", "buttonDownload", iniPath);

            if (OnCheckForUpdates != null)
            {
                await OnCheckForUpdates.Invoke();
            }

            await CheckForUpdates();

        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
        
        }
    }
}
