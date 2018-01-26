using Hotel.Data;
using System;

namespace Hotel.Logger.Services
{
    public interface ILogSystem
    {
        void ProcessLogMessage(DateTime time, string user, string logMessage);

        void ProcessLogMessageGuest(DateTime time, string user, string logMessage, Guest guest);

        void ProcessLogMessageRoom(DateTime time, string user, string logMessage, Room guest);

        void ProcessLogMessageBooking(DateTime time, string user, string logMessage, Booking guest);
    }
}
