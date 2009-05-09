using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Naming;
using Castle.Windsor;
using NUnit.Framework;

namespace Suteki.Blog.Tests.Spikes
{
    [TestFixture]
    public class NamingPartsSubsystemSpike
    {
        private IWindsorContainer container;

        [SetUp]
        public void SetUp()
        {
            container = new WindsorContainer();
            container.Kernel.AddSubSystem(SubSystemConstants.NamingKey, new NamingPartsSubSystem());

            container.Register(
                    Component.For<IThing>().ImplementedBy<Thing1>().Named("thing:colour=red,version=1"),
                    Component.For<IThing>().ImplementedBy<Thing2>().Named("thing:colour=blue,version=1"),
                    Component.For<IThing>().ImplementedBy<Thing3>().Named("thing:colour=red,version=2"),
                    Component.For<IThing>().ImplementedBy<Thing4>().Named("thing:colour=blue,version=2")
                );
        }

        [Test]
        public void Should_be_able_to_resolve_thing()
        {
            var defaultThing = container.Resolve("thing");

            Assert.That(defaultThing, Is.Not.Null);
            Assert.That(defaultThing, Is.TypeOf(typeof(Thing1)));
        }

        [Test]
        public void Should_be_able_to_resolve_redThing()
        {
            var redThing = container.Resolve("thing:colour=red");

            Assert.That(redThing, Is.Not.Null);
            Assert.That(redThing, Is.TypeOf(typeof(Thing1)));
        }

        [Test]
        public void Should_be_able_to_resolve_blueThing()
        {
            var blueThing = container.Resolve("thing:colour=blue");

            Assert.That(blueThing, Is.Not.Null);
            Assert.That(blueThing, Is.TypeOf(typeof(Thing2)));
        }

        [Test]
        public void Should_be_able_to_resolve_version2()
        {
            var version2 = container.Resolve("thing:version=2");

            Assert.That(version2, Is.Not.Null);
            Assert.That(version2, Is.TypeOf(typeof(Thing3)));
        }

        [Test]
        public void Should_be_able_to_resolve_blueThingVersion2()
        {
            var blueThingVersion2 = container.Resolve("thing:colour=blue,version=2");

            Assert.That(blueThingVersion2, Is.Not.Null);
            Assert.That(blueThingVersion2, Is.TypeOf(typeof(Thing4)));
        }

        [Test]
        public void Should_be_able_to_resolve_thing_from_kernel()
        {
            var thing = container.Kernel.Resolve<IThing>("thing");

            Assert.That(thing, Is.Not.Null);
            Assert.That(thing, Is.TypeOf(typeof(Thing1)));
        }

        [Test]
        public void Should_be_able_to_resolve_handler()
        {
            var thingHandler = container.Kernel.GetHandler("thing");
            Assert.That(thingHandler, Is.Not.Null);
        }

        private class IThing {}
        private class Thing1 : IThing {}
        private class Thing2 : IThing {}
        private class Thing3 : IThing {}
        private class Thing4 : IThing {}
    }
}