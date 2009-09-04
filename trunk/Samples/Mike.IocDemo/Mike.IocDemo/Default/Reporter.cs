using System;
using Mike.IocDemo.Model;

namespace Mike.IocDemo.Default
{
    public class Reporter : IReporter
    {
        public Reporter()
        {
            Console.WriteLine("Created instance of Reporter");
        }

        public void SendReports()
        {
            var reportBuilder = new ReportBuilder();
            var reportSender = new EmailReportSender();
            var logger = new Logger();

            var reports = reportBuilder.CreateReports();
            foreach (var report in reports)
            {
                logger.Log(string.Format("Sending report {0}", report.Name));
                reportSender.Send(report);    
            }
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing Reporter");
        }
    }
}