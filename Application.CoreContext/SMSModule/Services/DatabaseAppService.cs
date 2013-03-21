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


using SmsGateway.Application.CoreContext.SMSModule.Services.Contracts;
using SmsGateway.Domain.CoreContext.SMSModule.Services;

namespace SmsGateway.Application.CoreContext.SMSModule.Services
{
    using System;
    /// <summary>
    /// Implementação do serviço de gerência de clientes do sistema
    /// </summary>
    public class DatabaseAppService
        : IDatabaseAppService
    {
        #region Members

        private readonly IDatabases _service;
        #endregion

        #region Constructors

        
        /// <summary>
        /// Cria uma nova instância do serviço 
        /// </summary>
        /// <param name="service">Serviço associado, injeção de dependência</param>
        public DatabaseAppService(IDatabases service)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            _service = service;

             
        }

        #endregion

       public bool Start()
       {
           return _service.Start();
       }
         
        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            //dispose todos os recursos
           GC.SuppressFinalize(this);
        }

        #endregion
    }
}
