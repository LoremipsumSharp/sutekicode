namespace Mike.AdvancedWindsorTricks.Model
{
    public class ThingDecorator2 : IThing
    {
        private readonly IThing thing;

        public ThingDecorator2(IThing thing)
        {
            this.thing = thing;
        }

        public string SayHello(string name)
        {
            return thing.SayHello(name + " has been altered by ThingDecorator2... ha ha ha!");
        }
    }
}