using System;

namespace Mike.IocDemo.Model
{
    public interface IReportSender : IDisposable
    {
        void Send(Report report);
    }
}