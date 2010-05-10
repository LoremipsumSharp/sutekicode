using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Mike.AdvancedWindsorTricks.Model;

namespace Mike.AdvancedWindsorTricks
{
    public class GenericRegistrationSpike
    {
        public void Register_and_resolve_a_generic_repository()
        {
            var container = new WindsorContainer()
                .Register(
                    AllTypes.FromAssembly(typeof(IThing).Assembly)
                        .BasedOn(typeof(IRepository<>))
                        .WithService.Base()
                        .Configure(c => c.LifeStyle.Transient)
                );

            DependencyGraphWriter.WriteGraph(container, Console.Out);

            var productRepository = container.Resolve<IRepository<Product>>();

            Console.Out.WriteLine("productRepository.GetType().Name = {0}", productRepository.GetType().Name);

            var customerRepository = container.Resolve<IRepository<Customer>>();

            Console.Out.WriteLine("customerRepository.GetType().Name = {0}", customerRepository.GetType().Name);

        }
    }
}