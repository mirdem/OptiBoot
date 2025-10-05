using Microsoft.Win32.TaskScheduler;
using Mono.Unix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OptiBoot.FormTaskSchuelder;

namespace OptiBoot
{
    public partial class FormNewTask : Form
    {
        public Gorev Gorev { get; set; }
        private bool yeniGorevModu;
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



        public FormNewTask(Gorev gorev = null)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            Gorev = gorev;
            InitializeComponent();
            if (gorev == null)
            {
                Gorev = new Gorev();
                yeniGorevModu = true;
                this.Text = ReadIniValue("FormNewTask", "caption", iniPath);
                labelHeader.Text = ReadIniValue("FormNewTask", "caption", iniPath);
            }
            else
            {
                Gorev = gorev;
                yeniGorevModu = false;
                this.Text = ReadIniValue("FormNewTask", "caption2", iniPath);
                labelHeader.Text = ReadIniValue("FormNewTask", "caption2", iniPath);

                FormuDoldur();
            }
        }

        private void FormuDoldur()
        {
            textBoxTaskName.Text = Gorev.Adi;
            textBoxTaskDescription.Text = Gorev.Aciklama;
            dateTimePicker1.Value = Gorev.Zaman;

            if (comboBoxStatus.Items.Contains(Gorev.Durum))
                comboBoxStatus.SelectedItem = Gorev.Durum;

            if (comboBoxRepeat.Items.Contains(Gorev.Tekrar))
                comboBoxRepeat.SelectedItem = Gorev.Tekrar;

            if (comboBoxEylemTuru.Items.Contains(Gorev.EylemTuru))
                comboBoxEylemTuru.SelectedItem = Gorev.EylemTuru;

            txtProgramYolu.Text = Gorev.ProgramYolu;
            txtArgumanlar.Text = Gorev.Argumanlar;
            txtBaslangicYolu.Text = Gorev.BaslangicYolu;
        }

        private void buttonSaveTask_Click(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            if (!FormuKontrolEt())
                return;

            try
            {
                Gorev.Adi = textBoxTaskName.Text.Trim();
                Gorev.Aciklama = textBoxTaskDescription.Text.Trim();
                Gorev.Zaman = dateTimePicker1.Value;
                Gorev.Durum = comboBoxStatus.SelectedItem?.ToString() ?? ReadIniValue("FormTaskSchuelder", "taskEnabled", iniPath);
                Gorev.Tekrar = comboBoxRepeat.SelectedItem?.ToString() ?? "Yok";
                Gorev.EylemTuru = comboBoxEylemTuru.SelectedItem?.ToString() ?? ReadIniValue("FormNewTask", "caseActionStartApp", iniPath);
                Gorev.ProgramYolu = txtProgramYolu.Text;
                Gorev.Argumanlar = txtArgumanlar.Text;
                Gorev.BaslangicYolu = txtBaslangicYolu.Text;

                // Windows Görev Zamanlayıcı'ya kaydet
                if (GoreviKaydet())
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                labelSonuc.Text = $"Hata: {ex.Message}";
            }
        }
        private bool FormuKontrolEt()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            if (string.IsNullOrWhiteSpace(textBoxTaskName.Text))
            {
                labelSonuc.Text = ReadIniValue("FormNewTask", "labelStatusNoEmpty", iniPath);
                textBoxTaskName.Focus();
                return false;
            }

            if (dateTimePicker1.Value < DateTime.Now)
            {
                labelSonuc.Text = ReadIniValue("FormNewTask", "labelStatusErrorDate", iniPath);
                dateTimePicker1.Focus();
                return false;
            }

            if (comboBoxEylemTuru.SelectedItem?.ToString() == ReadIniValue("FormNewTask", "caseActionStartApp", iniPath) &&
                string.IsNullOrWhiteSpace(txtProgramYolu.Text))
            {
                labelSonuc.Text = ReadIniValue("FormNewTask", "labelStatusAppErrorPath", iniPath); ;
                txtProgramYolu.Focus();
                return false;
            }

            labelSonuc.Text = "";
            return true;
        }


        private bool GoreviKaydet()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            try
            {
                using (var taskService = new TaskService())
                {
                    var taskDefinition = taskService.NewTask();
                    taskDefinition.RegistrationInfo.Description = Gorev.Aciklama;
                    taskDefinition.RegistrationInfo.Date = DateTime.Now;

                    // Durum ayarı
                    taskDefinition.Settings.Enabled = (Gorev.Durum == ReadIniValue("FormNewTask", "labelStatusNoEmpty", iniPath));
                    taskDefinition.Settings.AllowDemandStart = true;

                    // Tetikleyici oluştur
                    TimeTrigger trigger = new TimeTrigger(Gorev.Zaman);

                    // Tekrar ayarları
                    if (Gorev.Tekrar == ReadIniValue("FormNewTask", "triggerRepeatDaily", iniPath))
                    {
                        trigger.Repetition.Interval = TimeSpan.FromDays(1);
                    }
                    else if (Gorev.Tekrar == ReadIniValue("FormNewTask", "triggerRepeatWeekly", iniPath))
                    {
                        trigger.Repetition.Interval = TimeSpan.FromDays(7);
                    }
                    else if (Gorev.Tekrar == ReadIniValue("FormNewTask", "triggerRepeatMonthly", iniPath))
                    {
                        // Basit aylık tekrar
                        trigger.Repetition.Interval = TimeSpan.FromDays(30);
                    }

                    taskDefinition.Triggers.Add(trigger);

                    // Eylem oluştur
                    ExecAction action = null;

                    switch (Gorev.EylemTuru)
                    {
                        case "Program Başlat":
                            action = new ExecAction(Gorev.ProgramYolu, Gorev.Argumanlar, Gorev.BaslangicYolu);
                            break;
                        case "Mesaj Göster":
                            action = new ExecAction("msg.exe", $"* {Gorev.Argumanlar}", null);
                            break;
                        case "E-posta Gönder":
                            // Basit e-posta eylemi (powershell kullanarak)
                            string powerShellCommand = $@"Send-MailMessage -To 'alici@example.com' -Subject '{Gorev.Adi}' -Body '{Gorev.Argumanlar}' -SmtpServer 'smtp.example.com'";
                            action = new ExecAction("powershell.exe", $"-Command \"{powerShellCommand}\"", null);
                            break;
                        default:
                            action = new ExecAction(Gorev.ProgramYolu, Gorev.Argumanlar, Gorev.BaslangicYolu);
                            break;
                    }

                    if (action != null)
                    {
                        taskDefinition.Actions.Add(action);
                    }

                    // Görevi kaydet
                    taskService.RootFolder.RegisterTaskDefinition(Gorev.Adi, taskDefinition);

                    MessageBox.Show($"{ReadIniValue("FormNewTask", "msgSuccessTask1", iniPath)} {(yeniGorevModu ? ReadIniValue("FormNewTask", "msgSuccessTask2", iniPath) : "güncellendi")}!",
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ReadIniValue("FormNewTask", "msgErrorTask", iniPath)} {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private void comboBoxEylemTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Eylem türüne göre kontrolleri güncelle
            string eylemTuru = comboBoxEylemTuru.SelectedItem?.ToString();

            switch (eylemTuru)
            {
                case "Program Başlat":
                    txtProgramYolu.Enabled = true;
                    btnProgramSec.Enabled = true;
                    txtArgumanlar.Enabled = true;
                    txtBaslangicYolu.Enabled = true;
                    break;
                case "Mesaj Göster":
                    txtProgramYolu.Text = "msg.exe";
                    txtProgramYolu.Enabled = false;
                    btnProgramSec.Enabled = false;
                    txtArgumanlar.Enabled = true;
                    txtBaslangicYolu.Enabled = false;
                    break;
                case "E-posta Gönder":
                    // Basit e-posta eylemi
                    txtProgramYolu.Enabled = false;
                    btnProgramSec.Enabled = false;
                    txtArgumanlar.Enabled = true;
                    txtBaslangicYolu.Enabled = false;
                    break;
            }
        }

        private void btnProgramSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Executable files (*.exe)|*.exe|Batch files (*.bat)|*.bat|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtProgramYolu.Text = openFileDialog.FileName;

                    // Başlangıç yolunu otomatik olarak ayarla
                    txtBaslangicYolu.Text = Path.GetDirectoryName(openFileDialog.FileName);
                }
            }
        }

        private void FormNewTask_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.AlwaysTop == true)
            {
                this.TopMost = true;
            }

            ThemeManager.ApplyTheme(this);

            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            comboBoxStatus.Items.Add(ReadIniValue("FormTaskSchuelder", "taskEnabled", iniPath));
            comboBoxStatus.Items.Add(ReadIniValue("FormTaskSchuelder", "taskDisabled", iniPath));

            comboBoxRepeat.Items.Add(ReadIniValue("FormTaskSchuelder", "triggerRepeatNo", iniPath));
            comboBoxRepeat.Items.Add(ReadIniValue("FormTaskSchuelder", "triggerRepeatDaily", iniPath));
            comboBoxRepeat.Items.Add(ReadIniValue("FormTaskSchuelder", "triggerRepeatWeekly", iniPath));
            comboBoxRepeat.Items.Add(ReadIniValue("FormTaskSchuelder", "triggerRepeatMonthly", iniPath));
            comboBoxRepeat.Items.Add(ReadIniValue("FormTaskSchuelder", "triggerRepeatOnce", iniPath));
            
            comboBoxEylemTuru.Items.Add(ReadIniValue("FormNewTask", "caseActionStartApp", iniPath));
            comboBoxEylemTuru.Items.Add(ReadIniValue("FormNewTask", "caseActionSendMessage", iniPath));
            comboBoxEylemTuru.Items.Add(ReadIniValue("FormNewTask", "caseActionSendMail", iniPath));


        }
    }
}
