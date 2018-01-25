using System;
using System.IO;

namespace Hotel.Logger.Services
{
    public class FileLogger : ILogSystem
    {
        private string _fileName;
        private StreamWriter _logFile;

        public string FileName
        {
            get
            {
                return _fileName;
            }
        }

        public FileLogger(string fileName)
        {
            _fileName = fileName;
        }

        public void Init()
        {
            _logFile = new StreamWriter(_fileName, append: true);
        }

        public void Terminate()
        {
            _logFile.Close();
        }

        public void ProcessLogMessage(DateTime time, string user, string logMessage)
        {
            this.Init();
            _logFile.WriteLine(string.Format("--> TIME: {0}, USER: {1}, MESSAGE: {2}", time.ToString("dd/MM/yyyy HH:mm:ss"), user, logMessage));
            this.Terminate();
        }
    }
}
