using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Mike.AdvancedWindsorTricks.Model;

namespace Mike.AdvancedWindsorTricks
{
    public class GenericHandlerSpike
    {
        public void Should_be_able_to_process_commands()
        {
            var container = new WindsorContainer()
                .Register(
                    Component.For<CommandProcessor>(),
                    AllTypes.FromAssembly(typeof(IHandle<>).Assembly)
                        .BasedOn(typeof(IHandle<>))
                        .WithService.Base()
                        .Configure(c => c.LifeStyle.Transient)
                );

            var commandProcessor = container.Resolve<CommandProcessor>();

            commandProcessor.ProcessCommand(new CreateCustomer());
            commandProcessor.ProcessCommand(new ProcessOrder("Joe"));
        }
    }
}