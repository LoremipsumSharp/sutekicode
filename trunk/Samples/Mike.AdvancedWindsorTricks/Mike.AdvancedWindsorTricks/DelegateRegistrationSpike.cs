using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Mike.AdvancedWindsorTricks.Model;

namespace Mike.AdvancedWindsorTricks
{
    public class DelegateRegistrationSpike
    {
        public void register_a_delegate()
        {
            var container = new WindsorContainer()
                .Register(
                    Component.For<Func<string>>().Instance(() => "Hello World")
                );

            var sayHello = container.Resolve<Func<string>>();
            Console.WriteLine(sayHello());
        }

        public void use_a_delegate_as_a_factory()
        {
            var container = new WindsorContainer();
            container
                .Register(
                    Component.For<IThing>().ImplementedBy<ThingOne>().Named("thingOne"),
                    Component.For<IThing>().ImplementedBy<ThingTwo>().Named("thingTwo"),
                    Component.For<Func<string,IThing>>().Instance(container.Resolve<IThing>),
                    Component.For<IUseThing>().ImplementedBy<UseThing>()
                    );

            var useDo = container.Resolve<IUseThing>();
            var thing1 = useDo.GetThing("thingOne");
            var thing2 = useDo.GetThing("thingTwo");

            Console.WriteLine(thing1.SayHello("Mike"));
            Console.WriteLine(thing2.SayHello("Mike"));
        }
    }
}