using System;
using System.Collections.Generic;

namespace Hotel.Common
{
    public class Logger : ILogger
    {

        private List<ILogSystem> _logSystems;

        public Logger()
        {
            _logSystems = new List<ILogSystem>();
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