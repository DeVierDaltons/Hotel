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
            ServiceHost guestCallbackhost = new ServiceHost(typeof(GuestCallbackService));
            ServiceHost roomCallbackHost = new ServiceHost(typeof(RoomCallbackService));
            ServiceHost bookingCallbackHost = new ServiceHost(typeof(BookingCallbackService));
            hotelServiceHost.Open();
            guestCallbackhost.Open();
            roomCallbackHost.Open();
            bookingCallbackHost.Open();
            Console.WriteLine("Services: Callback (guests, rooms and bookings) and hotel started. Press [Enter] to quit.");
            Console.ReadLine();
            hotelServiceHost.Close();
            guestCallbackhost.Close();
            roomCallbackHost.Close();
            bookingCallbackHost.Close();
        }
    }
}
