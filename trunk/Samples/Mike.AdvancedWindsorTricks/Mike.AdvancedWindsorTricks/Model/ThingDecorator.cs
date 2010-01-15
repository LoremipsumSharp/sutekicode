using System;

namespace Mike.AdvancedWindsorTricks.Model
{
    public class ThingDecorator : IThing
    {
        private readonly IThing thing;

        public ThingDecorator(IThing thing)
        {
            this.thing = thing;
        }

        public string SayHello(string name)
        {
            Console.WriteLine("Before calling wrapped IThing");
            var message = thing.SayHello(name);
            Console.WriteLine("After calling wrapped IThing");
            return message;
        }
    }
}