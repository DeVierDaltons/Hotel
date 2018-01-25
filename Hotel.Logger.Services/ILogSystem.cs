using System;

namespace Hotel.Logger.Services
{
    public interface ILogSystem
    {
        void ProcessLogMessage(DateTime time, string user, string logMessage);
    }
}
