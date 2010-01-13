namespace Mike.AdvancedWindsorTricks.Model
{
    public class ThingThree : IThing
    {
        public string SayHello(string name)
        {
            return string.Format("Hello {0} from ThingThree", name);
        }
    }
}