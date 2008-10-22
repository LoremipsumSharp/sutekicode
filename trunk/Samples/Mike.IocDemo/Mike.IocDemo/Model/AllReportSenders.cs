using System;

namespace Mike.IocDemo.Model
{
    public class AllReportSenders : IReportSender
    {
        private readonly IReportSender[] reportSenders;

        public AllReportSenders(IReportSender[] reportSenders)
        {
            this.reportSenders = reportSenders;

            Console.WriteLine("Created instance of AllReportSenders");
        }

        public void Send(Report report)
        {
            foreach (var reportSender in reportSenders)
            {
                reportSender.Send(report);
            }
        }
    }
}