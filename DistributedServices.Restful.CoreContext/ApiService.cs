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
    //[ApplicationErrorHandler()] // manage all unhandled exceptions
    [UnityInstanceProviderServiceBehavior()] //create instance and inject dependencies using unity container
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
    ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true)]
    public class ApiService : IApiService
    {
        #region Members

        readonly IAdministradoresAppService _adminAppService;
        readonly IClientesAppService _clientesAppService;

        readonly IContratosAppServices _contratosAppService;
        readonly IMensagensAppServices _mensagensAppService;
        readonly ISmsAppServices _smsAppService;
        readonly IContatosAppService _contatosAppService;
        readonly IListaDeContatosAppService _listasDeContatosService;
        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of the service
        /// </summary>
        public ApiService(IAdministradoresAppService administracaoService, IContratosAppServices contratosServices, IMensagensAppServices mensagensService,
            ISmsAppServices smsAppService, IContatosAppService contatosAppService, IListaDeContatosAppService listaDeContatosAppService, IClientesAppService clientesAppService)
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
            if (clientesAppService == null)
                throw new ArgumentNullException("clientesAppService");

            _adminAppService = administracaoService;
            _contratosAppService = contratosServices;
            _mensagensAppService = mensagensService;
            _smsAppService = smsAppService;
            _contatosAppService = contatosAppService;
            _listasDeContatosService = listaDeContatosAppService;
            _clientesAppService = clientesAppService;
        }
        #endregion

        #region IClientService Members


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



        public IEnumerable<ContatoDTO> ContatosDoCliente(Guid clienteId)
        {
            throw new NotImplementedException();
        }


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

        #endregion
        ///TODO: IMPLEMENTAR SERVIÇOS RESTANTES + LISTA DE CONTATO + CONFIDENCIALIDADE (SENHA DO CONTATO, SO EXIBE NO SITE)


        #endregion

        #region IApiServiceMembers

        public int GetSaldoDeMensagens(AutenticacaoDTO autenticacao)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            var contrato = _contratosAppService.GetContratoDoCliente(new Guid(autenticacao.Id));
            if (contrato != null) return contrato.SaldoDeMensagens;
            else return 0;
        }

        public List<MensagemDTO> GetMensagensEnviadas(AutenticacaoDTO autenticacao)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            return _mensagensAppService.GetMensagensDoCliente(new Guid(autenticacao.Id));
        }

        public MensagemDTO GetMensagem(AutenticacaoDTO autenticacao, string idMensagem)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            return _mensagensAppService.GetMensagem(new Guid(idMensagem));
        }

        public MensagemDTO EnviarMensagem(AutenticacaoDTO autenticacao, MensagemDTO mensagem)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            return _smsAppService.EnviarMensagem(autenticacao, mensagem);
        }

        public MensagemDTO EnviarMensagemStringRemetente(AutenticacaoDTO autenticacao, string destinatario, string remetente, string texto)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            var dto = new MensagemDTO { TextoDaMensagem = texto, NumeroDoDestinatario = destinatario, NumeroDoRemetente = remetente };
            return _smsAppService.EnviarMensagem(autenticacao, dto);
        }

        public MensagemDTO EnviarMensagemString(AutenticacaoDTO autenticacao, string destinatario, string texto)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            var dto = new MensagemDTO { TextoDaMensagem = texto, NumeroDoDestinatario = destinatario, NumeroDoRemetente = "" };
            return _smsAppService.EnviarMensagem(autenticacao, dto);
        }

        public ContatoDTO AdicionarContato(AutenticacaoDTO autenticacao, ContatoDTO contato)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            return _contatosAppService.Add(contato);
        }

        public void AtualizarContato(AutenticacaoDTO autenticacao, ContatoDTO contato)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            _contatosAppService.Update(contato);
        }

        public void RemoverContato(AutenticacaoDTO autenticacao, Guid contatoId)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            _contatosAppService.Remove(contatoId);
        }

        public List<MensagemDTO> EnviarMensagemParaContatos(AutenticacaoDTO autenticacao, string texto, string remetente, IEnumerable<ContatoDTO> contatos)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            return _smsAppService.EnviarMensagemParaContatos(autenticacao, texto, remetente, contatos);
        }

        public ContatoDTO Contato(AutenticacaoDTO autenticacao, Guid contatoId)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            return _contatosAppService.Find(contatoId);
        }

        public IEnumerable<ContatoDTO> TodosOsContatos(AutenticacaoDTO autenticacao)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            return _contatosAppService.ClientContracts(new Guid(autenticacao.Id));
        }

        public ListaDeContatosDTO GetListaDeContatos(AutenticacaoDTO autenticacao, string listaId)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            return _listasDeContatosService.Find(new Guid(listaId));
        }

        public List<ListaDeContatosDTO> ListListasDeContatos(AutenticacaoDTO autenticacao)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            return _listasDeContatosService.ListAllClientLists(new Guid(autenticacao.Id));
        }

        public ListaDeContatosDTO CreateListaDeContatos(AutenticacaoDTO autenticacao, ListaDeContatosDTO dtoInstance)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            return _listasDeContatosService.Add(dtoInstance);
        }

        public void UpdateListaDeContatos(AutenticacaoDTO autenticacao, ListaDeContatosDTO dtoInstance)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            _listasDeContatosService.Update(dtoInstance);
        }

        public void DisableListaDeContatos(AutenticacaoDTO autenticacao, ListaDeContatosDTO dtoInstance)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            _listasDeContatosService.Remove(dtoInstance.Id);
        }

        public List<MensagemDTO> EnviarMensagemParaListaDeContatos(AutenticacaoDTO autenticacao, string texto, string remetente, ListaDeContatosDTO dtoInstance)
        {
            if (!Autenticar(autenticacao))
                throw new Exception("Autenticação Inválida");
            return _smsAppService.EnviarMensagemParaListaDeContatos(autenticacao, texto,
                                                              remetente, dtoInstance);
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

        #region Private Members

        private bool Autenticar(AutenticacaoDTO autenticacao)
        {
            var cliente = _clientesAppService.Find(new Guid(autenticacao.Id));
            return cliente != null && cliente.IsEnabled && cliente.Senha == autenticacao.Senha;
        }

        #endregion

    }
}