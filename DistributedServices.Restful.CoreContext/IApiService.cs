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

    public interface IApiService : System.IDisposable
    {
         

        [OperationContract]
        [WebInvoke(UriTemplate = "/saldo/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int GetSaldoDeMensagens(AutenticacaoDTO autenticacao);

        [OperationContract]
        [WebInvoke(UriTemplate = "/mensagens/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        List<MensagemDTO> GetMensagensEnviadas(AutenticacaoDTO autenticacao);

        [OperationContract]
        [WebInvoke(UriTemplate = "/mensagem/", Method = "PUT",  RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MensagemDTO GetMensagem(AutenticacaoDTO autenticacao, string idMensagem);

        [OperationContract]
        [WebInvoke(UriTemplate = "/enviarmensagem/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MensagemDTO EnviarMensagem(AutenticacaoDTO autenticacao, MensagemDTO mensagem);

        [OperationContract]
        [WebInvoke(UriTemplate = "/enviarmensagemcomremetente/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MensagemDTO EnviarMensagemStringRemetente(AutenticacaoDTO autenticacao, string destinatario, string remetente, string texto);

        [OperationContract]
        [WebInvoke(UriTemplate = "/enviarmensagemsimples/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MensagemDTO EnviarMensagemString(AutenticacaoDTO autenticacao, string destinatario, string texto);

        [OperationContract]
        [WebInvoke(UriTemplate = "/adicionarcontato/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ContatoDTO AdicionarContato(AutenticacaoDTO autenticacao, ContatoDTO contato);

        [OperationContract]
        [WebInvoke(UriTemplate = "/atualizarcontato/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void AtualizarContato(AutenticacaoDTO autenticacao, ContatoDTO contato);

        [OperationContract]
        [WebInvoke(UriTemplate = "/removercontato/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void RemoverContato(AutenticacaoDTO autenticacao, Guid contatoId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/enviamensagemparacontatos/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<MensagemDTO> EnviarMensagemParaContatos(AutenticacaoDTO autenticacao, string texto,string remetente, IEnumerable<ContatoDTO> contatos);

        [OperationContract]
        [WebInvoke(UriTemplate = "/contato/", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ContatoDTO Contato(AutenticacaoDTO autenticacao, Guid contatoId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/contatos/", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        IEnumerable<ContatoDTO> TodosOsContatos(AutenticacaoDTO autenticacao);

        [OperationContract]
        [WebInvoke(UriTemplate = "/dadosdocliente/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DadosDoClienteDTO DadosDoCliente(AutenticacaoDTO autenticacao);


        #region ListasDeContatos

        [OperationContract]
        [WebInvoke(UriTemplate = "/listaDeContatos/", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ListaDeContatosDTO GetListaDeContatos(AutenticacaoDTO autenticacao, string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/listarListasDeContatos/", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<ListaDeContatosDTO> ListListasDeContatos(AutenticacaoDTO autenticacao);

        [OperationContract]
        [WebInvoke(UriTemplate = "/createlistaDeContatos/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ListaDeContatosDTO CreateListaDeContatos(AutenticacaoDTO autenticacao, ListaDeContatosDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/updatelistaDeContatos/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void UpdateListaDeContatos(AutenticacaoDTO autenticacao, ListaDeContatosDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/disablelistaDeContatos/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void DisableListaDeContatos(AutenticacaoDTO autenticacao, ListaDeContatosDTO dtoInstance);

        [OperationContract]
        [WebInvoke(UriTemplate = "/enviarmensagemparalista/", Method = "PUT", RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<MensagemDTO> EnviarMensagemParaListaDeContatos(AutenticacaoDTO autenticacao, string texto, string remetente,ListaDeContatosDTO dtoInstance);
        #endregion

    }
}
