using System;
using System.Reflection;
using Castle.Core.Configuration;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Mike.AdvancedWindsorTricks.Model;

namespace Mike.AdvancedWindsorTricks
{
    public class ArrayResolutionSpike
    {
        public void resolve_an_array_of_things()
        {
            var container = new WindsorContainer()
                .AddFacility<ArrayFacility>()
                .Register(
                    AllTypes
                        .Of<IThing>()
                        .FromAssembly(Assembly.GetExecutingAssembly())
                        .WithService.FirstInterface(),
                    Component.For<UsesThingArray>()
                );

            var usesThingArray = container.Resolve<UsesThingArray>();
            foreach (var thing in usesThingArray.Things)
            {
                Console.WriteLine(thing.SayHello("Leo"));
            }
        }
    }

    public class ArrayFacility : IFacility
    {
        public void Init(IKernel kernel, IConfiguration facilityConfig)
        {
            kernel.Resolver.AddSubResolver(new ArrayResolver(kernel));
        }

        public void Terminate(){ }
    }
}