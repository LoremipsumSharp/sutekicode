using System.Collections.Generic;

namespace Mike.RhinoMocksDemo.Tests.Spikes
{
    public class Thing : IThing
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IThing DoSomething(IThing thing)
        {
            return thing;
        }

        public string SomeStringOperation(string value)
        {
            return value;
        }

        public int SomeIntOperation(int value)
        {
            return value;
        }

        public void ListOperation(IEnumerable<IThing> things)
        {
            // do something cool with these things
        }
    }
}