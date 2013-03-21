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
			

namespace SmsGateway.Application.Seedwork
{
    using System;
    using System.Collections.Generic;
    using Resources;

    /// <summary>
    /// Exception customizada para erros de validação
    /// </summary>
    public class ApplicationValidationErrorsException
        :Exception
    {
        #region Properties

        IEnumerable<string> _validationErrors;
        /// <summary>
        /// get & Set para mensagens de erro da validação
        /// </summary>
        public IEnumerable<string> ValidationErrors
        {
            get
            {
                return _validationErrors;
            }
        }

        #endregion 

        #region Constructor

        /// <summary>
        /// Cria uma nova instância da exception de erros da validação
        /// </summary>
        /// <param name="validationErrors">Coleção de erros da validação</param>
        public ApplicationValidationErrorsException(IEnumerable<string> validationErrors)
            : base(Messages.exception_ApplicationValidationExceptionDefaultMessage) 
        {
            _validationErrors = validationErrors;
        }

        #endregion
    }
}
