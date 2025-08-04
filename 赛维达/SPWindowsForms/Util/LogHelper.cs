using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.Util
{
    public class LogHelper
    {
        private readonly string _logDirectory;
        private string _currentLogFile;
        private DateTime _lastWriteTime;

        public LogHelper(string logDirectory)
        {
            _logDirectory = logDirectory;
            _lastWriteTime = DateTime.MinValue;
            _currentLogFile = GetLogFileName(DateTime.Now);

            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }
        }

        private string GetLogFileName(DateTime dateTime)
        {
            return Path.Combine(_logDirectory, $"log_{dateTime:yyyyMMdd}.txt");
        }

        private void EnsureLogFile()
        {
            var now = DateTime.Now;
            if (_lastWriteTime.Date != now.Date)
            {
                _currentLogFile = GetLogFileName(now);
                _lastWriteTime = now;
            }
        }

        public void WriteLog(string message)
        {
            try
            {
                EnsureLogFile();

                var logEntry = $"{DateTime.Now}: {message}{Environment.NewLine}";
                File.AppendAllText(_currentLogFile, logEntry, Encoding.UTF8);
            }
            catch(Exception ex)
            {
              //  Work.ShowErrorMessage("WriteLog" + ex.Message);
            }
        }
    }
}
