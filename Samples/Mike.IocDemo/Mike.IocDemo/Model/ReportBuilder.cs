using System;

namespace Mike.IocDemo.Model
{
    public class ReportBuilder : IReportBuilder
    {
        public ReportBuilder()
        {
            Console.WriteLine("Created instance of ReportBuilder");
        }

        public Report[] CreateReports()
        {
            return new[]
                   {
                       new Report { Name = "Report 1" }, 
                       new Report { Name = "Report 2" }
                   };
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing ReportBuilder");
        }
    }
}