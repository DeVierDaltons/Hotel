using System.ServiceModel;

namespace Hotel.Logger.Contracts
{
    [ServiceContract]
    public interface ILoggerService
    {
        [OperationContract]
        void AddLogMessage(string message);
    }
}
