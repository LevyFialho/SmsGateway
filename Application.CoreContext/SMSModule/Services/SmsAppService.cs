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


using System.Security.Authentication;
using SmsGateway.Application.CoreContext.DTO.SMSModule;
using SmsGateway.Application.CoreContext.SMSModule.Services.Contracts;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.AdministradorAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContatoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg.StatusAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Services;

namespace SmsGateway.Application.CoreContext.SMSModule.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SmsGateway.Application.CoreContext.DTO;
    using SmsGateway.Application.CoreContext.Resources;
    using SmsGateway.Application.Seedwork;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
    using SmsGateway.Domain.Seedwork.Specification;
    using SmsGateway.Infrastructure.Crosscutting.Logging;
    using SmsGateway.Infrastructure.Crosscutting.Validator;

    /// <summary>
    /// Implementação do serviço de gerência de administradores do sistema
    /// </summary>
    public class SmsAppService
        : ISmsAppServices
    {
        #region Members

        private readonly IStatusRepository _repositorioStatus;
        private readonly IContratoRepository _repositorioContratos;
        private readonly IMensagemRepository _repositorioMensagens;
        private readonly IClienteRepository _repositorioClientes;
        private readonly IContatoRepository _repositorioContatos;
        private readonly IListaDeContatosRepository _listaDeContatosRepository;
        private readonly OperadoraFactory _operadoraFactory;
        #endregion

        #region Constructors


        public SmsAppService(IStatusRepository repositorioStatus, IContratoRepository repositorioContratos,
      IMensagemRepository repositorioMensagens, IClienteRepository repositorioClientes,
            IContatoRepository repositorioContatos, IListaDeContatosRepository listaDeContatosRepository, OperadoraFactory operadoraFactory)
        {
            if (repositorioStatus == null)
                throw new ArgumentNullException("repositorioStatus");
            if (repositorioContratos == null)
                throw new ArgumentNullException("repositorioContratos");
            if (repositorioClientes == null)
                throw new ArgumentNullException("repositorioClientes");
            if (repositorioMensagens == null)
                throw new ArgumentNullException("repositorioMensagens");
            if (repositorioContatos == null)
                throw new ArgumentNullException("repositorioContatos");
            if (listaDeContatosRepository == null)
                throw new ArgumentNullException("listaDeContatosRepository");
            if (operadoraFactory == null)
                throw new ArgumentNullException("operadoraFactory");
           
            _repositorioStatus = repositorioStatus;
            _repositorioContratos = repositorioContratos;
            _repositorioClientes = repositorioClientes;
            _repositorioMensagens = repositorioMensagens;
            _repositorioContatos = repositorioContatos;
            _listaDeContatosRepository = listaDeContatosRepository;
            _operadoraFactory = operadoraFactory;
        }

        #endregion

        #region Interface Members

        /// <summary>
        /// Contrato do serviço usado pelos clientes. 
        /// </summary>
        /// <param name="autenticacao"> </param> 
        /// <param name="mensagemdto">Mensagem a ser enviada</param>
        public MensagemDTO EnviarMensagem(AutenticacaoDTO autenticacao, MensagemDTO mensagemdto)
        {
            var idCliente = new Guid(autenticacao.Id);
            var cliente = _repositorioClientes.Get(idCliente);
            if (cliente == null || cliente.Senha != autenticacao.Senha) throw new AuthenticationException();
            if ((cliente.IsEnabled) & (cliente.ContratoAtualId != null))
            {
                var contrato = _repositorioContratos.Get((Guid)cliente.ContratoAtualId);
                if (contrato.Validar())
                {
                    //Get status default
                    var status = _repositorioStatus.GetFiltered(s => s.OperadoraApi == OperadoraApi.Null).FirstOrDefault();

                    //Persistir mensagem
                    var mensagem = MensagemFactory.Create(contrato, mensagemdto.TextoDaMensagem,
                                                          mensagemdto.NumeroDoDestinatario,
                                                          mensagemdto.NumeroDoRemetente, status);
                    _repositorioMensagens.Add(mensagem);
                    _repositorioMensagens.UnitOfWork.Commit();

                    //Enviar mensagem
                    var domainService = new SmsService(_repositorioContratos.GetContratosDeOperadorasAtivos(),
                        _repositorioStatus.GetAll().ToList(), _operadoraFactory);
                    mensagem.DataDeEnvio = System.DateTime.Now;
                    var result = domainService.EnviarMensagem(contrato, mensagem);
                    if (result != null)
                    {
                        var mensagemService = new MensagensAppService(_repositorioMensagens, _repositorioContratos, _repositorioStatus);
                        mensagemService.PersistirMensagem(result);
                    }
                    return result.ProjectedAs<MensagemDTO>();

                }
                else
                {
                    throw new Exception(Resources.Messages.error_InvalidClientContract);
                }
            }
            else
            {
                throw new Exception(Resources.Messages.error_InvalidClientContract);
            }
        }

        public List<MensagemDTO> EnviarMensagemParaContatos(AutenticacaoDTO autenticacao, string texto, string remetente, IEnumerable<ContatoDTO> contatos)
        {
            var result = new List<MensagemDTO>();
            var idCliente = new Guid(autenticacao.Id);
            var cliente = _repositorioClientes.Get(idCliente);
            if (cliente == null || cliente.Senha != autenticacao.Senha) throw new AuthenticationException();

            foreach (var contato in contatos)
            {
                if (
                    !_repositorioContatos.GetFiltered(
                        c => c.ClienteId == contato.ClienteId && c.Numero == contato.Numero).Any())
                {
                    var novo = ContatoFactory.Create(contato.Nome, contato.Numero, contato.ClienteId);
                    _repositorioContatos.Add(novo);
                }
                var mensagem = new MensagemDTO()
                    {
                        NumeroDoDestinatario = contato.Numero.ToString(),
                        TextoDaMensagem = texto,
                        NumeroDoRemetente = remetente
                    };
                result.Add(EnviarMensagem(autenticacao, mensagem));
                _repositorioContatos.UnitOfWork.Commit();
            }

            return result;
        }

        public List<MensagemDTO> EnviarMensagemParaListaDeContatos(AutenticacaoDTO autenticacao, string texto, string remetente, ListaDeContatosDTO lista)
        {
            var idCliente = new Guid(autenticacao.Id);
            var cliente = _repositorioClientes.Get(idCliente);
            if (cliente == null || cliente.Senha != autenticacao.Senha) throw new AuthenticationException();
            if (!_listaDeContatosRepository.GetFiltered(l => l.Nome == lista.Nome && l.ClienteId == idCliente).Any())
            {
                var novaLista = ListaDeContatosFactory.ListaDeContatos(lista.Nome, idCliente);
                foreach (var contato in lista.Contatos)
                {
                    var persisted =
                        _repositorioContatos.GetFiltered(
                            c => c.ClienteId == lista.ClienteId && c.Numero == contato.Numero).FirstOrDefault();
                    novaLista.Contatos.Add(persisted ?? ContatoFactory.Create(contato.Nome, contato.Numero, contato.ClienteId));
                }
                _listaDeContatosRepository.Add(novaLista);
                _listaDeContatosRepository.UnitOfWork.Commit();
                
            }

            return lista.Contatos.Select(contato => new MensagemDTO()
                {
                    NumeroDoDestinatario = contato.Numero.ToString(), TextoDaMensagem = texto, NumeroDoRemetente = remetente
                }).Select(mensagem => EnviarMensagem(autenticacao, mensagem)).ToList();
        }

        #endregion

        #region Private Methods

        
        #endregion

        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            //dispose todos os recursos

            _repositorioStatus.Dispose();
            _repositorioContratos.Dispose();
            _repositorioMensagens.Dispose();
            _repositorioClientes.Dispose();

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
