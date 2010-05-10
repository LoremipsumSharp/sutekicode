using System;
using Castle.Core.Configuration;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Mike.AdvancedWindsorTricks
{
    public class CurryFacilitySpike
    {
        public void Should_be_able_to_get_curried_functions_from_container()
        {
            var container = new WindsorContainer()
                .AddFacility<CurryFacility>()
                .Register(
                    Component.For<Func<string, SomeOutput>>(),//.SuppliedBy(FunctionsOne.SomeOperation),
                    Component.For<Func<SomeInput, SomeOutput>>()//.SuppliedBy(FunctionsTwo.SomeOtherOperation)
                );

            var getOutput = container.Resolve<Func<string, SomeOutput>>();

            // writes "Hello World" to the console
            Console.WriteLine(getOutput("Hello World"));
        }

        public void Should_be_able_to_register_a_delegate()
        {
            var container = new WindsorContainer()
                .Register(
                    Component.For<ComponentOne>(),
                    Component.For<Func<SomeInput, SomeOutput>>().Instance(FunctionsTwo.SomeOtherOperation)
                );

            var componentOne = container.Resolve<ComponentOne>();

            var output = componentOne.SomeOperation("Hello World");

            // writes "Hello World" to the console
            Console.WriteLine(output.SomeOtherParameter);
        }
    }

   

    public static class ComponentRegistrationExtensions
    {
        public static ComponentRegistration<T> SuppliedBy<T>(this ComponentRegistration<T> registration, Delegate @delegate)
        {
            return registration;
        }
    }

    public class CurryFacility : IFacility
    {
        public void Init(IKernel kernel, IConfiguration facilityConfig)
        {
            
        }

        public void Terminate()
        {
            
        }
    }

    public class Service
    {
        private readonly Func<string, SomeOutput> getOutput;

        public Service(Func<string, SomeOutput> getOutput)
        {
            this.getOutput = getOutput;
        }

        public void Execute()
        {
            var output = getOutput("Hello World");
            Console.WriteLine(output.SomeOtherParameter);
        }
    }

    public static class FunctionsOne
    {
        public static SomeOutput SomeOperation(Func<SomeInput, SomeOutput> getInput, string someParameter)
        {
            var input = new SomeInput { SomeParameter = someParameter };
            return getInput(input);
        }
    }

    public class ComponentOne
    {
        private readonly Func<SomeInput, SomeOutput> getInput;

        public ComponentOne(Func<SomeInput, SomeOutput> getInput)
        {
            this.getInput = getInput;
        }

        public SomeOutput SomeOperation(string someParameter)
        {
            var input = new SomeInput { SomeParameter = someParameter };
            return getInput(input);
        }
    }

    public static class FunctionsTwo
    {
        public static SomeOutput SomeOtherOperation(SomeInput input)
        {
            return new SomeOutput { SomeOtherParameter = input.SomeParameter };
        }
    }

    public class SomeInput
    {
        public string SomeParameter { get; set; }
    }
    public class SomeOutput
    {
        public string SomeOtherParameter { get; set; }
    }
}