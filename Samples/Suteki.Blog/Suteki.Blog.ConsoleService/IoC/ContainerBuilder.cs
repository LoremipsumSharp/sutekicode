using System.ServiceModel;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Suteki.Blog.Service;

namespace Suteki.Blog.ConsoleService.IoC
{
    public class ContainerBuilder
    {
        public static IWindsorContainer Build()
        {
            return new WindsorContainer()
                .AddFacility<WcfFacility>()
                .Register(
                    Component
                        .For<IBlogService>()
                        .ImplementedBy<DefaultBlogService>()
                        .Named("blogService")
                        .LifeStyle.Transient
                        .ActAs(new DefaultServiceModel()
                            .AddEndpoints(WcfEndpoint
                                .BoundTo(new NetTcpBinding())
                                .At("net.tcp://localhost/BlogService"))),
                    Component
                        .For<ILogger>()
                        .ImplementedBy<DefaultLogger>()
                        .LifeStyle.Transient
                );
           
        }
    }
}