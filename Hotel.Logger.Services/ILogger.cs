
using Hotel.Data;

namespace Hotel.Logger.Services
{
    public interface ILogger
    {
        void AddLogMessage(string user, string message);

        void AddLogMessageGuest(string user, string message, Guest guest);

        void AddLogMessagerRoom(string user, string message, Room room);

        void AddLogMessageBooking(string user, string message, Booking booking);
    }
}
