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

using System.Net.Mail;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmsGateway.Domain.Seedwork;
    using SmsGateway.Domain.CoreContext.Resources;
 
    /// <summary>
    /// Raiz do Aggregate Clientes.
    /// </summary>
    public class Cliente
        :Entity,IValidatableObject
    {

        #region Members

        bool _ativo;

        #endregion

        #region Properties

        
        /// <summary>
        /// Nome do cliente
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Senha do cliente
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Email do cliente
        /// </summary>
        public string Email { get; set; }

        public bool IsValidEmail()
        {
            try
            {
                var m = new MailAddress(Email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// Id do contrato atual do cliente
        /// </summary>
        public virtual Guid? ContratoAtualId { get; set; }

        /// <summary>
        /// Contrato atual do cliente
        /// </summary>
        public virtual Contrato ContratoAtual { get; set; }

        public virtual ICollection<Contrato> Contratos { get; set; }


        public virtual ICollection<TicketsAgg.Ticket> Tickets { get; set; } 
      
        /// <summary>
        /// True se o cliente está ativo
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
        /// Desativa o cliente
        /// </summary>
        public void Disable()
        {
            if ( IsEnabled)
                this._ativo = false;
        }

        /// <summary>
        /// Ativa o cliente
        /// </summary>
        public void Enable()
        {
            if( !IsEnabled)
                this._ativo = true;
        }

        /// <summary>
        /// Associa um contrato ativo ao cliente.
        /// </summary>
        /// <param name="contrato"></param>
        public void SetarContratoAtual(Contrato contrato)
        {
            if (contrato == null
                ||
                contrato.IsTransient() || !contrato.IsEnabled)
            {
                throw new ArgumentException(Messages.exception_SetarContratoAtual);
            }

            //fix relation
            this.ContratoAtualId = contrato.Id;

            this.ContratoAtual = contrato;
        }

        /// <summary>
        /// Setar a referência do contrato atual deste cliente
        /// </summary>
        /// <param name="contractId"></param>
        public void SetTheCurrentContractReference(Guid contractId)
        {
            if (contractId != Guid.Empty)
            {
                //fix relation
                this.ContratoAtualId = contractId;

                this.ContratoAtual = null;
            }
        }
 
        #endregion

        #region IValidatableObject Members

        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            //-->Check Nome
            if (String.IsNullOrWhiteSpace(this.Nome))
            {
                validationResults.Add(new ValidationResult(Messages.validation_NomeCliente, 
                                                           new string[] { "Nome" }));
            }

            //-->Check Senha
            if (String.IsNullOrWhiteSpace(this.Senha))
            {
                validationResults.Add(new ValidationResult(Messages.validation_SenhaCliente,
                                                           new string[] { "Senha" }));
            }

            //-->Check Email
            if (String.IsNullOrWhiteSpace(this.Email) || !IsValidEmail())
            {
                validationResults.Add(new ValidationResult(Messages.validation_EmailAdministrador,
                                                           new string[] { "Email" }));
            }
            return validationResults;
        }

        #endregion
    }
}
