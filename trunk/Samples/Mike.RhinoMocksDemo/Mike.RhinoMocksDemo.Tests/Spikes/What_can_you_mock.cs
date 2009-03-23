using NUnit.Framework;
using Rhino.Mocks;

namespace Mike.RhinoMocksDemo.Tests.Spikes
{
    [TestFixture]
    public class What_can_you_mock
    {
        [Test]
        public void You_can_mock_interfaces()
        {
            var mock = MockRepository.GenerateMock<IThing>();
            var thing = new Thing();

            mock.Stub(m => m.DoSomething(Arg<IThing>.Is.Anything)).Return(thing);

            Assert.That(mock.DoSomething(null), Is.SameAs(thing));
        }

        [Test]
        public void You_can_mock_abstract_base_classes()
        {
            var mock = MockRepository.GenerateMock<ThingBase>(4, "My Name");
            var thing = new Thing();

            mock.Stub(m => m.DoSomething(Arg<IThing>.Is.Anything)).Return(thing);

            Assert.That(mock.DoSomething(null), Is.SameAs(thing));
        }

        [Test]
        public void You_can_mock_virtual_members()
        {
            var mock = MockRepository.GenerateMock<Thing>();
            var thing = new Thing();

            mock.Stub(m => m.DoSomething(Arg<IThing>.Is.Anything)).Return(thing);

            Assert.That(mock.DoSomething(null), Is.SameAs(thing));
        }

        [Test]
        public void You_cant_mock_non_abstract_or_virtual_members()
        {
            var mock = MockRepository.GenerateMock<ThingBase>(4, "My Name");

            mock.Stub(m => m.SomeStringOperation(Arg<string>.Is.Anything)).Return("Hello");

            Assert.That(mock.SomeStringOperation(null), Is.EqualTo("Hello"));
        }

        [Test]
        public void You_cant_mock_extension_methods()
        {
            var mock = MockRepository.GenerateMock<IThing>();

            mock.Stub(m => m.ExtensionSaysHello()).Return("Hello");

            Assert.That(mock.ExtensionSaysHello(), Is.EqualTo("Hello"));
        }
    }
}