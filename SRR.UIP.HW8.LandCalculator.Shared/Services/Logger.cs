using SRR.UIP.HW8.LandCalculator.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRR.UIP.HW8.LandCalculator.Shared.Services
{
    public class Logger : ILogger
    {
        public List<ILogStorage> LogStorages { get; } = new List<ILogStorage>();

        public LogLevels LogLevel { get; set; }

        private void PrintMessage(string messageType, string message, LogLevels currentLogLevel)
        {
            if (this.LogLevel > currentLogLevel)
            {
                return;
            }

            foreach (ILogStorage logStorage in LogStorages)
            {
                logStorage.PrintMessage($"{DateTime.Now}: {messageType}: {message}");
            }
        }

        public void Debug(string message)
        {
            PrintMessage("DEBUG", message, LogLevels.DEBUG);
        }

        public void Error(string message)
        {
            PrintMessage("Error", message, LogLevels.ERROR);
        }

        public void Fatal(string message)
        {
            PrintMessage("Fatal", message, LogLevels.FATAL);
        }

        public void Info(string message)
        {
            PrintMessage("Info", message, LogLevels.INFO);
        }

        public void Warn(string message)
        {
            PrintMessage("Warn", message, LogLevels.WARNING);
        }
    }

    public enum LogLevels
    {
        INFO = 1,
        DEBUG = 2,
        WARNING = 3,
        ERROR = 4,
        FATAL = 5
    }

}
