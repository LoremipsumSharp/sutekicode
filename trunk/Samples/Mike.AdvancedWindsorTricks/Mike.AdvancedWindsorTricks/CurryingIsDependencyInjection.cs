using System;

namespace Mike.AdvancedWindsorTricks
{
    public class CurryingIsDependencyInjection
    {
        class Reporter
        {
            private readonly IReportRepository reportRepository;
            private readonly IReportSender reportSender;

            public Reporter(IReportRepository reportRepository, IReportSender reportSender)
            {
                this.reportRepository = reportRepository;
                this.reportSender = reportSender;
            }

            public void SendReportById(int id)
            {
                var report = reportRepository.GetReportById(id);
                reportSender.SendReport(report);
            }
        }

        interface IReportRepository
        {
            Report GetReportById(int id);
        }

        class ReportRepository : IReportRepository
        {
            public Report GetReportById(int id)
            {
                return new Report();
            }
        }

        interface IReportSender
        {
            void SendReport(Report report);
        }

        class ReportSender : IReportSender
        {
            public void SendReport(Report report)
            {
                Console.WriteLine("Sent report {0}", report);
            }
        }

        class Report
        {
            
        }
    }
}