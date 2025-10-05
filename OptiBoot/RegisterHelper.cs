using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
namespace OptiBoot
{
    internal class RegisterHelper
    {
        [DllImport("kernel32", EntryPoint = "GetShortPathNameA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int GetShortPathName([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszLongPath, StringBuilder lpszShortPath, int cchBuffer);

        public static void ExportKey(string sRegKeyPath, string sfile)
        {
            string text1 = "\"" + sRegKeyPath + "\"";
            FileAppend(sfile, "");
            ExecuteRegistry("regedit.exe", "/E " + GetDosPath(sfile) + " " + text1, ProcessWindowStyle.Normal);
        }

        public static void FileAppend(string path, string text) //Append the Path Of The Key  
        {
            StreamWriter writer1 = File.AppendText(path);
            writer1.Write(text);
            writer1.Close();
        }

        public static string GetDosPath(string path) //this will Return the Dos Command Path  
        {
            return GetShortFileName(path);
        }

        public static string GetShortFileName(string path)
        {
            StringBuilder builder1 = new StringBuilder(0x400);
            int num1 = GetShortPathName(ref path, builder1, builder1.Capacity);
            return builder1.ToString(0, num1);
        }


        public static string ExecuteRegistry(string path, string arguments, ProcessWindowStyle style)
        {


            string text1 = "";
            Process process1 = new Process();
            try
            {
                process1.StartInfo.FileName = path;
                process1.StartInfo.UseShellExecute = false;
                process1 = Process.Start(path, arguments);
                process1.WaitForExit();
            }
            finally
            {
                process1.Dispose();
            }
            return text1;
        }    
    }
}