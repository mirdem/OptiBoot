using Microsoft.Win32;
using Mono.Unix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptiBoot
{
    public partial class FormServices : Form
    {
        private ServiceController _rightClickedService;

        public FormServices()
        {
            InitializeComponent();
         //   InitButtons();
            InitializeContextMenu();
            this.listBoxServices.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxServices_MouseDown);

        }
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
        //private void InitButtons()
        //{

        //    btnStart.Text = "▶ Başlat";
        //    btnStart.Width = 100;
        //    btnStart.Height = 40;
        //    btnStart.BackColor = System.Drawing.Color.LightGreen;
        //    btnStart.FlatStyle = FlatStyle.Flat;
        //    btnStart.Click += (s, e) => MessageBox.Show("Servis başlatıldı!");


        //    btnStop.Text = "⏹ Durdur";
        //    btnStop.Width = 100;
        //    btnStop.Height = 40;
        //    btnStop.BackColor = System.Drawing.Color.IndianRed;
        //    btnStop.FlatStyle = FlatStyle.Flat;
        //    btnStop.Click += (s, e) => MessageBox.Show("Servis durduruldu!");


        //    btnPause.Text = "⏸ Duraklat";
        //    btnPause.Width = 100;
        //    btnPause.Height = 40;
        //    btnPause.BackColor = System.Drawing.Color.Khaki;
        //    btnPause.FlatStyle = FlatStyle.Flat;


        //    btnPause.Text = "⏯ Devam Et";
        //    btnPause.Width = 100;
        //    btnPause.Height = 40;
        //    btnPause.BackColor = System.Drawing.Color.LightSkyBlue;
        //    btnPause.FlatStyle = FlatStyle.Flat;

        //}

        private void InitializeContextMenu()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            contextMenuStripServices = new ContextMenuStrip();

            menuStart = new ToolStripMenuItem();
            menuStop = new ToolStripMenuItem();
            menuPause = new ToolStripMenuItem();
            menuContinue = new ToolStripMenuItem();

            menuStart.Text = ReadIniValue("FormServices", "ContextMenuStart", iniPath);
            menuStop.Text = ReadIniValue("FormServices", "ContextMenuStop", iniPath);
            menuPause.Text = ReadIniValue("FormServices", "ContextMenuPause", iniPath);
            menuContinue.Text = ReadIniValue("FormServices", "ContextMenuContiune", iniPath);

            menuStart.Click += menuStart_Click;
            menuStop.Click += menuStop_Click;
            menuPause.Click += menuPause_Click;
            menuContinue.Click += menuContinue_Click;

            contextMenuStripServices.Items.AddRange(new ToolStripItem[] {
            menuStart, menuStop, menuPause, menuContinue
        });

            listBoxServices.ContextMenuStrip = contextMenuStripServices;
            listBoxServices.MouseDown += listBoxServices_MouseDown;
        }

        private void LoadServices()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            try
            {
                listBoxServices.Items.Clear();
                ServiceController[] services = ServiceController.GetServices();

                foreach (ServiceController service in services)
                {
                    listBoxServices.Items.Add(service);
                }

                listBoxServices.DisplayMember = "DisplayName";

                lblStatus.Text = $"{ReadIniValue("FormServices", "labelTotalServices", iniPath)} {services.Length} {ReadIniValue("FormServices", "labelTotalServices2", iniPath)}";
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ReadIniValue("FormServices", "msgErrorLoadingServices", iniPath)} {ex.Message}",
                    ReadIniValue("Others", "msgHeaderError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LoadLanguage()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            this.Text = ReadIniValue("FormServices", "caption", iniPath);
            btnStart.Text = ReadIniValue("FormServices", "buttonStart", iniPath);
            btnStop.Text = ReadIniValue("FormServices", "buttonStop", iniPath);
            btnPause.Text = ReadIniValue("FormServices", "buttonPause", iniPath);
            btnContinue.Text = ReadIniValue("FormServices", "buttonContiune", iniPath);
            btnSetAutomatic.Text = ReadIniValue("FormServices", "buttonAutomatic", iniPath);
            btnSetDisabled.Text = ReadIniValue("FormServices", "buttonDisable", iniPath);
            btnSetManual.Text = ReadIniValue("FormServices", "buttonManual", iniPath);
            btnExportTxt.Text = ReadIniValue("FormServices", "buttonExport", iniPath);
            btnRefresh.Text = ReadIniValue("FormServices", "buttonRefresh", iniPath);
            groupBoxProcess.Text = ReadIniValue("FormServices", "groupBoxProcess", iniPath);
            groupBoxStartupSettings.Text = ReadIniValue("FormServices", "groupBoxStartupSettings", iniPath);
            groupBoxDetails.Text = ReadIniValue("FormServices", "groupboxServiceDetails", iniPath);

        }

        private void ShowServiceDetails(ServiceController service)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");


            try
            {
                service.Refresh();

                lblServiceName.Text = ReadIniValue("FormServices", "labelServiceName", iniPath) +" "+ service.ServiceName;
                lblDisplayName.Text = ReadIniValue("FormServices", "labelServiceDisplayName", iniPath) +" "+ service.DisplayName;
                lblDescription.Text = ReadIniValue("FormServices", "labelServiceDescription", iniPath) +" "+ GetServiceDescription(service.ServiceName);

                if (service.Status == ServiceControllerStatus.Running)
                {
                    lblStatusService.Text = ReadIniValue("FormServices", "labelServiceStatus", iniPath) + ReadIniValue("FormServices", "labelStatusRunning", iniPath);
                    lblStatusService.ForeColor = Color.Green;
                    lblStatusService.BackColor = Color.LightGreen;
                }
                else if (service.Status == ServiceControllerStatus.Stopped)
                {
                    lblStatusService.Text = ReadIniValue("FormServices", "labelServiceStatus", iniPath) + ReadIniValue("FormServices", "labelStatusStopped", iniPath);
                    lblStatusService.ForeColor = Color.Red;
                    lblStatusService.BackColor = Color.LightCoral;
                }
                else
                {
                    lblStatusService.Text = ReadIniValue("FormServices", "labelStatusError", iniPath) + GetServiceStatusText(service.Status);
                    lblStatusService.ForeColor = Color.Orange;
                    lblStatusService.BackColor = Color.LightYellow;
                }

                lblStartupType.Text = ReadIniValue("FormServices", "labelServiceStartupType", iniPath) +" "+ GetServiceStartType(service.ServiceName);
                lblServiceType.Text = ReadIniValue("FormServices", "labelServiceType", iniPath) +" "+  service.ServiceType.ToString();
                lblCanPause.Text = ReadIniValue("FormServices", "labelServiceCanPause", iniPath) +" "+ service.CanPauseAndContinue.ToString();
                lblCanStop.Text = ReadIniValue("FormServices", "labelServiceCanStop", iniPath) +" "+ service.CanStop.ToString();
                lblMachineName.Text = ReadIniValue("FormServices", "labelServiceMachineName", iniPath) +" "+ service.MachineName;

            }
            catch (Exception ex)
            {
                lblStatus.Text = ReadIniValue("FormServices", "labelStatusError", iniPath) + ex.Message;
                lblStatus.ForeColor = Color.DarkRed;
            }
        }


        private void UpdateButtonStates()
        {
            if (listBoxServices.SelectedItem is ServiceController selectedService)
            {
                try
                {
                    selectedService.Refresh();

                    Console.WriteLine($"Servis: {selectedService.ServiceName}");
                    Console.WriteLine($"Durum: {selectedService.Status}");
                    Console.WriteLine($"CanPause: {selectedService.CanPauseAndContinue}");

                    btnStart.Enabled = (selectedService.Status == ServiceControllerStatus.Stopped);
                    btnStop.Enabled = (selectedService.Status == ServiceControllerStatus.Running);
                    btnPause.Enabled = (selectedService.Status == ServiceControllerStatus.Running && selectedService.CanPauseAndContinue);
                    btnContinue.Enabled = (selectedService.Status == ServiceControllerStatus.Paused);

                    string currentStartType = GetServiceStartTypeRaw(selectedService.ServiceName);
                    btnSetAutomatic.Enabled = (currentStartType != "2");
                    btnSetManual.Enabled = (currentStartType != "3");
                    btnSetDisabled.Enabled = (currentStartType != "4");        
                }
                catch (Exception ex)
                {
                    
                    DisableAllButtons();
                }
            }
            else
            {
                DisableAllButtons();
            }
        }

        private void DisableAllButtons()
        {
            btnStart.Enabled = false;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            btnContinue.Enabled = false;
            btnSetAutomatic.Enabled = false;
            btnSetManual.Enabled = false;
            btnSetDisabled.Enabled = false;
        }
        //private bool IsUserAdministrator()
        //{
        //    try
        //    {
        //        var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
        //        var principal = new System.Security.Principal.WindowsPrincipal(identity);
        //        return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        private string GetServiceStatusText(ServiceControllerStatus status)
        {
            if (Properties.Settings.Default.language == "Turkish")
            {
                return status switch
                {
                    ServiceControllerStatus.Running => "Çalışıyor",
                    ServiceControllerStatus.Stopped => "Durdurulmuş",
                    ServiceControllerStatus.Paused => "Duraklatılmış",
                    ServiceControllerStatus.StopPending => "Durduruluyor",
                    ServiceControllerStatus.StartPending => "Başlatılıyor",
                    ServiceControllerStatus.PausePending => "Duraklatılıyor",
                    ServiceControllerStatus.ContinuePending => "Devam ettiriliyor",
                    _ => "Bilinmeyen"
                };
            }
            else 
            {
                return status switch
                {
                    ServiceControllerStatus.Running => "Running",
                    ServiceControllerStatus.Stopped => "Stopped",
                    ServiceControllerStatus.Paused => "Paused",
                    ServiceControllerStatus.StopPending => "Stopping",
                    ServiceControllerStatus.StartPending => "Starting",
                    ServiceControllerStatus.PausePending => "Pausing",
                    ServiceControllerStatus.ContinuePending => "Resuming",
                    _ => "Unknown"
                };
            }
           
        }

        private string GetServiceStartTypeRaw(string serviceName)
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey($@"SYSTEM\CurrentControlSet\Services\{serviceName}"))
                {
                    if (key != null)
                    {
                        var startValue = key.GetValue("Start");
                        return startValue?.ToString() ?? "";
                    }
                }
            }
            catch { }
            return "";
        }

        private string GetServiceStartType(string serviceName)
        {
            if (Properties.Settings.Default.language == "Turkish")
            {
                string startType = GetServiceStartTypeRaw(serviceName);
                return startType switch
                {
                    "0" => "Boot (Önyükleme)",
                    "1" => "System (Sistem)",
                    "2" => "Automatic (Otomatik)",
                    "3" => "Manual (Manuel)",
                    "4" => "Disabled (Devre Dışı)",
                    _ => "Bilinmeyen"
                };
            }
            else 
            {
                string startType = GetServiceStartTypeRaw(serviceName);
                return startType switch
                {
                    "0" => "Boot",
                    "1" => "System",
                    "2" => "Automatic",
                    "3" => "Manual",
                    "4" => "Disabled",
                    _ => "Unknown"

                };
            }
            
        }

        private string GetServiceDescription(string serviceName)
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey($@"SYSTEM\CurrentControlSet\Services\{serviceName}"))
                {
                    if (key != null)
                    {
                        var description = key.GetValue("Description");
                        return description?.ToString() ?? ReadIniValue("FormServices", "labelNoDescription", iniPath);
                    }
                }
                
            }
            catch { }
            return ReadIniValue("FormServices", "labelNoDescription", iniPath);
        }
        private void SetServiceStartType(string serviceName, string startType)
        {
          
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey($@"SYSTEM\CurrentControlSet\Services\{serviceName}", true))
                {
                    if (key != null)
                    {
                        key.SetValue("Start", int.Parse(startType), RegistryValueKind.DWord);
                        MessageBox.Show(ReadIniValue("FormServices", "msgServiceStartType", iniPath), ReadIniValue("FormServices", "msgSuccess", iniPath),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Bu işlem için yönetici izinleri gerekiyor!\n\nLütfen uygulamayı 'Yönetici olarak çalıştırın'.",
                    "Erişim Reddedildi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ReadIniValue("FormServices", "msgServiceStartTypeError", iniPath)}{ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadServices();
            lblServiceName.Text = "";
        }

        private void btnExportTxt_Click(object sender, EventArgs e)
        {
            ExportServicesToTxt();
        }

        private void ExportServicesToTxt()
        {
           
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

                saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
                saveFileDialog.Title = ReadIniValue("FormServices", "exportFileTitle", iniPath);
                saveFileDialog.FileName = "windows_services.txt";
                saveFileDialog.DefaultExt = "txt";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(saveFileDialog.FileName))
                        {
                            writer.WriteLine(ReadIniValue("FormServices", "exportWriteline1", iniPath));
                            writer.WriteLine("==========================");
                            writer.WriteLine($"{ReadIniValue("FormServices", "exportWriteline2", iniPath)} {DateTime.Now}");
                            writer.WriteLine(ReadIniValue("FormStartupApps", "exportText4", iniPath));
                            writer.WriteLine($"{ReadIniValue("FormServices", "exportWriteline3", iniPath)} {listBoxServices.Items.Count}");
                            writer.WriteLine();

                            foreach (ServiceController service in listBoxServices.Items)
                            {
                                writer.WriteLine($"{ReadIniValue("FormServices", "labelServiceName", iniPath)} {service.ServiceName}");
                                writer.WriteLine($"{ReadIniValue("FormServices", "labelServiceDisplayName", iniPath)} {service.DisplayName}");
                                writer.WriteLine($"{ReadIniValue("FormServices", "labelServiceDescription", iniPath)}{GetServiceDescription(service.ServiceName)}");
                                writer.WriteLine($"{ReadIniValue("FormServices", "labelServiceStatus", iniPath)} {GetServiceStatusText(service.Status)}");
                                writer.WriteLine($"{ReadIniValue("FormServices", "labelServiceStartupType", iniPath)} {GetServiceStartType(service.ServiceName)}");
                                writer.WriteLine($"{ReadIniValue("FormServices", "labelServiceType", iniPath)} {service.ServiceType}");
                                writer.WriteLine("----------------------------------------");
                            }
                        }

                        MessageBox.Show($"{ReadIniValue("FormServices", "exportSuccess", iniPath)}\n\nDosya: {saveFileDialog.FileName}",
                           ReadIniValue("FormServices", "msgSuccess", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LogManager.Logla("Servis listesi dışa aktarıldı:"+saveFileDialog.FileName+" "+DateTime.Now);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ReadIniValue("FormServices", "errorExport", iniPath)} {ex.Message}",
                            ReadIniValue("Others", "msgHeaderError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LogManager.Logla("Servis listesi dışa aktarılımadı:" + saveFileDialog.FileName + " " + DateTime.Now);
                    }
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            FilterServices();
        }

        private void FilterServices()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            string filterText = txtFilter.Text.ToLower();

            if (string.IsNullOrWhiteSpace(filterText))
            {
                LoadServices();
                return;
            }

            try
            {
                listBoxServices.Items.Clear();
                ServiceController[] allServices = ServiceController.GetServices();

                foreach (ServiceController service in allServices)
                {
                    if (service.ServiceName.ToLower().Contains(filterText) ||
                        service.DisplayName.ToLower().Contains(filterText))
                    {
                        listBoxServices.Items.Add(service);
                        listBoxServices.DisplayMember = "DisplayName";
                    }
                }

                lblStatus.Text = $"{listBoxServices.Items.Count} {ReadIniValue("FormServices", "labelFilterServices", iniPath)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ReadIniValue("FormServices", "labelFilterServicesError", iniPath)} {ex.Message}",
                    ReadIniValue("Others", "msgHeaderError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExecuteServiceAction(ServiceController service, string actionName, Action<ServiceController> action)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
         
            try
            {
                Cursor = Cursors.WaitCursor;
                action(service);

                System.Threading.Thread.Sleep(500);
                service.Refresh();

                MessageBox.Show($"{ReadIniValue("FormServices", "statusSuccess1", iniPath)} {actionName}   {ReadIniValue("FormServices", "statusSuccess2", iniPath)}", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ShowServiceDetails(service);
                UpdateButtonStates();
            }
            catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode == 5)
            {
                MessageBox.Show($"Bu işlem için yönetici izinleri gerekiyor!\n\nLütfen uygulamayı 'Yönetici olarak çalıştırın'.",
                    "Erişim Reddedildi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"{ReadIniValue("FormServices", "statusErrorMsg1", iniPath)} {actionName} {ReadIniValue("FormServices", "statusErrorMsg2", iniPath)}",
                   ReadIniValue("GetAvailability", "warn", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ReadIniValue("FormServices", "statusErrorMsg1", iniPath)}  {actionName}{ReadIniValue("FormServices", "statusErrorMsg2", iniPath)} {ex.Message}",
                    ReadIniValue("Others", "msgHeaderError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void FormServices_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);

            if (Properties.Settings.Default.AlwaysTop == true)
            {
                this.TopMost = true;
            }
            LoadLanguage();
            LoadServices();
            listBoxServices.ForeColor = Color.Black;
            listBoxServices.BackColor = Color.White;
        }

        private void listBoxServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxServices.SelectedItem is ServiceController selectedService)
            {
                ShowServiceDetails(selectedService);
                UpdateButtonStates();
            }
            else
            {
                lblServiceName.Text = "";
                DisableAllButtons();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            LogManager.Logla("Servis Başlatıldı: " + listBoxServices.SelectedItem);
            if (listBoxServices.SelectedItem is ServiceController service)
            {
                ExecuteServiceAction(service, "başlat", s => s.Start());
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            LogManager.Logla("Servis Durduruldu: " + listBoxServices.SelectedItem);
            if (listBoxServices.SelectedItem is ServiceController service)
            {
                ExecuteServiceAction(service, "durdur", s => s.Stop());
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            LogManager.Logla("Servis Duraklatıldı: " + listBoxServices.SelectedItem);
            if (listBoxServices.SelectedItem is ServiceController service)
            {
                ExecuteServiceAction(service, "duraklat", s => s.Pause());
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            LogManager.Logla("Servis Devam ettirildi: " + listBoxServices.SelectedItem);
            if (listBoxServices.SelectedItem is ServiceController service)
            {
                ExecuteServiceAction(service, "devam ettir", s => s.Continue());
            }
        }

        private void btnSetAutomatic_Click(object sender, EventArgs e)
        {
            LogManager.Logla("Servis Başlangıç durumu OTOMATİK olarak ayarlandı: " + listBoxServices.SelectedItem);
            if (listBoxServices.SelectedItem is ServiceController service)
            {
                SetServiceStartType(service.ServiceName, "2");
                ShowServiceDetails(service);
                UpdateButtonStates();
            }
        }

        private void btnSetManual_Click(object sender, EventArgs e)
        {
            LogManager.Logla("Servis Başlangıç durumu ELLE olarak ayarlandı: " + listBoxServices.SelectedItem);
            if (listBoxServices.SelectedItem is ServiceController service)
            {
                SetServiceStartType(service.ServiceName, "3");
                ShowServiceDetails(service);
                UpdateButtonStates();
            }
        }

        private void btnSetDisabled_Click(object sender, EventArgs e)
        {
            LogManager.Logla("Servis Başlangıç durumu DEVREDIŞI olarak ayarlandı: " + listBoxServices.SelectedItem);
            if (listBoxServices.SelectedItem is ServiceController service)
            {
                SetServiceStartType(service.ServiceName, "4");
                ShowServiceDetails(service);
                UpdateButtonStates();
            }
        }

        private void FormServices_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormSelectScreen frmSelect = new FormSelectScreen();
            frmSelect.Show();
            this.Hide();
        }

        private void listBoxServices_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                // Sağ tıklanan öğeyi bul
                int index = listBoxServices.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    listBoxServices.SelectedIndex = index;

                    if (listBoxServices.SelectedItem is ServiceController selectedService)
                    {
                        UpdateContextMenuStates(selectedService);
                    }
                }
            }
        }

        private void UpdateContextMenuStates(ServiceController service)
        {
            try
            {
                service.Refresh();

                // Menü öğelerini servis durumuna göre enable/disable yap
                menuStart.Enabled = (service.Status == ServiceControllerStatus.Stopped);
                menuStop.Enabled = (service.Status == ServiceControllerStatus.Running);
                menuPause.Enabled = (service.Status == ServiceControllerStatus.Running && service.CanPauseAndContinue);
                menuContinue.Enabled = (service.Status == ServiceControllerStatus.Paused);
            }
            catch
            {
                menuStart.Enabled = false;
                menuStop.Enabled = false;
                menuPause.Enabled = false;
                menuContinue.Enabled = false;
            }
        }

        // Menü click event'leri
        private void menuStart_Click(object sender, EventArgs e)
        {
            if (listBoxServices.SelectedItem is ServiceController selectedService)
            {
                ExecuteServiceAction(selectedService, "başlat", s => s.Start());
            }
        }

        private void menuStop_Click(object sender, EventArgs e)
        {
            if (listBoxServices.SelectedItem is ServiceController selectedService)
            {
                ExecuteServiceAction(selectedService, "durdur", s => s.Stop());
            }
        }

        private void menuPause_Click(object sender, EventArgs e)
        {
            if (listBoxServices.SelectedItem is ServiceController selectedService)
            {
                ExecuteServiceAction(selectedService, "duraklat", s => s.Pause());
            }
        }

        private void menuContinue_Click(object sender, EventArgs e)
        {
            if (listBoxServices.SelectedItem is ServiceController selectedService)
            {
                ExecuteServiceAction(selectedService, "devam ettir", s => s.Continue());
            }
        }

    }
}
