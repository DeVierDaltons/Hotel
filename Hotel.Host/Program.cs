using Hotel.Services;
using Hotel.Services.Repository;
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
            NHibernateHelper.CreateDatabaseIfNeeded();
            ServiceHost hotelServiceHost = new ServiceHost(typeof(HotelService));
            hotelServiceHost.Open();
           
            Console.WriteLine("Services started. Press [Enter] to quit.");
            Console.ReadLine();

            hotelServiceHost.Close();
        }
    }
}
