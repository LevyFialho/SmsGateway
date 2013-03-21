using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 
using SmsGateway.Domain.CoreContext.Resources; 
using SmsGateway.Domain.Seedwork;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.PacoteAgg
{
    public class Pacote : Entity, IValidatableObject
    {

        #region Members

        bool _ativo;

        #endregion

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


        public void Disable()
        {
            if (IsEnabled)
                this._ativo = false;
        }

   
        public void Enable()
        {
            if (!IsEnabled)
                this._ativo = true;
        }


        public string Nome { get; set; }

        public int QuantidadeDeMensagens { get; set; }

        public DateTime DataDeVencimento { get; set; }

        public double ValorCobradoPorMensagem { get; set; }

        public bool GratuitoAoNovoCliente { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
 
            if (String.IsNullOrWhiteSpace(this.Nome))
            {
                validationResults.Add(new ValidationResult(Messages.validation_NomeContato,
                                                           new string[] { "Nome" }));
            }

            if (QuantidadeDeMensagens <= 0)
            {
                validationResults.Add(new ValidationResult(Messages.validation_NumeroContato,
                                                           new string[] { "QuantidadeDeMensagens" }));
            }

            if (DataDeVencimento == DateTime.MinValue)
            {
                validationResults.Add(new ValidationResult(Messages.validation_ClienteContato,
                                                           new string[] { "DataDeVencimento" }));
            }

            if (ValorCobradoPorMensagem <= 0)
            {
                validationResults.Add(new ValidationResult(Messages.validation_NumeroContato,
                                                           new string[] { "ValorCobradoPorMensagem" }));
            }
            return validationResults;
        }
    }

   

    
}
