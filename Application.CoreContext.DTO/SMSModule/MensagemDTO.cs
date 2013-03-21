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
    public class MensagemDTO
    {
        #region Properties

        /// <summary>
        /// Id da entidade
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id do contrato do cliente
        /// </summary>
        public Guid ContratoDaOperadoraId { get; set; }

        /// <summary>
        /// Id do contrato da operadora
        /// </summary>
        public Guid ContratoDoClienteId { get; set; }

        /// <summary>
        /// texto do SMS Enviado
        /// </summary>
        public string TextoDaMensagem { get; set; }

        /// <summary>
        /// Número de telefone do destinatário
        /// </summary>
        public string NumeroDoDestinatario { get; set; }

        /// <summary>
        /// Número de telefone ou nome do remetente
        /// </summary>
        public string NumeroDoRemetente { get; set; }

        /// <summary>
        /// Data em que a mensagem foi persistida no sistema
        /// </summary>
        public DateTime DataDeRegistro { get; set; }

        /// <summary>
        /// Data da ultima atualização
        /// </summary>
        public DateTime DataDoUltimoUpdate { get; set; }

        /// <summary>
        /// Data em que a mensagem foi enviada 
        /// </summary>
        public DateTime? DataDeEnvio { get; set; }

        /// <summary>
        /// Id do Status da mensagem
        /// </summary>
        public Guid StatusId { get; set; }

        /// <summary>
        /// Código do Status da mensagem
        /// </summary>
        public string StatusCodigo { get; set; }

        /// <summary>
        /// Descrição do Status da mensagem
        /// </summary>
        public string StatusMensagemAoCliente { get; set; }

        /// <summary>
        /// Valor de saldo cobrado do cliente
        /// </summary>
        public int StatusQuantoDebitarDoContratoDoCliente { get; set; }
        
        /// <summary>
        /// True se a mensagem está ativa
        /// </summary>
        public bool IsEnabled { get; set; }


        #endregion
    }
}
