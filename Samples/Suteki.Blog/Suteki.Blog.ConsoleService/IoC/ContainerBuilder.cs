using System.ServiceModel;
using Castle.Facilities.WcfIntegration;
using Castle.Facilities.WcfIntegration.Behaviors;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Suteki.Blog.ConsoleService.Wcf;
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
                    Component.For<MessageLifecycleBehavior>(),
                    Component
                        .For<IBlogService>()
                        .ImplementedBy<DefaultBlogService>()
                        .Named("blogService")
                        .LifeStyle.Transient
                        .ActAs(new DefaultServiceModel()
                            .AddEndpoints(WcfEndpoint
                                .BoundTo(new NetTcpBinding())
                                .At("net.tcp://localhost/BlogService")
                                // adds this message action to this endpoint
                                .AddExtensions(new LifestyleMessageAction()) 
                                )),
                    Component
                        .For<ILogger>()
                        .ImplementedBy<DefaultLogger>()
                        .LifeStyle.Transient
                );
           
        }
    }
}