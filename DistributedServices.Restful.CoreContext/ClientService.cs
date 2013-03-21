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
using System.ServiceModel.Activation;
using System.Web;
using SmsGateway.DistributedServices.Seedwork.ErrorHandlers;
using SmsGateway.DistributedServices.Restful.CoreContext.InstanceProviders;

using System.ServiceModel;
using System.ServiceModel.Web;
using SmsGateway.Application.CoreContext.DTO.SMSModule;
using SmsGateway.Application.CoreContext.SMSModule.Services.Contracts;

namespace SmsGateway.DistributedServices.Restful.CoreContext
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    [ApplicationErrorHandler()] // manage all unhandled exceptions
    [UnityInstanceProviderServiceBehavior()] //create instance and inject dependencies using unity container
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ClientService: IClientService
    {
        #region Members

        readonly IAdministradoresAppService _adminAppService;
        readonly IContratosAppServices _contratosAppService;
        readonly IMensagensAppServices _mensagensAppService;
        readonly ISmsAppServices _smsAppService;
        readonly IContatosAppService _contatosAppService;
        readonly IListaDeContatosAppService _listasDeContatosService;
        readonly ISolicitacaoDeCadastroAppService _solicitacaoDeCadastroAppService;
        private readonly ITicketsAppService _ticketsAppService;
        private readonly IEmailAppService _emailAppService;
        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of the service
        /// </summary>
        public ClientService(IAdministradoresAppService administracaoService, IContratosAppServices contratosServices,IMensagensAppServices mensagensService,
            ISmsAppServices smsAppService, IContatosAppService contatosAppService, IListaDeContatosAppService listaDeContatosAppService,
             ISolicitacaoDeCadastroAppService solicitacaoDeCadastroAppService, IEmailAppService emailAppService, ITicketsAppService ticketsAppService)
        {
            if (administracaoService == null)
                throw new ArgumentNullException("administracaoService");
            if (contratosServices == null)
                throw new ArgumentNullException("contratosServices");
            if (mensagensService == null)
                throw new ArgumentNullException("mensagensService");
            if (smsAppService == null)
                throw new ArgumentNullException("smsAppService");
            if (smsAppService == null)
                throw new ArgumentNullException("contatosAppService");
            if (listaDeContatosAppService == null)
                throw new ArgumentNullException("listaDeContatosAppService");
            if (solicitacaoDeCadastroAppService == null)
                throw new ArgumentNullException("solicitacaoDeCadastroAppService");
            if (emailAppService == null)
                throw new ArgumentNullException("emailAppService");
            if (ticketsAppService == null)
                throw new ArgumentNullException("ticketsAppService");
            _adminAppService = administracaoService;
            _contratosAppService = contratosServices;
            _mensagensAppService = mensagensService;
            _smsAppService = smsAppService;
            _contatosAppService = contatosAppService;
            _listasDeContatosService = listaDeContatosAppService;
            _solicitacaoDeCadastroAppService = solicitacaoDeCadastroAppService;
            _emailAppService = emailAppService;
            _ticketsAppService = ticketsAppService;
        }
        #endregion

        #region IClientService Members

        public ContratoDTO GetContratoAtual(string idCliente)
        {

            return _contratosAppService.GetContratoDoCliente(new Guid(idCliente));
        }

        public int GetSaldoDeMensagens(string idCliente)
        {
            var contrato = _contratosAppService.GetContratoDoCliente(new Guid(idCliente));
            if (contrato != null) return contrato.SaldoDeMensagens;
            else return 0;
        }

        public List<MensagemDTO> GetMensagensEnviadas(string idCliente)
        {
            return _mensagensAppService.GetMensagensDoCliente(new Guid(idCliente));
        }

        public MensagemDTO GetMensagem(string idMensagem)
        {
            return _mensagensAppService.GetMensagem(new Guid(idMensagem));
        }

        public MensagemDTO EnviarMensagem(AutenticacaoDTO autenticacao, MensagemDTO mensagem)
        {
            return _smsAppService.EnviarMensagem(autenticacao, mensagem);

        }

        public MensagemDTO EnviarMensagemStringRemetente(AutenticacaoDTO autenticacao, string destinatario, string remetente, string texto)
        {
            var dto = new MensagemDTO();
            dto.TextoDaMensagem = texto;
            dto.NumeroDoDestinatario = destinatario;
            dto.NumeroDoRemetente = remetente;
            return _smsAppService.EnviarMensagem(autenticacao, dto);
        }

        public MensagemDTO EnviarMensagemString(AutenticacaoDTO autenticacao, string destinatario, string texto)
        {
            var dto = new MensagemDTO();
            dto.TextoDaMensagem = texto;
            dto.NumeroDoDestinatario = destinatario;
            dto.NumeroDoRemetente = "";
            return _smsAppService.EnviarMensagem(autenticacao, dto);
        }

        #region Contatos

        public ContatoDTO AdicionarContato(ContatoDTO contato)
        {
            return _contatosAppService.Add(contato);
        }

        public void AtualizarContato(ContatoDTO contato)
        {
            _contatosAppService.Update(contato);
        }

        public void RemoverContato(Guid contatoId)
        {
            _contatosAppService.Remove(contatoId);
        
        }

        public MensagemDTO EnviarMensagemParaContatos(string texto, ICollection<ContatoDTO> contatos)
        {
            throw new NotImplementedException();
        }

        public ContatoDTO Contato(Guid contatoId)
        {
            return _contatosAppService.Find(contatoId);
        }

        public IEnumerable<ContatoDTO> ContatosDoCliente(Guid clienteId)
        {
            return _contatosAppService.ClientContracts(clienteId);
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

        public void SolicitacaoDeCadastro(SolicitacaoDeCadastroDTO dtoInstance)
        {
            _solicitacaoDeCadastroAppService.Add(dtoInstance, _emailAppService);
        }

        public TicketDTO SalvaTicket(TicketDTO ticket)
        {
            ticket.Status = StatusDoTicketDTO.Pendente;
           
            if (ticket.Id == Guid.Empty)
              return  _ticketsAppService.Add(ticket);
            else
                return _ticketsAppService.Update(ticket);
        }

        public List<TicketDTO> TicketsDoCliente(string idCliente)
        {
            return _ticketsAppService.TicketsDoCliente(new Guid(idCliente));
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