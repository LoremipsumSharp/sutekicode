using System;

namespace Mike.IocDemo.Model
{
    public class EmailReportSender : IReportSender
    {
        public string SmtpServer { get; set; }

        public EmailReportSender()
        {
            Console.WriteLine("Created instance of EmailReportSender");
        }

        public void Send(Report report)
        {
            if (!string.IsNullOrEmpty(SmtpServer))
            {
                Console.WriteLine("Sent '{0}' to '{1}' by email", report.Name, SmtpServer);
            }
            else
            {
                Console.WriteLine("Sent '{0}' by email", report.Name);
            }
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing EmailReportSender");
        }
    }
}