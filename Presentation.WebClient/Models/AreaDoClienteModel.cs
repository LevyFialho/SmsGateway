using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmsGateway.Application.CoreContext.DTO.SMSModule;

namespace SmsGateway.Presentation.WebClient.Models
{
    public class AreaDoClienteModel
    {
        public ClienteDTO Cliente { get; set; }


        public ContratoDTO Contrato { get; set; }


        public List<ContatoDTO> Contatos { get; set; }


        public List<ListaDeContatosDTO> ListasDeContatos { get; set; }


        public List<MensagemDTO> Mensagens { get; set; }

        public List<TicketDTO> Tickets { get; set; }
    }
}