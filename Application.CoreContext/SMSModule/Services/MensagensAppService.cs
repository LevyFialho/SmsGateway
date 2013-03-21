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


using SmsGateway.Application.CoreContext.DTO.SMSModule;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;

namespace SmsGateway.Application.CoreContext.SMSModule.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SmsGateway.Application.CoreContext.DTO;
    using SmsGateway.Application.CoreContext.Resources;
    using SmsGateway.Application.Seedwork;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg.StatusAgg;
    using SmsGateway.Domain.Seedwork.Specification;
    using SmsGateway.Infrastructure.Crosscutting.Logging;
    using SmsGateway.Infrastructure.Crosscutting.Validator;
    using Contracts;
    /// <summary>
    /// Implementação do serviço de gerência de mensagens do sistema
    /// </summary>
    public class MensagensAppService
        : IMensagensAppServices
    {
        #region Members

        private readonly IMensagemRepository _repositorioMensagens;
        private readonly IContratoRepository _repositorioContratos;
        private readonly IStatusRepository _repositorioStatus;
        #endregion

        #region Constructors

        /// <summary>
        /// Cria uma nova instância do serviço 
        /// </summary>
        /// <param name="repositorioMensagens">Repositório associado, injeção de dependência</param>
        /// <param name="repositorioContratos">Repositório associado, injeção de dependência</param>
        public MensagensAppService(IMensagemRepository repositorioMensagens,
            IContratoRepository repositorioContratos, IStatusRepository repositorioStatus)
        {
            if (repositorioMensagens == null)
                throw new ArgumentNullException("repositorioMensagens");
            if (repositorioContratos == null)
                throw new ArgumentNullException("repositorioContratos");
            if (repositorioStatus == null)
                throw new ArgumentNullException("repositorioStatus");
           

            _repositorioMensagens = repositorioMensagens;
            _repositorioContratos = repositorioContratos;
            _repositorioStatus = repositorioStatus;

        }

        #endregion

        #region IMensagensAppService Members

        /// <summary>
        /// Cadastra uma nova mensagem no sistema
        /// </summary>
        /// <param name="mensagemDTO">Mensagem a cadastrar</param>
        /// <returns>Mensagem cadastrada</returns>
        public MensagemDTO NovaMensagem(MensagemDTO mensagemDTO){
            //validar pré-condições
            if (mensagemDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddMessageWithEmptyInformation);
 
            var contratodoCliente = _repositorioContratos.Get(mensagemDTO.ContratoDoClienteId);

            if(contratodoCliente == null)
                throw new ArgumentException(Messages.exception_CouldNotFindClientContract);

            var status = _repositorioStatus.GetFiltered(s => s.OperadoraApi == OperadoraApi.Null).FirstOrDefault();
            var message = MensagemFactory.Create(contratodoCliente, mensagemDTO.TextoDaMensagem,
                                               mensagemDTO.NumeroDoDestinatario, mensagemDTO.NumeroDoRemetente, status);

            //persiste a entidade
            SalvarMensagem(message);

            //retorna um DTO com ID preenchido
            return message.ProjectedAs<MensagemDTO>();
      }

        /// <summary>
        /// Atualiza uma mensagem no sistema
        /// </summary>
        /// <param name="mensagemDTO">Mensagem a persistir</param>
        /// <returns></returns>
        public void PersistirMensagem(MensagemDTO mensagemDTO){

            if (mensagemDTO == null || mensagemDTO.Id == Guid.Empty)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);

            //pegar item persistido
            var persisted = _repositorioMensagens.Get(mensagemDTO.Id);

            if (persisted != null)
            {
                //transforma o DTO passado em uma entidade
                var current = MaterializeMessageFromDto(mensagemDTO);

                //Merge 
                _repositorioMensagens.Merge(persisted, current);

                //commit 
                _repositorioMensagens.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotAddMessageWithEmptyInformation);
      }

        /// <summary>
        /// Atualiza uma mensagem no sistema
        /// </summary>
        /// <param name="mensagem">Mensagem a persistir</param>
        /// <returns></returns>
        public void PersistirMensagem(Mensagem mensagem)
        {

            if (mensagem == null || mensagem.Id == Guid.Empty)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);

            //pegar item persistido
            var persisted = _repositorioMensagens.Get(mensagem.Id);

            if (persisted != null)
            {
                 

                //Merge 
                _repositorioMensagens.Merge(persisted, mensagem);

                //commit 
                _repositorioMensagens.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotAddMessageWithEmptyInformation);
        }

        /// <summary>
        /// Pega uma mensagem no sistema
        /// </summary>
        /// <param name="id">Identificador da mensagem</param>
        /// <returns></returns>
        public MensagemDTO GetMensagem(Guid id){

            var customer = _repositorioMensagens.Get(id);

            if (customer != null) 
            {
                return customer.ProjectedAs<MensagemDTO>();
            }
            else
                return null;
      }

        /// <summary>
        /// Pega todas as mensagens de um cliente
        /// </summary>
        /// <param name="idCliente">Identificador do cliente</param>
        /// <returns></returns>
        public List<MensagemDTO> GetMensagensDoCliente(Guid idCliente){
            var messages = new List<MensagemDTO>();
            //Aplicação do pattern Specifications
            var specs = MensagemSpecifications.MensagensDoCliente(idCliente);
            var result = _repositorioMensagens.AllMatching(specs);
            if (result != null)
            {
                messages.AddRange(result.Select(mensagem => mensagem.ProjectedAs<MensagemDTO>()));
                return messages;
            }
            else
                return null;
      }

        /// <summary>
        /// Pega todas as mensagens de um contrato
        /// </summary>
        /// <param name="idContrato">Identificador do contrato</param>
        /// <returns></returns>
        public List<MensagemDTO> GetMensagensDoContrato(Guid idContrato){
             var messages = new List<MensagemDTO>();
            //Aplicação do pattern Specifications
             var specs = MensagemSpecifications.MensagensDoContrato(idContrato);
             var result = _repositorioMensagens.AllMatching(specs);
           
            if (result != null)
            {
                messages.AddRange(result.Select(mensagem => mensagem.ProjectedAs<MensagemDTO>()));
                return messages;
            }
            else
                return null;
      }
       
        #endregion

        #region Private Methods

        void SalvarMensagem(Mensagem message)
        {
            var validator = EntityValidatorFactory.CreateValidator();

            if (validator.IsValid(message))  
            {
                _repositorioMensagens.Add(message);
                 
                _repositorioMensagens.UnitOfWork.Commit();
            }
            else  
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<Mensagem>(message));
        }

        /// <summary>
        /// Materializa uma entidade a partir de  um DTO 
        /// </summary>
        /// <param name="mensagemDTO"></param>
        /// <returns></returns>
        Mensagem MaterializeMessageFromDto(MensagemDTO mensagemDTO)
        {
            
             //Cria a partir da fábrica
            var contratodoCliente = _repositorioContratos.Get(mensagemDTO.ContratoDoClienteId);

            if(contratodoCliente == null)
                throw new ArgumentException(Messages.exception_CouldNotFindClientContract);

            var status = _repositorioStatus.GetFiltered(s => s.OperadoraApi == OperadoraApi.Null).FirstOrDefault();
            var current = MensagemFactory.Create(contratodoCliente, mensagemDTO.TextoDaMensagem,
                                               mensagemDTO.NumeroDoDestinatario, mensagemDTO.NumeroDoRemetente, status);

            //set identity
            current.ChangeCurrentIdentity(mensagemDTO.Id);
            current.SetTheCurrentStatusReference(mensagemDTO.StatusId);
            

            return current;
        }
        #endregion

        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            //dispose todos os recursos
            _repositorioMensagens.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
