using System;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;

namespace Mike.AdvancedWindsorTricks.AutoFactory
{
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
            var delegateCreationMethod = IsResolver(dependency) ? "GetResolveDelegate" : "GetReleaseDelegate";

            var getResolveDelegateGeneric = GetType().GetMethod(delegateCreationMethod);
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
            return IsResolver(dependency) || IsReleaser(dependency);
        }

        private bool IsResolver(DependencyModel dependency)
        {
            return dependency.TargetType.IsGenericType
                   && (dependency.TargetType.GetGenericTypeDefinition() == typeof(Func<>))
                   && (kernel.HasComponent(dependency.TargetType.GetGenericArguments()[0]));
        }

        private bool IsReleaser(DependencyModel dependency)
        {
            return dependency.TargetType.IsGenericType
                   && (dependency.TargetType.GetGenericTypeDefinition() == typeof(Action<>))
                   && (kernel.HasComponent(dependency.TargetType.GetGenericArguments()[0]));
        }

        public Func<T> GetResolveDelegate<T>()
        {
            return () => kernel.Resolve<T>();
        }

        public Action<T> GetReleaseDelegate<T>()
        {
            return component => kernel.ReleaseComponent(component);
        }
    }
}