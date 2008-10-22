using System;

namespace Mike.IocDemo.Model
{
    public class Logger : ILogger
    {
        public Logger()
        {
            Console.WriteLine("Created instance of Logger");
        }

        public void Log(string message)
        {
            Console.WriteLine("Log Message: {0}", message);
        }
    }
}