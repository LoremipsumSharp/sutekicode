using System;

namespace Mike.AdvancedWindsorTricks.Model
{
    public class UseThing : IUseThing
    {
        private readonly Func<string, IThing> thingFactory;

        public UseThing(Func<string, IThing> thingFactory)
        {
            this.thingFactory = thingFactory;
        }

        public IThing GetThing(string nameOfThing)
        {
            return thingFactory(nameOfThing);
        }
    }
}