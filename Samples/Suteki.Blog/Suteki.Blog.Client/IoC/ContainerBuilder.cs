using System.Reflection;
using System.ServiceModel;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Suteki.Blog.Client.Controller;
using Suteki.Blog.Service;

namespace Suteki.Blog.Client.IoC
{
    public class ContainerBuilder
    {
        public static IWindsorContainer BuildForInProcess()
        {
            return new WindsorContainer()
                .Register(
                    AllTypes
                        .Of<IController>()
                        .FromAssembly(Assembly.GetExecutingAssembly())
                        .Configure(c => c.LifeStyle.Transient.Named(c.Implementation.Name)),

                    Component.For<IBlogService>().ImplementedBy<DefaultBlogService>().LifeStyle.Transient,
                    Component.For<ILogger>().ImplementedBy<DefaultLogger>().LifeStyle.Transient
                );
        }

        public static IWindsorContainer BuildForWebservice()
        {
            return new WindsorContainer()
                .AddFacility<WcfFacility>()
                .Register(
                    AllTypes
                        .Of<IController>()
                        .FromAssembly(Assembly.GetExecutingAssembly())
                        .Configure(c => c.LifeStyle.Transient.Named(c.Implementation.Name)),

                    Component.For<IBlogService>()
                        .ActAs(new DefaultClientModel
                        {
                            Endpoint = WcfEndpoint
                                .BoundTo(new BasicHttpBinding())
                                .At("http://ipv4.fiddler:50388/BlogService.svc")
                        })
                );
        }
    }
}