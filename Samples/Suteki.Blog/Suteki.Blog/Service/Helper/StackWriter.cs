using System.Diagnostics;

namespace Suteki.Blog.Service.Helper
{
    public class StackWriter
    {
        public static void WriteStack()
        {
            Debug.WriteLine("--- stack trace ---");
            
            var stackTrace = new StackTrace();
            foreach (var stackFrame in stackTrace.GetFrames())
            {
                Debug.WriteLine(string.Format("{0}.{1}",
                    stackFrame.GetMethod().ReflectedType.FullName, 
                    stackFrame.GetMethod().Name));    
            }

            Debug.WriteLine("--- end stack trace ---");

        }
    }
}