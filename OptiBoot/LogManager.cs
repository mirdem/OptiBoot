using System;
using System.IO;
using System.Windows.Forms;

namespace OptiBoot
{
    public static class LogManager
    {
        private static readonly string LogDosyaYolu = Path.Combine(Application.StartupPath, "log.txt");

        public static void Logla(string mesaj, string kullanici = "Kullanıcı")
        {
            try
            {
                string logMesaji = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - [{kullanici}] - {mesaj}";

                // Log dosyasına ekle (append)
                using (StreamWriter writer = new StreamWriter(LogDosyaYolu, true))
                {
                    writer.WriteLine(logMesaji);
                }

                Console.WriteLine(logMesaji); // Debug için konsola da yaz
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Log yazılırken hata: {ex.Message}");
            }
        }

        public static void Temizle()
        {
            try
            {
                if (File.Exists(LogDosyaYolu))
                {
                    File.Delete(LogDosyaYolu);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Log temizlenirken hata: {ex.Message}");
            }
        }

        public static string LoglariOku()
        {
            try
            {
                if (File.Exists(LogDosyaYolu))
                {
                    return File.ReadAllText(LogDosyaYolu);
                }
                return "Henüz log bulunmamaktadır.";
            }
            catch (Exception ex)
            {
                return $"Log okunurken hata: {ex.Message}";
            }
        }
    }
}