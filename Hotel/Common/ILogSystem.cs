using System;

namespace Hotel.Common
{
    public interface ILogSystem
    {
        void ProcessLogMessage(DateTime time, string user, string logMessage);
    }
}
