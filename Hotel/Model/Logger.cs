using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{ 
    public enum ActionType
    {
        Add
    }

    public class Logger
    {
                                           /* private static readonly SingletonLogger loggerInstance = new SingletonLogger();

                                            static SingletonLogger()
                                            {
                                            }

                                            private SingletonLogger()
                                            {
                                            }

                                            public static SingletonLogger LoggerInstance
                                            {
                                                get
                                                {
                                                    return loggerInstance;
                                                }
                                            }*/

        public static void LogAction<T>(T item, ActionType actionType)
        {
            string message = item.GetType().Name;

            //todo
            //WriteAction(new LogEntry());
        }

        private static void WriteAction(LogEntry entry)
        {
            //todo
        }

        private void WriteToTxtFile(LogEntry entry)
        {
            string writeString = "Time: " + entry.Time.ToString("ddMMyyyyHHmmss") + " User: " + entry.User + " Action: " + entry.Message;
            using (StreamWriter streamWriter = new StreamWriter("Log.txt"))
            {
                streamWriter.WriteLine(writeString);
            }
        }
    }
}
