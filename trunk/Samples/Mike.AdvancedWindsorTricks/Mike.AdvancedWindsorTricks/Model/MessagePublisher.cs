using System;

namespace Mike.AdvancedWindsorTricks.Model
{
    public class MessagePublisher : IStartable
    {
        public event Action<string> MessagePublished;

        public void PublishMessage(string message)
        {
            if (MessagePublished != null)
            {
                MessagePublished(message);
            }
        }

        public void Start(){}
        public void Stop(){}
    }
}