using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Mike.RhinoMocksDemo.Tests.Spikes
{
    [TestFixture]
    public class Whats_the_difference_between_mocks_and_stubs
    {
        private IThing mock;
        private IThing stub;

        [SetUp]
        public void SetUp()
        {
            mock = MockRepository.GenerateMock<IThing>();
            stub = MockRepository.GenerateStub<IThing>();
        }

        [Test]
        public void Mock_returns_default_values_from_properties()
        {
            Assert.That(mock.Id, Is.EqualTo(0));
            Assert.That(mock.Name, Is.Null);
        }

        [Test]
        public void Stub_returns_default_values_from_properties()
        {

            Assert.That(stub.Id, Is.EqualTo(0));
            Assert.That(stub.Name, Is.Null);
        }

        [Test]
        public void Mock_returns_default_values_from_methods()
        {
            Assert.That(mock.DoSomething(null), Is.Null);
        }

        [Test]
        public void Stub_returns_default_values_from_methods()
        {
            Assert.That(stub.DoSomething(null), Is.Null);
        }

        [Test]
        public void Mock_properties_can_NOT_be_set()
        {
            mock.Name = "The Thing";
            Assert.That(mock.Name, Is.EqualTo("The Thing"));
        }

        [Test]
        public void Stub_properties_CAN_be_set()
        {
            stub.Name = "The Thing";
            Assert.That(stub.Name, Is.EqualTo("The Thing"));
        }

        [Test]
        public void Mock_expectations_not_met_will_fail()
        {
            mock.Expect(m => m.DoSomething(null)).Return(null);

            mock.VerifyAllExpectations();
        }

        [Test]
        public void Stub_expectations_not_met_will_NOT_fail()
        {
            stub.Expect(s => s.DoSomething(null)).Return(null);

            stub.VerifyAllExpectations();
        }

        [Test]
        public void Mock_property_expectations_will_work()
        {
            mock.Expect(m => m.Id).Return(4);
            Assert.That(mock.Id, Is.EqualTo(4));
            mock.VerifyAllExpectations();
        }

        [Test]
        public void Stub_cannot_have_property_expectations()
        {
            stub.Expect(m => m.Id).Return(4);
        }

        [Test]
        public void Mock_AssertWasCalled_will_fail_if_not_called()
        {
            mock.AssertWasCalled(m => m.DoSomething(null));
        }

        [Test]
        public void Stub_AssertWasCalled_WILL_fail_if_not_called()
        {
            stub.AssertWasCalled(s => s.DoSomething(null));
        }
    }

    public class Thing : IThing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IThing DoSomething(IThing thing)
        {
            return thing;
        }
    }

    public interface IThing
    {
        int Id { get; set; }
        string Name { get; set; }
        IThing DoSomething(IThing thing);
    }
}