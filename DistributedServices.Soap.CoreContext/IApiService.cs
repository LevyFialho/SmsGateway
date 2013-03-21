using System;
using System.Collections.Generic; 
using System.ServiceModel; 
using SmsGateway.Application.CoreContext.DTO.SMSModule;

namespace SmsGateway.DistributedServices.Soap.CoreContext
{
    [ServiceContract]
    public interface IApiService
    {

        [OperationContract]
       
        int GetSaldoDeMensagens(AutenticacaoDTO autenticacao);

        [OperationContract]
        
        List<MensagemDTO> GetMensagensEnviadas(AutenticacaoDTO autenticacao);

        [OperationContract]
       
        MensagemDTO GetMensagem(AutenticacaoDTO autenticacao, string idMensagem);

        [OperationContract]
       
        MensagemDTO EnviarMensagem(AutenticacaoDTO autenticacao, MensagemDTO mensagem);

        [OperationContract]
       
        MensagemDTO EnviarMensagemStringRemetente(AutenticacaoDTO autenticacao, string destinatario, string remetente, string texto);

        [OperationContract]
       
        MensagemDTO EnviarMensagemString(AutenticacaoDTO autenticacao, string destinatario, string texto);

        [OperationContract]
       
        ContatoDTO AdicionarContato(AutenticacaoDTO autenticacao, ContatoDTO contato);

        [OperationContract]
       
        void AtualizarContato(AutenticacaoDTO autenticacao, ContatoDTO contato);

        [OperationContract]
       
        void RemoverContato(AutenticacaoDTO autenticacao, Guid contatoId);

        [OperationContract]

        List<MensagemDTO> EnviarMensagemParaContatos(AutenticacaoDTO autenticacao, string texto, string remetente, IEnumerable<ContatoDTO> contatos);

        [OperationContract]
        
        ContatoDTO Contato(AutenticacaoDTO autenticacao, Guid contatoId);

        [OperationContract]
       
        IEnumerable<ContatoDTO> TodosOsContatos(AutenticacaoDTO autenticacao);

        #region ListasDeContatos

        [OperationContract]
        
        ListaDeContatosDTO GetListaDeContatos(AutenticacaoDTO autenticacao, string id);

        [OperationContract]
      
        List<ListaDeContatosDTO> ListListasDeContatos(AutenticacaoDTO autenticacao);

        [OperationContract]
      
        ListaDeContatosDTO CreateListaDeContatos(AutenticacaoDTO autenticacao, ListaDeContatosDTO dtoInstance);

        [OperationContract]
       
        void UpdateListaDeContatos(AutenticacaoDTO autenticacao, ListaDeContatosDTO dtoInstance);

        [OperationContract]
       
        void DisableListaDeContatos(AutenticacaoDTO autenticacao, ListaDeContatosDTO dtoInstance);

        [OperationContract]

        List<MensagemDTO> EnviarMensagemParaListaDeContatos(AutenticacaoDTO autenticacao, string texto, string remetente, ListaDeContatosDTO dtoInstance);
        #endregion
    }
}
