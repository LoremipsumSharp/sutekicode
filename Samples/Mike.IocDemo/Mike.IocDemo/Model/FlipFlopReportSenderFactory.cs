namespace Mike.IocDemo.Model
{
    public class FlipFlopReportSenderFactory : IReportSenderFactory
    {
        private bool useEmail = true;

        public IReportSender Create()
        {
            useEmail = !useEmail;
            if (useEmail) return new EmailReportSender();
            return new SmsReportSender();
        }
    }
}
