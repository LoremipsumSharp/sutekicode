namespace Mike.IocDemo.Model
{
    public class EmailReportSenderFactory : IReportSenderFactory
    {
        public IReportSender Create()
        {
            return new EmailReportSender();
        }
    }
}