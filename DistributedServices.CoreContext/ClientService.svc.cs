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
using System.Collections.Generic;
using System.ServiceModel;
using SmsGateway.Application.CoreContext.DTO;
using SmsGateway.Application.CoreContext.DTO.SMSModule;
using SmsGateway.Application.CoreContext.SMSModule.Services;
using SmsGateway.DistributedServices.CoreContext.InstanceProviders;
using SmsGateway.DistributedServices.Seedwork.ErrorHandlers;
    
using SmsGateway.Application.CoreContext.SMSModule.Services.Contracts;

namespace SmsGateway.DistributedServices.CoreContext
{
    
    [ApplicationErrorHandlerAttribute()] // manage all unhandled exceptions
    [UnityInstanceProviderServiceBehavior()] //create instance and inject dependencies using unity container
    public class ClientService : IClientService
    {

       #region Members

        readonly IAdministradoresAppService  _adminAppService;
        readonly IContratosAppServices _contratosAppService;
        readonly IMensagensAppServices _mensagensAppService;
        readonly ISmsAppServices _smsAppService;
        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of the service
        /// </summary>
       public ClientService(IAdministradoresAppService administracaoService, IContratosAppServices contratosServices,
            IMensagensAppServices mensagensService, ISmsAppServices smsAppService)
        {
            if (administracaoService == null)
                throw new ArgumentNullException("administracaoService");
            if (contratosServices == null)
                throw new ArgumentNullException("contratosServices");
            if (mensagensService == null)
                throw new ArgumentNullException("mensagensService");
            if (smsAppService == null)
                throw new ArgumentNullException("smsAppService");

            _adminAppService = administracaoService;
            _contratosAppService = contratosServices;
            _mensagensAppService = mensagensService;
            _smsAppService = smsAppService;
        }
        #endregion

        #region IClientService Members
        
        public ContratoDTO GetContratoAtual(Guid idCliente)
        {

            return _contratosAppService.GetContratoDoCliente(idCliente);
        }

        public int GetSaldoDeMensagens(Guid idCliente)
        {
          var contrato =  _contratosAppService.GetContratoDoCliente(idCliente);
          if (contrato != null) return contrato.SaldoDeMensagens;
          else return 0;
        }

        public List<MensagemDTO> GetMensagensEnviadas(Guid idCliente)
        {
            return _mensagensAppService.GetMensagensDoCliente(idCliente);
        }

        public MensagemDTO GetMensagem(Guid idMensagem)
        {
            return _mensagensAppService.GetMensagem(idMensagem);
        }

        public MensagemDTO EnviarMensagem(Guid idCliente, MensagemDTO mensagem)
        {
          return _smsAppService.EnviarMensagem(idCliente, mensagem);
          
        }

        public MensagemDTO EnviarMensagem(string idCliente, string destinatario, string remetente, string texto)
        {
            var dto = new MensagemDTO();
            dto.TextoDaMensagem = texto;
            dto.NumeroDoDestinatario = destinatario;
            dto.NumeroDoRemetente = remetente;
            return _smsAppService.EnviarMensagem(new Guid(idCliente), dto);
        }

        public MensagemDTO EnviarMensagem(string idCliente, string destinatario, string texto)
        {
            var dto = new MensagemDTO();
            dto.TextoDaMensagem = texto;
            dto.NumeroDoDestinatario = destinatario;
            dto.NumeroDoRemetente = "";
            return _smsAppService.EnviarMensagem(new Guid(idCliente), dto);
        }
          


        #endregion

       #region IDisposable Members

       /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            //dispose all resources
            _adminAppService.Dispose();
            _contratosAppService.Dispose();
            _mensagensAppService.Dispose();
        }
        #endregion
    }
}
