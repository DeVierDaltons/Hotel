using Hotel.Logger.Services;
using System;
using System.ServiceModel;

namespace Hotel.Logger.Host
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostLogger = new ServiceHost(typeof(LoggerService));
            hostLogger.Open();

            Console.WriteLine("Logger service started. Press [Enter] to quit.");
            Console.ReadLine();

            hostLogger.Close();
        }
    }
}
