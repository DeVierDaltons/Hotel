using Hotel.Data;
using System;
using System.IO;

namespace Hotel.Logger.Services
{
    public class FileLogger : ILogSystem
    {
        private string _fileName;
        private StreamWriter _logFile;

        public string FileName
        {
            get
            {
                return _fileName;
            }
        }

        public FileLogger(string fileName)
        {
            _fileName = fileName;
        }

        public void Init()
        {
            _logFile = new StreamWriter(_fileName, append: true);
        }

        public void Terminate()
        {
            _logFile.Close();
        }

        public void ProcessLogMessage(DateTime time, string user, string logMessage)
        {
            this.Init();
            _logFile.WriteLine(string.Format("--> TIME: {0}, USER: {1}, MESSAGE: {2}", time.ToString("dd/MM/yyyy HH:mm:ss"), user, logMessage));
            this.Terminate();
        }

        public void ProcessLogMessageGuest(DateTime time, string user, string logMessage, Guest guest)
        {
                this.Init();
                _logFile.WriteLine(string.Format("--> TIME: {0}, USER: {1}, MESSAGE: {2} [GUEST " +
                    "Name: {3} {4} Address: {5} PostalCode: {6} City: {7} Country: {8} Tel.: {9} E-mail: {10} ICENR: {11}]",
                    time.ToString("dd/MM/yyyy HH:mm:ss"), user, logMessage, guest.FirstName, guest.LastName, guest.Address,
                    guest.PostalCode, guest.City, guest.Country, guest.PhoneNumber, guest.EmailAdress, guest.ICEPhoneNumber));
                this.Terminate();
        }

        public void ProcessLogMessageRoom(DateTime time, string user, string logMessage, Room room)
        {
            this.Init();
            _logFile.WriteLine(string.Format("--> TIME: {0}, USER: {1}, MESSAGE: {2} [ROOM " +
                "RoomNr: {3} Beds: {4} Quality: {5} HasNiceView: {6} Price/Day: {7}]",
                time.ToString("dd/MM/yyyy HH:mm:ss"), user, logMessage, room.RoomNumber, room.Beds, room.Quality, room.HasNiceView, room.PricePerDay));
            this.Terminate();
        }

        public void ProcessLogMessageBooking(DateTime time, string user, string logMessage, Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
