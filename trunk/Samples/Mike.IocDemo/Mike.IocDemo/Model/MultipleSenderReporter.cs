namespace Mike.IocDemo.Model
{
    public class MultipleSenderReporter : IReporter
    {
        private readonly IReportBuilder reportBuilder;
        private readonly IReportSenderFactory reportSenderFactory;

        public MultipleSenderReporter(IReportBuilder reportBuilder, IReportSenderFactory reportSenderFactory)
        {
            this.reportBuilder = reportBuilder;
            this.reportSenderFactory = reportSenderFactory;
        }

        public void SendReports()
        {
            var reports = reportBuilder.CreateReports();
            foreach (var report in reports)
            {
                var reportSender = reportSenderFactory.Create();
                reportSender.Send(report);
            }
        }
    }
}