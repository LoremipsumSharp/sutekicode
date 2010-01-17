using System;

namespace Mike.AdvancedWindsorTricks.Model
{
    public class MessageListener
    {
        public void OnMessagePublished(string message)
        {
            Console.WriteLine("MessageListener got message: '{0}'", message);
        }
    }
}