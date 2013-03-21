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
			

namespace SmsGateway.DistributedServices.Seedwork.ErrorHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ServiceModel.Dispatcher;
    using SmsGateway.Infrastructure.Crosscutting.Logging;
    using SmsGateway.DistributedServices.Seedwork.Resources;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    /// <summary>
    /// Error handler default para Facade WCF Service  
    /// </summary>
    public sealed class ApplicationErrorHandler
        : IErrorHandler
    {
        /// <summary>
        /// Retorna um valor indicando se a sessão foi abortada assim como a instãncia do contexto
        /// </summary>
        /// <param name="error">Exception lançada durante o processamento</param>
        /// <returns>
        /// true se não deveria abortar a sessão , senão retorna falso(default).
        /// </returns>
        public bool HandleError(Exception error)
        {
            if (error != null)
                LoggerFactory.CreateLog().LogError(Messages.error_unmanagederror, error);

            //set  error as handled 
            return true;
        }

        /// <summary>
        /// Permite a criação de uma System.ServiceModel.FaultException{TDetail} customizada
        /// </summary>
        /// <param name="error">A System.Exception lançada durante a operação do serviço.</param>
        /// <param name="version">A versão SOAP da mensagem.</param>
        /// <param name="fault">The System.ServiceModel.Channels.Message object that is returned to the client, or service in duplex case</param>
        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            if (error is FaultException<ApplicationServiceError>)
            {
                MessageFault messageFault = ((FaultException<ApplicationServiceError>)error).CreateMessageFault();

                //propagar FaultException
                fault = Message.CreateMessage(version, messageFault, ((FaultException<ApplicationServiceError>)error).Action);
            }
            else
            {
                //criar service error
                ApplicationServiceError defaultError = new ApplicationServiceError()
                {
                    ErrorMessage = Resources.Messages.message_DefaultErrorMessage
                };

                //Criar fault exception e message fault
                FaultException<ApplicationServiceError> defaultFaultException = new FaultException<ApplicationServiceError>(defaultError);
                MessageFault defaultMessageFault = defaultFaultException.CreateMessageFault();

                //propagar FaultException
                fault = Message.CreateMessage(version, defaultMessageFault, defaultFaultException.Action);
            }
        }
    }
}
