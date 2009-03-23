using NUnit.Framework;
using Rhino.Mocks;

namespace Mike.RhinoMocksDemo.Tests.Spikes
{
    [TestFixture]
    public class Demonstrate_when_called
    {
        [Test]
        public void You_can_alter_the_return_value()
        {
            var thing = MockRepository.GenerateStub<IThing>();

            thing.Stub(x => x.DoSomething(Arg<IThing>.Is.Anything))
                .WhenCalled(invocation => invocation.ReturnValue = new Thing {Name = "Hello"});

            Assert.That(thing.DoSomething(null).Name, Is.EqualTo("Hello"));
        }

        [Test]
        public void You_can_inspect_arguments()
        {
            var thing = MockRepository.GenerateStub<IThing>();

            IThing argument = null;

            thing.Stub(x => x.DoSomething(Arg<IThing>.Is.Anything))
                .WhenCalled(invocation => argument = (IThing) invocation.Arguments[0]);

            thing.DoSomething(new Thing {Name = "Hello"});

            Assert.That(argument.Name, Is.EqualTo("Hello"));
        }
    }
}