using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

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

        public void can_I_register_a_delegate()
        {
            var container = new WindsorContainer();
            container
                .Register(
                    Component.For<Do>().Named("do1"),
                    Component.For<Do>().ImplementedBy<BigDo>().Named("do2"),
                    Component.For<Func<String,Do>>().Instance(container.Resolve<Do>),
                    Component.For<UseDo>()
                    );

            var useDo = container.Resolve<UseDo>();
            var do1 = useDo.GetDo("do1");
            var do2 = useDo.GetDo("do2");

            do1.SayHello();
            do2.SayHello();
        }
    }

    public class Do
    {
        public virtual void SayHello()
        {
            Console.WriteLine("Hello from Do");
        }
    }

    public class BigDo : Do
    {
        public override void SayHello()
        {
            Console.WriteLine("Hello from BigDo");
        }
    }

    public class UseDo
    {
        private readonly Func<String, Do> doFactory;

        public UseDo(Func<String, Do> doFactory)
        {
            this.doFactory = doFactory;
        }

        public Do GetDo(string name)
        {
            return doFactory(name);
        }
    }
}