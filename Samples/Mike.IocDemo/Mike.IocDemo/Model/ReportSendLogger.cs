using System;

namespace Mike.IocDemo.Model
{
    public class ReportSendLogger : IReportSender
    {
        private readonly IReportSender reportSender;
        private readonly ILogger logger;

        public ReportSendLogger(IReportSender reportSender, ILogger logger)
        {
            this.reportSender = reportSender;
            this.logger = logger;

            Console.WriteLine("Created instance of ReportSendLogger");
        }

        public void Send(Report report)
        {
            logger.Log(string.Format("Sending report {0}", report.Name));
            reportSender.Send(report);
        }
    }
}