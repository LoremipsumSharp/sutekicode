using System;

namespace Mike.AdvancedWindsorTricks.Model
{
    public class UsesThingFactory
    {
        private readonly Func<IThing> createThing;
        private readonly Action<IThing> releaseThing;

        public UsesThingFactory(Func<IThing> createThing, Action<IThing> releaseThing)
        {
            this.createThing = createThing;
            this.releaseThing = releaseThing;
        }

        public string SayHello(string name)
        {
            var thing = createThing();
            var message = thing.SayHello(name);
            releaseThing(thing);
            return message;
        }
    }
}