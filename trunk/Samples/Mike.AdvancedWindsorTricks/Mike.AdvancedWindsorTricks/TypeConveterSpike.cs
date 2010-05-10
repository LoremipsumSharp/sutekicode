using System;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.MicroKernel.SubSystems.Conversion;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Mike.AdvancedWindsorTricks.Model;

namespace Mike.AdvancedWindsorTricks
{
    public class TypeConveterSpike
    {
        public void should_be_able_to_use_a_typeConverter_to_build_complex_configuration()
        {
            var container = new WindsorContainer()
                .Install(new HealthMonitorTypeConveterInstaller())
                .Install(Configuration.FromXmlFile("windsor.config"))
                .Register(
                    Component.For<IHealthMonitor>().ImplementedBy<HealthMonitor>().Named("healthMonitor")
                );

            var healthMonitor = container.Resolve<IHealthMonitor>();

            foreach (var healthEndpoint in healthMonitor.HealthEndpoints)
            {
                Console.Out.WriteLine("healthEndpoint.Url = {0}", healthEndpoint.Url);
                Console.Out.WriteLine("healthEndpoint.Expect = {0}", healthEndpoint.Expect);
                Console.Out.WriteLine("healthEndpoint.TimeoutSeconds = {0}", healthEndpoint.TimeoutSeconds);
            }
        }
    }

    public class HealthMonitorTypeConveterInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var manager = (IConversionManager)container.Kernel.GetSubSystem(SubSystemConstants.ConversionManagerKey);
            manager.Add(new HealthEndpointTypeConverter());
        }
    }
}