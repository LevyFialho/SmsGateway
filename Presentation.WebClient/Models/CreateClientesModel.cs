//=================================================================================== 
// INSTITUTO INFNET - GRADUAÇÃO EM ANÁLISE E DESENVOLVIMENTO DE SISTEMAS
// TRABALHO DE CONCLUSÃO DO CURSO
// AUTORES:
// JAIR MARTINS
// LEVY FIALHO
// MARCELO SÁ
//===================================================================================
// Este código foi desenvolvido com o objetivo de demonstrar a aplicação prática de 
// padrões de desenvolvimento de software adotados no mercado no ano de 2012.

// Mais especificamente, o código demonstra a aplicação prática de conceitos abordados
// em Domain driven Design e Patterns of Application Architechture na plataforma .Net
//===================================================================================

using System.ComponentModel.DataAnnotations;
using SmsGateway.Application.CoreContext.DTO.SMSModule;

namespace SmsGateway.Presentation.WebClient.Models
{
    public class CreateClientesModel
    {
        [Required]
        public ClienteDTO Cliente { get; set; }

        [Required]
        public ContratoDTO ContratoAtual { get; set; }
    }
}