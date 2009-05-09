using System.Diagnostics;

namespace Suteki.Blog.Service
{
    public class DefaultLogger : ILogger
    {
        public void Log(string message)
        {
            Debug.WriteLine("I am DefaultLogger: {0}", message);
        }
    }
}