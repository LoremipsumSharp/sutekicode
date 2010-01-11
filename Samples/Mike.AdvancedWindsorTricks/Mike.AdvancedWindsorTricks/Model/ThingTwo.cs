namespace Mike.AdvancedWindsorTricks.Model
{
    public class ThingTwo : IThing
    {
        public string SayHello(string name)
        {
            return string.Format("ThingTwo says hello to {0}", name);
        }
    }
}