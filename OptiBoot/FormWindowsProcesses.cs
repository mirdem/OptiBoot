using Mono.Unix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace OptiBoot
{
    public partial class FormWindowsProcesses : Form
    {
        // private System.Windows.Forms.Timer countdownTimer;
        private DateTime targetTime;
        private Process scheduledProcess;
        private System.Windows.Forms.Timer performanceTimer;
        private Dictionary<string, List<Process>> processGroups;

        // Process detayları için değişkenler
        private PerformanceCounter cpuCounter;
        private Process selectedProcess;
        public string ProcessPathOpenURL;
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

        public FormWindowsProcesses()
        {
            InitializeComponent();
            InitializeTimers();
            InitializePerformanceCounter();
            processGroups = new Dictionary<string, List<Process>>();
            listBoxProcess.ContextMenuStrip = contextMenu;

        }

        private bool IsSystemProcess(Process process)
        {
            try
            {
                // Sistem işlemlerini belirleme kriterleri
                string processName = process.ProcessName.ToLower();

                return processName.Contains("system") ||
                       processName.Contains("svchost") ||
                       processName.Contains("csrss") ||
                       processName.Contains("winlogon") ||
                       processName.Contains("services") ||
                       processName.Contains("lsass") ||
                       processName.Contains("wininit") ||
                       processName.Contains("smss") ||
                       processName.Contains("lsm") ||
                       processName.Contains("taskhost") ||
                       processName.Contains("dwm") ||
                       processName.Contains("explorer") && process.SessionId == 0 || // Session 0 explorer
                       process.Id <= 8 || // Düşük PID'ler genellikle sistem işlemleri
                       string.IsNullOrEmpty(process.MainWindowTitle) && process.SessionId == 0; // Session 0 sistem işlemleri
            }
            catch
            {
                return false;
            }
        }

        public void LoadLanguage()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            this.Text = ReadIniValue("FormWindowsProcesses", "caption", iniPath);
            groupBoxCountdown.Text = ReadIniValue("FormWindowsProcesses", "grpCountdown", iniPath);
            labelTime.Text = ReadIniValue("FormWindowsProcesses", "labelTime", iniPath);
            labelCountdownInfo.Text = ReadIniValue("FormWindowsProcesses", "labelCountdownInfo", iniPath);
            buttonSetTimer.Text = ReadIniValue("FormWindowsProcesses", "buttonSetCountdown", iniPath);
            toolTip1.SetToolTip(buttonCancel, ReadIniValue("FormWindowsProcesses", "buttonCancelCountdown", iniPath));
            buttonKillProcess.Text = ReadIniValue("FormWindowsProcesses", "buttonKillProcess", iniPath);
            buttonOpenLocation.Text = ReadIniValue("FormWindowsProcesses", "btnOpenApp", iniPath);
            labelMemory.Text = ReadIniValue("FormWindowsProcesses", "labelMemory", iniPath);
            labelRunningTime.Text = ReadIniValue("FormWindowsProcesses", "labelRunningTime", iniPath);
            labelStartTime.Text = ReadIniValue("FormWindowsProcesses", "labelStartTime", iniPath);
            labelFilePath.Text = ReadIniValue("FormWindowsProcesses", "labelFile", iniPath);
            labelPriority.Text = ReadIniValue("FormWindowsProcesses", "labelPriority", iniPath);
            checkBoxHideSystemProcesses.Text = ReadIniValue("FormWindowsProcesses", "checkBoxHideSystem", iniPath);
        }
        private void InitializeTimers()
        {
            countdownTimer = new System.Windows.Forms.Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;


            performanceTimer = new System.Windows.Forms.Timer();
            performanceTimer.Interval = 2000;
            performanceTimer.Tick += PerformanceTimer_Tick;
            performanceTimer.Start();
        }


        private void InitializePerformanceCounter()
        {
            try
            {
                cpuCounter = new PerformanceCounter("Process", "% Processor Time", "_Total");
                cpuCounter.NextValue();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Performance counter error: {ex.Message}");
            }
        }

        private void RefreshProcessList()
        {
            listBoxProcess.Items.Clear();
            Process[] processes = Process.GetProcesses();
            if (checkBoxHideSystemProcesses.Checked)
            {
                processes = processes.Where(p => !IsSystemProcess(p)).ToArray();
            }

            // Process'leri isimlerine göre gruplayarak göster
            var processGroups = processes
                .GroupBy(p => p.ProcessName)
                .Select(g => new
                {
                    ProcessName = g.Key,
                    Count = g.Count(),
                    Processes = g.ToList()
                })
                .OrderByDescending(g => g.Count)
                .ThenBy(g => g.ProcessName);

            foreach (var group in processGroups)
            {
                if (group.Count > 1)
                {
                    // Birden fazla instance'ı olan process'ler
                    listBoxProcess.Items.Add($"📦 {group.ProcessName} ({group.Count} {ReadIniValue("FormWindowsProcesses", "processInstance", iniPath)})");
                }
                else
                {
                    // Tek instance'ı olan process'ler
                    listBoxProcess.Items.Add($"{group.ProcessName} (ID: {group.Processes.First().Id})");
                }
                UpdateTotalProcessCount();

            }
        }
        
        private void UpdateTotalProcessCount()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            if (labelTotalProcess != null)
            {
                labelTotalProcess.Text = $"{ReadIniValue("FormWindowsProcesses", "labelTotalProcess", iniPath)} {listBoxProcess.Items.Count} {ReadIniValue("FormWindowsProcesses", "labelTotalProcess2", iniPath)}";
            }
        }

        private void PerformanceTimer_Tick(object sender, EventArgs e)
        {
            UpdateProcessDetails();
            //UpdateGroupedProcesses();
        }

        private void UpdateProcessDetails()
        {
            if (listBoxProcess.SelectedItem == null || selectedProcess == null)
                return;


            try
            {
                float cpuUsage = 0;
                try
                {
                    using (var pc = new PerformanceCounter("Process", "% Processor Time", selectedProcess.ProcessName))
                    {
                        cpuUsage = pc.NextValue() / Environment.ProcessorCount;
                    }
                }
                catch { }


                double memoryMB = selectedProcess.WorkingSet64 / 1024.0 / 1024.0;
                TimeSpan runningTime = DateTime.Now - selectedProcess.StartTime;
                string priority = selectedProcess.PriorityClass.ToString();


                if (labelCPU != null) labelCPU.Text = $"CPU: {cpuUsage:F1}%";
                if (labelMemory != null) labelMemory.Text = $"{ReadIniValue("FormWindowsProcesses", "labelMemory", iniPath)} {memoryMB:F1} MB";
                if (labelRunningTime != null) labelRunningTime.Text = $"{ReadIniValue("FormWindowsProcesses", "labelRunningTime", iniPath)} {runningTime:hh\\:mm\\:ss}";
                if (labelStartTime != null) labelStartTime.Text = $"{ReadIniValue("FormWindowsProcesses", "labelStartTime", iniPath)} {selectedProcess.StartTime:HH:mm:ss}";
                if (labelFilePath != null) labelFilePath.Text = $"{ReadIniValue("FormWindowsProcesses", "labelFile", iniPath)} {GetProcessFilePath(selectedProcess)}";
                if (labelPriority != null) labelPriority.Text = $"{ReadIniValue("FormWindowsProcesses", "labelPriority", iniPath)} {priority}";
                ProcessPathOpenURL = GetProcessFilePath(selectedProcess);
            }
            catch
            {
                ClearProcessDetails();
            }
        }


        private string GetProcessFilePath(Process process)
        {
            try
            {
                return process.MainModule?.FileName ?? "Erişilemedi";
            }
            catch
            {
                return "Erişilemedi";
            }
        }

        private void ClearProcessDetails()
        {
            if (labelCPU != null) labelCPU.Text = "CPU: -";
            if (labelMemory != null) labelMemory.Text = ReadIniValue("FormWindowsProcesses", "labelMemory", iniPath);
            if (labelRunningTime != null) labelRunningTime.Text = ReadIniValue("FormWindowsProcesses", "labelRunningTime", iniPath);
            if (labelStartTime != null) labelStartTime.Text = ReadIniValue("FormWindowsProcesses", "labelStartTime", iniPath);
            if (labelFilePath != null) labelFilePath.Text = ReadIniValue("FormWindowsProcesses", "labelFile", iniPath);
            if (labelPriority != null) labelPriority.Text = ReadIniValue("FormWindowsProcesses", "labelPriority", iniPath);
        }

        //private void UpdateGroupedProcesses()
        //{
        //    if (listBoxGroupProcesses != null && listBoxGroupProcesses.Visible)
        //    {
        //        listBoxGroupProcesses.Items.Clear();
        //        foreach (var group in processGroups)
        //        {
        //            listBoxGroupProcesses.Items.Add($"=== {group.Key} ===");
        //            foreach (var process in group.Value)
        //            {
        //                try
        //                {
        //                    double memoryMB = process.WorkingSet64 / 1024.0 / 1024.0;
        //                    listBoxGroupProcesses.Items.Add($" {process.ProcessName} (ID: {process.Id}) - {memoryMB:F1} MB");
        //                }
        //                catch
        //                {
        //                    listBoxGroupProcesses.Items.Add($" {process.ProcessName} (Erişilemez)");
        //                }
        //            }
        //        }
        //    }
        //}

        private void listBoxGroupProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxProcess.SelectedItem == null)
            {
                selectedProcess = null;
                ClearProcessDetails();
                return;
            }

            string selectedItem = listBoxProcess.SelectedItem.ToString();

            try
            {
                if (selectedItem.Contains("(") && selectedItem.Contains("ID:"))
                {
                    // Tek process seçildi
                    int processId = int.Parse(selectedItem.Split(new[] { "(ID: " }, StringSplitOptions.None)[1].TrimEnd(')'));
                    selectedProcess = Process.GetProcessById(processId);
                }
                else if (selectedItem.Contains("instance"))
                {
                    // Gruplanmış process seçildi - ilk process'i göster
                    string processName = selectedItem.Split(' ')[1];
                    var processes = Process.GetProcessesByName(processName);
                    selectedProcess = processes.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Process seçim hatası: {ex.Message}");
                selectedProcess = null;
            }

            UpdateProcessDetails();
        }


        private void KillSelectedProcess()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");


            if (listBoxProcess.SelectedItem == null)
            {
                MessageBox.Show(ReadIniValue("FormWindowsProcesses", "msgSelectProcess", iniPath));
                return;
            }

            string selectedItem = listBoxProcess.SelectedItem.ToString();
            int processId = int.Parse(selectedItem.Split(new[] { "(ID: " }, StringSplitOptions.None)[1].TrimEnd(')'));

            try
            {
                Process process = Process.GetProcessById(processId);
                process.Kill();
                RefreshProcessList();
                MessageBox.Show(ReadIniValue("FormWindowsProcesses", "msgSuccessKill", iniPath));
                LogManager.Logla("Killed Process: " + process + " " + DateTime.Now);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }
        private void LoadProcessesAsync()
        {
            var processes = Process.GetProcesses();

            if (checkBoxHideSystemProcesses.Checked)
            {
                processes = processes.Where(p => !IsSystemProcess(p)).ToArray();
            }

            var groupedProcesses = processes
                .GroupBy(p => p.ProcessName)
                .Select(g => new
                {
                    ProcessName = g.Key,
                    Count = g.Count(),
                    Processes = g.ToList()
                })
                .OrderByDescending(g => g.Count)
                .ThenBy(g => g.ProcessName)
                .ToList();

            this.Invoke((Action)(() =>
            {
                listBoxProcess.Items.Clear();
                foreach (var group in groupedProcesses)
                {
                    if (group.Count > 1)
                        listBoxProcess.Items.Add($"📦 {group.ProcessName} ({group.Count}  {ReadIniValue("FormWindowsProcesses", "processInstance", iniPath)})");
                    else
                        listBoxProcess.Items.Add($"{group.ProcessName} (ID: {group.Processes.First().Id})");
                }
                UpdateTotalProcessCount();
            }));
        }

        private async void FormWindowsProcesses_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);

            await Task.Run(() => LoadProcessesAsync());

            if (Properties.Settings.Default.AlwaysTop == true)
            {
                this.TopMost = true;
            }
            LoadLanguage();
        }

        private void buttonKillProcess_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(ReadIniValue("FormWindowsProcesses", "msgQuestionTerminate", iniPath),
                   "", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                KillSelectedProcess();
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text.ToLower();
            listBoxProcess.Items.Clear();
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.ProcessName.ToLower().Contains(searchText))
                {
                    listBoxProcess.Items.Add($"{process.ProcessName} (ID: {process.Id})");
                    labelTotalProcess.Text = ReadIniValue("FormWindowsProcesses", "labelTotalProcess", iniPath) + " " + listBoxProcess.Items.Count.ToString() + " " + ReadIniValue("FormWindowsProcesses", "labelTotalProcess2", iniPath);
                }
            }
        }

        private void buttonSetTimer_Click(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            if (listBoxProcess.SelectedItem == null)
            {
                MessageBox.Show(ReadIniValue("FormWindowsProcesses", "msgSelectProcess", iniPath));
                return;
            }

            if (!DateTime.TryParse(dateTimePicker1.Text, out targetTime))
            {
                MessageBox.Show(ReadIniValue("FormWindowsProcesses", "msgCorrectTimeKill", iniPath));
                return;
            }

            string selectedItem = listBoxProcess.SelectedItem.ToString();
            int processId = int.Parse(selectedItem.Split(new[] { "(ID: " }, StringSplitOptions.None)[1].TrimEnd(')'));
            scheduledProcess = Process.GetProcessById(processId);

            countdownTimer.Start();
            UpdateCountdownLabel();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            countdownTimer.Stop();
            scheduledProcess = null;
            labelCountdown.Text = ReadIniValue("FormWindowsProcesses", "labelCountdownCancel", iniPath);
        }
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now >= targetTime)
            {
                countdownTimer.Stop();
                try
                {
                    scheduledProcess?.Kill();
                    labelCountdown.Text = $"{ReadIniValue("FormWindowsProcesses", "labelCountDownKilled1", iniPath)} {scheduledProcess?.ProcessName} {ReadIniValue("FormWindowsProcesses", "labelCountDownKilled2", iniPath)}";
                    scheduledProcess = null;
                    RefreshProcessList();
                }
                catch (Exception ex)
                {
                    labelCountdown.Text = $"Hata: {ex.Message}";
                }
            }
            else
            {
                UpdateCountdownLabel();
            }
        }

        private void UpdateCountdownLabel()
        {
            TimeSpan remaining = targetTime - DateTime.Now;
            labelCountdown.Text = $"{ReadIniValue("FormWindowsProcesses", "labelCountdownRemain", iniPath)} {remaining:hh\\:mm\\:ss} - {scheduledProcess?.ProcessName}";
        }

        private void sonlandırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KillSelectedProcess();
        }

        private void listBoxProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxProcess.SelectedItem == null)
            {
                selectedProcess = null;
                ClearProcessDetails();
                return;
            }

            string selectedItem = listBoxProcess.SelectedItem.ToString();
            Debug.WriteLine($"Seçilen item: {selectedItem}"); // Debug için

            try
            {
                if (selectedItem.Contains("(ID:"))
                {
                    // Tek process seçildi - Format: "processname (ID: 1234)"
                    int startIndex = selectedItem.IndexOf("(ID:") + 4;
                    int endIndex = selectedItem.IndexOf(")");
                    string processIdStr = selectedItem.Substring(startIndex, endIndex - startIndex).Trim();

                    if (int.TryParse(processIdStr, out int processId))
                    {
                        selectedProcess = Process.GetProcessById(processId);
                        Debug.WriteLine($"Process bulundu: {selectedProcess.ProcessName}");
                    }
                }
                else if (selectedItem.Contains("instance") && selectedItem.Contains("📦"))
                {
                    // Gruplanmış process seçildi - Format: "📦 processname (3 instance)"
                    string processName = selectedItem.Replace("📦", "").Split('(')[0].Trim();
                    var processes = Process.GetProcessesByName(processName);

                    if (processes.Length > 0)
                    {
                        selectedProcess = processes[0]; // İlk process'i göster
                        Debug.WriteLine($"Grup process bulundu: {selectedProcess.ProcessName}");
                    }
                }
                else
                {
                    // Basit process ismi
                    string processName = selectedItem.Split(' ')[0];
                    var processes = Process.GetProcessesByName(processName);

                    if (processes.Length > 0)
                    {
                        selectedProcess = processes[0];
                        Debug.WriteLine($"Basit process bulundu: {selectedProcess.ProcessName}");
                    }
                }

                // Detayları güncelle
                UpdateProcessDetails();
                if (labelFilePath.Text == "")
                {
                    buttonOpenLocation.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hata: {ex.Message}");
                selectedProcess = null;
                ClearProcessDetails();
                MessageBox.Show($"Process seçim hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOpenLocation_Click(object sender, EventArgs e)
        {
            if (ProcessPathOpenURL == null || ProcessPathOpenURL == "-")
            {
                MessageBox.Show(ReadIniValue("FormWindowsProcesses", "msgCannotRunApp", iniPath), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Process.Start("explorer.exe", ProcessPathOpenURL);
            }
        }

        private void FormWindowsProcesses_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormSelectScreen formSelectScreen = new FormSelectScreen();
            formSelectScreen.Show();
            this.Hide();
        }

        private void checkBoxHideSystemProcesses_CheckedChanged(object sender, EventArgs e)
        {
            RefreshProcessList();
        }
    }
}
