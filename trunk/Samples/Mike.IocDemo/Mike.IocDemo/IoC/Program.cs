using System;
using System.Linq;
using System.Threading;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Microsoft.Practices.ServiceLocation;
using Mike.IocDemo.Model;
using Mike.IocDemo.Windsor;

namespace Mike.IocDemo.IoC
{
    public class Program
    {
        public static void DemonstrateDependencyInjection()
        {
            var builder = new ReportBuilder();
            var sender = new EmailReportSender();
            var reporter = new Reporter(builder, sender);
            reporter.SendReports();
        }










        public static void DemonstrateBasicWindsorUsage()
        {
            var container = new WindsorContainer();

            container.Register(
                Component.For<IReporter>().ImplementedBy<Reporter>(),
                Component.For<IReportBuilder>().ImplementedBy<ReportBuilder>(),
                Component.For<IReportSender>().ImplementedBy<EmailReportSender>()
                );

            var reporter = container.Resolve<IReporter>();
            reporter.SendReports();
        }










        public static void DemonstrateXmlConfig()
        {
            var container = new WindsorContainer(new XmlInterpreter());

            var reporter = container.Resolve<IReporter>();
            reporter.SendReports();
        }










        public static void DemonstrateNameBasedResolution()
        {
            var container = new WindsorContainer();

            container.Register(
                Component.For<IReporter>().ImplementedBy<Reporter>()
                    .ServiceOverrides(
                        ServiceOverride.ForKey("reportSender").Eq("email_report_sender")
                    ),
                Component.For<IReportBuilder>().ImplementedBy<ReportBuilder>(),
                Component.For<IReportSender>().ImplementedBy<EmailReportSender>().Named("email_report_sender"),
                Component.For<IReportSender>().ImplementedBy<SmsReportSender>().Named("sms_report_sender")
                );

            var reporter = container.Resolve<IReporter>();
            reporter.SendReports();
        }










        public static void DemonstratePropertyBasedConfiguration()
        {
            var container = new WindsorContainer();

            container.Register(
                Component.For<IReporter>().ImplementedBy<Reporter>(),
                Component.For<IReportBuilder>().ImplementedBy<ReportBuilder>(),
                Component.For<IReportSender>().ImplementedBy<EmailReportSender>()
                    .DependsOn(
                        Property.ForKey("SmtpServer").Eq("smtp.mikehadlow.com")
                    )
                );

            var reporter = container.Resolve<IReporter>();
            reporter.SendReports();
        }










        public static void DemonstrateInstanceRegistration()
        {
            var container = new WindsorContainer();

            var configuration = new Configuration("Mike", "The important connection string");

            container.Register(
                Component.For<Configuration>().Instance(configuration)
                );

            var resolvedConfiguration = container.Resolve<Configuration>();

            Console.WriteLine("Connection String is: '{0}'", resolvedConfiguration.ConnectionString);
        }










        public static void DemonstrateLifestyles()
        {
            var container = new WindsorContainer();

            container.Register(
                Component.For<IReporter>().ImplementedBy<Reporter>().LifeStyle.Transient,
                Component.For<IReportBuilder>().ImplementedBy<ReportBuilder>().LifeStyle.Singleton,
                Component.For<IReportSender>().ImplementedBy<SmsReportSender>().LifeStyle.Singleton
                );

            var reporter = container.Resolve<IReporter>();
            reporter.SendReports();

            var reporter2 = container.Resolve<IReporter>();
            reporter2.SendReports();
        }










        public static void DemonstratePerThreadLifestyle()
        {
            var container = new WindsorContainer();

            container.Register(
                Component.For<IReporter>().ImplementedBy<Reporter>().LifeStyle.PerThread,
                Component.For<IReportBuilder>().ImplementedBy<ReportBuilder>().LifeStyle.Singleton,
                Component.For<IReportSender>().ImplementedBy<SmsReportSender>().LifeStyle.Singleton
                );

            ThreadStart sendReports = () =>
                              {
                                  var reporter = container.Resolve<IReporter>();
                                  reporter.SendReports();
                                  var reporter2 = container.Resolve<IReporter>();
                                  reporter2.SendReports();
                              };

            var thread1 = new Thread(sendReports);
            thread1.Start();
            thread1.Join();
            var thread2 = new Thread(sendReports);
            thread2.Start();
            thread2.Join();
        }










        public static void DemonstrateInitialisableDisposable()
        {
            var container = new WindsorContainer();
            container.Register(
                Component.For<IReporter>().ImplementedBy<Reporter>().LifeStyle.Singleton,
                Component.For<IReportBuilder>().ImplementedBy<ReportBuilder>().LifeStyle.Transient,
                Component.For<IReportSender>().ImplementedBy<SmsReportSender>().LifeStyle.Transient
                );

            var reporter = container.Resolve<IReporter>();
            reporter.SendReports();
            container.Release(reporter);
        }










        public static void DemonstrateDecorator()
        {
            var container = new WindsorContainer();

            container.Register(
                Component.For<IReporter>().ImplementedBy<Reporter>()
                    .ServiceOverrides(
                        ServiceOverride.ForKey("reportSender").Eq("report_logger")
                    ),
                Component.For<IReportBuilder>().ImplementedBy<ReportBuilder>(),
                Component.For<IReportSender>().ImplementedBy<EmailReportSender>(),
                Component.For<IReportSender>().ImplementedBy<ReportSendLogger>().Named("report_logger"),
                Component.For<ILogger>().ImplementedBy<Logger>()
                );

            var reporter = container.Resolve<IReporter>();
            reporter.SendReports();
        }










        public static void DemonstrateAbstractFactory()
        {
            var container = new WindsorContainer();

            container.Register(
                Component.For<IReporter>().ImplementedBy<MultipleSenderReporter>(),
                Component.For<IReportSenderFactory>().ImplementedBy<FlipFlopReportSenderFactory>(),
                Component.For<IReportBuilder>().ImplementedBy<ReportBuilder>()
                );

            var reporter = container.Resolve<IReporter>();
            reporter.SendReports();
        }










        public static void DemonstrateComposite()
        {
            var container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));

            container.Register(
                Component.For<IReportBuilder>().ImplementedBy<ReportBuilder>(),
                Component.For<IReportSender>().ImplementedBy<AllReportSenders>(),
                Component.For<IReportSender>().ImplementedBy<SmsReportSender>(),
                Component.For<IReportSender>().ImplementedBy<EmailReportSender>(),
                Component.For<IReporter>().ImplementedBy<Reporter>()
                );

            var reporter = container.Resolve<IReporter>();
            reporter.SendReports();
        }










        public static void DemonstrateGenerics()
        {
            var container = new WindsorContainer();

            container.Register(
                Component.For<IRepository<Report>>().ImplementedBy<ReportRepository>(),
                Component.For(typeof(IRepository<>)).ImplementedBy(typeof(Repository<>))
                );

            var thingRepository = container.Resolve<IRepository<Report>>();
            var things = thingRepository.GetAll();

            Console.WriteLine("There are {0} things", things.Count());
        }










        public static void DemonstrateTypeSelector()
        {
            var container = new WindsorContainer();

            container.Kernel.AddHandlerSelector(new ReportSendTypeSelector(SenderType.Email));

            container.Register(
                Component.For<IReportBuilder>().ImplementedBy<ReportBuilder>(),
                Component.For<IReportSender>().ImplementedBy<SmsReportSender>(),
                Component.For<IReportSender>().ImplementedBy<EmailReportSender>(),
                Component.For<IReporter>().ImplementedBy<Reporter>()
                );

            var reporter = container.Resolve<IReporter>();
            reporter.SendReports();
        }










        public static void DemonstrateActivator()
        {
            var container = new WindsorContainer();

            container.Register(
                Component.For<HttpContext>().Activator<HttpContextActivator>()
                );

            var httpContext = container.Resolve<HttpContext>();

            if(httpContext == HttpContext.Current)
            {
                Console.WriteLine("The container returned HttpContext from HttpContext.Current");
            }
            else
            {
                Console.WriteLine("Boo! It didn't work!");
            }
        }










        public static void DemonstrateServiceLocator()
        {
            var container = new WindsorContainer();

            container.Register(
                Component.For<IReportBuilder>().ImplementedBy<ReportBuilder>(),
                Component.For<IReportSender>().ImplementedBy<EmailReportSender>(),
                Component.For<IReporter>().ImplementedBy<Reporter>()
                );

            // register windsor container with the ServiceLocator
            var windsorServiceLocator = new WindsorServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => windsorServiceLocator);

            // use a framework that leverages ServiceLocator
            var framework = new MegaFramework();
            framework.Execute();
        }
    }
}