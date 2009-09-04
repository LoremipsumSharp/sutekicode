using System;

namespace Mike.IocDemo.Model
{
    public class SmsReportSender : IReportSender
    {
        public SmsReportSender()
        {
            Console.WriteLine("Created instance of SmtpReportSender");
        }

        public void Send(Report report)
        {
            Console.WriteLine("Sent '{0}' by SMS", report.Name);
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing SmsReportSender");
        }
    }
}