using Hotel.Logger.Contracts;
using System.ServiceModel;

namespace Hotel.Logger.Proxy
{
    public class LoggerProxy : ClientBase<ILoggerService>, ILoggerService
    {
        public void AddLogMessage(string message)
        {
            Channel.AddLogMessage(message);
        }
    }
}