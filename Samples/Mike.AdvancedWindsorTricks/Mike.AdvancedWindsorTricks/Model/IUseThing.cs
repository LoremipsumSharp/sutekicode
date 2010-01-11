namespace Mike.AdvancedWindsorTricks.Model
{
    public interface IUseThing
    {
        IThing GetThing(string nameOfThing);
    }
}