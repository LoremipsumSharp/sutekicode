using System;
using Castle.MicroKernel;

namespace Mike.AdvancedWindsorTricks.Model
{
    public interface ICommand {}

    public interface IHandle<TCommand> : IHandler where TCommand : ICommand
    {
    }

    public interface IHandler
    {
        void Execute(ICommand command);
    }

    /// <summary>
    /// Simple command processor with ugly hard reference to the container
    /// </summary>
    public class CommandProcessor
    {
        private readonly IKernel kernel;

        public CommandProcessor(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public void ProcessCommand(ICommand command)
        {
            var handlerType = typeof (IHandle<>).MakeGenericType(command.GetType());
            var handlers = kernel.ResolveAll(handlerType);
            foreach (IHandler handler in handlers)
            {
                try
                {
                    handler.Execute(command);
                }
                finally
                {
                    kernel.ReleaseComponent(handler);
                }
            }
        }        
    }

    // ------------------------- some commands and handlers -------------------------------

    public class CreateCustomer : ICommand
    {
        public void CreateACustomer()
        {
            Console.WriteLine("Created a customer");
        }
    }

    public class ProcessOrder : ICommand
    {
        public string Name { get; private set; }

        public ProcessOrder(string name)
        {
            Name = name;
        }
    }

    public class HandleCreateCustomer : IHandle<CreateCustomer>
    {
        public void Execute(ICommand command)
        {
            ((CreateCustomer)command).CreateACustomer();
        }
    }

    public class HandleTakePayment : IHandle<ProcessOrder>
    {
        public void Execute(ICommand command)
        {
            var name = ((ProcessOrder)command).Name;
            Console.WriteLine("Taking payment from {0}", name);
        }
    }

    public class HandleDispatchOrder : IHandle<ProcessOrder>
    {
        public void Execute(ICommand command)
        {
            var name = ((ProcessOrder)command).Name;
            Console.WriteLine("Dispatching order to {0}", name);
        }
    }
}