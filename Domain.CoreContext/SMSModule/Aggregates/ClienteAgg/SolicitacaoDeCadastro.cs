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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.TicketsAgg;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmsGateway.Domain.Seedwork;
    using SmsGateway.Domain.CoreContext.Resources;
 
     
    public class SolicitacaoDeCadastro
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

        public long Telefone { get; set; }

        public DateTime Data { get; set; }
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
 

            //-->Check Email
            if (String.IsNullOrWhiteSpace(this.Email) || !IsValidEmail())
            {
                validationResults.Add(new ValidationResult(Messages.validation_EmailAdministrador,
                                                           new string[] { "Email" }));
            }

            if (this.Telefone <= 0)
            {
                validationResults.Add(new ValidationResult(Messages.validation_EmailAdministrador,
                                                           new string[] { "Telefone" }));
            }

            if (this.Data == DateTime.MinValue)
            {
                validationResults.Add(new ValidationResult(Messages.validation_EmailAdministrador,
                                                           new string[] { "Data" }));
            }
            return validationResults;
        }

        #endregion
    }
}
