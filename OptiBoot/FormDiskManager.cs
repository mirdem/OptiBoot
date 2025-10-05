using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OptiBoot
{
    public partial class FormDiskManager : Form
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
        private bool IsSystemDisk(string driveLetter)
        {
            try
            {
                // Windows'un kurulu olduğu sürücüyü bul
                string systemDrive = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 2);

                // Seçilen sürücü sistem sürücüsü mü kontrol et
                return driveLetter.Equals(systemDrive, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        public void LoadLanguage()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            this.Text = ReadIniValue("FormDiskManager", "caption", iniPath);
            labelDiskManager.Text = ReadIniValue("FormDiskManager", "labelDiskManagement", iniPath);
            groupBoxDiskInfo.Text = ReadIniValue("FormDiskManager", "grpDiskInfo", iniPath);
            buttonReload.Text = ReadIniValue("FormDiskManager", "buttonRefresh", iniPath);
            groupBoxDiskFormat.Text = ReadIniValue("FormDiskManager", "grpDiskFormat", iniPath);
            labelFormatType.Text = ReadIniValue("FormDiskManager", "labelDiskFormat", iniPath);
            labelFormatLabel.Text = ReadIniValue("FormDiskManager", "labelDiskLabel", iniPath);
            buttonFormat.Text = ReadIniValue("FormDiskManager", "buttonFormat", iniPath);
        }

        private ColorProgressBar progressBarColored;

        public FormDiskManager()
        {
            InitializeComponent();
            comboBoxDisks.SelectedIndexChanged += comboBoxDisks_SelectedIndexChanged;
            comboBoxFormat.Items.Add("NTFS");
            comboBoxFormat.Items.Add("FAT32");
            comboBoxFormat.Items.Add("exFAT");
            comboBoxFormat.SelectedIndex = 0;
        }

        private void LoadDisks()
        {
            comboBoxDisks.Items.Clear();

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady) // sadece erişilebilir diskler
                {
                    string model = GetDiskModel(drive.Name);
                    string displayName = $"{drive.Name} - {model} [{drive.DriveType}]";
                    comboBoxDisks.Items.Add(displayName);
                }
            }

            if (comboBoxDisks.Items.Count > 0)
                comboBoxDisks.SelectedIndex = 0;
        }
        private string GetDiskModel(string driveLetter)
        {
            try
            {
                // DriveLetter örn: "C:\"
                string letter = driveLetter.Substring(0, 2);

                var searcher = new ManagementObjectSearcher(
                    "ASSOCIATORS OF {Win32_LogicalDisk.DeviceID='" + letter + "'} " +
                    "WHERE AssocClass=Win32_LogicalDiskToPartition");

                foreach (ManagementObject partition in searcher.Get())
                {
                    var drives = new ManagementObjectSearcher(
                        "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" + partition["DeviceID"] +
                        "'} WHERE AssocClass=Win32_DiskDriveToDiskPartition");

                    foreach (ManagementObject disk in drives.Get())
                    {
                        return disk["Model"]?.ToString() ?? "Bilinmiyor";
                    }
                }
            }
            catch { }

            return "Bilinmiyor";
        }

        private void FormDiskManager_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);

            if (Properties.Settings.Default.AlwaysTop == true)
            {
                this.TopMost = true;
            }

            LoadDisks();
            LoadLanguage();
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(buttonFormat, "Bu işlem disteki tüm bilgileri siler GERİ ALINAMAZ");

        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            LoadDisks();
        }

        private void comboBoxDisks_SelectedIndexChanged(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            if (comboBoxDisks.SelectedItem == null) return;

            string selected = comboBoxDisks.SelectedItem.ToString();
            string driveLetter = selected.Substring(0, 2); // "C:"

            DriveInfo drive = new DriveInfo(driveLetter);

            if (drive.IsReady)
            {
                long totalSize = drive.TotalSize;
                long freeSpace = drive.TotalFreeSpace;
                long usedSpace = totalSize - freeSpace;
                string model = GetDiskModel(driveLetter);
                string type = drive.DriveType.ToString();
                string format = drive.DriveFormat.ToString();
                bool isSystemDisk = IsSystemDisk(driveLetter);
                string systemDiskText = isSystemDisk ? ReadIniValue("FormDiskManager", "labelYesSystemDisk", iniPath) : ReadIniValue("FormDiskManager", "labelNOSystemDisk", iniPath);

                string totaldisk = ReadIniValue("FormDiskManager", "labelTotalSize", iniPath);
                string useddisk = ReadIniValue("FormDiskManager", "labelUsedSize", iniPath);
                string freespacedisk = ReadIniValue("FormDiskManager", "labelFreeSpace", iniPath);
                string disktype = ReadIniValue("FormDiskManager", "labelDiskType", iniPath);
                string formatype = ReadIniValue("FormDiskManager", "labelFormat", iniPath);
                string modelname = ReadIniValue("FormDiskManager", "labelModel", iniPath);
                string systemdisk = ReadIniValue("FormDiskManager", "labelSystemDisk", iniPath);

                labelTotalDiskSize.Text = $"{totaldisk} {BytesToGB(totalSize)} GB";
                labelUsedSpace.Text = $"{useddisk} {BytesToGB(usedSpace)} GB";
                labelFreeSpace.Text = $"{freespacedisk} {BytesToGB(freeSpace)} GB";
                labelDiskModel.Text = $"{modelname} {model}";
                labelDiskType.Text = $"{disktype} {type}";
                labelDiskFormat.Text = $"{formatype} {format}";
                labelSystemDisk.Text = $"{systemdisk} {systemDiskText}";

                colorProgressBar1.Maximum = 100;
                double percentUsed = (usedSpace * 100.0) / totalSize;
                colorProgressBar1.Value = (int)Math.Round(percentUsed);
            }

        }
        private string BytesToGB(long bytes)
        {
            return (bytes / (1024.0 * 1024 * 1024)).ToString("0.00");
        }

        private void buttonFormat_Click(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            this.Text = ReadIniValue("FormDiskManager", "caption", iniPath);
            if (comboBoxDisks.SelectedItem == null) return;

            string driveLetter = comboBoxDisks.SelectedItem.ToString().Substring(0, 2); // "E:"
            string formatType = comboBoxFormat.SelectedItem.ToString();
            string volumeLabel = string.IsNullOrWhiteSpace(txtDiskName.Text) ? "NewDisk" : txtDiskName.Text;
            bool isSystemDisk = IsSystemDisk(driveLetter);
            if (isSystemDisk)
            {
                DialogResult systemWarning = MessageBox.Show(
                    $"{ReadIniValue("FormDiskManager", "msgSystemDiskWarning", iniPath)}",
                    $"{ReadIniValue("FormDiskManager", "msgWarning", iniPath)}",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error);

                if (systemWarning != DialogResult.Yes)
                    return;
            }

            DialogResult result = MessageBox.Show(
                $"{driveLetter} {ReadIniValue("FormDiskManager", "msg1", iniPath)} {formatType} {ReadIniValue("FormDiskManager", "msg2", iniPath)} '{volumeLabel}' {ReadIniValue("FormDiskManager", "msg3", iniPath)}\n" +
                $"{ReadIniValue("FormDiskManager", "msg4", iniPath)}",
                $"{ReadIniValue("FormDiskManager", "msg5", iniPath)}",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo("cmd.exe",
                        $"/C format {driveLetter} /FS:{formatType} /V:{volumeLabel} /Q /Y")
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };

                    using (Process proc = new Process { StartInfo = psi })
                    {
                        proc.Start();
                        proc.WaitForExit(); // işlem bitene kadar bekle

                        if (proc.ExitCode == 0)
                        {
                            MessageBox.Show($"{ReadIniValue("FormDiskManager", "msgFormatSuccess", iniPath)}", ReadIniValue("Others", "msgHeaderInfo", iniPath),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LogManager.Logla("Disk formatlandı: " + txtDiskName.Text + " " + comboBoxDisks.Text + " " + comboBoxFormat.Text);
                        }
                        else
                        {
                            string errorMsg = proc.StandardError.ReadToEnd();
                            if (string.IsNullOrWhiteSpace(errorMsg))
                                errorMsg = $"{ReadIniValue("FormDiskManager", "msgFormatFail", iniPath)}";

                            MessageBox.Show($"{ReadIniValue("FormDiskManager", "msgFormatFail", iniPath)}" + errorMsg, ReadIniValue("Others", "msgHeaderError", iniPath),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LogManager.Logla("Disk fortmatlanırken hata oluştu : " + txtDiskName.Text + " " + comboBoxDisks.Text + " " + errorMsg);

                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ReadIniValue("FormServices", "labelStatusError", iniPath) + ex.Message, ReadIniValue("Others", "msgHeaderError", iniPath),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LogManager.Logla("Disk fortmatlanırken hata oluştu : " + txtDiskName.Text + " " + comboBoxDisks.Text + ex.Message);
                }
            }
        }

        private void FormDiskManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormSelectScreen formSelectScreen = new FormSelectScreen();
            formSelectScreen.Show();
            this.Hide();

        }
    }
}
