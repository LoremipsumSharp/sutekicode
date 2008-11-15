using System.Linq;
using System;
using Castle.MicroKernel;

namespace Mike.IocDemo.Model
{
    public enum SenderType
    {
        Email,
        Sms
    }

    public class ReportSendTypeSelector : IHandlerSelector 
    {
        private readonly SenderType senderType;

        public ReportSendTypeSelector(SenderType senderType)
        {
            this.senderType = senderType;
        }

        public bool HasOpinionAbout(string key, Type service)
        {
            if(service == null)
            {
                throw new ArgumentNullException("service");
            }

            return service == typeof (IReportSender);
        }

        public IHandler SelectHandler(string key, Type service, IHandler[] handlers)
        {
            if (handlers == null)
            {
                throw new ArgumentNullException("handlers");
            }

            Type implementationType = null;
            switch (senderType)
            {
                case SenderType.Email:
                    implementationType = typeof (EmailReportSender);
                    break;
                case SenderType.Sms:
                    implementationType = typeof (SmsReportSender);
                    break;
            }

            var emailReportSenderHandlers = handlers
                .Where(handler => handler.ComponentModel.Implementation == implementationType);

            if(!emailReportSenderHandlers.Any())
            {
                throw new ApplicationException(
                    string.Format("No components of type {0} have been registered", implementationType.Name));
            }

            return emailReportSenderHandlers.First();
        }
    }
}