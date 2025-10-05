using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace OptiBoot
{
    public partial class FormCheckAdmin : Form
    {
        public FormCheckAdmin()
        {
            InitializeComponent();
            CheckAdminStatus();
        }

        private void CheckAdminStatus()
        {
            if (!IsRunningAsAdmin())
            {
                // Yönetici olarak çalıştırılmamışsa uyarı göster
                DialogResult result = MessageBox.Show(
                    "Bu uygulama yönetici haklarıyla çalıştırılmalıdır!\n\n" +
                    "Yönetici olarak yeniden başlatmak ister misiniz?",
                    "Yönetici Hakları Gerekli",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Yeniden yönetici olarak başlat
                    RestartAsAdmin();
                }

                // Uygulamayı kapat
                Application.Exit();
            }
            else
            {
                // Yönetici olarak çalışıyorsa Form2'yi aç ve mevcut formu kapat
                OpenForm2();
            }
        }

        private bool IsRunningAsAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void RestartAsAdmin()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Application.ExecutablePath;
            startInfo.Verb = "runas"; // Yönetici olarak çalıştır

            try
            {
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yeniden başlatma hatası: {ex.Message}",
                                "Hata",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void OpenForm2()
        {
            // Form2'yi aç ve mevcut formu kapat
            FormSelectScreen form2 = new FormSelectScreen();
            form2.Show();
            this.Hide(); // Mevcut formu gizle
        }
    }
}