using System;

namespace Mike.AdvancedWindsorTricks.Model
{
    public class UsesThingFactory
    {
        private readonly Func<IThing> thingFactory;

        public UsesThingFactory(Func<IThing> thingFactory)
        {
            this.thingFactory = thingFactory;
        }

        public IThing GetMeAThing()
        {
            return thingFactory();
        }
    }
}