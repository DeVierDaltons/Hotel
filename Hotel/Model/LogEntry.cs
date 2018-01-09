using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    public class LogEntry
    {
        private string _user;

        public string User
        {
            get { return _user; }
            set { _user = value; }
        }

        private DateTime _time;

        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public LogEntry(string message)
        {
            User = Environment.UserName;
            Time = DateTime.Now;
            Message = message;
        }

        public LogEntry(string user, string message)
        {
            User = user;
            Time = DateTime.Now;
            Message = message;
        }

        public override string ToString()
        {
            return Time.ToString("ddMMyyyyHHmmss") + " " + User + " " + Message;
        }


    }
}
