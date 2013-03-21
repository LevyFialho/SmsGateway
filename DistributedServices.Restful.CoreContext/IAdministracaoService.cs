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
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using SmsGateway.Application.CoreContext.DTO.SMSModule;

namespace SmsGateway.DistributedServices.Restful.CoreContext
{
    [ServiceContract]
    public interface IAdministracaoService : System.IDisposable
    {
        #region Administradores
        [OperationContract]
        [WebGet(UriTemplate = "/admin/{id}", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        AdministradorDTO GetAdministrador(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/administradores/", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Xml)]
        List<AdministradorDTO> ListAdministrador();

        [OperationContract]
        [WebInvoke(UriTemplate = "/createadmin/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        AdministradorDTO CreateAdministrador(AdministradorDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/updateadmin/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        void UpdateAdministrador(AdministradorDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/disableadmin/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        void DisableAdministrador(AdministradorDTO dtoInstance);
 
        #endregion

        #region Clientes
        [OperationContract]
        [WebGet(UriTemplate = "/cliente/{id}", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        ClienteDTO GetCliente(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/clientes/", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        List<ClienteDTO> ListCliente();
         
        [OperationContract]
        [WebInvoke(UriTemplate = "/RecuperarSenha/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        void RecuperarSenha(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/createcliente/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        ClienteDTO CreateCliente(ClienteDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/updatecliente/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        void UpdateCliente(ClienteDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/disablecliente/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        void DisableCliente(ClienteDTO dtoInstance);
 
        #endregion

        #region Contrato
        [OperationContract]
        [WebGet(UriTemplate = "/contrato/{id}", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        ContratoDTO GetContrato(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/contratosclientes/", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        List<ContratoDTO> ListContratosDeClientes();

        [OperationContract]
        [WebGet(UriTemplate = "/contratooperadoras/", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        List<ContratoDTO> ListContratosDeOperadoras();

        [OperationContract]
        [WebInvoke(UriTemplate = "/createcontrato/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        ContratoDTO CreateContrato(ContratoDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/renovarcontrato/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        void RenovarContrato(ContratoDTO dtoInstance);
        #endregion

        #region Status
        [OperationContract]
        [WebGet(UriTemplate = "/status/{id}", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        StatusDTO GetStatus(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/liststatus/", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        List<StatusDTO> ListStatus();

        [OperationContract]
        [WebInvoke(UriTemplate = "/createstatus/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        StatusDTO CreateStatus(StatusDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/updatestatus/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        void UpdateStatus(StatusDTO dtoInstance);
        #endregion

        #region Contatos
        [OperationContract]
        [WebGet(UriTemplate = "/contatos/{id}", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        ContatoDTO GetContato(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/contatos/", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        List<ContatoDTO> ListContatos();

        [OperationContract]
        [WebInvoke(UriTemplate = "/createcontato/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        ContatoDTO CreateContato(ContatoDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/updatecontato/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        void UpdateContato(ContatoDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/disablecontato/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        void DisableContato(ContatoDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/addcontatotolist/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void AddContatoToList(string contatoId, string listaId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/removecontatofromlist/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void RemoveContatoFromList(string contatoId, string listaId);
        #endregion

        #region Pacotes
        [OperationContract]
        [WebGet(UriTemplate = "/pacotes/{id}", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        PacoteDTO GetPacote(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/pacotes/", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        List<PacoteDTO> ListPacotes();

        [OperationContract]
        [WebInvoke(UriTemplate = "/createpacote/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        PacoteDTO CreatePacote(PacoteDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/updatepacote/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        void UpdatePacote(PacoteDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/disablepacote/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        void DisablePacote(PacoteDTO dtoInstance);
        #endregion

        #region ListasDeContatos
        [OperationContract]
        [WebGet(UriTemplate = "/listaDeContatoss/{id}", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        ListaDeContatosDTO GetListaDeContatos(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/listaDeContatoss/", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        List<ListaDeContatosDTO> ListListasDeContatos();

        [OperationContract]
        [WebInvoke(UriTemplate = "/createlistaDeContatoss/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        ListaDeContatosDTO CreateListaDeContatos(ListaDeContatosDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/updatelistaDeContatoss/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        void UpdateListaDeContatos(ListaDeContatosDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/disablelistaDeContatoss/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        void DisableListaDeContatos(ListaDeContatosDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/listasolicitacaodecadastro/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<SolicitacaoDeCadastroDTO> ListaSolicitacoesDeCadastro();

        [OperationContract]
        [WebInvoke(UriTemplate = "/updatesolicitacaodecadastro/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        void AddSolicitacaoDeCadastro(SolicitacaoDeCadastroDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/removesolicitacaodecadastro/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        void RemoveSolicitacaoDeCadastro(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/salvaticket/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        TicketDTO SalvaTicket(TicketDTO ticket);

        [OperationContract]
        [WebInvoke(UriTemplate = "/Tickets/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        List<TicketDTO> Tickets();
        [OperationContract]
        [WebInvoke(UriTemplate = "/Ticket/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        TicketDTO Ticket(string id); 
       
        #endregion

        [OperationContract]
        [WebGet(UriTemplate = "/startdatabase/", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        bool StartAppDatabase();
    }
}
