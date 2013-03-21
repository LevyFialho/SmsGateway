using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmsGateway.Application.CoreContext.DTO.SMSModule;

namespace SmsGateway.Presentation.WebClient.Models
{
    public class AdicionarContatoModel
    {
        public string ListaId { get; set; }
         
        public List<ContatoDTO> ContatosForaDaLista { get; set; }


    }
}