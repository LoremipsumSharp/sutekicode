using System;

namespace Mike.IocDemo.Model
{
    public interface IReporter : IDisposable
    {
        void SendReports();
    }
}