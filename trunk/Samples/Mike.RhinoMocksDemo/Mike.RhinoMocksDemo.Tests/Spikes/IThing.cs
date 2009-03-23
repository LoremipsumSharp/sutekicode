using System.Collections.Generic;

namespace Mike.RhinoMocksDemo.Tests.Spikes
{
    public interface IThing
    {
        int Id { get; set; }
        string Name { get; set; }

        IThing DoSomething(IThing thing);
        string SomeStringOperation(string value);
        int SomeIntOperation(int value);
        void ListOperation(IEnumerable<IThing> things);
    }
}