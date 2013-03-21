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

using System;
using System.Collections.Generic;

namespace SmsGateway.Application.CoreContext.DTO.SMSModule
{
    /// <summary>
    /// Data transfer object para uma entidade
    /// </summary>
    public class TicketDTO
    {
        /// <summary>
        /// Id da entidade
        /// </summary>
        public Guid Id { get; set; }
 
        public Guid ClienteId { get; set; }
         
        public string ClienteNome { get; set; }
         
        public string Assunto { get; set; }

        public DateTime Data { get; set; }
 
        public bool IsEnabled { get; set; }

        public List<MensagemDoTicketDTO> Mensagens { get; set; }

        public StatusDoTicketDTO Status { get; set; }
        
    }

    public class MensagemDoTicketDTO
    {
        /// <summary>
        /// Id da entidade
        /// </summary>
        public Guid Id { get; set; }

        public Guid TicketId { get; set; }

        public string Texto { get; set; }

        public DateTime Data { get; set; }

        public bool IsEnabled { get; set; }



    }

    public enum StatusDoTicketDTO
    {
        Pendente,
        Resolvido
    }
}
