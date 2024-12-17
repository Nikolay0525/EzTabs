using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Presentation.Services.AppServices
{
    public static class Logger
    {
        private static readonly string logFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "AppLogs", "log.txt"
        );

        static Logger()
        {
            string? directory = Path.GetDirectoryName(logFilePath);
            if (directory != null && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public static void Log(string message, LogLevel level = LogLevel.Info)
        {
            string logMessage = $"{DateTime.Now:G} [{level}] {message}";
            try
            {
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
            catch
            {
                Console.WriteLine("Error writing to log file.");
            }
        }

        public static void LogException(Exception ex)
        {
            Log($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}", LogLevel.Error);
        }
    }

    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }
}
