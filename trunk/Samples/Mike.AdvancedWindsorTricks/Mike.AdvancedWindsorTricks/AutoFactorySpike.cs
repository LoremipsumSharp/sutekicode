using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Mike.AdvancedWindsorTricks.AutoFactory;
using Mike.AdvancedWindsorTricks.Model;

namespace Mike.AdvancedWindsorTricks
{
    public class AutoFactorySpike
    {
        public void can_resolve_component_using_autoFactory()
        {
            var container = new WindsorContainer()
                .AddFacility<AutoFactoryFacility>()
                .Register(
                    Component.For<IThing>().ImplementedBy<ThingOne>(),
                    Component.For<UsesThingFactory>()
                );

            var usesThingFactory = container.Resolve<UsesThingFactory>();

            Console.WriteLine(usesThingFactory.SayHello("Tuna"));
            Console.WriteLine(usesThingFactory.SayHello("Tuna"));
        }

        public void will_not_resolve_factory_for_component_that_is_not_registered()
        {
            var container = new WindsorContainer()
                .AddFacility<AutoFactoryFacility>()
                .Register(
                    Component.For<UsesThingFactory>()
                );

            container.Resolve<UsesThingFactory>();
        }
    }
}