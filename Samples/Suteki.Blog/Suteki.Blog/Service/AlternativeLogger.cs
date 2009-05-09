using System.Diagnostics;

namespace Suteki.Blog.Service
{
    public class AlternativeLogger : ILogger
    {
        public void Log(string message)
        {
            Debug.WriteLine(string.Format("I am AlternativeLogger: {0}", message));
        }
    }
}