namespace Mike.AdvancedWindsorTricks.Model
{
    public class ThingOne : IThing
    {
        public string SayHello(string name)
        {
            return string.Format("ThingOne says hello to {0}", name);
        }
    }
}