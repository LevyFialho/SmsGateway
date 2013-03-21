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
    public class SolicitacaoDeCadastroDTO
    {
        /// <summary>
        /// Id da entidade
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do cliente
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Email do cliente
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Telefone do cliente
        /// </summary>
        public long Telefone { get; set; }

        /// <summary>
        /// Id do contrato atual do cliente
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// True se o cliente está ativo
        /// </summary>
        public bool IsEnabled { get; set; }


        
    }
}
