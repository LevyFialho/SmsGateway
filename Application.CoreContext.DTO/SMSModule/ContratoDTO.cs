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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;

namespace SmsGateway.Application.CoreContext.DTO.SMSModule
{
    /// <summary>
    /// Data transfer object para uma entidade
    /// </summary>
    public class ContratoDTO
    {
        #region Properties

        /// <summary>
        /// Id da entidade
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Data em que o contrato entra em vigor
        /// </summary>
        public DateTime DataInicial { get; set; }

        /// <summary>
        /// Data em que o contrato termina
        /// </summary>
        public DateTime DataFinal { get; set; }

        /// <summary>
        /// Quantidade total de mensagens que o contrato permite enviar
        /// </summary>
        public int SaldoDeMensagens { get; set; }

        /// <summary>
        /// Valor cobrado por mesnsagem enviada a aprtir deste contrato
        /// </summary>
        public double ValorMensagem { get; set; }

        /// <summary>
        /// Id do contrato associado a este contrato quando este é renovado
        /// </summary>
        public Guid ContratoRenovadoId { get; set; }

        /// <summary>
        /// Id do cliente associado a este contrato
        /// </summary>
        public Guid ClienteId { get; set; }

        /// <summary>
        /// Get or set se o contrato está ativo
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Verifica a api usada pelo contrato
        /// </summary>
        public OperadoraApiDTO OperadoraApi { get; set; }

        /// <summary>
        /// Tipo de contrato (Operadora ou cliente)
        /// </summary>
        public TipoDeContratoDTO TipoDeContrato { get; set; }

        #endregion
    }
}
