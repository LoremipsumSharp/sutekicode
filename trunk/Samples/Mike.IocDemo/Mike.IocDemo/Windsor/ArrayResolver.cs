using System;
using System.Collections;
using System.Reflection;
using Castle.Core;
using Castle.MicroKernel;

namespace Mike.IocDemo.Windsor
{
    /// <summary>
    /// ArrayResolver from:
    /// http://hammett.castleproject.org/?p=257
    /// </summary>
    public class ArrayResolver : ISubDependencyResolver
    {
        private readonly IKernel kernel;

        public ArrayResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public object Resolve(CreationContext context, ISubDependencyResolver parentResolver,
                              ComponentModel model,
                              DependencyModel dependency)
        {
            var service = dependency.TargetType.GetElementType();
            var list = new ArrayList();
            var handlers = kernel.GetAssignableHandlers(service);

            foreach (var handler in handlers)
            {
                if (handler.CurrentState != HandlerState.Valid)
                    continue;

                // don't include the component who's dependencies we are currently resolving
                if (handler.ComponentModel.Implementation == model.Implementation)
                    continue;

                var component = ResolveComponent(handler, service);
                list.Add(component);
            }
            
            return list.ToArray(service);
        }

        /// <summary>
        /// Have to use some risky reflection here because ResolveComponent is protected on the DefaultKernel
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="serivce"></param>
        /// <returns></returns>
        private object ResolveComponent(IHandler handler, Type serivce)
        {
            if(!(kernel is DefaultKernel))
            {
                throw new ApplicationException(string.Format(
                    "Can only use ArrayResolver on DefaultKernel, your kernel is of type: {0}", kernel.GetType().Name));
            }

            var resolveComponentMethod = typeof (DefaultKernel)
                .GetMethod("ResolveComponent", 
                    BindingFlags.Instance | BindingFlags.NonPublic, 
                    null,
                    new Type[] { typeof(IHandler), typeof(Type), typeof(IDictionary) }, 
                    null);

            return resolveComponentMethod.Invoke(kernel, new object[] {handler, serivce, null});
        }

        public bool CanResolve(CreationContext context, ISubDependencyResolver parentResolver,
                               ComponentModel model,
                               DependencyModel dependency)
        {
            return dependency.TargetType != null &&
                   dependency.TargetType.IsArray &&
                   dependency.TargetType.GetElementType().IsInterface;
        }
    }
}