using Mono.Unix;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptiBoot
{
    public partial class FormGPUInfo : Form
    {
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

        public class GPUInfo
        {

            public string Name { get; set; }
            public string Description { get; set; }
            public string AdapterRAM { get; set; }
            public string DriverVersion { get; set; }
            public string VideoProcessor { get; set; }
            public string VideoArchitecture { get; set; }
            public string VideoMemoryType { get; set; }
            public string Status { get; set; }
            public string Availability { get; set; }
            public string InstalledDisplayDrivers { get; set; }
            public string DriverDate { get; set; }
            public bool IsExternal { get; set; }
            public string Manufacturer { get; set; }
            public string Temperature { get; set; }
            public string CoreClock { get; set; }
            public string MemoryClock { get; set; }
            public string BIOSVersion { get; set; }
            public string MemoryBandwidth { get; set; }
            public string OpenCLSupport { get; set; }
            public string CUDASupport { get; set; }
            public string DirectComputeSupport { get; set; }
            public string DirectMLSupport { get; set; }
            public string VulkanSupport { get; set; }
            public string RayTracingSupport { get; set; }
            public string PhysXSupport { get; set; }
            public string OpenGLSupport { get; set; }
            public string Utilization { get; set; }
            public string PowerUsage { get; set; }
        }

        public class GPUService
        {
            public List<GPUInfo> GetGPUInformation()
            {
                var gpus = new List<GPUInfo>();

                try
                {
                    using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController"))
                    {
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            var gpu = new GPUInfo
                            {
                                Name = obj["Name"]?.ToString() ?? "Bilinmiyor",
                                Description = obj["Description"]?.ToString() ?? "Bilinmiyor",
                                AdapterRAM = obj["AdapterRAM"] != null ?
                                    FormatBytes(Convert.ToUInt64(obj["AdapterRAM"])) : "Bilinmiyor",
                                DriverVersion = obj["DriverVersion"]?.ToString() ?? "Bilinmiyor",
                                VideoProcessor = obj["VideoProcessor"]?.ToString() ?? "Bilinmiyor",
                                VideoArchitecture = GetVideoArchitecture(Convert.ToInt16(obj["VideoArchitecture"] ?? 0)),
                                VideoMemoryType = GetVideoMemoryType(Convert.ToInt16(obj["VideoMemoryType"] ?? 0)),
                                Status = obj["Status"]?.ToString() ?? "Bilinmiyor",
                                Availability = GetAvailability(Convert.ToInt16(obj["Availability"] ?? 0)),
                                InstalledDisplayDrivers = obj["InstalledDisplayDrivers"]?.ToString() ?? "Bilinmiyor",
                                DriverDate = obj["DriverDate"]?.ToString() ?? "Bilinmiyor"
                            };

                            gpu.IsExternal = !string.IsNullOrEmpty(gpu.Name) &&
                                           (gpu.Name.Contains("USB") || gpu.Name.Contains("External") ||
                                            gpu.Name.Contains("Harici"));

                            gpu.Manufacturer = GetManufacturer(gpu.Name);

                            GetAdvancedGPUInfo(gpu);
                            GetGPUTechnologies(gpu);

                            gpus.Add(gpu);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"GPU bilgileri alınırken hata: {ex.Message}");
                }

                return gpus;
            }

            private void GetAdvancedGPUInfo(GPUInfo gpu)
            {
                try
                {
                    using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PerfFormattedData_Counters_ThermalZoneInformation"))
                    {
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            if (obj["Temperature"] != null)
                            {
                                gpu.Temperature = $"{Convert.ToInt32(obj["Temperature"]) - 273}°C"; // Kelvin'den Celsius'a
                                break;
                            }
                        }
                    }

                    using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController WHERE Name LIKE '%" + gpu.Name + "%'"))
                    {
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            gpu.BIOSVersion = obj["AdapterDACType"]?.ToString() ?? "Bilinmiyor";
                        }
                    }

                    GetNVIDIAInfo(gpu);
                    GetAMDInfo(gpu);
                    GetIntelInfo(gpu);

                }
                catch
                {
                    gpu.Temperature = "Bilinmiyor";
                    gpu.CoreClock = "Bilinmiyor";
                    gpu.MemoryClock = "Bilinmiyor";
                    gpu.BIOSVersion = "Bilinmiyor";
                    gpu.MemoryBandwidth = "Bilinmiyor";
                }
            }

            private void GetAMDInfo(GPUInfo gpu)
            {
                if (!gpu.Name.ToLower().Contains("amd") && !gpu.Name.ToLower().Contains("radeon")) return;
            }

            private void GetIntelInfo(GPUInfo gpu)
            {
                if (!gpu.Name.ToLower().Contains("intel")) return;
            }

            private void GetGPUTechnologies(GPUInfo gpu)
            {             
                string gpuName = gpu.Name.ToLower();

                gpu.OpenCLSupport = CheckTechnologySupport(gpuName, "opencl");
                gpu.CUDASupport = CheckTechnologySupport(gpuName, "cuda");
                gpu.DirectComputeSupport = "Evet";
                gpu.DirectMLSupport = CheckDirectMLSupport(gpuName);
                gpu.VulkanSupport = CheckVulkanSupport(gpuName);
                gpu.RayTracingSupport = CheckRayTracingSupport(gpuName);
                gpu.PhysXSupport = CheckPhysXSupport(gpuName);
                gpu.OpenGLSupport = "Evet"; 
            }

            private string CheckTechnologySupport(string gpuName, string technology)
            {
                if (gpuName.Contains("nvidia") || gpuName.Contains("geforce"))
                {
                    return technology switch
                    {
                        "opencl" => "Evet",
                        "cuda" => "Evet",
                        "vulkan" => "Evet",
                        "raytracing" => gpuName.Contains("rtx") || gpuName.Contains("20") ||
                                       gpuName.Contains("30") || gpuName.Contains("40") ? "Evet" : "Hayır",
                        "physx" => "Evet",
                        "directml" => "Evet",
                        _ => "Evet"
                    };
                }
                else if (gpuName.Contains("amd") || gpuName.Contains("radeon"))
                {
                    return technology switch
                    {
                        "opencl" => "Evet",
                        "cuda" => "Hayır",
                        "vulkan" => "Evet",
                        "raytracing" => gpuName.Contains("rx 6") || gpuName.Contains("rx 7") ? "Evet" : "Hayır",
                        "physx" => "Hayır",
                        "directml" => "Evet",
                        _ => "Evet"
                    };
                }
                else if (gpuName.Contains("intel"))
                {
                    return technology switch
                    {
                        "opencl" => "Evet",
                        "cuda" => "Hayır",
                        "vulkan" => "Evet",
                        "raytracing" => gpuName.Contains("arc") ? "Evet" : "Hayır",
                        "physx" => "Hayır",
                        "directml" => "Evet",
                        _ => "Evet"
                    };
                }

                return "Bilinmiyor";
            }

            private string CheckDirectMLSupport(string gpuName)
            {
                if (Properties.Settings.Default.language=="Turkish")
                {
                    return (gpuName.Contains("nvidia") || gpuName.Contains("amd") ||
                     gpuName.Contains("intel") || gpuName.Contains("radeon")) ? "Evet" : "Bilinmiyor";
                }
                else
                {
                    return (gpuName.Contains("nvidia") || gpuName.Contains("amd") ||
                    gpuName.Contains("intel") || gpuName.Contains("radeon")) ? "Yes" : "Unknown";
                }
               
            }

            private string CheckVulkanSupport(string gpuName)
            {
                if (Properties.Settings.Default.language == "Turkish")
                {
                    return (gpuName.Contains("nvidia") || gpuName.Contains("amd") ||
                     gpuName.Contains("intel") || gpuName.Contains("radeon")) ? "Evet" : "Hayır";
                }
                else
                {
                    return (gpuName.Contains("nvidia") || gpuName.Contains("amd") ||
                           gpuName.Contains("intel") || gpuName.Contains("radeon")) ? "Yes" : "No";
                }

            }

            private string CheckRayTracingSupport(string gpuName)
            {
                if (Properties.Settings.Default.language == "Turkish")
                {
                    if (gpuName.Contains("nvidia"))
                        return gpuName.Contains("rtx") || gpuName.Contains("20") ||
                               gpuName.Contains("30") || gpuName.Contains("40") ? "Evet" : "Hayır";

                    if (gpuName.Contains("amd"))
                        return gpuName.Contains("rx 6") || gpuName.Contains("rx 7") ? "Evet" : "Hayır";

                    if (gpuName.Contains("intel"))
                        return gpuName.Contains("arc") ? "Evet" : "Hayır";

                    return "Hayır";
                }
                else
                {
                    if (gpuName.Contains("nvidia"))
                        return gpuName.Contains("rtx") || gpuName.Contains("20") ||
                               gpuName.Contains("30") || gpuName.Contains("40") ? "Yes" : "No";

                    if (gpuName.Contains("amd"))
                        return gpuName.Contains("rx 6") || gpuName.Contains("rx 7") ? "Yes" : "No";

                    if (gpuName.Contains("intel"))
                        return gpuName.Contains("arc") ? "Yes" : "No";

                    return "No";
                }
               
            }

            private string CheckPhysXSupport(string gpuName)
            {
                if (Properties.Settings.Default.language == "Turkish")
                {
                    return gpuName.Contains("nvidia") ? "Evet" : "Hayır";
                }
                else
                {
                    return gpuName.Contains("nvidia") ? "Yes" : "No";
                }
               
            }

            private void GetNVIDIAInfo(GPUInfo gpu)
            {
                if (!gpu.Name.ToLower().Contains("nvidia")) return;

                try
                {
                    var process = new System.Diagnostics.Process
                    {
                        StartInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = "nvidia-smi",
                            Arguments = "--query-gpu=temperature.gpu,clocks.gr,clocks.mem --format=csv,noheader,nounits",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };

                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    var lines = output.Split('\n');
                    if (lines.Length > 0)
                    {
                        var values = lines[0].Split(',');
                        if (values.Length >= 3)
                        {
                            gpu.Temperature = $"{values[0].Trim()}°C";
                            gpu.CoreClock = $"{values[1].Trim()} MHz";
                            gpu.MemoryClock = $"{values[2].Trim()} MHz";
                        }
                    }
                }
                catch
                {
                }
            }

            public string GetManufacturer(string gpuName)
            {
                if (string.IsNullOrEmpty(gpuName))
                    return "Bilinmiyor";

                gpuName = gpuName.ToLower();

                if (gpuName.Contains("nvidia") || gpuName.Contains("geforce"))
                    return "NVIDIA";
                else if (gpuName.Contains("amd") || gpuName.Contains("radeon"))
                    return "AMD";
                else if (gpuName.Contains("intel") || gpuName.Contains("hd graphics") || gpuName.Contains("iris"))
                    return "Intel";
                else if (gpuName.Contains("microsoft") || gpuName.Contains("basic display"))
                    return "Microsoft";

                return "Diğer";
            }

            private string FormatBytes(ulong bytes)
            {
                string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
                int counter = 0;
                decimal number = bytes;

                while (Math.Round(number / 1024) >= 1)
                {
                    number /= 1024;
                    counter++;
                }

                return $"{number:n2} {suffixes[counter]}";
            }

            private string GetVideoArchitecture(short architecture)
            {
                if (Properties.Settings.Default.language == "Turkish")
                {
                    return architecture switch
                    {
                        1 => "Diğer",
                        2 => "Bilinmiyor",
                        3 => "CGA",
                        4 => "EGA",
                        5 => "VGA",
                        6 => "SVGA",
                        7 => "MDA",
                        8 => "HGC",
                        9 => "MCGA",
                        10 => "8514A",
                        11 => "XGA",
                        12 => "Linear Frame Buffer",
                        13 => "PC-98",
                        _ => "Bilinmiyor"
                    };
                }
                else
                {
                    return architecture switch
                    {
                        1 => "Other",
                        2 => "Unknown",
                        3 => "CGA",
                        4 => "EGA",
                        5 => "VGA",
                        6 => "SVGA",
                        7 => "MDA",
                        8 => "HGC",
                        9 => "MCGA",
                        10 => "8514A",
                        11 => "XGA",
                        12 => "Linear Frame Buffer",
                        13 => "PC-98",
                        _ => "Unknown"
                    };
                }
            
            }

            private string GetVideoMemoryType(short memoryType)
            {
                if (Properties.Settings.Default.language == "Turkish")
                {
                    return memoryType switch
                    {
                        1 => "Diğer",
                        2 => "Bilinmiyor",
                        3 => "VRAM",
                        4 => "DRAM",
                        5 => "SRAM",
                        6 => "WRAM",
                        7 => "EDO RAM",
                        8 => "Burst Synchronous DRAM",
                        9 => "Pipelined Burst SRAM",
                        10 => "CDRAM",
                        11 => "3DRAM",
                        12 => "SDRAM",
                        13 => "SGRAM",
                        _ => "Bilinmiyor"
                    };
                }
                else
                {
                    return memoryType switch
                    {
                        1 => "Other",
                        2 => "Unknown",
                        3 => "VRAM",
                        4 => "DRAM",
                        5 => "SRAM",
                        6 => "WRAM",
                        7 => "EDO RAM",
                        8 => "Burst Synchronous DRAM",
                        9 => "Pipelined Burst SRAM",
                        10 => "CDRAM",
                        11 => "3DRAM",
                        12 => "SDRAM",
                        13 => "SGRAM",
                        _ => "Unknown"
                    };

                }
             
            }

            private string GetAvailability(short availability)
            {
                if(Properties.Settings.Default.language=="Turkish")
                {
                    return availability switch
                    {
                        1 => "Diğer",
                        2 => "Bilinmiyor",
                        3 => "Çalışıyor/Tam Güç",
                        4 => "Uyarı",
                        5 => "Test Modunda",
                        6 => "Uygulanamaz",
                        7 => "Güç Kapalı",
                        8 => "Çevrimdışı",
                        9 => "Görev Dışı",
                        10 => "Düşük Performans",
                        11 => "Yüklü Değil",
                        12 => "Kurulum Hatası",
                        13 => "Güç Tasarrufu - Bilinmiyor",
                        14 => "Güç Tasarrufu - Düşük Güç",
                        15 => "Güç Tasarrufu - Bekleme",
                        16 => "Güç Döngüsü",
                        17 => "Güç Tasarrufu - Uyarı",
                        18 => "Duraklatıldı",
                        19 => "Hazır Değil",
                        20 => "Yapılandırılmamış",
                        21 => "Askıya Alındı",
                        _ => "Bilinmiyor"
                    };

                }
                else
                {
                    return availability switch
                    {
                        1 => "Other",
                        2 => "Unknown",
                        3 => "Working/Full Power",
                        4 => "Warning",
                        5 => "In Test Mode",
                        6 => "Not Applicable",
                        7 => "Power Off",
                        8 => "Offline",
                        9 => "Out of Service",
                        10 => "Low Performance",
                        11 => "Not Installed",
                        12 => "Installation Error",
                        13 => "Power Save - Unknown",
                        14 => "Power Save - Low Power",
                        15 => "Power Save - Standby",
                        16 => "Power Cycle",
                        17 => "Power Save - Warning",
                        18 => "Paused",
                        19 => "Not Ready",
                        20 => "Unconfigured",
                        21 => "Suspended",
                        _ => "Unknown"
                    };

                }

            }
        }

        public class LogoService
        {
            private Dictionary<string, string> gpuLogos = new Dictionary<string, string>
            {
                {"NVIDIA", "https://www.nvidia.com/content/dam/en-zz/Solutions/about-nvidia/logo-and-brand/01-nvidia-logo-vert-500x200-2c50-p@2x.png"},
                {"AMD", "https://www.amd.com/themes/custom/amd/logo.png"},
                {"Intel", "https://www.intel.com/content/dam/www/global/brand/intel-new-logo-blue.png"},
                {"Microsoft", "https://upload.wikimedia.org/wikipedia/commons/thumb/9/96/Microsoft_logo_%282012%29.svg/2560px-Microsoft_logo_%282012%29.svg.png"}
            };

            public async Task<Image> GetLogoAsync(string manufacturer)
            {
                try
                {
                    if (gpuLogos.ContainsKey(manufacturer))
                    {
                        using (var httpClient = new HttpClient())
                        {
                            httpClient.Timeout = TimeSpan.FromSeconds(10);
                            var imageBytes = await httpClient.GetByteArrayAsync(gpuLogos[manufacturer]);
                            using (var ms = new MemoryStream(imageBytes))
                            {
                                return Image.FromStream(ms);
                            }
                        }
                    }
                }
                catch
                {
                }

                return null;
            }

            public string GetLogoUrl(string manufacturer)
            {
                return gpuLogos.ContainsKey(manufacturer) ? gpuLogos[manufacturer] : null;
            }
        }

        private GPUService gpuService;
        private LogoService logoService;
        private List<GPUInfo> gpuList;
        private int currentGPUIndex = 0;

        public FormGPUInfo()
        {
            InitializeComponent();
            gpuService = new GPUService();
            logoService = new LogoService();
            picLogo.BackColor = Color.Transparent;
            LoadGPUInfo();
        }

        private async void LoadGPUInfo()
        {
            try
            {
                gpuList = gpuService.GetGPUInformation();
                currentGPUIndex = 0;

                if (gpuList.Count > 0)
                {
                    DisplayGPUInfo(gpuList[currentGPUIndex]);
                    await LoadLogo(gpuList[currentGPUIndex].Manufacturer);
                    UpdateNavigationButtons();
                }
                else
                {
                    MessageBox.Show("GPU bulunamadı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private async void ShowNextGPU()
        {
            if (gpuList == null || gpuList.Count == 0)
                return;

            currentGPUIndex++;
            if (currentGPUIndex >= gpuList.Count)
            {
                currentGPUIndex = 0;
            }

            DisplayGPUInfo(gpuList[currentGPUIndex]);
            await LoadLogo(gpuList[currentGPUIndex].Manufacturer);
            UpdateNavigationButtons();
        }

        private async void ShowPreviousGPU()
        {
            if (gpuList == null || gpuList.Count == 0)
                return;

            currentGPUIndex--;
            if (currentGPUIndex < 0)
            {
                currentGPUIndex = gpuList.Count - 1;
            }

            DisplayGPUInfo(gpuList[currentGPUIndex]);
            await LoadLogo(gpuList[currentGPUIndex].Manufacturer);
            UpdateNavigationButtons();
        }

        private void UpdateNavigationButtons()
        {
            if (gpuList == null || gpuList.Count <= 1)
            {
                btnNextGPU2.Enabled = false;
                btnPreviousGPU2.Enabled = false;
            }
            else
            {
                btnNextGPU2.Enabled = true;
                btnPreviousGPU2.Enabled = true;
            }
        }

        private void DisplayGPUInfo(GPUInfo gpu)
        {

            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            string gpuexternal = ReadIniValue("Others", "gpuInternal", iniPath);
            string gpuinternal = ReadIniValue("Others", "gpuExternal", iniPath);

            lblGPUName.Text = $" {ReadIniValue("FormGPUInfo", "labelModel", iniPath)} {gpu.Name}";
            lblGPUDescription.Text = $"{ReadIniValue("FormGPUInfo", "labelDesc", iniPath)}{gpu.Description}";
            lblGPUMemory.Text = $"{ReadIniValue("FormGPUInfo", "labelMemSize", iniPath)} {gpu.AdapterRAM}";
            lblGPUDriver.Text = $"{ReadIniValue("FormGPUInfo", "labelDriverVersion", iniPath)}{gpu.DriverVersion}";
            lblGPUProcessor.Text = $"{ReadIniValue("FormGPUInfo", "labelVideoCPU", iniPath)}{gpu.VideoProcessor}";
            lblGPUArchitecture.Text = $"{ReadIniValue("FormGPUInfo", "labelArch", iniPath)}{gpu.VideoArchitecture}";
            lblGPUMemoryType.Text = $"{ReadIniValue("FormGPUInfo", "labelMemType", iniPath)}{gpu.VideoMemoryType}";
            lblGPUStatus.Text = $"{ReadIniValue("FormGPUInfo", "labelStatusInfo", iniPath)}{gpu.Status}";
            lblGPUAvailability.Text = $"{ReadIniValue("FormGPUInfo", "labelAvailable", iniPath)}{gpu.Availability}";
            lblGPUDriverDate.Text = $"{ReadIniValue("FormGPUInfo", "labelDriverDate", iniPath)}{gpu.DriverDate}";
            lblGPUType.Text = $"{ReadIniValue("FormGPUInfo", "labelGPUType", iniPath)}{(gpu.IsExternal ? gpuexternal : gpuinternal)}";
            lblGPUManufacturer.Text = $"{ReadIniValue("FormGPUInfo", "labelGPUManufacturer", iniPath)}{gpu.Manufacturer}";

            lblGPUTemperature.Text = $"{ReadIniValue("FormGPUInfo", "labelTemp", iniPath)} {gpu.Temperature}";
            lblGPUCoreClock.Text = $"{ReadIniValue("FormGPUInfo", "labelCoreClockSpeed", iniPath)} {gpu.CoreClock}";
            lblGPUMemoryClock.Text = $"{ReadIniValue("FormGPUInfo", "labelMemSpeed", iniPath)}{gpu.MemoryClock}";
            lblGPUBIOSVersion.Text = $"{ReadIniValue("FormGPUInfo", "labelBIOSVersion", iniPath)}  {gpu.BIOSVersion}";
            lblGPUMemoryBandwidth.Text = $"{ReadIniValue("FormGPUInfo", "labelMemBandwith", iniPath)}  {gpu.MemoryBandwidth}";
            lblGPUOpenCL.Text = $"OpenCL: {gpu.OpenCLSupport}";
            lblGPUCUDA.Text = $"CUDA: {gpu.CUDASupport}";
            lblGPUDirectCompute.Text = $"DirectCompute: {gpu.DirectComputeSupport}";
            lblGPUDirectML.Text = $"DirectML: {gpu.DirectMLSupport}";
            lblGPUVulkan.Text = $"Vulkan: {gpu.VulkanSupport}";
            lblGPURayTracing.Text = $"Ray Tracing: {gpu.RayTracingSupport}";
            lblGPUPhysX.Text = $"PhysX: {gpu.PhysXSupport}";
            lblGPUOpenGL.Text = $"OpenGL: {gpu.OpenGLSupport}";
        }

        private async Task LoadLogo(string manufacturer)
        {
            try
            {
                var logo = await logoService.GetLogoAsync(manufacturer);
                if (logo != null)
                {
                    picLogo.Image = logo;
                }
                else
                {
                    picLogo.Image = Properties.Resources.DefaultGPU;
                }
            }
            catch
            {
                picLogo.Image = Properties.Resources.DefaultGPU;
            }
        }

        private void btnNextGPU_Click(object sender, EventArgs e)
        {
            ShowNextGPU();
        }

        private void btnPreviousGPU_Click(object sender, EventArgs e)
        {
            ShowPreviousGPU();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGPUInfo();
        }

        private void FormGPUInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormSelectScreen frmSelect = new FormSelectScreen();
            frmSelect.Show();
            this.Hide();
        }

        private void FormGPUInfo_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);

            if (Properties.Settings.Default.AlwaysTop == true)
            {
                this.TopMost = true;
            }

            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            this.Text = ReadIniValue("FormGPUInfo", "caption", iniPath);
            labelGPUInfoCaption.Text = ReadIniValue("FormGPUInfo", "labelGPUInfo", iniPath);
            lblGPUUtilization.Text = ReadIniValue("FormGPUInfo", "labelGPUUsage", iniPath);
            lblGPUPowerUsage.Text = ReadIniValue("FormGPUInfo", "labelPowerManagement", iniPath);
        }
    }
}