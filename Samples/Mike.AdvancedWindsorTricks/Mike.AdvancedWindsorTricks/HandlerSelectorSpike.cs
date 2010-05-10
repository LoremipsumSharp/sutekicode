using System;
using System.Linq;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Naming;
using Castle.Windsor;

namespace Mike.AdvancedWindsorTricks
{
    public class HandlerSelectorSpike
    {
        public void Should_be_able_to_choose_which_class_gets_resolved_at_runtime()
        {
            var container = new WindsorContainer();
            var context = new Context
            {
                Colour = "red",
                Version = "1"
            };
            container.Kernel.AddHandlerSelector(new ContextBasedHandlerSelector(context, container.Kernel));
            container.Kernel.AddSubSystem(SubSystemConstants.NamingKey, new NamingPartsSubSystem());
            container.Register(
                    Component.For<IThingy>().ImplementedBy<Thing1>().Named("thing1"),
                    Component.For<IThingy>().ImplementedBy<Thing2>().Named("thing2")
                );


            Console.WriteLine("thing is {0}", container.Resolve("thing").GetType().Name);

            context.Colour = "blue";

            Console.WriteLine("thing is {0}", container.Resolve("thing").GetType().Name);
        }

        private interface IThingy { }
        private class Thing1 : IThingy { }
        private class Thing2 : IThingy { }
        private class Thing3 : IThingy { }
        private class Thing4 : IThingy { }

        private class Context
        {
            public string Colour { get; set; }
            public string Version { get; set; }
        }

        private class ContextBasedHandlerSelector : IHandlerSelector
        {
            private readonly Context context;
            private readonly IKernel kernel;

            public ContextBasedHandlerSelector(Context context, IKernel kernel)
            {
                this.context = context;
                this.kernel = kernel;
            }

            public bool HasOpinionAbout(string key, Type service)
            {
                Console.WriteLine("key:{0} service:{1}", key, service.Name);
                if(string.IsNullOrEmpty(key)) return false;
                var keyWithParameters = GetKeyWithParameters(key);
                Console.WriteLine(keyWithParameters);
                return kernel.HasComponent(keyWithParameters);
            }

            public IHandler SelectHandler(string key, Type service, IHandler[] handlers)
            {
                return handlers.First(h => h.ComponentModel.Name == GetKeyWithParameters(key));
            }

            private string GetKeyWithParameters(string key)
            {
                return string.Format("{0}:colour={1},version={2}", key, context.Colour, context.Version);
            }
        }
    }
}