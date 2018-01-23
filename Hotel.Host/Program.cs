using Hotel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hotelServiceHost = new ServiceHost(typeof(HotelService));
            hotelServiceHost.Open();
           
            Console.WriteLine("Services started. Press [Enter] to quit.");
            Console.ReadLine();

            hotelServiceHost.Close();
        }
    }
}
