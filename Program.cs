using System;
using System.IO;
using System.Windows.Forms;

namespace TTMPLReplacer
{
    static class Program
    {
        public const string VersionNumber = "1.3.0";
        
        public static string BasePath => AppDomain.CurrentDomain.BaseDirectory;

        public static string ConvertedFolder => Path.Combine(BasePath, "Converted");

        private static bool _startedLog;

        private static string LogFile => Path.Combine(BasePath, "Output.log");

        public static void Log(string logText)
        {
            Console.WriteLine(logText);
            if (!_startedLog && File.Exists(LogFile))
            {
                File.Delete(LogFile);
                _startedLog = true;
            }

            using StreamWriter sw = File.AppendText(LogFile);
            sw.WriteLine(logText);
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ReplacerForm());
        }
    }
}