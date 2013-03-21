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
    public class StatusDTO
    {
        #region Properties

        /// <summary>
        /// Id da entidade
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Codigo retornado pela operadora que identifica o Status
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Descrição do Status para os administradores do sistema
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Descrição do Status para os clientes do sistema
        /// </summary>
        public string MensagemAoCliente { get; set; }

        /// <summary>
        /// Valor  de créditos a serem debitados do contrato do cliente
        /// </summary>
        public int QuantoDebitarDoContratoDoCliente { get; private set; }
        /// <summary>
        /// Valor de créditos a serem debitados do contrato com a operadora
        /// </summary>
        public int QuantoDebitarDoContratoDaOperadora { get; private set; }

        /// <summary>
        /// Valor em $ da operação realizada.
        /// </summary>
        public double ValorDaOperacao { get; private set; }


        /// <summary>
        /// Tipo de API que a Operadora utiliza
        /// </summary>
        public OperadoraApiDTO OperadoraApi { get; set; }

        /// <summary>
        /// Caso seja verdadeiro, o sistema precisa tentar enviar a mensagem novamente
        /// </summary>
        public bool PrecisaReenviar { get; set; }

        /// <summary>
        /// Caso seja verdadeiro, o sistema precisa enviar por outra Operadora
        /// </summary>
        public bool PrecisaReenviarPorOutraOperadora { get; set; }

        /// <summary>
        /// Caso seja verdadeiro, o sistema precisa atualizar o status da mensagem com a Operadora
        /// </summary>
        public bool PrecisaAtualizar { get; set; }

        
        /// <summary>
        /// True se a mensagem está ativa
        /// </summary>
        public bool IsEnabled { get; set; }

         


        #endregion
    }
}
