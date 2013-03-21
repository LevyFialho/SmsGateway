using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmsGateway.Presentation.WebClient.Models
{
    public class MinhasMensagensModel
    {
        public AreaDoClienteModel DadosDoCliente { get; set; }

        public string ListaId { get; set; }

        public string ContatoId { get; set; }
        
        [MaxLength(150)]
        public string Mensagem { get; set; }

        public string Resultado { get; set; }
    }
}