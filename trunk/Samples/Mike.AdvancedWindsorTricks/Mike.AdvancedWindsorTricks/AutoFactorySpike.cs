using System;
using Castle.Core;
using Castle.Core.Configuration;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
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

            Console.WriteLine(usesThingFactory.GetMeAThing().SayHello("Tuna"));
            Console.WriteLine(usesThingFactory.GetMeAThing().SayHello("Tuna"));
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

    public class AutoFactoryFacility : IFacility
    {
        public void Init(IKernel kernel, IConfiguration facilityConfig)
        {
            kernel.Resolver.AddSubResolver(new AutoFactoryResolver(kernel));
        }

        public void Terminate(){}
    }

    public class AutoFactoryResolver : ISubDependencyResolver
    {
        private readonly IKernel kernel;

        public AutoFactoryResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public object Resolve(
            CreationContext context, 
            ISubDependencyResolver contextHandlerResolver, 
            ComponentModel model, 
            DependencyModel dependency)
        {
            var getResolveDelegateGeneric = GetType().GetMethod("GetResolveDelegate");
            var getResolveDelegateMethod = 
                getResolveDelegateGeneric.MakeGenericMethod(dependency.TargetType.GetGenericArguments()[0]);
            return getResolveDelegateMethod.Invoke(this, null);
        }

        public bool CanResolve(
            CreationContext context, 
            ISubDependencyResolver contextHandlerResolver, 
            ComponentModel model, 
            DependencyModel dependency)
        {
            return dependency.TargetType.IsGenericType 
                && (dependency.TargetType.GetGenericTypeDefinition() == typeof (Func<>))
                && (kernel.HasComponent(dependency.TargetType.GetGenericArguments()[0]));
        }

        public Func<T> GetResolveDelegate<T>()
        {
            return () => kernel.Resolve<T>();
        }
    }
}