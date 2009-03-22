using System;
using NUnit.Framework;

namespace Mike.RhinoMocksDemo.Tests.Spikes
{
    [TestFixture]
    public class Demonstrate_NUnit_TestDriven_NET
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            Console.WriteLine("TestFixtureSetUp");
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            Console.WriteLine("TestFixtureTearDown");
        }

        [SetUp]
        public void SetUp()
        {
            Console.WriteLine("SetUp");
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("Tear Down");
        }

        [Test]
        public void This_is_a_simple_test()
        {
            Console.WriteLine("This_is_a_simple_test");
        }

        [Test, ExpectedException(typeof (InvalidCastException), ExpectedMessage = "Nasty!")]
        public void This_test_throws()
        {
            throw new InvalidCastException("Nasty!");
        }

        [TestCase(1, Result = 1)]
        [TestCase(2, Result = 4)]
        [TestCase(3, Result = 9)]
        [TestCase(0, ExpectedException = typeof(ArgumentException))]
        public double Input_should_be_squared(double input)
        {
            if(input == 0)
            {
                throw new ArgumentException("input cannot be null");
            }
            return Math.Pow(input, 2);
        }
    }

    public class Not_a_test
    {
        public int SomeMethod(int i)
        {
            Console.WriteLine("Hello World");
            return i;
        }
    }
}