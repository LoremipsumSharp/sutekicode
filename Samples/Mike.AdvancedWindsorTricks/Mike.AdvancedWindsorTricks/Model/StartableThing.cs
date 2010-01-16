using System;
using Castle.Core;

namespace Mike.AdvancedWindsorTricks.Model
{
    public class StartableThing : IThing, IStartable
    {
        public void Start()
        {
            Console.WriteLine("Starting");
        }

        public void Stop()
        {
            Console.WriteLine("Stopping");
        }

        public string SayHello(string name)
        {
            return string.Format("Hello, {0}, from StartableThing", name);
        }
    }

    public class NonInterfaceStartableThing : IThing
    {
        public void SomeStartMethod()
        {
            Console.WriteLine("Starting");
        }

        public void SomeStopMethod()
        {
            Console.WriteLine("Stopping");
        }

        public string SayHello(string name)
        {
            return string.Format("Hello, {0}, from NonInterfaceStartableThing", name);
        }
    }
}