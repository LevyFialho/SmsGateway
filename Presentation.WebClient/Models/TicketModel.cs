using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmsGateway.Application.CoreContext.DTO.SMSModule;

namespace SmsGateway.Presentation.WebClient.Models
{
    public class TicketModel
    {
        public TicketDTO Ticket { get; set; }

        public MensagemDoTicketDTO NovaMensagem { get; set; }
    }
}