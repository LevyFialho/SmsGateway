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
			

namespace SmsGateway.Infrastructure.Crosscutting.NetFramework.Validator
{

    using System.Collections.Generic;
    using System.Linq;
    using Crosscutting.Validator;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    /// <summary>
    /// Validator baseado em System.ComponentModel.DataAnnotations. 
    /// Usa IValidatableObject interface e
    /// ValidationAttribute para validações
    /// </summary>
    public class DataAnnotationsEntityValidator
        : IEntityValidator
    {
        #region Private Methods

        /// <summary>
        /// Pega os erros se o objeto implementa IValidatableObject
        /// </summary>
        /// <typeparam name="TEntity">Tipo de Entidade</typeparam>
        /// <param name="item">Item a ser validado</param>
        /// <param name="errors">Coleção dos erros atuais</param>
        void SetValidatableObjectErrors<TEntity>(TEntity item, List<string> errors) where TEntity : class
        {
            if (typeof(IValidatableObject).IsAssignableFrom(typeof(TEntity)))
            {
                var validationContext = new ValidationContext(item, null, null);

                var validationResults = ((IValidatableObject)item).Validate(validationContext);

                errors.AddRange(validationResults.Select(vr => vr.ErrorMessage));
            }
        }

        /// <summary>
        /// Pega os erros no ValidationAttribute
        /// </summary>
        /// <typeparam name="TEntity">Tipo de Entidade</typeparam>
        /// <param name="item">Item a ser validado</param>
        /// <param name="errors">Coleção dos erros atuais</param>
        void SetValidationAttributeErrors<TEntity>(TEntity item, List<string> errors) where TEntity : class
        {
            var result = from property in TypeDescriptor.GetProperties(item).Cast<PropertyDescriptor>()
                         from attribute in property.Attributes.OfType<ValidationAttribute>()
                         where !attribute.IsValid(property.GetValue(item))
                         select attribute.FormatErrorMessage(string.Empty);

            if (result != null
                &&
                result.Any())
            {
                errors.AddRange(result);
            }
        }

        #endregion

        #region IEntityValidator Members


        /// <summary>
        /// <see cref="SmsGateway.Infrastructure.CrossCutting.Validator.IEntityValidator"/>
        /// </summary>
        /// <typeparam name="TEntity"><see cref="SmsGateway.Infrastructure.CrossCutting.Validator.IEntityValidator"/></typeparam>
        /// <param name="item"><see cref="SmsGateway.Infrastructure.CrossCutting.Validator.IEntityValidator"/></param>
        /// <returns><see cref="SmsGateway.Infrastructure.CrossCutting.Validator.IEntityValidator"/></returns>
        public bool IsValid<TEntity>(TEntity item) where TEntity : class
        {

            if (item == null)
                return false;

            List<string> validationErrors = new List<string>();

            SetValidatableObjectErrors(item, validationErrors);
            SetValidationAttributeErrors(item, validationErrors);

            return !validationErrors.Any();
        }
        /// <summary>
        /// <see cref="SmsGateway.Infrastructure.CrossCutting.Validator.IEntityValidator"/>
        /// </summary>
        /// <typeparam name="TEntity"><see cref="SmsGateway.Infrastructure.CrossCutting.Validator.IEntityValidator"/></typeparam>
        /// <param name="item"><see cref="SmsGateway.Infrastructure.CrossCutting.Validator.IEntityValidator"/></param>
        /// <returns><see cref="SmsGateway.Infrastructure.CrossCutting.Validator.IEntityValidator"/></returns>
        public IEnumerable<string> GetInvalidMessages<TEntity>(TEntity item) where TEntity : class
        {
            if (item == null)
                return null;

            List<string> validationErrors = new List<string>();

            SetValidatableObjectErrors(item, validationErrors);
            SetValidationAttributeErrors(item, validationErrors);


            return validationErrors;
        }

        #endregion
    }
}
