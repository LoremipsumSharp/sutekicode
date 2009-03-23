using System;
using System.Collections.Generic;

namespace Mike.RhinoMocksDemo.Tests.Spikes
{
    public abstract class ThingBase : IThing
    {
        protected ThingBase(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public abstract IThing DoSomething(IThing thing);
        public string SomeStringOperation(string value)
        {
            throw new NotImplementedException();
        }

        public abstract int SomeIntOperation(int value);
        public abstract void ListOperation(IEnumerable<IThing> things);

        public static string SayHello()
        {
            throw new NotImplementedException();
        }
    }
}