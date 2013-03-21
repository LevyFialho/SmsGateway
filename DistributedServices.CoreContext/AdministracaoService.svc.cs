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
using SmsGateway.Application.CoreContext.DTO.SMSModule;
using SmsGateway.Application.CoreContext.SMSModule.Services;
using SmsGateway.Application.CoreContext.SMSModule.Services.Contracts;
using SmsGateway.DistributedServices.CoreContext.InstanceProviders;
using SmsGateway.DistributedServices.Seedwork.ErrorHandlers;
using SmsGateway.Application.CoreContext.DTO;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;
using SmsGateway.Infrastructure.Data.CoreContext.SMSModule.Services;

namespace SmsGateway.DistributedServices.CoreContext
{



    [ApplicationErrorHandlerAttribute()] // manage all unhandled exceptions
    [UnityInstanceProviderServiceBehavior()] //create instance and inject dependencies using unity container
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class AdministracaoService : IAdministracaoService
    {
        #region Members

        readonly IAdministradoresAppService _adminAppService;
        readonly IContratosAppServices _contratosAppService;
        readonly IMensagensAppServices _mensagensAppService;
        readonly IClientesAppService _clientesAppService;
        readonly IStatusAppService _statusAppService;
        readonly IDatabases _databaseService;
        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of the service
        /// </summary>
        public AdministracaoService(IAdministradoresAppService administracaoService, IContratosAppServices contratosServices,
             IMensagensAppServices mensagensService, IClientesAppService clientesAppService, 
            IStatusAppService statusAppService, IDatabases databaseService)
        {
            if (administracaoService == null)
                throw new ArgumentNullException("administracaoService");
            if (contratosServices == null)
                throw new ArgumentNullException("contratosServices");
            if (mensagensService == null)
                throw new ArgumentNullException("mensagensService");
            if (clientesAppService == null)
                throw new ArgumentNullException("clientesAppService");
            if (statusAppService == null)
                throw new ArgumentNullException("statusAppService");
            if (databaseService == null)
                throw new ArgumentNullException("databaseService");
            _adminAppService = administracaoService;
            _contratosAppService = contratosServices;
            _mensagensAppService = mensagensService;
            _clientesAppService = clientesAppService;
            _statusAppService = statusAppService;
            _databaseService = databaseService;
        }
        #endregion

        #region IAdministracaoService Members

        #region Administradores
        public AdministradorDTO GetAdministrador(Guid id)
        {
            return _adminAppService.Find(id);
        }


        public List<AdministradorDTO> ListAdministrador()
        {
            return _adminAppService.ListAll();
        }

        public AdministradorDTO CreateAdministrador(AdministradorDTO dtoInstance)
        {
            return _adminAppService.Add(dtoInstance);
        }

        public void UpdateAdministrador(AdministradorDTO dtoInstance)
        {
            _adminAppService.Update(dtoInstance);
        }


        #endregion

        #region Clientes
        public ClienteDTO GetCliente(Guid id)
        {
            return _clientesAppService.Find(id);
        }

        public List<ClienteDTO> ListCliente()
        {
            return _clientesAppService.ListAll();
        }

        public ClienteDTO CreateCliente(ClienteDTO dtoInstance)
        {
            return _clientesAppService.Add(dtoInstance);
        }

        public void UpdateCliente(ClienteDTO dtoInstance)
        {
            _clientesAppService.Update(dtoInstance);
        }
        #endregion



        #region Contrato
        public ContratoDTO GetContrato(Guid id)
        {
            return _contratosAppService.GetContrato(id);
        }

        public List<ContratoDTO> ListContratosDeClientes()
        {
            return _contratosAppService.GetContratosAtivosDeClientes();
        }
        public List<ContratoDTO> ListContratosDeOperadoras()
        {
            return _contratosAppService.GetContratosAtivosDeOperadoras();
        }
        public ContratoDTO CreateContrato(ContratoDTO dtoInstance)
        {
            switch(dtoInstance.TipoDeContrato)
            {
                case "Cliente":
                    return _contratosAppService.NovoContratoComCliente(dtoInstance);
                    break;

                case "Operadora":
                    return _contratosAppService.NovoContratoComOperadora(dtoInstance);
                    break;
                default:
                    return null;
            }
           
        }

        public void UpdateContrato(ContratoDTO dtoInstance)
        {
             _contratosAppService.AtualizarContrato(dtoInstance);
        }
        #endregion

        #region Status
        public StatusDTO GetStatus(Guid id)
        {
            return _statusAppService.Find(id);
        }
        public List<StatusDTO> ListStatus()
        {
            return _statusAppService.ListAll();
        }

        public StatusDTO CreateStatus(StatusDTO dtoInstance)
        {
            return _statusAppService.Add(dtoInstance);
        }

        public void UpdateStatus(StatusDTO dtoInstance)
        {
            _statusAppService.Update(dtoInstance);
        }
        public bool StartAppDatabase()
        {
            return _databaseService.Start();
        }

        #endregion


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
