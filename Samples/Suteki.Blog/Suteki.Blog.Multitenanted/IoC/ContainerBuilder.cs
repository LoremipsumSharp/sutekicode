using System.ServiceModel.Description;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Naming;
using Castle.Windsor;
using Suteki.Blog.Service;

namespace Suteki.Blog.Multitenanted.IoC
{
    public class ContainerBuilder
    {
        public static IKernel GlobalKernel;

        public static IWindsorContainer Build()
        {
            var container = new WindsorContainer();
            container.Kernel.AddSubSystem(SubSystemConstants.NamingKey, new NamingPartsSubSystem());

            var debug = new ServiceDebugBehavior
            {
                IncludeExceptionDetailInFaults = true
            };

            container.AddFacility<WcfFacility>()
                .Register(
                    Component.For<IServiceBehavior>().Instance(debug),
                    Component.For<ILogger>().ImplementedBy<DefaultLogger>().Named("ILogger:host=blue.shop"),
                    Component.For<ILogger>().ImplementedBy<AlternativeLogger>().Named("ILogger:host=red.shop"),
                    Component
                        .For<IBlogService>()
                        .ImplementedBy<DefaultBlogService>()
                        .Named("blogService")
                        .LifeStyle.Transient
                );

            container.Kernel.AddHandlerSelector(new HostBasedComponentSelector(container.Kernel));
            GlobalKernel = container.Kernel;
            return container;
        }
    }
}