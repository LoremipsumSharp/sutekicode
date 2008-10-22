using Mike.IocDemo.Model;

namespace Mike.IocDemo.Default
{
    public class Program
    {
        public static void Main()
        {
            IReporter reporter = new Reporter();
            reporter.SendReports();
        }
    }
}