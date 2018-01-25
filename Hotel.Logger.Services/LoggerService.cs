using Hotel.Logger.Contracts;
using System;
using System.Collections.Generic;

namespace Hotel.Logger.Services
{
    public class LoggerService : ILoggerService, ILogger
    {

        private List<ILogSystem> _logSystems;

        public LoggerService()
        {
            _logSystems = new List<ILogSystem>();

            var fileLogger = new FileLogger("App.log");
            this.RegisterLogSystem(fileLogger);
        }

        public void RegisterLogSystem(ILogSystem logSystem)
        {
            if (!_logSystems.Contains(logSystem))
            {
                _logSystems.Add(logSystem);
            }
        }

        public void AddLogMessage(string message)
        {
            foreach (ILogSystem logSystem in _logSystems)
            {
                logSystem.ProcessLogMessage(DateTime.Now, Environment.UserName, message);
            }
        }
    }
}

