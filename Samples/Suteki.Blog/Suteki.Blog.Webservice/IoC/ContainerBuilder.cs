using System.ServiceModel.Description;
using Castle.Facilities.WcfIntegration;
using Castle.Facilities.WcfIntegration.Behaviors;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Suteki.Blog.Service;

namespace Suteki.Blog.Webservice.IoC
{
    public class ContainerBuilder
    {
        public static IWindsorContainer Build()
        {
            var metadata = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true
            };

            var debug = new ServiceDebugBehavior
            {
                IncludeExceptionDetailInFaults = true
            };

            return new WindsorContainer()
                .AddFacility<WcfFacility>()
                .Register(
                    Component.For<IServiceBehavior>().Instance(metadata),
                    Component.For<IServiceBehavior>().Instance(debug),
                    Component
                        .For<IBlogService>()
                        .ImplementedBy<DefaultBlogService>()
                        .Named("blogService")
                        .LifeStyle.Transient
                        .ActAs(new DefaultServiceModel().Hosted()),
                    Component
                        .For<ILogger>()
                        .ImplementedBy<DefaultLogger>()
                        .LifeStyle.Transient
                );
        }
    }
}