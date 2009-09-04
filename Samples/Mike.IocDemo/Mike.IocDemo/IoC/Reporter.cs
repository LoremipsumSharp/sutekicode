using System;
using Mike.IocDemo.Model;

namespace Mike.IocDemo.IoC
{
public class Reporter : IReporter
{
    private readonly IReportBuilder reportBuilder;
    private readonly IReportSender reportSender;

    public Reporter(IReportBuilder reportBuilder, IReportSender reportSender)
    {
        this.reportBuilder = reportBuilder;
        this.reportSender = reportSender;

        Console.WriteLine("Created instance of Reporter");
    }

    public void SendReports()
    {
        var reports = reportBuilder.CreateReports();
        foreach (var report in reports)
        {
            reportSender.Send(report);
        }
    }

    public void Dispose()
    {
        Console.WriteLine("Disposing Reporter");
    }
}
}