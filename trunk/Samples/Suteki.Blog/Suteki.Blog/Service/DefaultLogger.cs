using System;

namespace Suteki.Blog.Service
{
    public class DefaultLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}