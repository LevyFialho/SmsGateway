
using System;
using System.Collections.Generic;
using System.Linq;
using SmsAgileSoapApi.SmsAgileSoapService;

namespace SmsAgileSoapApi
{
    public sealed class Service
    {

        private readonly AutenticacaoDTO _autenticacao = new AutenticacaoDTO();
        private ApiServiceClient Api
        {
            get 
            { 
                if(string.IsNullOrWhiteSpace(_proxyAddress) && _useDefaultWebProxy== false)
                return Factory.Service();
                
                return Factory.ProxyService(_useDefaultWebProxy, _proxyAddress);
            }
        }
        private Guid IdCliente
        {
            get
            {
                return new Guid(_autenticacao.Id);
            }
        }
        private readonly string _proxyAddress = string.Empty;
        private readonly bool _useDefaultWebProxy = false;


        /// <summary>
        /// Cria um serviço para envio de mensagens que se comunica através de um servidor proxy
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <param name="senha">Senha paa autenticação</param>
        /// <param name="proxyAddress">Endereço do servidor Proxy</param>
        /// <param name="useDefaultWebPoxy">True para usar o servidor proxy default do sistema, se estiver disponível</param>
        public Service(string id, string senha, string proxyAddress, bool useDefaultWebPoxy = false)
        {
            _autenticacao.Id = id;
            _autenticacao.Senha = senha;
            _proxyAddress = proxyAddress;
            _useDefaultWebProxy = useDefaultWebPoxy;
        }
        /// <summary>
        /// Cria um serviço para envio de mensagens
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <param name="senha">Senha paa autenticação</param>
        public Service(string id, string senha)
        {
            _autenticacao.Id = id;
            _autenticacao.Senha = senha;
        }

        /// <summary>
        /// Envvia uma mensagem
        /// </summary>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public Mensagem EnviarMensagem(Mensagem mensagem)
        {
            return Assembler.Convert(Api.EnviarMensagem(_autenticacao, Assembler.Convert(mensagem)));
        }

        /// <summary>
        /// Retorna a quantidade de mensagens que podem ser enviadas.
        /// </summary>
        /// <returns></returns>
        public int Saldo()
        {
            return Api.GetSaldoDeMensagens(_autenticacao);
        }

        /// <summary>
        /// Histórico de Mensagens enviadas
        /// </summary>
        /// <returns></returns>
        public List<Mensagem> ListarMensagensEnviadas()
        {
            var mensagens = Api.GetMensagensEnviadas(_autenticacao);
            return mensagens.Select(Assembler.Convert).ToList();
        }

        /// <summary>
        /// Retorna uma mensagem já enviada com seu status atualizado
        /// </summary>
        /// <param name="idMensagem"></param>
        /// <returns></returns>
        public Mensagem GetMensagem(string idMensagem)
        {
            return Assembler.Convert(Api.GetMensagem(_autenticacao, idMensagem));
        }

        /// <summary>
        /// Adiciona um contato
        /// </summary>
        /// <param name="contato"></param>
        /// <returns></returns>
        public Contato AdicionarContato(Contato contato)
        {
            return Assembler.Convert(Api.AdicionarContato(_autenticacao, Assembler.Convert(contato, IdCliente)));
        }

        /// <summary>
        /// Atualiza os dados de um contato
        /// </summary>
        /// <param name="contato"></param>
        public void AtualizarContato(Contato contato)
        {
            Api.AtualizarContato(_autenticacao, Assembler.Convert(contato, IdCliente));
        }

        /// <summary>
        /// Exclui um contato
        /// </summary>
        /// <param name="contatoId"></param>
        public void RemoverContato(Guid contatoId)
        {
            Api.RemoverContato(_autenticacao, contatoId);
        }

        /// <summary>
        /// Envia uma mensagem para N Contatos
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="remetente"></param>
        /// <param name="contatos"></param>
        /// <returns></returns>
        public List<Mensagem> EnviarMensagemParaContatos(string texto, string remetente, IEnumerable<Contato> contatos)
        {
            var array = new ContatoDTO[0];
            if (contatos.Any())
                array = contatos.ToList().Select(c => Assembler.Convert(c, IdCliente)).ToArray();

            var result = Api.EnviarMensagemParaContatos(_autenticacao, texto,
                                                                      remetente, array);

            return result.Select(Assembler.Convert).ToList();
        }

        /// <summary>
        /// etorna os dados de um contato
        /// </summary>
        /// <param name="contatoId"></param>
        /// <returns></returns>
        public Contato Contato(Guid contatoId)
        {
            return Assembler.Convert(Api.Contato(_autenticacao, contatoId));
        }

        /// <summary>
        /// Lista todos os contatos
        /// </summary>
        /// <returns></returns>
        public List<Contato> ListarContatos()
        {
            var result = Api.TodosOsContatos(_autenticacao);
            return result.Where(r => r.IsEnabled).Select(Assembler.Convert).ToList();
        }

        #region ListasDeContatos


        public ListaDeContatos GetListaDeContatos(Guid id)
        {
            return Assembler.Convert(Api.GetListaDeContatos(_autenticacao, id.ToString()));
        }

        /// <summary>
        /// Retorna as Listas de Contatos cadastradas
        /// </summary>
        /// <returns></returns>
        public List<ListaDeContatos> ListarListasDeContatos()
        {
            var result = Api.ListListasDeContatos(_autenticacao);
            return result.Select(Assembler.Convert).ToList();
        }

        /// <summary>
        /// Adicona uma lista de contatos
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        public ListaDeContatos AdicionarListaDeContatos(ListaDeContatos lista)
        {
            return Assembler.Convert(Api.CreateListaDeContatos(_autenticacao, Assembler.Convert(lista, IdCliente)));
        }


        /// <summary>
        /// Atualiza uma lista de contatos
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        public void UpdateListaDeContatos(ListaDeContatos lista)
        {
            Api.UpdateListaDeContatos(_autenticacao, Assembler.Convert(lista, IdCliente));
        }


        /// <summary>
        /// Exclui uma lista de contatos
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        public void RemoverListaDeContatos(ListaDeContatos lista)
        {
            Api.DisableListaDeContatos(_autenticacao, Assembler.Convert(lista, IdCliente));
        }


        /// <summary>
        /// Envia uma mensagem para uma lista de contatos
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        public List<Mensagem> EnviarMensagemParaListaDeContatos(string texto, string remetente, ListaDeContatos lista)
        {
            var result = Api.EnviarMensagemParaListaDeContatos(_autenticacao, texto, remetente, Assembler.Convert(lista, IdCliente));
            return result.Select(Assembler.Convert).ToList();
        }
        #endregion
    }


    


}
