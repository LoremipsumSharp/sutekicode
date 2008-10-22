using Microsoft.Practices.ServiceLocation;

namespace Mike.IocDemo.Model
{
    public class MegaFramework
    {
        public void Execute()
        {
            var reporter = ServiceLocator.Current.GetInstance<IReporter>();
            reporter.SendReports();
        }
    }
}