using Hotel.Data;
using System.ServiceModel;

namespace Hotel.Logger.Contracts
{
    [ServiceContract]
    public interface ILoggerService
    {
        [OperationContract]
        void AddLogMessage(string user, string message);

        [OperationContract]
        void AddLogMessageGuest(string user, string message, Guest guest);

        [OperationContract]
        void AddLogMessagerRoom(string user, string message, Room room);

        [OperationContract]
        void AddLogMessageBooking(string user, string message, Booking booking);
    }
}
