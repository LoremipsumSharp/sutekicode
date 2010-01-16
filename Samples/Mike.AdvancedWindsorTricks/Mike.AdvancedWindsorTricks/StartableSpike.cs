using System;
using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Mike.AdvancedWindsorTricks.Model;

namespace Mike.AdvancedWindsorTricks
{
    public class StartableSpike
    {
        public void startable_thing_should_start_before_resolution()
        {
            var container = new WindsorContainer()
                .AddFacility<StartableFacility>()
                .Register(
                    Component.For<IThing>().ImplementedBy<StartableThing>()
                );

            Console.WriteLine("After container registration, before resolving IThing");

            var thing = container.Resolve<IThing>();
            Console.WriteLine(thing.SayHello("Jack"));

            container.Dispose();
        }

        public void non_interface_startable_thing_should_start_before_resolution()
        {
            var container = new WindsorContainer()
                .AddFacility<StartableFacility>()
                .Register(
                    Component.For<IThing>().ImplementedBy<NonInterfaceStartableThing>()
                        .StartUsingMethod("SomeStartMethod")
                        .StopUsingMethod("SomeStopMethod")
                );

            Console.WriteLine("After container registration, before resolving IThing");

            var thing = container.Resolve<IThing>();
            Console.WriteLine(thing.SayHello("Jack"));

            container.Dispose();
        }
    }
}