using Mono.Unix;
using NAudio.CoreAudioApi;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace OptiBoot
{
    public partial class FormSoundAdapters : Form
    {
        public FormSoundAdapters()
        {
            InitializeComponent();
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


        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        private static extern uint waveInGetNumDevs();

        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        private static extern uint waveOutGetNumDevs();

        private List<MMDevice> tumSesAygıtları = new List<MMDevice>();

        private void FormSoundAdapters_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);

            if (Properties.Settings.Default.AlwaysTop == true)
            {
                this.TopMost = true;
            }
            LoadSoundDevices();
            LoadLanguage();

        }

        public class DeviceItem
        {
            public MMDevice Device { get; private set; }
            public string GörünenAd { get; private set; }

            public DeviceItem(MMDevice device, string görünenAd)
            {
                Device = device;
                GörünenAd = görünenAd;
            }

            public override string ToString()
            {
                return GörünenAd;
            }
        }
        public void LoadLanguage()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            this.Text = ReadIniValue("FormSoundAdapters", "caption", iniPath);
            labelFormName.Text = ReadIniValue("FormSoundAdapters", "caption", iniPath);
            labelSelectDevice.Text = ReadIniValue("FormSoundAdapters", "labelSelectDevice", iniPath);
            groupBoxGeneral.Text = ReadIniValue("FormSoundAdapters", "groupboxGeneralInfo", iniPath);
            groupBoxDriver.Text = ReadIniValue("FormSoundAdapters", "groupboxDriverInfo", iniPath);
            buttonExport.Text = ReadIniValue("FormSoundAdapters", "buttonExport", iniPath);
            buttonReload.Text = ReadIniValue("FormSoundAdapters", "buttonRefresh", iniPath);
        }
        private void LoadSoundDevices()
        {
            cihazComboBox.Items.Clear();
            tumSesAygıtları.Clear();

            try
            {
                var aygıtNumaralandırıcı = new MMDeviceEnumerator();
                var sesAygıtları = aygıtNumaralandırıcı.EnumerateAudioEndPoints(DataFlow.All, DeviceState.All);
                tumSesAygıtları = sesAygıtları.ToList();

                var fizikselCihazlar = FizikselSesCihazlarınıAl();

                foreach (var cihazAdı in fizikselCihazlar)
                {
                    var ilgiliAygıt = tumSesAygıtları.FirstOrDefault(a =>
                        a.FriendlyName.Contains(cihazAdı) || cihazAdı.Contains(a.FriendlyName));

                    if (ilgiliAygıt != null)
                    {
                        cihazComboBox.Items.Add(new DeviceItem(ilgiliAygıt, cihazAdı));
                    }
                }

                if (cihazComboBox.Items.Count > 0)
                {
                    cihazComboBox.SelectedIndex = 0;
                }
                else
                {
                    FiltreUygulaVeGoster();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ses aygıtları yüklenirken hata oluştu: {ex.Message}", ReadIniValue("Others", "msgHeaderError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FiltreUygulaVeGoster();
            }
        }



        private List<string> FizikselSesCihazlarınıAl()
        {
            var cihazListesi = new List<string>();

            try
            {
                var sorgu = new ObjectQuery("SELECT * FROM Win32_SoundDevice");
                var arayıcı = new ManagementObjectSearcher(sorgu);

                foreach (ManagementObject nesne in arayıcı.Get())
                {
                    string cihazAdı = nesne["ProductName"]?.ToString();
                    if (!string.IsNullOrEmpty(cihazAdı) && !cihazListesi.Contains(cihazAdı))
                    {
                        cihazListesi.Add(cihazAdı);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WMI sorgu hatası: {ex.Message}");
            }

            return cihazListesi;
        }

        private (string Sürüm, string Tarih, string Sağlayıcı) SürücüBilgileriniAl(string cihazAdı)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            try
            {
                var sorgu = new ObjectQuery("SELECT * FROM Win32_PnPSignedDriver WHERE DeviceName LIKE '%" + cihazAdı + "%'");
                var arayıcı = new ManagementObjectSearcher(sorgu);

                foreach (ManagementObject nesne in arayıcı.Get())
                {
                    string sürüm = nesne["DriverVersion"]?.ToString() ?? ReadIniValue("MemoryTypes", "unknown", iniPath);
                    string tarih = nesne["DriverDate"]?.ToString() ?? ReadIniValue("MemoryTypes", "unknown", iniPath);
                    string sağlayıcı = nesne["Manufacturer"]?.ToString() ?? ReadIniValue("MemoryTypes", "unknown", iniPath);

                    if (tarih != ReadIniValue("MemoryTypes", "unknown", iniPath) && tarih.Length >= 8)
                    {
                        try
                        {
                            string yıl = tarih.Substring(0, 4);
                            string ay = tarih.Substring(4, 2);
                            string gün = tarih.Substring(6, 2);
                            tarih = $"{gün}/{ay}/{yıl}";
                        }
                        catch
                        {
                            tarih = ReadIniValue("MemoryTypes", "unknown", iniPath);
                        }
                    }

                    return (sürüm, tarih, sağlayıcı);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sürücü bilgileri alınırken hata: {ex.Message}");
            }

            return (ReadIniValue("MemoryTypes", "unknown", iniPath), ReadIniValue("MemoryTypes", "unknown", iniPath), ReadIniValue("MemoryTypes", "unknown", iniPath));
        }

        private void FiltreUygulaVeGoster()
        {
            var anaCihazlar = new Dictionary<string, MMDevice>();

            foreach (var aygıt in tumSesAygıtları)
            {
                string cihazAdı = aygıt.FriendlyName;

                if (!cihazAdı.Contains("Output") &&
                    !cihazAdı.Contains("Input") &&
                    !cihazAdı.Contains("Girişi") &&
                    !cihazAdı.Contains("Çıkışı") &&
                    !cihazAdı.Contains("Digital Audio") &&
                    !cihazAdı.Contains("Headphones") &&
                    !cihazAdı.Contains("Hoparlör") &&
                    !cihazAdı.Contains("Microphone"))
                {
                    string anahtar = cihazAdı.Split('(')[0].Trim();
                    if (!anaCihazlar.ContainsKey(anahtar))
                    {
                        anaCihazlar.Add(anahtar, aygıt);
                    }
                }
            }

            foreach (var cihaz in anaCihazlar.Values)
            {
                cihazComboBox.Items.Add(new DeviceItem(cihaz, cihaz.FriendlyName));
            }

            if (cihazComboBox.Items.Count == 0 && tumSesAygıtları.Count > 0)
            {
                cihazComboBox.Items.Add(new DeviceItem(tumSesAygıtları[0], tumSesAygıtları[0].FriendlyName));
            }
        }

        private void cihazComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            if (cihazComboBox.SelectedItem is DeviceItem seçiliAygıtItem)
            {
                var seçiliAygıt = seçiliAygıtItem.Device;

                try
                {
                    var sürücüBilgileri = SürücüBilgileriniAl(seçiliAygıt.FriendlyName);


                    labelNameGeneral.Text = ReadIniValue("FormSoundAdapters", "labelGenName", iniPath) + seçiliAygıt.FriendlyName;
                    labelDeviceIDGeneral.Text = ReadIniValue("FormSoundAdapters", "labelHardwareID", iniPath) + seçiliAygıt.ID;

                    var idParts = seçiliAygıt.ID.Split('\\');
                    labelManufIDGeneral.Text = idParts.Length > 1 ? idParts[1] : ReadIniValue("FormSoundAdapters", "labelManufacturerID", iniPath) +ReadIniValue("MemoryTypes", "unknown", iniPath);

                    string ürünId = ReadIniValue("MemoryTypes", "unknown", iniPath);
                    int guidIndex = seçiliAygıt.ID.IndexOf('{');
                    if (guidIndex > 0)
                    {
                        ürünId = seçiliAygıt.ID.Substring(guidIndex);
                    }
                    labelProductIDGeneral.Text = ReadIniValue("FormSoundAdapters", "labelProductID", iniPath) + ürünId;

                    labelTypeGeneral.Text = ReadIniValue("FormSoundAdapters", "labelType", iniPath) + seçiliAygıt.DataFlow.ToString();
                    labelDefaultDeviceGeneral.Text = seçiliAygıt.State == DeviceState.Active ? ReadIniValue("Others", "default", iniPath) + ReadIniValue("Others", "yes", iniPath) : ReadIniValue("Others", "default", iniPath) + ReadIniValue("Others", "no", iniPath);
                    labelDriverName.Text = ReadIniValue("FormSoundAdapters", "labelGenName", iniPath) + seçiliAygıt.FriendlyName;
                    labelDriverVersion.Text = ReadIniValue("FormSoundAdapters", "labelDriverVersion", iniPath) + sürücüBilgileri.Sürüm;
                    labelDriverDate.Text = ReadIniValue("FormSoundAdapters", "labelDriverDate", iniPath) + sürücüBilgileri.Tarih;
                    labelDriverProvider.Text = ReadIniValue("FormSoundAdapters", "labelDriverProvider", iniPath) + sürücüBilgileri.Sağlayıcı;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ReadIniValue("FormSoundAdapters", "msgErrorLoadingDevices", iniPath)} {ex.Message}", ReadIniValue("Others", "msgHeaderError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void buttonExport_Click(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            
            if (cihazComboBox.SelectedItem is DeviceItem seçiliAygıtItem)
            {
                var seçiliAygıt = seçiliAygıtItem.Device;

                var sürücüBilgileri = SürücüBilgileriniAl(seçiliAygıt.FriendlyName);

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text File|*.txt";
                saveFileDialog.Title = "Ses Aygıtı Bilgilerini Kaydet";

                string temizDosyaAdı = new string(seçiliAygıt.FriendlyName
                    .Where(c => !Path.GetInvalidFileNameChars().Contains(c))
                    .ToArray());

                saveFileDialog.FileName = $"{temizDosyaAdı}_Info.txt";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var idParts = seçiliAygıt.ID.Split('\\');
                        string ürünId = "Bilinmiyor";
                        int guidIndex = seçiliAygıt.ID.IndexOf('{');
                        if (guidIndex > 0)
                        {
                            ürünId = seçiliAygıt.ID.Substring(guidIndex);
                        }

                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            writer.WriteLine(ReadIniValue("FormSoundAdapters", "exportText1", iniPath));
                            writer.WriteLine();
                            writer.WriteLine($"{ReadIniValue("FormSoundAdapters", "exportText2", iniPath)} {seçiliAygıt.FriendlyName}**");
                            writer.WriteLine();
                            writer.WriteLine("---");
                            writer.WriteLine();
                            writer.WriteLine(ReadIniValue("FormSoundAdapters", "exportText3", iniPath));
                            writer.WriteLine($"{ReadIniValue("FormSoundAdapters", "exportText4", iniPath)} {seçiliAygıt.FriendlyName}");
                            writer.WriteLine($"{ReadIniValue("FormSoundAdapters", "exportText5", iniPath)} {seçiliAygıt.ID}");
                            writer.WriteLine($"{ReadIniValue("FormSoundAdapters", "exportText6", iniPath)} {(idParts.Length > 1 ? idParts[1] : "Bilinmiyor")}");
                            writer.WriteLine($"{ReadIniValue("FormSoundAdapters", "exportText7", iniPath)} {ürünId}");
                            writer.WriteLine($"{ReadIniValue("FormSoundAdapters", "exportText8", iniPath)} {seçiliAygıt.DataFlow}");
                            writer.WriteLine($"{ReadIniValue("FormSoundAdapters", "exportText9", iniPath)} {(seçiliAygıt.State == DeviceState.Active ? "Evet" : "Hayır")}");
                            writer.WriteLine();
                            writer.WriteLine("---");
                            writer.WriteLine();
                            writer.WriteLine(ReadIniValue("FormSoundAdapters", "exportText10", iniPath));
                            writer.WriteLine($"{ReadIniValue("FormSoundAdapters", "exportText11", iniPath)}{seçiliAygıt.FriendlyName}");
                            writer.WriteLine($"{ReadIniValue("FormSoundAdapters", "exportText12", iniPath)}{sürücüBilgileri.Sürüm}");
                            writer.WriteLine($"{ReadIniValue("FormSoundAdapters", "exportText13", iniPath)}{sürücüBilgileri.Tarih}");
                            writer.WriteLine($"{ReadIniValue("FormSoundAdapters", "exportText14", iniPath)}{sürücüBilgileri.Sağlayıcı}");
                        }

                        MessageBox.Show(ReadIniValue("FormSoundAdapters", "msgExportSuccess", iniPath), ReadIniValue("FormServices", "msgSuccess", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LogManager.Logla("Ses aygıtları listesi dışa aktarıldı:" + saveFileDialog.FileName + " " + DateTime.Now);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Dosya yazılırken hata oluştu: {ex.Message}", ReadIniValue("Others", "msgHeaderError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LogManager.Logla("Ses aygıtları listesi dışa aktarılamadı:" + saveFileDialog.FileName + " " + DateTime.Now);

                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir ses aygıtı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            LoadSoundDevices();

        }

        private void FormSoundAdapters_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormSelectScreen frmSelect = new FormSelectScreen();
            frmSelect.Show();
            this.Hide();
        }
    }
}
