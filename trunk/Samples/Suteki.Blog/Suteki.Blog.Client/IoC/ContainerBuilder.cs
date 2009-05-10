using System.Reflection;
using System.ServiceModel;
using Castle.Facilities.WcfIntegration;
using Castle.Facilities.WcfIntegration.Rest;
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
                    AllControllers(),

                    Component.For<IBlogService>().ImplementedBy<DefaultBlogService>().LifeStyle.Transient,
                    Component.For<ILogger>().ImplementedBy<DefaultLogger>().LifeStyle.Transient
                );
        }

        public static IWindsorContainer BuildForConsoleService()
        {
            return new WindsorContainer()
                .AddFacility<WcfFacility>()
                .Register(
                    AllControllers(),

                    Component.For<IBlogService>()
                        .ActAs(new DefaultClientModel
                        {
                            Endpoint = WcfEndpoint
                                .BoundTo(new NetTcpBinding())
                                .At("net.tcp://localhost/BlogService")
                        })
                );
        }

        public static IWindsorContainer BuildForWebservice()
        {
            return new WindsorContainer()
                .AddFacility<WcfFacility>()
                .Register(
                    AllControllers(),

                    Component.For<IBlogService>()
                        .ActAs(new DefaultClientModel
                        {
                            Endpoint = WcfEndpoint
                                .BoundTo(new WSHttpBinding(SecurityMode.None))
                                .At("http://localhost:50388/BlogService.svc/ws")
                        })
                );
        }

        public static IWindsorContainer BuildForRestService()
        {
            return new WindsorContainer()
                .AddFacility<WcfFacility>()
                .Register(
                    AllControllers(),

                    Component.For<IBlogService>()
                        .ActAs(new RestClientModel("http://ipv4.fiddler:51223/BlogService.svc"))
                );
        }

        public static IWindsorContainer BuildForMultitenanted()
        {
            return new WindsorContainer()
                .AddFacility<WcfFacility>()
                .Register(
                    AllControllers(),

                    Component.For<IBlogService>()
                        .ActAs(new DefaultClientModel
                        {
                            Endpoint = WcfEndpoint
                                .BoundTo(new BasicHttpBinding())
                                .At("http://blue.shop/BlogService.svc")
                        })
                );
        }

        private static IRegistration AllControllers()
        {
            return AllTypes
                .Of<IController>()
                .FromAssembly(Assembly.GetExecutingAssembly())
                .Configure(c => c.LifeStyle.Transient.Named(c.Implementation.Name));
        }
    }
}