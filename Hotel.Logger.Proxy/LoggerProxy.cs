using Hotel.Logger.Contracts;
using System.ServiceModel;

namespace Hotel.Logger.Proxy
{
    public class LoggerProxy : ClientBase<ILoggerService>, ILoggerService
    {

        public void AddLogMessage(string user, string message)
        {
            Channel.AddLogMessage(user, message);
        }

        public void AddLogMessageBooking(string user, string message, Hotel.Data.Booking booking)
        {
            Channel.AddLogMessageBooking(user, message, booking);
        }

        public void AddLogMessageGuest(string user, string message, Hotel.Data.Guest guest)
        {
            Channel.AddLogMessageGuest(user, message, guest);
        }

        public void AddLogMessagerRoom(string user, string message, Hotel.Data.Room room)
        {
            Channel.AddLogMessagerRoom(user, message, room);
        }
    }
}