using System.Collections.Generic;

namespace Mike.RhinoMocksDemo.Tests.Spikes
{
    public class OtherThing : IThing
    {
        #region Implementation of IThing

        public int Id { get; set; }
        public string Name { get; set; }

        public IThing DoSomething(IThing thing)
        {
            throw new System.NotImplementedException();
        }

        public string SomeStringOperation(string value)
        {
            throw new System.NotImplementedException();
        }

        public int SomeIntOperation(int value)
        {
            throw new System.NotImplementedException();
        }

        public void ListOperation(IEnumerable<IThing> things)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}