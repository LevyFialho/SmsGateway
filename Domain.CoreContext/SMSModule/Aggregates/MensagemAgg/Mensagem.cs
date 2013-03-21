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

using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg.StatusAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;


namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Seedwork;
    using Resources;
   

    /// <summary>
    /// Aggregate root para Mensagem.
    /// </summary>
    public class Mensagem
        :Entity,IValidatableObject
    {

        #region Members

        bool _ativo;

        #endregion

        #region Properties

        /// <summary>
        /// Id do contrato da operadora
        /// </summary>
        public Guid? ContratoDaOperadoraId { get; private set; }

        /// <summary>
        /// Id do contrato do cliente
        /// </summary>
        public Guid ContratoDoClienteId { get; private set; }

        /// <summary>
        /// Contrato através do qual a mensagem foi criada
        /// </summary>
        public virtual Contrato ContratoDoCliente { get; set; }

        /// <summary>
        /// Contrato através do qual a mensagem foi enviada
        /// </summary>
        public virtual Contrato ContratoDaOperadora { get; set; }

        private string _textoDaMensagem;
        /// <summary>
        /// texto do SMS Enviado
        /// </summary>
        public string TextoDaMensagem 
        { 
            get { return _textoDaMensagem; } 
            set { _textoDaMensagem = Encryption.ToMd5(value);  } 
        }

        public string TextoDescriptografado
        {
            get { return Encryption.FromMd5(_textoDaMensagem); }
        }

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
        /// Id do status atual
        /// </summary>
        public Guid StatusId { get; private set; }

        /// <summary>
        /// Status da mensagem
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// True se a mensagem está ativa
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return _ativo;
            }
            private set
            {
                _ativo = value;
            }
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Desativa o Administrador
        /// </summary>
        public void Disable()
        {
            if ( IsEnabled)
                this._ativo = false;
        }

        /// <summary>
        /// Ativa o Administrador
        /// </summary>
        public void Enable()
        {
            if( !IsEnabled)
                this._ativo = true;
        }

        /// <summary>
        /// Associa ao contrato do cliente que solicitou a mensagem.
        /// </summary>
        /// <param name="contrato"></param>
        public void SetarContratoDoCliente(Contrato contrato)
        {
            if (contrato == null
                ||
                contrato.IsTransient() || !contrato.IsEnabled)
            {
                throw new ArgumentException(Messages.exception_SetarContratoAtual);
            }

            //fix relation
            this.ContratoDoClienteId = contrato.Id;

            this.ContratoDoCliente = contrato;
        }

        /// <summary>
        /// Setar a referência do contrato do cliente
        /// </summary>
        /// <param name="contractId"></param>
        public void SetTheCurrentClientContractReference(Guid contractId)
        {
            if (contractId != Guid.Empty)
            {
                //fix relation
                this.ContratoDoClienteId = contractId;

                this.ContratoDoCliente = null;
            }
        }

        /// <summary>
        /// Associa ao contrato do cliente que solicitou a mensagem.
        /// </summary>
        /// <param name="contrato"></param>
        public void SetarContratoDaOperadora(Contrato contrato)
        {
            if (contrato == null
                ||
                contrato.IsTransient() || !contrato.IsEnabled)
            {
                throw new ArgumentException(Messages.exception_SetarContratoAtual);
            }

            //fix relation
            this.ContratoDaOperadoraId = contrato.Id;

            this.ContratoDaOperadora = contrato;
        }

        /// <summary>
        /// Setar a referência do contrato do cliente
        /// </summary>
        /// <param name="contractId"></param>
        public void SetTheCurrentOperadoraContractReference(Guid contractId)
        {
            if (contractId != Guid.Empty)
            {
                //fix relation
                this.ContratoDaOperadoraId = contractId;

                this.ContratoDaOperadora = null;
            }
        }

        /// <summary>
        /// Modifica o status do contrato.
        /// </summary>
        /// <param name="status"></param>
        public void SetarStatus(Status status)
        {
            if (status == null
                ||
                status.IsTransient() || !status.IsEnabled)
            {
                throw new ArgumentException(Messages.exception_SetarContratoAtual);
            }

            //fix relation
            this.StatusId = status.Id;

            this.Status = status;
        }

        /// <summary>
        /// Setar a referência do status do cliente
        /// </summary>
        /// <param name="statusId"></param>
        public void SetTheCurrentStatusReference(Guid statusId)
        {
            if (statusId != Guid.Empty)
            {
                //fix relation
                this.StatusId = statusId;

                this.Status = null;
            }
        }

        /// <summary>
        /// Atuaiza o status de envio da mensagem
        /// </summary>
        /// <param name="codigoDoStatus"></param>
        /// <param name="operadora"></param>
        public void AtualizarStatus(string codigoDoStatus)
        {
            //Get lita de status, procurar pelo codigo e atualizar a referencia

            throw new NotImplementedException();

        }
 
        public bool Validar()
        {
            return true;
            throw  new NotImplementedException();
        }
        #endregion

        #region IValidatableObject Members

    
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            //-->Check first name property
            if (String.IsNullOrWhiteSpace(this.TextoDaMensagem))
            {
                validationResults.Add(new ValidationResult(Messages.validation_NomeAdministrador,
                                                           new string[] { "TextoDaMensagem" }));
            }

            //-->Check last name property
            if (String.IsNullOrWhiteSpace(this.NumeroDoDestinatario))
            {
                validationResults.Add(new ValidationResult(Messages.validation_SenhaAdministrador,
                                                           new string[] { "NumeroDoDestinatario" }));
            }
            if(this.ContratoDoCliente == null)
            {
                validationResults.Add(new ValidationResult(Messages.validation_SenhaAdministrador,
                                                           new string[] { "ContratoDoCliente" }));
            }
            return validationResults;
        }
     
        #endregion
    }
}
