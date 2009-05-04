using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Suteki.Blog.Client.Controller;
using Suteki.Blog.Service;

namespace Suteki.Blog.Client.IoC
{
    public class ContainerBuilder
    {
        public static IWindsorContainer Build()
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
    }
}