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
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SmsGateway.DistributedServices.Seedwork.ErrorHandlers;
using SmsGateway.DistributedServices.Restful.CoreContext.InstanceProviders;
using System.Web;
using SmsGateway.Application.CoreContext.DTO.SMSModule;
using SmsGateway.Application.CoreContext.SMSModule.Services.Contracts;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContatoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Services;
using SmsGateway.Infrastructure.Data.CoreContext.SMSModule.Services;

namespace SmsGateway.DistributedServices.Restful.CoreContext
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    [ApplicationErrorHandler()] // manage all unhandled exceptions
    [UnityInstanceProviderServiceBehavior()] //create instance and inject dependencies using unity container
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AdministracaoService : IAdministracaoService
    {
        #region Members

        readonly IAdministradoresAppService _adminAppService;
        readonly ISolicitacaoDeCadastroAppService _solicitacaoDecadastroAppService;
        private readonly ITicketsAppService _ticketsAppService;
        readonly IContratosAppServices _contratosAppService;
        readonly IMensagensAppServices _mensagensAppService;
        readonly IClientesAppService _clientesAppService;
        readonly IStatusAppService _statusAppService;
        private IContatosAppService _contatosAppService;
        readonly IListaDeContatosAppService _listasDeContatosService;
        readonly IPacotesAppService _pacotesService;
        readonly IDatabases _databaseService;
        readonly IEmailAppService _emailAppService;
        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of the service
        /// </summary>
        public AdministracaoService(IAdministradoresAppService administracaoService, IContratosAppServices contratosServices,
             IMensagensAppServices mensagensService, IClientesAppService clientesAppService, IStatusAppService statusAppService, IContatosAppService contatosAppService,
            IDatabases databaseService, IEmailAppService emailService, IListaDeContatosAppService listasDeContatosService,
            IPacotesAppService pacotesService, ISolicitacaoDeCadastroAppService solicitacaoDecadastroAppService, ITicketsAppService ticketsAppService)
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
            if (emailService == null)
                throw new ArgumentNullException("emailService");
            if (listasDeContatosService == null)
                throw new ArgumentNullException("listasDeContatosService");
            if (pacotesService == null)
                throw new ArgumentNullException("pacotesService");
            if (contatosAppService == null)
                throw new ArgumentNullException("contatosAppService");
            if (solicitacaoDecadastroAppService == null)
                throw new ArgumentNullException("solicitacaoDecadastroAppService");
            if (ticketsAppService == null)
                throw new ArgumentNullException("ticketsAppService");

            _ticketsAppService = ticketsAppService;
            _solicitacaoDecadastroAppService = solicitacaoDecadastroAppService;
            _adminAppService = administracaoService;
            _contratosAppService = contratosServices;
            _mensagensAppService = mensagensService;
            _clientesAppService = clientesAppService;
            _statusAppService = statusAppService;
            _databaseService = databaseService;
            _emailAppService = emailService;
            _pacotesService = pacotesService;
            _listasDeContatosService = listasDeContatosService;
            _contatosAppService = contatosAppService;
        }
        #endregion

        #region IAdministracaoService Members

        #region Administradores
        public AdministradorDTO GetAdministrador(string id)
        {
            return _adminAppService.Find(new Guid(id));
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

        public void DisableAdministrador(AdministradorDTO dtoInstance)
        {
            _adminAppService.Remove(dtoInstance.Id);
        }

        #endregion

        #region Clientes
        public ClienteDTO GetCliente(string id)
        {
            return _clientesAppService.Find(new Guid(id));
        }

        public List<ClienteDTO> ListCliente()
        {
            return _clientesAppService.ListAll();
        }

      

        public void RecuperarSenha(string id)
        {
            var cliente = _clientesAppService.Find(new Guid(id));
            if(cliente != null)
            _emailAppService.RecuperarSenha(cliente.Nome, cliente.Email, cliente.Senha);
        }

        public ClienteDTO CreateCliente(ClienteDTO dtoInstance)
        {
            return _clientesAppService.Add(dtoInstance);
        }

        public void UpdateCliente(ClienteDTO dtoInstance)
        {
            _clientesAppService.Update(dtoInstance);
        }
        public void DisableCliente(ClienteDTO dtoInstance)
        {
            _clientesAppService.Remove(dtoInstance.Id);
        }

        #endregion

        #region Contrato
        public ContratoDTO GetContrato(string id)
        {
            return _contratosAppService.GetContrato(new Guid(id));
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
            switch (dtoInstance.TipoDeContrato)
            {
                case TipoDeContratoDTO.Cliente:
                    return _contratosAppService.NovoContratoComCliente(dtoInstance);
                    break;

                case TipoDeContratoDTO.Operadora:
                    return _contratosAppService.NovoContratoComOperadora(dtoInstance);
                    break;
                default:
                    return null;
            }

        }

        public void RenovarContrato(ContratoDTO dtoInstance)
        {
            _contratosAppService.RenovarContrato(dtoInstance);
        }
        #endregion

        #region Status

        public StatusDTO GetStatus(string id)
        {
            return _statusAppService.Find(new Guid(id));
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

        #endregion

        #region Contatos

        public ContatoDTO GetContato(string id)
        {
            return _contatosAppService.Find(new Guid(id));
        }

        public List<ContatoDTO> ListContatos()
        {
            return _contatosAppService.ListAll();
        }

        public ContatoDTO CreateContato(ContatoDTO dtoInstance)
        {
            return _contatosAppService.Add(dtoInstance);
        }

        public void UpdateContato(ContatoDTO dtoInstance)
        {
            _contatosAppService.Update(dtoInstance);
        }

        public void DisableContato(ContatoDTO dtoInstance)
        {
            _contatosAppService.Remove(dtoInstance.Id);
        }

        public void AddContatoToList(string contatoId, string listaId)
        {
            _contatosAppService.AddToList(new Guid(contatoId), new Guid(listaId));
        }

        public void RemoveContatoFromList(string contatoId, string listaId)
        {
            _contatosAppService.RemoveFromList(new Guid(contatoId), new Guid(listaId));
        }

        #endregion

        #region Listas De Contato

        public ListaDeContatosDTO GetListaDeContatos(string id)
        {
            return _listasDeContatosService.Find(new Guid(id));
        }

        public List<ListaDeContatosDTO> ListListasDeContatos()
        {
            return _listasDeContatosService.ListAll();
        }

        public ListaDeContatosDTO CreateListaDeContatos(ListaDeContatosDTO dtoInstance)
        {
            return _listasDeContatosService.Add(dtoInstance);
        }

        public void UpdateListaDeContatos(ListaDeContatosDTO dtoInstance)
        {
            _listasDeContatosService.Update(dtoInstance);
        }

        public void DisableListaDeContatos(ListaDeContatosDTO dtoInstance)
        {
            _listasDeContatosService.Remove(dtoInstance.Id);
        }

        public IEnumerable<SolicitacaoDeCadastroDTO> ListaSolicitacoesDeCadastro()
        {
            return _solicitacaoDecadastroAppService.ListAll();
        }
        public void AddSolicitacaoDeCadastro(SolicitacaoDeCadastroDTO dtoInstance)
        {
            _solicitacaoDecadastroAppService.Add(dtoInstance, _emailAppService);
        }
        
        public void RemoveSolicitacaoDeCadastro(string id)
        {
            _solicitacaoDecadastroAppService.Remove(new Guid(id));
        }
        public TicketDTO SalvaTicket(TicketDTO ticket)
        {
            ticket.Status = StatusDoTicketDTO.Resolvido;
            if (ticket.Id == Guid.Empty)
                return _ticketsAppService.Add(ticket);
            else
                return _ticketsAppService.Update(ticket);
        }

        public List<TicketDTO> Tickets()
        {
            return _ticketsAppService.ListAll();
        }

        public TicketDTO Ticket(string id)
        {
            return _ticketsAppService.Find(new Guid(id));
        }

        #endregion

        #region Pacotes

        public PacoteDTO GetPacote(string id)
        {
            return _pacotesService.Find(new Guid(id));
        }

        public List<PacoteDTO> ListPacotes()
        {
            return _pacotesService.ListAll();
        }

        public PacoteDTO CreatePacote(PacoteDTO dtoInstance)
        {
            return _pacotesService.Add(dtoInstance);
        }

        public void UpdatePacote(PacoteDTO dtoInstance)
        {
            _pacotesService.Update(dtoInstance);
        }

        public void DisablePacote(PacoteDTO dtoInstance)
        {
            _pacotesService.Remove(dtoInstance.Id);
        }
        #endregion

        #region Database

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