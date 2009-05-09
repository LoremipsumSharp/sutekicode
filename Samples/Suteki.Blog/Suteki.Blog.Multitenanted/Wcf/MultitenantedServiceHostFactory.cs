using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel;
using Suteki.Blog.Multitenanted.IoC;

namespace Suteki.Blog.Multitenanted.Wcf
{
    public class MultitenantedServiceHostFactory : WindsorServiceHostFactory<DefaultServiceModel>
    {
		public MultitenantedServiceHostFactory()
		{
		}

        public MultitenantedServiceHostFactory(IKernel kernel)
			: base(kernel)
		{
		}

        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {
            AddEndpoints(constructorString, baseAddresses);

            // passing no baseAddresses forces WCF to use the endpoint address
            return base.CreateServiceHost(constructorString, new Uri[0]);
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            AddEndpoints(serviceType, baseAddresses);
            return base.CreateServiceHost(serviceType, new Uri[0]);
        }

        private static void AddEndpoints(Type serviceType, Uri[] baseAddresses)
        {
            Debug.WriteLine(string.Format("serviceType: '{0}'", serviceType));
            var handler = ContainerBuilder.GlobalKernel.GetHandler(serviceType);

            AddEnpoints(baseAddresses, handler);
        }

        private static void AddEndpoints(string constructorString, Uri[] baseAddresses)
        {
            Debug.WriteLine(string.Format("constructorString: '{0}'", constructorString));
            var handler = ContainerBuilder.GlobalKernel.GetHandler(constructorString);

            AddEnpoints(baseAddresses, handler);
        }

        private static void AddEnpoints(Uri[] baseAddresses, IHandler handler)
        {
            Debug.WriteLine("Selecting base address from:");

            var endpoints = new List<IWcfEndpoint>();

            // create an endpoint for each base address
            foreach (var uri in baseAddresses)
            {
                Debug.WriteLine(string.Format("\t{0}", uri));
                endpoints.Add(WcfEndpoint.BoundTo(new BasicHttpBinding()).At(uri.ToString()));
            }

            // add the endpoints to the service
            handler.ComponentModel.CustomDependencies.Add(
                Guid.NewGuid().ToString(), 
                new DefaultServiceModel().Hosted().AddEndpoints(endpoints.ToArray()));
        }
    }
}