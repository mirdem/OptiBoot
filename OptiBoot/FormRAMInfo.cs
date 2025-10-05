using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace OptiBoot
{
    public partial class FormRAMInfo : Form
    {
        public class RAMInfo
        {
            public string Manufacturer { get; set; }
            public string MemoryType { get; set; }
            public ulong Capacity { get; set; }
            public uint Speed { get; set; }
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

        public FormRAMInfo()
        {
            InitializeComponent();
        }
        public static List<RAMInfo> GetDetailedRAMInfo()
        {
            List<RAMInfo> ramList = new List<RAMInfo>();

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    RAMInfo ram = new RAMInfo();

                    ram.Manufacturer = obj["Manufacturer"]?.ToString() ?? "Bilinmiyor";
                    ram.Capacity = Convert.ToUInt64(obj["Capacity"]);
                    ram.Speed = obj["Speed"] != null ? Convert.ToUInt32(obj["Speed"]) : 0;

                    if (obj["MemoryType"] != null)
                    {
                        uint memoryType = Convert.ToUInt32(obj["MemoryType"]);
                        ram.MemoryType = GetMemoryTypeString(memoryType);
                    }
                    else
                    {
                        ram.MemoryType = "Bilinmiyor";
                    }

                    ramList.Add(ram);
                }
            }

            return ramList;
        }

        private static string GetMemoryTypeString(uint memoryType)
        {
            if (Properties.Settings.Default.language == "Turkish")
            {
                switch (memoryType)
                {
                    case 20: return "DDR";
                    case 21: return "DDR2";
                    case 24: return "DDR3";
                    case 26: return "DDR4";
                    case 34: return "DDR5";
                    default: return "Bilinmeyen";
                }
            }
            else 
            {
                switch (memoryType)
                {
                    case 20: return "DDR";
                    case 21: return "DDR2";
                    case 24: return "DDR3";
                    case 26: return "DDR4";
                    case 34: return "DDR5";
                    default: return "Unknown";
                }
            }
           
        }

        private void FormRAMInfo_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);

            if (Properties.Settings.Default.AlwaysTop == true)
            {
                this.TopMost = true;
            }
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            labelRAMDetails.Text = ReadIniValue("FormRAMInfo", "labelRAMDetails", iniPath);
            this.Text = ReadIniValue("FormRAMInfo", "caption", iniPath);

            refreshTimer.Interval = 1000;
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
            UpdateRAMInfo();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            UpdateRAMInfo();
        }

        private void UpdateRAMInfo()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            try
            {
                var computerInfo = new ComputerInfo();

                double totalMemoryGB = computerInfo.TotalPhysicalMemory / (1024.0 * 1024.0 * 1024.0);

                double availableMemoryGB = computerInfo.AvailablePhysicalMemory / (1024.0 * 1024.0 * 1024.0);

                double usedMemoryGB = totalMemoryGB - availableMemoryGB;

                int usagePercentage = (int)((usedMemoryGB * 100) / totalMemoryGB);

                totalMemoryLabel.Text = $"{ReadIniValue("FormRAMInfo", "labelTotalRAM", iniPath)} {totalMemoryGB:F2} GB";
                availableMemoryLabel.Text = $"{ReadIniValue("FormRAMInfo", "labelAvailableRAM", iniPath)} {availableMemoryGB:F2} GB";
                usedMemoryLabel.Text = $"{ReadIniValue("FormRAMInfo", "labelUsedRAM", iniPath)} {usedMemoryGB:F2} GB";
                ramUsageProgressBar.Value = usagePercentage;
                usagePercentageLabel.Text = $"{ReadIniValue("FormRAMInfo", "labelUsagePercent", iniPath)} {usagePercentage}%";

                ShowDetailedRAMInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ReadIniValue("FormRAMInfo", "msgErrorRAMInfo", iniPath)} {ex.Message}");
            }
        }

        private void ShowDetailedRAMInfo()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            try
            {
                var ramModules = GetDetailedRAMInfo();

                if (ramModules.Count > 0)
                {
                    var firstModule = ramModules[0];

                    manufacturerLabel.Text = $"{ReadIniValue("FormRAMInfo", "labelManufacturer", iniPath)} {firstModule.Manufacturer}";
                    memoryTypeLabel.Text = $"{ReadIniValue("FormRAMInfo", "labelMemoryType", iniPath)} {firstModule.MemoryType}";
                    speedLabel.Text = $"{ReadIniValue("FormRAMInfo", "labelMemorySpeed", iniPath)}{" "}{firstModule.Speed} MHz";

                    double moduleCapacityGB = firstModule.Capacity / (1024.0 * 1024.0 * 1024.0);
                    capacityLabel.Text = $"{ReadIniValue("FormRAMInfo", "labelModuleCapacity", iniPath)} {moduleCapacityGB:F1} GB";

                    modulesLabel.Text = $"{ReadIniValue("FormRAMInfo", "labelRAMModules", iniPath)}  {ramModules.Count} ";

                    LoadBrandLogo(firstModule.Manufacturer);
                }
                else
                {
                    if(Properties.Settings.Default.language=="Turkish")
                    {
                        manufacturerLabel.Text = "Üretici: Bilinmiyor";
                        memoryTypeLabel.Text = "Bellek Türü: Bilinmiyor";
                        speedLabel.Text = "Hız: Bilinmiyor";
                        capacityLabel.Text = "Modül Kapasitesi: Bilinmiyor";
                        modulesLabel.Text = "RAM Modülleri: Bilinmiyor";
                        ramBrandPictureBox.Image = null;
                    }
                    else
                    {
                        manufacturerLabel.Text = "Manufacturer: Unknown";
                        memoryTypeLabel.Text = "Memory Type: Unknown";
                        speedLabel.Text = "Speed: Unknown";
                        capacityLabel.Text = "Module Capacity: Unknown";
                        modulesLabel.Text = "RAM Modules: Unknown";
                        ramBrandPictureBox.Image = null;
                    }
                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ReadIniValue("FormRAMInfo", "msgErrorRAMInfo", iniPath)}{ex.Message}");
            }
        }


        public static List<RAMInfo> GetDetailedRAMInfo2()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            List<RAMInfo> ramList = new List<RAMInfo>();

            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        RAMInfo ram = new RAMInfo();

                        ram.Manufacturer = obj["Manufacturer"]?.ToString() ?? "Bilinmiyor";
                        ram.Capacity = obj["Capacity"] != null ? Convert.ToUInt64(obj["Capacity"]) : 0;
                        ram.Speed = obj["Speed"] != null ? Convert.ToUInt32(obj["Speed"]) : 0;

                        if (obj["MemoryType"] != null)
                        {
                            uint memoryType = Convert.ToUInt32(obj["MemoryType"]);
                            ram.MemoryType = GetMemoryTypeString(memoryType);
                        }
                        else
                        {
                            ram.MemoryType = "Bilinmiyor";
                        }

                        ramList.Add(ram);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"WMI sorgu hatası: {ex.Message}");
            }

            return ramList;
        }
        private void LoadBrandLogo(string manufacturer)
        {
            try
            {
                string cleanManufacturer = manufacturer.ToLower().Trim();

                if (cleanManufacturer.Contains("samsung"))
                {
                    ramBrandPictureBox.Image = LoadEmbeddedImage("samsung");
                }
                else if (cleanManufacturer.Contains("kingston") || cleanManufacturer.Contains("hyperx"))
                {
                    ramBrandPictureBox.Image = LoadEmbeddedImage("kingston");
                }
                else if (cleanManufacturer.Contains("corsair"))
                {
                    ramBrandPictureBox.Image = LoadEmbeddedImage("corsair");
                }
                else if (cleanManufacturer.Contains("g.skill"))
                {
                    ramBrandPictureBox.Image = LoadEmbeddedImage("gskill");
                }
                else if (cleanManufacturer.Contains("crucial"))
                {
                    ramBrandPictureBox.Image = LoadEmbeddedImage("crucial");
                }
                else if (cleanManufacturer.Contains("micron"))
                {
                    ramBrandPictureBox.Image = LoadEmbeddedImage("micron");
                }
                else if (cleanManufacturer.Contains("team") || cleanManufacturer.Contains("group"))
                {
                    ramBrandPictureBox.Image = LoadEmbeddedImage("teamGroup");
                }
                else if (cleanManufacturer.Contains("adata") || cleanManufacturer.Contains("xpg"))
                {
                    ramBrandPictureBox.Image = LoadEmbeddedImage("adata");
                }
                else if (cleanManufacturer.Contains("TwinMOS") || cleanManufacturer.Contains("twinmos"))
                {
                    ramBrandPictureBox.Image = LoadEmbeddedImage("twinmos");
                }
                else
                {
                    ramBrandPictureBox.Image = LoadEmbeddedImage("Unknown");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logo yükleme hatası: {ex.Message}");
                ramBrandPictureBox.Image = null;
            }
        }

        private Bitmap LoadEmbeddedImage(string brandName)
        {
            int size = 150;
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {

                using (Font font = new Font("Arial", 14, FontStyle.Bold))
                using (StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    switch (brandName)
                    {
                        case "Samsung":
                            ramBrandPictureBox.Image = OptiBoot.Properties.Resources.samsung;
                            //g.FillRectangle(Brushes.Blue, 0, 0, size, size);
                            //g.DrawString("SAMSUNG", font, Brushes.White, new Rectangle(0, 0, size, size), sf);
                            break;
                        case "Kingston":
                            ramBrandPictureBox.Image = OptiBoot.Properties.Resources.kingston;
                            //g.FillRectangle(Brushes.Red, 0, 0, size, size);
                            //g.DrawString("KINGSTON", font, Brushes.White, new Rectangle(0, 0, size, size), sf);
                            break;
                        case "Corsair":
                            ramBrandPictureBox.Image = OptiBoot.Properties.Resources.corsair;
                            //g.FillRectangle(Brushes.Yellow, 0, 0, size, size);
                            //g.DrawString("CORSAIR", font, Brushes.Black, new Rectangle(0, 0, size, size), sf);
                            break;
                        case "GSkill":
                            ramBrandPictureBox.Image = OptiBoot.Properties.Resources.gskill;
                            //g.FillRectangle(Brushes.Orange, 0, 0, size, size);
                            //g.DrawString("G.SKILL", font, Brushes.White, new Rectangle(0, 0, size, size), sf);
                            break;
                        case "Crucial":
                            ramBrandPictureBox.Image = OptiBoot.Properties.Resources.crucial;
                            //g.FillRectangle(Brushes.Purple, 0, 0, size, size);
                            //g.DrawString("CRUCIAL", font, Brushes.White, new Rectangle(0, 0, size, size), sf);
                            break;
                        case "Micron":
                            ramBrandPictureBox.Image = OptiBoot.Properties.Resources.micron;
                            //g.FillRectangle(Brushes.DarkBlue, 0, 0, size, size);
                            //g.DrawString("MICRON", font, Brushes.White, new Rectangle(0, 0, size, size), sf);
                            break;
                        case "TeamGroup":
                            ramBrandPictureBox.Image = OptiBoot.Properties.Resources.teamgroup;
                            //g.FillRectangle(Brushes.Green, 0, 0, size, size);
                            //g.DrawString("TEAM GROUP", font, Brushes.White, new Rectangle(0, 0, size, size), sf);
                            break;
                        case "Adata":
                            ramBrandPictureBox.Image = OptiBoot.Properties.Resources.Adata;
                            //g.FillRectangle(Brushes.Red, 0, 0, size, size);
                            //g.DrawString("ADATA", font, Brushes.White, new Rectangle(0, 0, size, size), sf);
                            break;
                        case "TwinMos":
                            ramBrandPictureBox.Image = OptiBoot.Properties.Resources.twinmos;
                            //g.FillRectangle(Brushes.Red, 0, 0, size, size);
                            //g.DrawString("TwinMos", font, Brushes.White, new Rectangle(0, 0, size, size), sf);
                            break;
                        default:
                            ramBrandPictureBox.Image = OptiBoot.Properties.Resources.DefaultGPU;
                            break;
                    }
                }
            }
            return bmp;
        }

        private void FormRAMInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormSelectScreen frmSelect = new FormSelectScreen();
            frmSelect.Show();
            this.Hide();
        }
    }
}

