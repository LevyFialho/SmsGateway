using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmsGateway.Application.CoreContext.DTO.SMSModule;

namespace SmsGateway.Presentation.WebClient.Models
{
    public class SmsTestModel
    {
        public List<ClienteDTO> ListaDeClientes { get; set; }
        public Guid IdCliente { get; set; }
        public MensagemDTO Mensagem { get; set; }
    }
}