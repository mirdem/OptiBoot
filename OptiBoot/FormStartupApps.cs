using Microsoft.Win32;
using Mono.Unix;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;


namespace OptiBoot
{
    public partial class FormStartupApps : Form
    {
        public string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

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

        Dictionary<string, string> startupPrograms = new Dictionary<string, string>();
        private Dictionary<string, string> programPaths = new Dictionary<string, string>();
        private Dictionary<string, StartupItem> startupItems = new Dictionary<string, StartupItem>();


        private ContextMenuStrip contextMenuStartup;
        private ToolStripMenuItem menuDelete;
        private ToolStripMenuItem menuOpenLocation;
        private ToolStripMenuItem menuBackup;
        private ToolStripMenuItem menuExport;

        private void InitializeContextMenu()
        {
           
            // ContextMenuStrip oluştur
            contextMenuStartup = new ContextMenuStrip();

            // Menü öğelerini oluştur
            menuDelete = new ToolStripMenuItem();
            menuOpenLocation = new ToolStripMenuItem();
            menuBackup = new ToolStripMenuItem();
            menuExport = new ToolStripMenuItem();

            // Menü öğelerini ayarla
            menuDelete.Text = ReadIniValue("FormStartupApps", "ContextMenuDelete", iniPath);
            menuOpenLocation.Text = ReadIniValue("FormStartupApps", "ContextMenuOpenLocation", iniPath);
            menuBackup.Text = ReadIniValue("FormStartupApps", "ContextMenuBackup", iniPath);
            menuExport.Text = ReadIniValue("FormStartupApps", "ContextMenuExport", iniPath);

            // Event'leri bağla
            menuDelete.Click += MenuDelete_Click;
            menuOpenLocation.Click += MenuOpenLocation_Click;
            menuBackup.Click += MenuBackup_Click;
            menuExport.Click += MenuExport_Click;

            // Menü öğelerini ContextMenuStrip'e ekle
        contextMenuStartup.Items.AddRange(new ToolStripItem[] {
        menuDelete,
        menuOpenLocation,
        new ToolStripSeparator(), // Ayraç ekle
        menuBackup,
        menuExport
    });

            // ListBox'a ContextMenuStrip'i ata
            listBox1.ContextMenuStrip = contextMenuStartup;

            // MouseDown event'ini bağla
            listBox1.MouseDown += listBox1_MouseDown;
        }
        public class StartupItem
        {
            public string Name { get; set; }      // Programın gerçek adı
            public string Path { get; set; }      // Tam exe yolu
            public RegistryKey RootKey { get; set; }
            public string SubKeyPath { get; set; }
            public bool Enabled { get; set; }     // Devre dışı bırakılmış mı?
        }

        public void LoadLanguage()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            this.Text = ReadIniValue("FormStartupApps", "caption", iniPath);
            labelStartupAppsHeader.Text = ReadIniValue("FormStartupApps", "labelStartupApps", iniPath);
            btnSil.Text = ReadIniValue("FormStartupApps", "buttonDelete", iniPath);
            btnOpenLocation.Text = ReadIniValue("FormStartupApps", "buttonOpenLocation", iniPath);
            btnExportToTXT.Text = ReadIniValue("FormStartupApps", "buttonExport", iniPath);
            btnBackupREGFile.Text = ReadIniValue("FormStartupApps", "buttonBackup", iniPath);
        }
        public FormStartupApps()
        {
            InitializeComponent();
        }
        private void ListeleTumStartupOgeleri()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
         
            listBox1.Items.Clear();
            programPaths.Clear();


            // Registry konumları
            string[] registryPaths = new string[]
             {
                @"Software\Microsoft\Windows\CurrentVersion\Run",
                @"Software\Microsoft\Windows\CurrentVersion\RunOnce",
                @"Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Run"
             };

            // HKCU
            foreach (var path in registryPaths)
            {
                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(path))
                {
                    if (key != null)
                        foreach (var name in key.GetValueNames())
                            AddProgram(name, key.GetValue(name)?.ToString());
                }
            }

            // HKLM
            foreach (var path in registryPaths)
            {
                using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(path))
                {
                    if (key != null)
                        foreach (var name in key.GetValueNames())
                            AddProgram(name, key.GetValue(name)?.ToString());
                }
            }

            // Startup klasörleri
            EkleStartupKlasor(Environment.GetFolderPath(Environment.SpecialFolder.Startup));
            EkleStartupKlasor(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup));
            lblCountApps.Text = ReadIniValue("FormStartupApps", "labelCountApps1", iniPath) +" "+ listBox1.Items.Count.ToString() +" "+ ReadIniValue("FormStartupApps", "labelCountApps2", iniPath);
        }


        private void AddProgram(string name, string value)
        {
            if (!listBox1.Items.Contains(name))
            {
                listBox1.Items.Add(name);
                programPaths[name] = ExtractExePath(value);
            }
        }

        private void EkleStartupKlasor(string path)
        {
            if (!Directory.Exists(path)) return;

            foreach (var file in Directory.GetFiles(path))
            {
                string name = Path.GetFileNameWithoutExtension(file);
                string realPath = file;

                if (Path.GetExtension(file).ToLower() == ".lnk")
                    realPath = ResolveShortcut(file);

                if (!listBox1.Items.Contains(name))
                {
                    listBox1.Items.Add(name);
                    programPaths[name] = realPath;
                }
            }
        }
        private string ExtractExePath(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            value = value.Trim();

            if (value.StartsWith("\""))
            {
                int endQuote = value.IndexOf("\"", 1);
                if (endQuote > 1) return value.Substring(1, endQuote - 1);
            }

            int exeIndex = value.IndexOf(".exe", StringComparison.OrdinalIgnoreCase);
            if (exeIndex > 0) return value.Substring(0, exeIndex + 4);

            return value;
        }

        // .lnk dosyasını çözmek için COM kullanımı (Shell32)
        private string ResolveShortcut(string shortcutPath)
        {
            try
            {
                Type shellType = Type.GetTypeFromProgID("WScript.Shell");
                dynamic shell = Activator.CreateInstance(shellType);
                dynamic lnk = shell.CreateShortcut(shortcutPath);
                string targetPath = lnk.TargetPath;
                Marshal.ReleaseComObject(lnk);
                Marshal.ReleaseComObject(shell);
                return targetPath;
            }
            catch
            {
                return shortcutPath;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            if (listBox1.SelectedItem != null)
            {
                string name = listBox1.SelectedItem.ToString();
                lblAppName.Text = ReadIniValue("FormStartupApps", "labelSelectedApp", iniPath) +" "+ name;
                lblAppLocation.Text = programPaths.ContainsKey(name) ? programPaths[name] : ReadIniValue("FormStartupApps", "labelAppLocationError", iniPath);
                btnBackupREGFile.Enabled = true;
                btnExportToTXT.Enabled = true;
                btnOpenLocation.Enabled = true;
                btnSil.Enabled = true;
            }
        }




        private void FormStartupApps_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);

            if (Properties.Settings.Default.AlwaysTop == true)
            {
                this.TopMost = true;
            }
            LoadLanguage();
            InitializeContextMenu();
            ListeleTumStartupOgeleri();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show(ReadIniValue("FormStartupApps", "msgPlsSelectApp", iniPath), ReadIniValue("FormStartupApps", "msgHeader", iniPath), MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            string selectedAppName = listBox1.SelectedItem.ToString();

            if (MessageBox.Show($"'{selectedAppName}' {ReadIniValue("FormStartupApps", "msgQuestionDelete", iniPath)}",
                ReadIniValue("FormStartupApps", "msgQuestionDeleteHeader", iniPath), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    RemoveFromStartupRegistry(selectedAppName);
                    ListeleTumStartupOgeleri(); // Listeyi yenile
                    MessageBox.Show(ReadIniValue("FormStartupApps", "msgSuccessDelete", iniPath),
                        ReadIniValue("FormServices", "msgSuccess", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogManager.Logla(selectedAppName+" adlı uygulama başlangıçtan silindi." +DateTime.Now);
                    lblAppName.Text = "";
                    lblAppLocation.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ReadIniValue("FormStartupApps", "msgFailDelete", iniPath)} {ex.Message}",
                        ReadIniValue("Others", "msgHeaderError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LogManager.Logla(selectedAppName + " adlı uygulama başlangıçtan silinemedi. "+ex.Message + DateTime.Now);
                }
            }

        }

        private void RemoveFromStartupRegistry(string appName)
        {
            // Tüm olası registry konumlarından uygulamayı kaldır
            RemoveRegistryValue(Registry.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Run", appName);
            RemoveRegistryValue(Registry.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\RunOnce", appName);
            RemoveRegistryValue(Registry.LocalMachine, @"Software\Microsoft\Windows\CurrentVersion\Run", appName);
            RemoveRegistryValue(Registry.LocalMachine, @"Software\Microsoft\Windows\CurrentVersion\RunOnce", appName);
            RemoveRegistryValue(Registry.LocalMachine, @"Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Run", appName);
        }

        private void RemoveRegistryValue(RegistryKey baseKey, string subKeyPath, string valueName)
        {
            try
            {
                using (var key = baseKey.OpenSubKey(subKeyPath, true))
                {
                    if (key != null)
                    {
                        // Değer var mı kontrol et
                        var value = key.GetValue(valueName);
                        if (value != null)
                        {
                            key.DeleteValue(valueName, false); // false = value yoksa hata verme
                            Console.WriteLine($"✓ {valueName} removed from {baseKey.Name}\\{subKeyPath}");
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show($"'{valueName}' silinirken erişim reddedildi.\n\nLütfen uygulamayı YÖNETİCİ olarak çalıştırın.",
                    "Erişim Reddedildi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing {valueName}: {ex.Message}");
            }
        }

        public class StartUpProgram
        {
            public string Name { get; set; }
            public string Path { get; set; }
            //show name in checkboxitem
            public override string ToString()
            {
                return Name;
            }
        }

        const string runKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";


        private void btnDevreDisi_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;

            string name = listBox1.SelectedItem.ToString();
            if (!startupItems.ContainsKey(name)) return;

            var item = startupItems[name];

            if (item.RootKey != null && item.SubKeyPath != null)
            {
                using (var key = item.RootKey.OpenSubKey(item.SubKeyPath, true))
                {
                    if (key != null && key.GetValue(name) != null)
                    {
                        object value = key.GetValue(name);
                        key.DeleteValue(name);

                        // Registry'de "_disabled_" ekle
                        key.SetValue("_disabled_" + name, value);

                        item.Enabled = false;  // sadece arka planda takip
                    }
                }
            }
        }

        private void btnEtkinlestir_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;

            string name = listBox1.SelectedItem.ToString();
            if (!startupItems.ContainsKey(name)) return;

            var item = startupItems[name];

            using (var key = item.RootKey.OpenSubKey(item.SubKeyPath, true))
            {
                if (key != null && key.GetValue("_disabled_" + item.Name) != null)
                {
                    object value = key.GetValue("_disabled_" + item.Name);
                    key.DeleteValue("_disabled_" + item.Name);
                    key.SetValue(item.Name, value);
                    item.Enabled = true;
                    MessageBox.Show($"{name} tekrar etkinleştirildi.");
                }
            }
        }

        private void btnOpenLocation_Click(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            if (listBox1.SelectedItem == null) return;

            string name = listBox1.SelectedItem.ToString();
            if (programPaths.ContainsKey(name) && File.Exists(programPaths[name]))
            {
                Process.Start("explorer.exe", Path.GetDirectoryName(programPaths[name]));
                LogManager.Logla("Başlangıç uygulamasının konumuna erişildi." + DateTime.Now);

            }
            else
            {
                MessageBox.Show(ReadIniValue("FormStartupApps", "msgErrorFileLocation", iniPath));
            }

        }

        private void btnExportToTXT_Click(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show(ReadIniValue("FormStartupApps", "msgNoAppsList", iniPath));
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text Files|*.txt";
                sfd.FileName = "StartupPrograms.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(sfd.FileName))
                        {
                            foreach (var item in listBox1.Items)
                            {
                                string name = item.ToString();
                                string path = programPaths.ContainsKey(name) ? programPaths[name] : "Bulunamadı";
                                writer.WriteLine($"{name} -> {path}");
                            }
                        }
                        MessageBox.Show(ReadIniValue("FormStartupApps", "msgSuccessExport", iniPath), ReadIniValue("FormServices", "msgSuccess", iniPath));
                        LogManager.Logla("Başlangıç uygulamaları listesi dışa aktarıldı." + DateTime.Now);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ReadIniValue("FormStartupApps", "msgFailDelete", iniPath) + ex.Message);
                        LogManager.Logla("Başlangıç uygulamaları listesi dışa aktarılamadı." + DateTime.Now);
                    }
                }
            }
        }
        private List<StartupApplication> GetStartupApplications()
        {
            var applications = new List<StartupApplication>();

            // Current User Run registry key
            GetApplicationsFromRegistry(Registry.CurrentUser,
                @"Software\Microsoft\Windows\CurrentVersion\Run", applications, "Current User - Run");

            // Local Machine Run registry key
            GetApplicationsFromRegistry(Registry.LocalMachine,
                @"Software\Microsoft\Windows\CurrentVersion\Run", applications, "Local Machine - Run");

            // Current User RunOnce registry key
            GetApplicationsFromRegistry(Registry.CurrentUser,
                @"Software\Microsoft\Windows\CurrentVersion\RunOnce", applications, "Current User - RunOnce");

            // Local Machine RunOnce registry key
            GetApplicationsFromRegistry(Registry.LocalMachine,
                @"Software\Microsoft\Windows\CurrentVersion\RunOnce", applications, "Local Machine - RunOnce");

            return applications;
        }

        private void btnBackupREGFile_Click(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Registry Files (*.reg)|*.reg|All Files (*.*)|*.*";
                saveDialog.Title = ReadIniValue("FormStartupApps", "backupTitle", iniPath) ?? "Backup Startup Apps";
                saveDialog.FileName = "startup_backup_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".reg";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExportRegistryKeys(saveDialog.FileName);
                        MessageBox.Show(ReadIniValue("FormStartupApps", "msgBackupSuccess", iniPath) ?? "Yedekleme başarılı!",
                            ReadIniValue("FormServices", "msgSuccess", iniPath) ?? ReadIniValue("FormServices", "msgSuccess", iniPath),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LogManager.Logla("Başlangıç uygulamaları REG dosyası olarak yedeklendi: " + saveDialog.FileName + " - " + DateTime.Now);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ReadIniValue("FormStartupApps", "msgBackupFail", iniPath) ?? "Yedekleme hatası:"} {ex.Message}",
                            ReadIniValue("FormStartupApps", "msgError", iniPath) ?? ReadIniValue("Others", "msgHeaderError", iniPath),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LogManager.Logla("Başlangıç uygulamaları REG dosyası olarak yedeklenemedi: " + ex.Message + " - " + DateTime.Now);
                    }
                }
            }

            //string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            //try
            //{
            //    var startupApps = GetStartupApplications();

            //    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            //    {
            //        saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            //        saveFileDialog.Title = ReadIniValue("FormStartupApps", "exportTitle", iniPath);
            //        saveFileDialog.FileName = "startup_applications.txt";
            //        saveFileDialog.DefaultExt = "txt";

            //        if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //        {
            //            ExportToTxt(startupApps, saveFileDialog.FileName);
            //            MessageBox.Show($"{ReadIniValue("FormStartupApps", "msgexportREGFile", iniPath)}\n\nDosya: {saveFileDialog.FileName}",
            //                ReadIniValue("FormServices", "msgSuccess", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            LogManager.Logla("Başlangıç uygulamaları listesi dışa aktarıldı.(REG file) "+DateTime.Now);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"{ReadIniValue("FormStartupApps", "msgFailDelete", iniPath)} {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    LogManager.Logla("Başlangıç uygulamaları listesi dışa aktarılamadı.(REG file)" + DateTime.Now);

            //}
        }

        private void GetApplicationsFromRegistry(RegistryKey baseKey, string subKeyPath,
     List<StartupApplication> applications, string registryLocation)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            try
            {
                using (var key = baseKey.OpenSubKey(subKeyPath))
                {
                    if (key != null)
                    {
                        foreach (var valueName in key.GetValueNames())
                        {
                            var value = key.GetValue(valueName)?.ToString();
                            if (!string.IsNullOrEmpty(value))
                            {
                                applications.Add(new StartupApplication
                                {
                                    Name = valueName,
                                    Path = value,
                                    RegistryLocation = registryLocation,
                                    RegistryKey = $"{baseKey.Name}\\{subKeyPath}"
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Erişim hatası olabilir, devam et
                MessageBox.Show($"{ReadIniValue("FormStartupApps", "msgRegistryAccessError", iniPath)} ({registryLocation}): {ex.Message}",
                    ReadIniValue("Others", "msgHeaderWarn", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ExportToTxt(List<StartupApplication> applications, string fileName)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            using (var writer = new StreamWriter(fileName))
            {
                writer.WriteLine(ReadIniValue("FormStartupApps", "exportText1", iniPath));
                writer.WriteLine("==============================");
                writer.WriteLine($"{ReadIniValue("FormStartupApps", "exportText2", iniPath)} {DateTime.Now}");
                writer.WriteLine($"{ReadIniValue("FormStartupApps", "exportText3", iniPath)}{applications.Count}");
                writer.WriteLine(ReadIniValue("FormStartupApps", "exportText4", iniPath));
                writer.WriteLine();

                if (applications.Count == 0)
                {
                    writer.WriteLine(ReadIniValue("FormStartupApps", "exportText5", iniPath));
                    return;
                }

                foreach (var app in applications)
                {
                    writer.WriteLine($"{ReadIniValue("FormStartupApps", "exportText6", iniPath)}{app.Name}");
                    writer.WriteLine($"{ReadIniValue("FormStartupApps", "exportText7", iniPath)}{app.Path}");
                    writer.WriteLine($"{ReadIniValue("FormStartupApps", "exportText8", iniPath)}{app.RegistryLocation}");
                    writer.WriteLine($"{ReadIniValue("FormStartupApps", "exportText9", iniPath)} {app.RegistryKey}");
                    writer.WriteLine("----------------------------------------");
                }
            }
        }

        private void ExportToTxtMenu()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
                saveFileDialog.Title = ReadIniValue("FormStartupApps", "exportTitle", iniPath);
                saveFileDialog.FileName = "startup_applications.txt";
                saveFileDialog.DefaultExt = "txt";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // ListBox'taki tüm öğeleri al
                    List<string> applications = new List<string>();
                    foreach (var item in listBox1.Items)
                    {
                        applications.Add(item.ToString());
                    }

                    // Dosyaya yaz
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            writer.WriteLine(ReadIniValue("FormStartupApps", "exportTxtTitle", iniPath));
                            writer.WriteLine("==============================");
                            writer.WriteLine($"{ReadIniValue("FormStartupApps", "exportTxt1", iniPath)} {DateTime.Now}");
                            writer.WriteLine($"{ReadIniValue("FormStartupApps", "exportTxt2", iniPath)} {applications.Count}");
                            writer.WriteLine();

                            foreach (string app in applications)
                            {
                                writer.WriteLine($"- {app}");
                            }
                        }

                        MessageBox.Show($"{ReadIniValue("FormStartupApps", "exportTxtMsg1", iniPath)}\n\nDosya: {saveFileDialog.FileName}",
                            ReadIniValue("FormServices", "msgSuccess", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LogManager.Logla("Başlangıç uygulamaları listesi dışa aktarıldı." + DateTime.Now);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ReadIniValue("FormStartupApps", "exportTxtMsg2", iniPath)}{ex.Message}",
                            ReadIniValue("Others", "msgHeaderError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LogManager.Logla("Başlangıç uygulamaları listesi dışa aktarılamadı." + DateTime.Now);

                    }
                }
            }
        }
        public class StartupApplication
        {
            public string Name { get; set; }
            public string Path { get; set; }
            public string RegistryLocation { get; set; }
            public string RegistryKey { get; set; }
        }

        private void FormStartupApps_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormSelectScreen frmSelect = new FormSelectScreen();
            frmSelect.Show();
            this.Hide();
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Sağ tıklanan öğeyi bul ve seç
                int index = listBox1.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    listBox1.SelectedIndex = index;
                }
            }
        }

        private void MenuDelete_Click(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            if (listBox1.SelectedItem != null)
            {
                string selectedApp = listBox1.SelectedItem.ToString();

                if (MessageBox.Show($"'{selectedApp}'{ReadIniValue("FormStartupApps", "msgQuestionDelete", iniPath)}",
                    "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    RemoveFromStartupRegistry(selectedApp);
                    ListeleTumStartupOgeleri(); // Listeyi yenile
                    MessageBox.Show(ReadIniValue("FormStartupApps", "msgSuccessDelete", iniPath), ReadIniValue("FormStartupApps", "msgSuccess", iniPath),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogManager.Logla(selectedApp + " adlı uygulama başlangıçtan silindi." + DateTime.Now);

                }
            }
        }

        private void MenuOpenLocation_Click(object sender, EventArgs e)
        {

            btnOpenLocation.PerformClick();

            //if (listBox1.SelectedItem != null)
            //{
            //    string selectedApp = listBox1.SelectedItem.ToString();
            //    OpenApplicationLocation(selectedApp);
            //}
        }

        private void MenuBackup_Click(object sender, EventArgs e)
        {
            BackupStartupItems();
        }

        private void MenuExport_Click(object sender, EventArgs e)
        {
            ExportToTxtMenu();
        }

        private void OpenApplicationLocation(string appName)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            try
            {
                // Uygulamanın yolunu registry'den bul
                string appPath = FindApplicationPath(appName);

                if (!string.IsNullOrEmpty(appPath) && File.Exists(appPath))
                {
                    // Dosyanın bulunduğu klasörü aç
                    string directory = Path.GetDirectoryName(appPath);
                    Process.Start("explorer.exe", $"/select,\"{appPath}\"");
                }
                else
                {
                    MessageBox.Show(ReadIniValue("FormStartupApps", "msgErrorFileLocation", iniPath),
                        ReadIniValue("Others", "msgHeaderWarn", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Konum açılırken hata: {ex.Message}",
                    ReadIniValue("Others", "msgHeaderError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FindApplicationPath(string appName)
        {
            // Tüm registry konumlarında uygulama yolunu ara
            string[] registryPaths = {
        @"Software\Microsoft\Windows\CurrentVersion\Run",
        @"Software\Microsoft\Windows\CurrentVersion\RunOnce",
        @"Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Run"
    };

            foreach (var path in registryPaths)
            {
                try
                {
                    using (var key = Registry.CurrentUser.OpenSubKey(path))
                    {
                        var value = key?.GetValue(appName);
                        if (value != null) return value.ToString();
                    }

                    using (var key = Registry.LocalMachine.OpenSubKey(path))
                    {
                        var value = key?.GetValue(appName);
                        if (value != null) return value.ToString();
                    }
                }
                catch { /* Devam et */ }
            }

            return null;
        }

        private void BackupStartupItems()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Registry Files (*.reg)|*.reg|All Files (*.*)|*.*";
                saveDialog.Title = "Backup Startup Apps";
                saveDialog.FileName = "startup_backup.reg";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportRegistryKeys(saveDialog.FileName);
                    MessageBox.Show("Yedekleme başarılı!", ReadIniValue("FormServices", "msgSuccess", iniPath),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ExportRegistryKeys(string filePath)
        {
            try
            {
                StringBuilder regContent = new StringBuilder();
                regContent.AppendLine("Windows Registry Editor Version 5.00");
                regContent.AppendLine();

                // Current User başlangıç öğeleri
                ExportRegistryKey(Registry.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Run", regContent);
                ExportRegistryKey(Registry.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\RunOnce", regContent);

                // Local Machine başlangıç öğeleri
                ExportRegistryKey(Registry.LocalMachine, @"Software\Microsoft\Windows\CurrentVersion\Run", regContent);
                ExportRegistryKey(Registry.LocalMachine, @"Software\Microsoft\Windows\CurrentVersion\RunOnce", regContent);
                ExportRegistryKey(Registry.LocalMachine, @"Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Run", regContent);

                File.WriteAllText(filePath, regContent.ToString(), Encoding.Unicode);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yedekleme hatası: {ex.Message}", ReadIniValue("Others", "msgHeaderError", iniPath),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportRegistryKey(RegistryKey baseKey, string subKey, StringBuilder regContent)
        {
            try
            {
                using (var key = baseKey.OpenSubKey(subKey))
                {
                    if (key != null)
                    {
                        string keyPath = baseKey.Name.Replace("HKEY_CURRENT_USER", "HKEY_CURRENT_USER")
                                                   .Replace("HKEY_LOCAL_MACHINE", "HKEY_LOCAL_MACHINE");

                        regContent.AppendLine($"[{keyPath}\\{subKey}]");

                        foreach (var valueName in key.GetValueNames())
                        {
                            var value = key.GetValue(valueName);
                            string valueStr = value?.ToString().Replace("\\", "\\\\");
                            regContent.AppendLine($"\"{valueName}\"=\"{valueStr}\"");
                        }

                        regContent.AppendLine();
                    }
                }
            }
            catch { /* Devam et */ }
        }
    }
}