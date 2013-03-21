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

using System.Text.RegularExpressions;


namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.AdministradorAgg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Seedwork;
    using Resources;
   

    /// <summary>
    /// Raiz do Aggregate Administradores.
    /// </summary>
    public class Administrador
        :Entity,IValidatableObject
    {

        #region Members

        bool _ativo;

        #endregion

        #region Properties

        
        /// <summary>
        /// Nome do Administrador
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Senha do Administrador
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Email do Administrador
        /// </summary>
        public string Email { get; set; }

       
        /// <summary>
        /// True se o Administrador está ativo
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

       
 
        #endregion

        #region IValidatableObject Members

    
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            //-->Verifica o nome
            if (String.IsNullOrWhiteSpace(this.Nome))
            {
                validationResults.Add(new ValidationResult(Messages.validation_NomeAdministrador, 
                                                           new string[] { "Nome" }));
            }

            //-->Verifica a Senha
            if (String.IsNullOrWhiteSpace(this.Senha))
            {
                validationResults.Add(new ValidationResult(Messages.validation_SenhaAdministrador,
                                                           new string[] { "Senha" }));
            }

            //-->Verifica o email
            if (!ValidaFormatoDeEmail(this.Email))
            {
                validationResults.Add(new ValidationResult(Messages.validation_EmailAdministrador,
                                                           new string[] { "Senha" }));
            }
            return validationResults;
        }

        /// <summary>
        /// Verifica se uma string atende o formato padrão de emails
        /// </summary>
        /// <param name="strIn">string a ser verificada</param>
        /// <returns>True se o formato é válido</returns>
        public bool ValidaFormatoDeEmail(string strIn)
        {
            
            if (String.IsNullOrEmpty(strIn))
                return false;
            
            // Return true if strIn is in valid e-mail format. 
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        #endregion
    }
}
