using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Mike.AdvancedWindsorTricks.Model;

namespace Mike.AdvancedWindsorTricks
{
    public class TypeForwardingSpike
    {

        public void Should_be_able_to_have_one_class_satisfy_several_services()
        {
            var container = new WindsorContainer()
                .Register(
                    Component
                        .For<IThing, IWidget, IWonder>()
                        .ImplementedBy<SrpViolator>()
                );

            var thing = container.Resolve<IThing>();
            Console.WriteLine(thing.SayHello("Krzysztof"));

            var widget = container.Resolve<IWidget>();
            Console.WriteLine("The answer is {0}", widget.Calculate(2, 3));

            var wonder = container.Resolve<IWonder>();
            wonder.DoesNothing();

            Console.Out.WriteLine(thing == widget);
            Console.Out.WriteLine(thing == wonder);
            Console.Out.WriteLine(wonder == widget);
        }
    }
}