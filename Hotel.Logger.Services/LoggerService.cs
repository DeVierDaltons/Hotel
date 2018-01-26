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

        public void AddLogMessage(string user, string message)
        {
            foreach (ILogSystem logSystem in _logSystems)
            {
                logSystem.ProcessLogMessage(DateTime.Now, user, message);
            }
        }

        public void AddLogMessageGuest(string user, string message, Hotel.Data.Guest guest)
        {
            foreach (ILogSystem logSystem in _logSystems)
            {
                logSystem.ProcessLogMessageGuest(DateTime.Now, user, message, guest);
            }
        }

        public void AddLogMessagerRoom(string user, string message, Hotel.Data.Room room)
        {
            foreach (ILogSystem logSystem in _logSystems)
            {
                logSystem.ProcessLogMessageRoom(DateTime.Now, user, message, room);
            }
        }

        public void AddLogMessageBooking(string user, string message, Hotel.Data.Booking booking)
        {
            foreach (ILogSystem logSystem in _logSystems)
            {
                logSystem.ProcessLogMessageBooking(DateTime.Now, user, message, booking);
            }
        }
    }
}

