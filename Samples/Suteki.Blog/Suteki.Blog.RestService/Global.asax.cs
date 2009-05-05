using System;
using System.Web;
using Castle.Windsor;
using Suteki.Blog.RestService.IoC;

namespace Suteki.Blog.RestService
{
    public class Global : HttpApplication, IContainerAccessor
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Container = ContainerBuilder.Build();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (Container != null)
            {
                Container.Dispose();
            }
        }

        public IWindsorContainer Container { get; private set; }
    }
}