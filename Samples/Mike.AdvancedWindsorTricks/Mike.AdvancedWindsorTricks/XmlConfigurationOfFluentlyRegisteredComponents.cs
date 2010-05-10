using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Mike.AdvancedWindsorTricks.Model;

namespace Mike.AdvancedWindsorTricks
{
    public class XmlConfigurationOfFluentlyRegisteredComponents
    {
        public void can_use_xml_to_configure_fluenty_registered_components()
        {
            var container = new WindsorContainer()
                .Install(Configuration.FromXmlFile("windsor.config"))
                .Register(
                    Component
                        .For<IConfigurationThing>()
                        .ImplementedBy<ConfigurationThing>()
                        .Named("configuration")
                );

            var configuration = container.Resolve<IConfigurationThing>();

            Console.Out.WriteLine("configuration.Server = {0}", configuration.Server);
            Console.Out.WriteLine("configuration.Database = {0}", configuration.Database);
            Console.Out.WriteLine("configuration.User = {0}", configuration.User);
            Console.Out.WriteLine("configuration.Password = {0}", configuration.Password);
        }
    }
}