namespace Mike.IocDemo.Model
{
    public class SmsReportSenderFactory : IReportSenderFactory
    {
        public IReportSender Create()
        {
            return new SmsReportSender();
        }
    }
}