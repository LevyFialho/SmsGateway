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

    public interface IClientService : System.IDisposable
    {
        [OperationContract]
        [WebGet(UriTemplate = "/contrato/{idCliente}", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        ContratoDTO GetContratoAtual(string idCliente);

        [OperationContract]
        [WebGet(UriTemplate = "/saldo/{idCliente}", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        int GetSaldoDeMensagens(string idCliente);

        [OperationContract]
        [WebGet(UriTemplate = "/mensagens/{idCliente}", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        List<MensagemDTO> GetMensagensEnviadas(string idCliente);

        [OperationContract]
        [WebGet(UriTemplate = "/mensagem/{idMensagem}", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        MensagemDTO GetMensagem(string idMensagem);

        [OperationContract]
        [WebInvoke(UriTemplate = "/enviarmensagemcomdto/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        MensagemDTO EnviarMensagem(AutenticacaoDTO autenticacao, MensagemDTO mensagem);

        [OperationContract]
        [WebInvoke(UriTemplate = "/enviarmensagemcomremetente/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        MensagemDTO EnviarMensagemStringRemetente(AutenticacaoDTO autenticacao, string destinatario, string remetente, string texto);

        [OperationContract]
        [WebInvoke(UriTemplate = "/enviarmensagemsimples/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        MensagemDTO EnviarMensagemString(AutenticacaoDTO autenticacao, string destinatario, string texto);

        [OperationContract]
        [WebInvoke(UriTemplate = "/adicionarcontato/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ContatoDTO AdicionarContato(ContatoDTO contato);

        [OperationContract]
        [WebInvoke(UriTemplate = "/atualizarcontato/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void AtualizarContato(ContatoDTO contato);

        [OperationContract]
        [WebInvoke(UriTemplate = "/removercontato/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void RemoverContato(Guid contatoId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/enviamensagemparacontatos/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        MensagemDTO EnviarMensagemParaContatos(string texto, ICollection<ContatoDTO> contatos);

        [OperationContract]
        [WebInvoke(UriTemplate = "/contato/", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ContatoDTO Contato(Guid contatoId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/contatos/", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<ContatoDTO> ContatosDoCliente(Guid clienteId);

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
        [WebInvoke(UriTemplate = "/solicitacaodecadastro/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        void SolicitacaoDeCadastro(SolicitacaoDeCadastroDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/salvaticket/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        TicketDTO SalvaTicket(TicketDTO ticket);

        [OperationContract]
        [WebInvoke(UriTemplate = "/TicketsDoCliente/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        List<TicketDTO> TicketsDoCliente(string idCliente); 
        #endregion

    }
}
