using ClosedXML.Excel;
using Microsoft.Win32.TaskScheduler;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptiBoot
{
    public partial class FormTaskSchuelder : Form
    {
        public class Gorev
        {
            public string Adi { get; set; }
            public string Aciklama { get; set; }
            public DateTime Zaman { get; set; }
            public string Durum { get; set; }
            public string Tekrar { get; set; }
            public string EylemTuru { get; set; }
            public string ProgramYolu { get; set; }
            public string Argumanlar { get; set; }
            public string BaslangicYolu { get; set; }

            public Gorev()
            {
                Durum = "Bekliyor";
                Tekrar = "Yok";
                Zaman = DateTime.Now.AddHours(1);
                EylemTuru = "ProgramBaşlat";
            }
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

        private void ExportToExcel(DataGridView dataGridView, string fileName)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Sheet1");

                    for (int i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = dataGridView.Columns[i].HeaderText;
                        worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                    }

                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        if (dataGridView.Rows[i].IsNewRow) continue;

                        for (int j = 0; j < dataGridView.Columns.Count; j++)
                        {
                            var cellValue = dataGridView.Rows[i].Cells[j].Value;
                            worksheet.Cell(i + 2, j + 1).Value = cellValue?.ToString() ?? "";
                        }
                    }

                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(fileName);
                }

                MessageBox.Show(ReadIniValue("FormTaskSchuelder", "msgSuccessExportExcel", iniPath), ReadIniValue("Others", "msgHeaderInfo", iniPath),
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ReadIniValue("FormServices", "msgError", iniPath)} {ex.Message}", ReadIniValue("Others", "msgHeaderError", iniPath),
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Gorev> gorevler;
        private int sonId = 0;

        public FormTaskSchuelder()
        {
            InitializeComponent();
            WindowsGorevleriniYukle();
            dataGridView1.Columns[2].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm".ToString();

        }
        public void LoadLanguage()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");
            this.Text = ReadIniValue("FormTaskSchuelder", "caption", iniPath);
            buttonDeleteTask.Text = ReadIniValue("FormTaskSchuelder", "buttonDeleteTask", iniPath);
            buttonTaskEdit.Text = ReadIniValue("FormTaskSchuelder", "buttonEditTask", iniPath);
            buttonTaskNew.Text = ReadIniValue("FormTaskSchuelder", "buttonNewTask", iniPath);
            buttonRefresh.Text = ReadIniValue("FormTaskSchuelder", "buttonRefresh", iniPath);
            buttonExport.Text = ReadIniValue("FormServices", "buttonExport", iniPath);
            labelSearch.Text = ReadIniValue("FormTaskSchuelder", "labelSearch", iniPath);
            dataGridView1.Columns[0].HeaderText = ReadIniValue("FormTaskSchuelder", "dtwName", iniPath);
            dataGridView1.Columns[1].HeaderText = ReadIniValue("FormTaskSchuelder", "dtwDescription", iniPath);
            dataGridView1.Columns[2].HeaderText = ReadIniValue("FormTaskSchuelder", "dtwDateTime", iniPath);
            dataGridView1.Columns[3].HeaderText = ReadIniValue("FormTaskSchuelder", "dtwStatus", iniPath);
            dataGridView1.Columns[4].HeaderText = ReadIniValue("FormTaskSchuelder", "dtwRepeat", iniPath);
        }
        private void WindowsGorevleriniYukle()
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            try
            {
                dataGridView1.Rows.Clear();

                // Task Scheduler'a bağlan
                using (var taskService = new TaskService())
                {
                    // Tüm görevleri al - Regex kullanarak veya parametresiz
                    var allTasks = taskService.FindAllTasks(new System.Text.RegularExpressions.Regex(".*")); // Parametresiz versiyon

                    // VEYA belirli bir pattern ile:
                    // var allTasks = taskService.FindAllTasks(new System.Text.RegularExpressions.Regex(".*"));

                    foreach (var task in allTasks)
                    {
                        try
                        {
                            string gorevAdi = task.Name;
                            string aciklama = task.Definition.RegistrationInfo.Description ?? "";
                            string durum = task.State.ToString();
                            string sonuc = "0x" + task.LastTaskResult.ToString("X");
                            DateTime zaman = DateTime.MinValue;
                            string tekrar = "Yok";

                            // Tetikleyici bilgilerini al
                            if (task.Definition.Triggers.Count > 0)
                            {
                                var trigger = task.Definition.Triggers[0];

                                if (trigger is DailyTrigger)
                                    tekrar = ReadIniValue("FormTaskSchuelder", "triggerRepeatDaily", iniPath);
                                else if (trigger is WeeklyTrigger)
                                    tekrar = ReadIniValue("FormTaskSchuelder", "triggerRepeatWeekly", iniPath);
                                else if (trigger is MonthlyTrigger)
                                    tekrar = ReadIniValue("FormTaskSchuelder", "triggerRepeatMonthly", iniPath);
                                else if (trigger is TimeTrigger)
                                    tekrar = ReadIniValue("FormTaskSchuelder", "triggerRepeatOnce", iniPath);
                                else
                                    tekrar = trigger.TriggerType.ToString();

                                zaman = trigger.StartBoundary;
                            }

                            // Son çalışma zamanı geçerliyse onu kullan
                            if (task.LastRunTime.Year > 1900)
                            {
                                zaman = task.LastRunTime;
                            }

                            string zamanStr = (zaman > DateTime.MinValue) ?
                                zaman.ToString("dd.MM.yyyy HH:mm") : "Belirlenmemiş";

                            dataGridView1.Rows.Add(
                                gorevAdi,
                                aciklama,
                                zamanStr,
                                durum,
                                tekrar,
                                sonuc
                            );
                        }
                        catch (Exception ex)
                        {
                            dataGridView1.Rows.Add(
                                task.Name,
                                "Hata: " + ex.Message,
                                "",
                                "Hata",
                                "",
                                ""
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ReadIniValue("FormTaskSchuelder", "msgErrorLoadTasks", iniPath)} {ex.Message}",
                    ReadIniValue("Others", "msgHeaderError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            WindowsGorevleriniYukle();
            MessageBox.Show(ReadIniValue("FormTaskSchuelder", "msgRefreshTask", iniPath), ReadIniValue("Others", "msgHeaderInfo", iniPath),
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonDeleteTask_Click(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(ReadIniValue("FormTaskSchuelder", "msgSelectToDeleteTask", iniPath), ReadIniValue("Others", "msgHeaderWarn", iniPath),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridView1.SelectedRows[0];
            var taskName = selectedRow.Cells[0].Value?.ToString();

            if (string.IsNullOrEmpty(taskName))
                return;

            try
            {
                var dialogResult = MessageBox.Show($"'{taskName}' {ReadIniValue("FormTaskSchuelder", "msgDeleteTaskConfirm1", iniPath)}\n\n{ReadIniValue("FormTaskSchuelder", "msgDeleteTaskConfirm2", iniPath)}",
                    ReadIniValue("Others", "msgHeaderConfirm", iniPath), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    using (var taskService = new TaskService())
                    {
                        // Görevi root folder'dan sil
                        taskService.RootFolder.DeleteTask(taskName);
                    }

                    WindowsGorevleriniYukle();
                    MessageBox.Show(ReadIniValue("FormTaskSchuelder", "msgDeleteTaskSuccess", iniPath), ReadIniValue("Others", "msgHeaderInfo", iniPath),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogManager.Logla("Task Deleted: " + taskName + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ReadIniValue("FormTaskSchuelder", "msgDeleteTaskError", iniPath)} {ex.Message}",
                    ReadIniValue("Others", "msgHeaderError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.Logla("An error occurred while deleting the task: " + ex.Message + DateTime.Now);
            }
        }

        private void buttonTaskEdit_Click(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(ReadIniValue("FormTaskSchuelder", "msgSelectToEditTask", iniPath), ReadIniValue("Others", "msgHeaderWarn", iniPath),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var gorevAdi = selectedRow.Cells[0].Value?.ToString();

                if (string.IsNullOrEmpty(gorevAdi))
                    return;

                // Mevcut görevin detaylarını almak için TaskService kullan
                Gorev mevcutGorev = null;

                using (var taskService = new TaskService())
                {
                    var task = taskService.GetTask(gorevAdi);
                    if (task != null)
                    {
                        mevcutGorev = new Gorev
                        {
                            Adi = task.Name,
                            Aciklama = task.Definition.RegistrationInfo.Description ?? "",
                            Durum = task.Enabled ? ReadIniValue("FormTaskSchuelder", "taskEnabled", iniPath) : ReadIniValue("FormTaskSchuelder", "taskDisabled", iniPath),
                            Zaman = task.NextRunTime,
                            ProgramYolu = task.Definition.Actions.Count > 0 ?
                                (task.Definition.Actions[0] as ExecAction)?.Path : ""
                        };
                    }
                }

                if (mevcutGorev == null)
                {
                    mevcutGorev = new Gorev
                    {
                        Adi = gorevAdi,
                        Aciklama = selectedRow.Cells[1].Value?.ToString() ?? "",
                        Zaman = DateTime.TryParse(selectedRow.Cells[2].Value?.ToString(), out var zaman) ? zaman : DateTime.Now,
                        Durum = selectedRow.Cells[3].Value?.ToString() ?? ReadIniValue("FormTaskSchuelder", "taskDisabled", iniPath)
                    };
                }

                using (var gorevForm = new FormNewTask(mevcutGorev))
                {
                    if (gorevForm.ShowDialog() == DialogResult.OK)
                    {
                        WindowsGorevleriniYukle();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ReadIniValue("FormTaskSchuelder", "msgEditTaskError", iniPath)} {ex.Message}",
                    ReadIniValue("Others", "msgHeaderError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonTaskNew_Click(object sender, EventArgs e)
        {
            string iniPath = Path.Combine(Application.StartupPath, "data", "language", Properties.Settings.Default.language + ".ini");

            try
            {
                using (var gorevForm = new FormNewTask())
                {
                    if (gorevForm.ShowDialog() == DialogResult.OK)
                    {
                        // Yeni görev eklendi, listeyi yenile
                        WindowsGorevleriniYukle();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ReadIniValue("FormTaskSchuelder", "msgNewTaskError", iniPath)} {ex.Message}",
                    ReadIniValue("Others", "msgHeaderError", iniPath), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                buttonTaskEdit.PerformClick();
            }
        }

        private void FormTaskSchuelder_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);
            if (Properties.Settings.Default.Theme == "Dark")
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.Black;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
                dataGridView1.BackgroundColor = Color.Black;
                dataGridView1.EnableHeadersVisualStyles = false; // stilin kendi temasını devre dışı bırakır
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // yazı rengi
                dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Black;
                dataGridView1.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            }
            LoadLanguage();
            
            if (Properties.Settings.Default.AlwaysTop == true)
            {
                this.TopMost = true;
            }
        }

        private void FormTaskSchuelder_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormSelectScreen formSelectScreen = new FormSelectScreen();
            formSelectScreen.Show();
            this.Hide();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string aramaMetni = textBoxSearch.Text.ToLower();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    bool eslesme = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && cell.Value.ToString().ToLower().Contains(aramaMetni))
                        {
                            eslesme = true;
                            break;
                        }
                    }
                    row.Visible = eslesme;
                }
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Export Excel";
                saveFileDialog.FileName = "Tasks.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToExcel(dataGridView1, saveFileDialog.FileName);
                }
            }
        }
    }
}