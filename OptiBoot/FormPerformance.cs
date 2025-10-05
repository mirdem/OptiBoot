using Mono.Unix;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace OptiBoot
{
    public partial class FormPerformance : Form
    {

        private Chart chart;
        private Label lblCurrentUsage;
        private Label lblCpuInfo;
        private Label lblCpuDetails;
        private Label lblProcessCount;
        private Label lblClockSpeed;
        private Button btnScreenshot2;
        private Button btnRefreshDetails;
        private int dataPointCount = 0;
        private const int MAX_DATA_POINTS = 60;
        private List<float> cpuUsageHistory = new List<float>();
        private DateTime lastUpdateTime = DateTime.MinValue;
        private bool isFormClosed = false;
        private System.Windows.Forms.Timer timer;

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

        public FormPerformance()
        {
            InitializeComponent();
            InitializeUI();
            _ = InitializeCpuInfoAsync();
            InitializeTimer();
        }
        private async Task InitializeCpuInfoAsync()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            //this.Text = ReadIniValue("FormDiskManager", "caption", iniPath);

            try
            {
                string cpuName = await GetCpuNameAsync();
                int coreCount = await GetCpuCoreCountAsync();
                int logicalProcessors = await GetLogicalProcessorsAsync();
                string baseSpeed = await GetBaseClockSpeedAsync();
                string currentSpeed = await GetCurrentClockSpeedAsync();
                string l1Cache = await GetCacheSizeAsync("L1");
                string l2Cache = await GetCacheSizeAsync("L2");
                string l3Cache = await GetCacheSizeAsync("L3");
                string virtualization = await GetVirtualizationSupportAsync();
                string sockets = await GetSocketCountAsync();

                lblCpuInfo.Text = $"{cpuName}";

                lblCpuDetails.Text = $"{ReadIniValue("FormCPUInfo", "labelCores", iniPath)} {coreCount} | {ReadIniValue("FormCPUInfo", "labelCores2", iniPath)} {logicalProcessors}\n" +
                                   $"{ReadIniValue("FormCPUInfo", "labelBaseSpeed", iniPath)} {baseSpeed} | {ReadIniValue("FormCPUInfo", "labelInstantSpeed", iniPath)} {currentSpeed}\n" +
                                   $"{ReadIniValue("FormCPUInfo", "labelCPUSockets", iniPath)} {sockets} | {ReadIniValue("FormCPUInfo", "labelVirtualization", iniPath)} {virtualization}\n" +
                                   $"{ReadIniValue("FormCPUInfo", "labelL1Cache", iniPath)} {l1Cache} | {ReadIniValue("FormCPUInfo", "labelL2Cache", iniPath)} {l2Cache} | {ReadIniValue("FormCPUInfo", "labelL3Cache", iniPath)} {l3Cache}";
            }
            catch (Exception ex)
            {
                lblCpuInfo.Text =ReadIniValue("FormCPUInfo", "labelGetErrorCPUInfo", iniPath)+ ex.Message;
            }
        }

        private async Task<string> GetCpuNameAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_Processor"))
                    {
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            return obj["Name"].ToString();
                        }
                    }
                }
                catch
                {
                }
                return "Bilinmeyen İşlemci";
            });
        }

        private async Task<int> GetCpuCoreCountAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var searcher = new ManagementObjectSearcher("SELECT NumberOfCores FROM Win32_Processor"))
                    {
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            return Convert.ToInt32(obj["NumberOfCores"]);
                        }
                    }
                }
                catch
                {
                }
                return Environment.ProcessorCount;
            });
        }

        private async Task<int> GetLogicalProcessorsAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var searcher = new ManagementObjectSearcher("SELECT NumberOfLogicalProcessors FROM Win32_Processor"))
                    {
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            return Convert.ToInt32(obj["NumberOfLogicalProcessors"]);
                        }
                    }
                }
                catch
                {
                }
                return Environment.ProcessorCount;
            });
        }

        private async Task<string> GetBaseClockSpeedAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var searcher = new ManagementObjectSearcher("SELECT MaxClockSpeed FROM Win32_Processor"))
                    {
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            int speed = Convert.ToInt32(obj["MaxClockSpeed"]);
                            return $"{speed} MHz";
                        }
                    }
                }
                catch
                {
                }
                return "Bilinmiyor";
            });
        }

        private async Task<string> GetCurrentClockSpeedAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var searcher = new ManagementObjectSearcher("SELECT CurrentClockSpeed FROM Win32_Processor"))
                    {
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            int speed = Convert.ToInt32(obj["CurrentClockSpeed"]);
                            return $"{speed} MHz";
                        }
                    }
                }
                catch
                {
                }
                return "Bilinmiyor";
            });
        }

        private async Task<string> GetCacheSizeAsync(string cacheLevel)
        {
            return await Task.Run(() =>
            {
                try
                {
                    string query = "SELECT * FROM Win32_CacheMemory";
                    using (var searcher = new ManagementObjectSearcher(query))
                    {
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            string level = obj["Level"].ToString();
                            if (level == GetCacheLevelNumber(cacheLevel))
                            {
                                uint size = Convert.ToUInt32(obj["MaxCacheSize"]);
                                if (size > 0)
                                {
                                    return FormatCacheSize(size);
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
                return "Bilinmiyor";
            });
        }

        private string GetCacheLevelNumber(string cacheLevel)
        {
            switch (cacheLevel)
            {
                case "L1": return "3";
                case "L2": return "4";
                case "L3": return "5";
                default: return "0";
            }
        }

        private string FormatCacheSize(uint size)
        {
            if (size < 1024)
                return $"{size} KB";
            else
                return $"{size / 1024} MB";
        }

        private async Task<string> GetVirtualizationSupportAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
                    {
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            if (obj["VirtualizationFirmwareEnabled"] != null)
                            {
                                bool enabled = (bool)obj["VirtualizationFirmwareEnabled"];
                                return enabled ? "Etkin" : "Devre Dışı";
                            }
                        }
                    }
                }
                catch
                {
                }
                return "Bilinmiyor";
            });
        }

        private async Task<string> GetSocketCountAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
                    {
                        int count = 0;
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            count++;
                        }
                        return count.ToString();
                    }
                }
                catch
                {
                }
                return "1";
            });
        }

        private async Task<float> GetCpuUsageAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var searcher = new ManagementObjectSearcher("SELECT LoadPercentage FROM Win32_Processor"))
                    {
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            return Convert.ToSingle(obj["LoadPercentage"]);
                        }
                    }
                }
                catch
                {
                }
                return 0;
            });
        }

        private int GetProcessCount()
        {
            try
            {
                return Process.GetProcesses().Length;
            }
            catch
            {
                return 0;
            }
        }

        private void InitializeUI()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            this.Text = ReadIniValue("FormCPUInfo", "caption", iniPath);
            this.Size = new Size(1000, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblCpuInfo = new Label();
            lblCpuInfo.Font = new Font("Arial", 12, FontStyle.Bold);
            lblCpuInfo.ForeColor = Color.DarkBlue;
            lblCpuInfo.Location = new Point(20, 10);
            lblCpuInfo.Size = new Size(800, 20);
            lblCpuInfo.Text = ReadIniValue("FormCPUInfo", "labelCPUInfo", iniPath);
            this.Controls.Add(lblCpuInfo);

            lblCpuDetails = new Label();
            lblCpuDetails.Font = new Font("Arial", 9, FontStyle.Regular);
            lblCpuDetails.ForeColor = Color.DarkGreen;
            lblCpuDetails.Location = new Point(20, 35);
            lblCpuDetails.Size = new Size(800, 60);
            lblCpuDetails.Text = ReadIniValue("FormCPUInfo", "labelCPUDetails", iniPath);
            this.Controls.Add(lblCpuDetails);

            lblCurrentUsage = new Label();
            lblCurrentUsage.Font = new Font("Arial", 14, FontStyle.Bold);
            lblCurrentUsage.ForeColor = Color.DarkBlue;
            lblCurrentUsage.Location = new Point(20, 100);
            lblCurrentUsage.Size = new Size(300, 30);
            lblCurrentUsage.Text = ReadIniValue("FormCPUInfo", "labelCPUUsage", iniPath);
            this.Controls.Add(lblCurrentUsage);

            lblProcessCount = new Label();
            lblProcessCount.Font = new Font("Arial", 10, FontStyle.Regular);
            lblProcessCount.ForeColor = Color.DarkRed;
            lblProcessCount.Location = new Point(20, 130);
            lblProcessCount.Size = new Size(300, 20);
            lblProcessCount.Text = ReadIniValue("FormCPUInfo", "labelRunningProcess", iniPath);
            this.Controls.Add(lblProcessCount);

            lblClockSpeed = new Label();
            lblClockSpeed.Font = new Font("Arial", 10, FontStyle.Regular);
            lblClockSpeed.ForeColor = Color.DarkOrange;
            lblClockSpeed.Location = new Point(20, 150);
            lblClockSpeed.Size = new Size(300, 20);
            lblClockSpeed.Text = ReadIniValue("FormCPUInfo", "labelInstantSpeed2", iniPath);
            this.Controls.Add(lblClockSpeed);

            btnScreenshot = new Button();
            btnScreenshot.Text = ReadIniValue("FormCPUInfo", "labelTakeScreenshots", iniPath);
            btnScreenshot.Font = new Font("Arial", 10, FontStyle.Bold);
            btnScreenshot.ForeColor = Color.White;
            btnScreenshot.BackColor = Color.SteelBlue;
            btnScreenshot.Size = new Size(180, 35);
            btnScreenshot.Location = new Point(700, 100);
            btnScreenshot.Click += BtnScreenshot_Click;
            this.Controls.Add(btnScreenshot);

            btnRefreshDetails = new Button();
            btnRefreshDetails.Text = ReadIniValue("FormCPUInfo", "labelRefreshInfo", iniPath);
            btnRefreshDetails.Font = new Font("Arial", 10, FontStyle.Bold);
            btnRefreshDetails.ForeColor = Color.White;
            btnRefreshDetails.BackColor = Color.Green;
            btnRefreshDetails.Size = new Size(180, 35);
            btnRefreshDetails.Location = new Point(700, 140);
            btnRefreshDetails.Click += async (sender, e) => await InitializeCpuInfoAsync();
            this.Controls.Add(btnRefreshDetails);

            chart = new Chart();
            chart.Location = new Point(20, 180);
            chart.Size = new Size(940, 500);

            ChartArea chartArea = new ChartArea();
            chartArea.AxisY.Maximum = 100;
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.Title = ReadIniValue("FormCPUInfo", "labelChartCPUUsage", iniPath);
            chartArea.AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chartArea.AxisX.Title = ReadIniValue("FormCPUInfo", "labelChartTime", iniPath);
            chartArea.AxisX.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chartArea.BackColor = Color.LightGray;
            chartArea.AxisX.MajorGrid.LineColor = Color.LightBlue;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightBlue;
            chart.ChartAreas.Add(chartArea);

            Series series = new Series();
            series.ChartType = SeriesChartType.Line;
            series.Color = Color.Blue;
            series.BorderWidth = 2;
            series.Name = ReadIniValue("FormCPUInfo", "labelChartTitle", iniPath);
            chart.Series.Add(series);

            Legend legend = new Legend();
            legend.Docking = Docking.Bottom;
            chart.Legends.Add(legend);

            this.Controls.Add(chart);

            StatusStrip statusStrip = new StatusStrip();
            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel(ReadIniValue("FormCPUInfo", "chartTooltipCPU", iniPath));
            ToolStripStatusLabel updateLabel = new ToolStripStatusLabel(ReadIniValue("FormCPUInfo", "chartTooltipUpdateTime", iniPath));
            statusStrip.Items.Add(statusLabel);
            statusStrip.Items.Add(updateLabel);
            this.Controls.Add(statusStrip);
            statusStrip.Dock = DockStyle.Bottom;
        }
        private Bitmap CaptureFormScreenshot()
        {
            Bitmap bitmap = new Bitmap(this.Width, this.Height);

            this.DrawToBitmap(bitmap, new Rectangle(0, 0, this.Width, this.Height));

            return bitmap;
        }

        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += async (sender, e) => await Timer_TickAsync();
            timer.Start();
        }

        private async Task Timer_TickAsync()
        {
            if (isFormClosed) return;

            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            try
            {
                float cpuUsage = await GetCpuUsageAsync();
                int processCount = GetProcessCount();
                string currentSpeed = await GetCurrentClockSpeedAsync();

                lblCurrentUsage.Text = $"{ReadIniValue("FormCPUInfo", "labelCPUUsage2", iniPath)} {cpuUsage:F1}%";
                lblProcessCount.Text = $"{ReadIniValue("FormCPUInfo", "labelRunningProcess2", iniPath)} {processCount}";
                lblClockSpeed.Text = $"{ReadIniValue("FormCPUInfo", "labelInstantSpeed", iniPath)} {currentSpeed}";

                cpuUsageHistory.Add(cpuUsage);
                if (cpuUsageHistory.Count > MAX_DATA_POINTS)
                {
                    cpuUsageHistory.RemoveAt(0);
                }

                chart.Series[ReadIniValue("FormCPUInfo", "labelChartTitle", iniPath)].Points.Clear();
                for (int i = 0; i < cpuUsageHistory.Count; i++)
                {
                    chart.Series[ReadIniValue("FormCPUInfo", "labelChartTitle", iniPath)].Points.AddXY(i, cpuUsageHistory[i]);
                }

                UpdateXAxis();

                dataPointCount++;

                this.Text = $"{ReadIniValue("FormCPUInfo", "updatedCaption", iniPath)} {cpuUsage:F1}% - {ReadIniValue("FormCPUInfo", "labelRunningProcess2", iniPath)} {processCount}";

                lastUpdateTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ReadIniValue("FormCPUInfo", "errorCPUUsage", iniPath)} {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }      
        }

        private void UpdateXAxis()
        {
            if (cpuUsageHistory.Count > 0)
            {
                chart.ChartAreas[0].AxisX.Minimum = 0;
                chart.ChartAreas[0].AxisX.Maximum = Math.Max(MAX_DATA_POINTS, cpuUsageHistory.Count);

                chart.ChartAreas[0].AxisX.Interval = 10;
            }
        }

        private void FormPerformance_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);

            if (Properties.Settings.Default.AlwaysTop == true)
            {
                this.TopMost = true;
            }
        }
        private void BtnScreenshot_Click(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            try
            {
                Bitmap screenshot = CaptureFormScreenshot();

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp";
                saveDialog.Title = ReadIniValue("FormCPUInfo", "saveScreenshotCaption", iniPath);
                saveDialog.FileName = $"CPU_Monitor_{DateTime.Now:yyyyMMdd_HHmmss}";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ImageFormat format = ImageFormat.Png;
                    switch (Path.GetExtension(saveDialog.FileName).ToLower())
                    {
                        case ".jpg":
                        case ".jpeg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                        default:
                            format = ImageFormat.Png;
                            break;
                    }

                    screenshot.Save(saveDialog.FileName, format);
                    LogManager.Logla("CPU ekran görüntüsü kaydedildi: "+DateTime.Now +" Dosya Adı: " + $"CPU_Monitor_{DateTime.Now:yyyyMMdd_HHmmss}");
                    MessageBox.Show($"{ReadIniValue("FormCPUInfo", "saveScreenshotMsg", iniPath)}\n{saveDialog.FileName}",
                                  "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                screenshot.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ReadIniValue("FormCPUInfo", "saveScreenshotMsgError", iniPath)} {ex.Message}",
                             ReadIniValue("Others", "msgHeaderError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPerformance_FormClosed(object sender, FormClosedEventArgs e)
        {
            isFormClosed = true;
            timer.Stop();
            timer.Dispose();

            FormSelectScreen frmSelect = new FormSelectScreen();
            frmSelect.Show();
            this.Dispose();
            this.Hide();
        }
    }
}