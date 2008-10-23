using Mike.IocDemo.Model;
using NUnit.Framework;
using Rhino.Mocks;

namespace Mike.IocDemo.IoC
{
    [TestFixture]
    public class ReporterTests
    {
        [Test]
        public void SendReports_ShouldSendSomeReports()
        {
            // create mocks
            var reportBuilder = MockRepository.GenerateMock<IReportBuilder>();
            var reportSender = MockRepository.GenerateMock<IReportSender>();

            // stub
            var report1 = new Report();
            var report2 = new Report();
            var reports = new [] { report1, report2 };
            reportBuilder.Stub(rb => rb.CreateReports()).Return(reports);

            // exercise method
            var reporter = new Reporter(reportBuilder, reportSender);
            reporter.SendReports();

            // assert calls on reportSender
            reportSender.AssertWasCalled(rs => rs.Send(report1));
            reportSender.AssertWasCalled(rs => rs.Send(report2));
        }
    }
}