using System;

namespace Mike.IocDemo.Model
{
    public interface IReportBuilder : IDisposable
    {
        Report[] CreateReports();
    }
}