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

namespace SmsGateway.Application.CoreContext.DTO.SMSModule
{
    /// <summary>
    /// Data transfer object para uma entidade
    /// </summary>
    public class ContatoDTO
    {
        /// <summary>
        /// Id da entidade
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do Contato
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Numero do Contato
        /// </summary>
        public long Numero { get; set; }

        /// <summary>
        /// Id do cliente do Contato
        /// </summary>
        public Guid ClienteId { get; set; }

        /// <summary>
        /// True se o Contato está ativo
        /// </summary>
        public bool IsEnabled { get; set; }
        
        
        
    }

   
}
