using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Services;

namespace SmsGateway.Infrastructure.Data.CoreContext.SMSModule.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(MailMessage message)
        {
            var remetente = "levy.fialho@gmail.com";
            var senha = "XDR56tfc";
                
            var server = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(remetente,senha),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true
                };
            message.From = new MailAddress(remetente);
            message.IsBodyHtml = true;
            message.Body = MensagemFormatadaComHtml(message.Body);
            server.Send(message);
          
             
        }

        

        public string MensagemFormatadaComHtml(string msg)
        {
            return "<html><body><table><tr><th>SMSAGILE.COM</th></tr><tr><td>" +
                "<p>" + msg + "</p></td></tr><tr><td><a href='http://www.smsagile.com'>SMSAGILE.COM</a></td></tr></body></html>";
        }
    }
}

