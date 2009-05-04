using System;
using Castle.Windsor;
using Suteki.Blog.Webservice.IoC;

namespace Suteki.Blog.Webservice
{
    public class Global : System.Web.HttpApplication, IContainerAccessor
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            container = ContainerBuilder.Build();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            container.Dispose();
        }

        private IWindsorContainer container;

        public IWindsorContainer Container
        {
            get { return container; }
        }
    }
}