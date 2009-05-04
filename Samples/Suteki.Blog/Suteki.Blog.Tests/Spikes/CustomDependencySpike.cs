using System;
using System.Collections;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace Suteki.Blog.Tests.Spikes
{
    [TestFixture]
    public class CustomDependencySpike
    {
        [Test]
        public void CreateAndResolveCustomDependencies()
        {
            IWindsorContainer container = new WindsorContainer()
                .Register(
                    Component.For<IThing>().ImplementedBy<Thing>().ActAs(new MyCustomDependency())
                );

            var thing = container.Resolve<IThing>();

            Assert.That(thing, Is.Not.Null);

            var thingHandler = container.Kernel.GetHandler(typeof (IThing));
            

            foreach (DictionaryEntry customDependency in thingHandler.ComponentModel.CustomDependencies)
            {
                Assert.That(customDependency.Value is MyCustomDependency);
                Console.WriteLine(customDependency.Key);
            } 
        }

        [Test]
        public void CanProvideAlternativeServicesAtResolution()
        {
            IWindsorContainer container = new WindsorContainer()
                .Register(
                    Component.For<IThing>().ImplementedBy<Thing>(),
                    Component.For<IWidget>().ImplementedBy<Widget1>()
                );

            var thing1 = container.Resolve<IThing>(new { Name = "Fred", Widget = new Widget2() }) as Thing;
            Assert.That(thing1.Widget, Is.TypeOf(typeof(Widget2)));

            var thing2 = container.Resolve<IThing>() as Thing;
            Assert.That(thing2.Name, Is.EqualTo("Fred"));
            Assert.That(thing2.Widget, Is.TypeOf(typeof(Widget2)));
        }
    }

    public interface IThing{}
    public class Thing : IThing
    {
        public IWidget Widget { get; set; }
        public string Name { get; set; }
    }
    public class MyCustomDependency {}

    public interface IWidget {}
    public class Widget1 : IWidget {}
    public class Widget2 : IWidget {}
}