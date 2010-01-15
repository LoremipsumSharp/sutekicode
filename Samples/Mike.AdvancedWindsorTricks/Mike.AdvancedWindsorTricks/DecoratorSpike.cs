using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Mike.AdvancedWindsorTricks.Model;

namespace Mike.AdvancedWindsorTricks
{
    public class DecoratorSpike
    {
        public void decorator_can_be_used_without_a_container()
        {
            var thing = new ThingDecorator(new ThingOne());
            Console.WriteLine(thing.SayHello("Roger"));
        }

        public void decorator_should_work_without_any_special_registration()
        {
            var container = new WindsorContainer()
                .Register(
                    Component.For<IThing>().ImplementedBy<ThingDecorator>(),
                    Component.For<IThing>().ImplementedBy<ThingDecorator2>(),
                    Component.For<IThing>().ImplementedBy<ThingOne>()
                );

            var thing = container.Resolve<IThing>();

            Console.WriteLine(thing.SayHello("Roger"));
        }
    }
}