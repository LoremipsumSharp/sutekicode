using System;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Mike.AdvancedWindsorTricks.Model;

namespace Mike.AdvancedWindsorTricks
{
    public class AutoRegistrationSpike
    {
        public void can_register_all_types_that_implement_an_interface()
        {
            var container = new WindsorContainer()
                .Register(
                    AllTypes
                        .Of<IThing>()
                        .FromAssembly(Assembly.GetExecutingAssembly())
                        .Configure(component => component.LifeStyle.Transient)
                        .WithService.FirstInterface()
                );

            foreach (var thing in container.ResolveAll<IThing>())
            {
                Console.WriteLine(thing.SayHello("Mike"));
            }
        }

        public void can_register_all_types_in_a_namespace()
        {
            var container = new WindsorContainer()
                .Register(
                    AllTypes
                        .FromAssembly(Assembly.GetExecutingAssembly())
                        .Where(Component.IsInNamespace("Mike.AdvancedWindsorTricks.Model"))
                );

            foreach (var thing in container.ResolveAll<IThing>())
            {
                Console.WriteLine(thing.SayHello("Jim"));
            }
        }

        public void can_register_all_types_that_satisfy_a_condition()
        {
            var container = new WindsorContainer()
                .Register(
                    AllTypes
                        .Of<IThing>()
                        .FromAssembly(Assembly.GetExecutingAssembly())
                        .Where(Component.IsInNamespace("Mike.AdvancedWindsorTricks.Model"))
                        .Where(t => t.GetInterfaces().Any())
                        .Configure(component => component.LifeStyle.Transient)
                        .ConfigureFor<ThingTwo>(component => component.Named("thing.the.second"))
                        .WithService.FirstInterface()
                );

            foreach (var thing in container.ResolveAll<IThing>())
            {
                Console.WriteLine(thing.SayHello("Leo"));
            }
        }

        public void can_use_basedOn_and_conditional()
        {
            var container = new WindsorContainer()
                .Register(
                    AllTypes
                        .FromAssembly(Assembly.GetExecutingAssembly())
                        .BasedOn<IThing>()
                        .If(type => type.Name.Contains("Thing"))
                );

            foreach (var thing in container.ResolveAll<IThing>())
            {
                Console.WriteLine(thing.SayHello("Jane"));
            }
        }
    }
}