using System.Linq;
namespace Mike.AdvancedWindsorTricks.Model
{
    public class UsesThingArray
    {
        public IThing[] Things { get; private set; }

        public UsesThingArray(IThing[] things)
        {
            Things = things;
        }
    }
}