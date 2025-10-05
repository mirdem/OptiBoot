using Mono.Unix;
using OpenHardwareMonitor.Hardware;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows.Forms;

namespace OptiBoot
{
    public partial class FormSensors : Form
    {
        private Computer computer;
        private System.Timers.Timer updateTimer;

        public FormSensors()
        {
            InitializeComponent();
            InitializeHardwareMonitor();
            InitializeTimer();
            labelCPUSicaklik.Text = "N/A";
            labelCPUYuzdesi.Text = "N/A";
            labelGPUSicaklik.Text = "N/A";
            labelRAMSicaklik.Text = "N/A";
            labelAnakartSicaklik.Text = "N/A";
            labelDiskSicaklik.Text = "N/A";
        }
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

        private void InitializeHardwareMonitor()
        {
            try
            {
                computer = new Computer()
                {
                    IsCpuEnabled = true,
                    IsGpuEnabled = true,
                    IsMemoryEnabled = true,
                    IsMotherboardEnabled = true,
                    IsStorageEnabled = true
                };
                computer.Open(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hardware monitor başlatılamadı: {ex.Message}");
            }
        }

        private void InitializeTimer()
        {
            updateTimer = new System.Timers.Timer(1000); // 1 saniye
            updateTimer.Elapsed += UpdateTimer_Elapsed;
            updateTimer.AutoReset = true;
            updateTimer.Enabled = true;
        }

        private void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // UI thread'inde güncelleme yapmak için
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateHardwareInfo));
                return;
            }
            UpdateHardwareInfo();
        }

        private void UpdateHardwareInfo()
        {
            try
            {
                if (computer == null || computer.Hardware == null) return;

                foreach (var hardware in computer.Hardware)
                {
                    hardware.Update();
                }

                // CPU Bilgileri
                UpdateCPUInfo();

                // GPU Bilgileri
                UpdateGPUInfo();

                // RAM Bilgileri
                UpdateRAMInfo();

                // Anakart Bilgileri
                UpdateMainboardInfo();

                // Disk Bilgileri
                UpdateDiskInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ReadIniValue("Others", "msgHeaderError", iniPath)} {ex.Message}");
            }
        }

        private void UpdateCPUInfo()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.Cpu)
                {
                    var temperature = hardware.Sensors
                        .FirstOrDefault(s => s.SensorType == SensorType.Temperature &&
                                           (s.Name.Contains("Package") || s.Name.Contains("CPU") || s.Name.Contains("Core")))
                        ?? hardware.Sensors
                            .FirstOrDefault(s => s.SensorType == SensorType.Temperature);
                    if (temperature != null && temperature.Value.HasValue)
                    {
                        labelCPUSicaklik.Text = ReadIniValue("FormSensors", "labelCPUTemp", iniPath) + $"{temperature.Value.Value:0.0} °C";
                    }

                    var load = hardware.Sensors
                        .FirstOrDefault(s => s.SensorType == SensorType.Load &&
                                           (s.Name.Contains("Total") || s.Name.Contains("CPU")))
                        ?? hardware.Sensors
                            .FirstOrDefault(s => s.SensorType == SensorType.Load);
                    if (load != null && load.Value.HasValue)
                    {
                        labelCPUYuzdesi.Text = ReadIniValue("FormSensors", "labelCPUUsage", iniPath) + $"{load.Value.Value:0.0} %";
                    }
                    break;
                }
            }
        }

        private void UpdateGPUInfo()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.GpuNvidia ||
                    hardware.HardwareType == HardwareType.GpuAmd ||
                    hardware.HardwareType == HardwareType.GpuIntel)
                {
                    // GPU Sıcaklık
                    var temperature = hardware.Sensors
                        .FirstOrDefault(s => s.SensorType == SensorType.Temperature &&
                                           (s.Name.Contains("GPU Core") || s.Name.Contains("GPU")));
                    if (temperature != null && temperature.Value.HasValue)
                    {
                        labelGPUSicaklik.Text = ReadIniValue("FormSensors", "labelGPUTemp", iniPath) + $"{temperature.Value.Value:0.0} °C";
                    }

                    // GPU Kullanımı
                    var load = hardware.Sensors
                        .FirstOrDefault(s => s.SensorType == SensorType.Load &&
                                           (s.Name.Contains("GPU Core") || s.Name.Contains("GPU")));
                    if (load != null && load.Value.HasValue)
                    {
                        // GPU kullanım yüzdesi için ek label varsa
                        labelGPUKullanimi.Text = ReadIniValue("FormSensors", "labelGPUUsage", iniPath) + $"{load.Value.Value:0.0} %";
                    }
                    break;
                }
            }
        }

        private void UpdateRAMInfo()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.Memory)
                {
                    // RAM Sıcaklık
                    var temperature = hardware.Sensors
                        .FirstOrDefault(s => s.SensorType == SensorType.Temperature);
                    if (temperature != null && temperature.Value.HasValue)
                    {
                        labelRAMSicaklik.Text = ReadIniValue("FormSensors", "labelRAMTemp", iniPath) + $"{temperature.Value.Value:0.0} °C";
                    }
                    break;
                }
            }
        }

        private void UpdateMainboardInfo()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.Motherboard)
                {
                    // Anakart Sıcaklık
                    var temperature = hardware.Sensors
                        .FirstOrDefault(s => s.SensorType == SensorType.Temperature);
                    if (temperature != null && temperature.Value.HasValue)
                    {
                        labelAnakartSicaklik.Text = ReadIniValue("FormSensors", "labelMainboardTemp", iniPath) + $"{temperature.Value.Value:0.0} °C";
                    }
                    break;
                }
            }
        }

        private void UpdateDiskInfo()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.Storage)
                {
                    // Disk Sıcaklık
                    var temperature = hardware.Sensors
                        .FirstOrDefault(s => s.SensorType == SensorType.Temperature);
                    if (temperature != null && temperature.Value.HasValue)
                    {
                        labelDiskSicaklik.Text = ReadIniValue("FormSensors", "labelDiskTemp", iniPath) + $"{temperature.Value.Value:0.0} °C";
                    }
                    break;
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            updateTimer?.Stop();
            updateTimer?.Dispose();
            computer?.Close();
        }

        private void FormSensors_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormSelectScreen frmSelect = new FormSelectScreen();
            frmSelect.Show();
            this.Hide();
        }

        private void FormSensors_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);

            if (Properties.Settings.Default.AlwaysTop == true)
            {
                this.TopMost = true;
            }
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            groupBoxCPU.Text = ReadIniValue("FormSensors", "groupboxCPU", iniPath);
            groupBoxGPU.Text = ReadIniValue("FormSensors", "groupboxGPU", iniPath);
            groupBoxRAM.Text = ReadIniValue("FormSensors", "groupboxRAM", iniPath);
            groupBoxMainboard.Text = ReadIniValue("FormSensors", "groupboxMainboard", iniPath);
            groupBoxDisk.Text = ReadIniValue("FormSensors", "groupboxDisk", iniPath);

        }
    }
}