using System.ServiceModel.Description;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Suteki.Blog.RestService.Atom;
using Suteki.Blog.Service;

namespace Suteki.Blog.RestService.IoC
{
    public class ContainerBuilder
    {
        public static IWindsorContainer Build()
        {
            var debug = new ServiceDebugBehavior
            {
                IncludeExceptionDetailInFaults = true
            };

            return new WindsorContainer()
                .AddFacility<WcfFacility>()
                .Register(
                    Component.For<IServiceBehavior>().Instance(debug),
                    Component.For<IPostAtomFeedMapper>().ImplementedBy<DefaultPostAtomFeedMapper>(),
                    Component
                        .For<IAtomFeed>()
                        .ImplementedBy<DefaultAtomFeed>()
                        .Named("atomFeed")
                        .LifeStyle.Transient,
                    Component
                        .For<IBlogService>()
                        .ImplementedBy<DefaultBlogService>()
                        .Named("blogService")
                        .LifeStyle.Transient,
                    Component.For<ILogger>().ImplementedBy<DefaultLogger>().LifeStyle.Transient
                );
        }
    }
}