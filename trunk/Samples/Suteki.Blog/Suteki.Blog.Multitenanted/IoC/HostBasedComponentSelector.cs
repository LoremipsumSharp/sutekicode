using System;
using System.Linq;
using System.Diagnostics;
using System.ServiceModel;
using Castle.MicroKernel;

namespace Suteki.Blog.Multitenanted.IoC
{
    public class HostBasedComponentSelector : IHandlerSelector
    {
        private readonly IKernel kernel;

        public HostBasedComponentSelector(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public bool HasOpinionAbout(string key, Type service)
        {
            Debug.WriteLine("OperationContext.Current is {0}", OperationContext.Current == null ? "null" : "not null");

            if (OperationContext.Current == null) return false;
            
            Debug.WriteLine(string.Format("key = '{0}', service = '{1}'", key, service));

            var componentKey = GetComponentKeyWithHostnameParameter(key, service);
            Debug.WriteLine("Looking up componentKey '{0}'", componentKey);
            return kernel.HasComponent(componentKey);
        }

        private static string GetComponentKeyWithHostnameParameter(string key, Type service)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = service.Name;
            }

            var hostname = OperationContext.Current.Channel.LocalAddress.Uri.Host;
            return string.Format("{0}:host={1}", key, hostname);
        }

        public IHandler SelectHandler(string key, Type service, IHandler[] handlers)
        {
            return handlers.First(h => h.ComponentModel.Name == GetComponentKeyWithHostnameParameter(key, service));
        }
    }
}