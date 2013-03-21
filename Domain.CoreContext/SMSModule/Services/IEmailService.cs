using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;

namespace SmsGateway.Domain.CoreContext.SMSModule.Services
{
    public interface IEmailService
    {
        void SendEmail(MailMessage message);
         }
}
