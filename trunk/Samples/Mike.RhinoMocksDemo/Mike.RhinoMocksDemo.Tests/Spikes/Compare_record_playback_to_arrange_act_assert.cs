using NUnit.Framework;
using Rhino.Mocks;

namespace Mike.RhinoMocksDemo.Tests.Spikes
{
    [TestFixture]
    public class Compare_record_playback_to_arrange_act_assert
    {
        private MockRepository mocks;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
        }

        [Test]
        public void Demonstrate_record_playback()
        {
            var mock = mocks.DynamicMock<IThing>();
            var thing1 = new Thing();

            using (mocks.Record())
            {
                Expect.Call(mock.DoSomething(thing1)).Return(thing1);
            }

            using (mocks.Playback())
            {
                mock.DoSomething(thing1);

                mocks.VerifyAll();
            }
        }

        [Test]
        public void Demonstrate_arrange_act_assert()
        {
            // arrange
            var mock = MockRepository.GenerateMock<IThing>();
            var thing1 = new Thing();

            // act
            mock.DoSomething(thing1);

            // assert
            mock.AssertWasCalled(m => m.DoSomething(thing1));
        }

        [Test]
        public void Show_different_kinds_of_mocks()
        {
            var strictMock = mocks.StrictMock<IThing>();

            var dynamicMock = mocks.DynamicMock<IThing>();
            
            var stub = mocks.Stub<IThing>();
            
            var partialMock = mocks.PartialMock<IThing>();
        }
    }
}