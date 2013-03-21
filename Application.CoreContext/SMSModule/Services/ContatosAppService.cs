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
using SmsGateway.Application.CoreContext.SMSModule.Services.Contracts;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.AdministradorAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;

namespace SmsGateway.Application.CoreContext.SMSModule.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SmsGateway.Application.CoreContext.DTO;
    using SmsGateway.Application.CoreContext.Resources;
    using SmsGateway.Application.Seedwork;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContatoAgg;
    using SmsGateway.Domain.Seedwork.Specification;
    using SmsGateway.Infrastructure.Crosscutting.Logging;
    using SmsGateway.Infrastructure.Crosscutting.Validator;

    /// <summary>
    /// Implementação do serviço de gerência de Contatos do sistema
    /// </summary>
    public class ContatosAppService
        : IContatosAppService
    {
        #region Members

        private readonly IContatoRepository _repositorioContatos;
        private readonly IClienteRepository _repositorioClientes;
        private readonly IListaDeContatosRepository _repositorioListaDeContatos;
       
        #endregion

        #region Constructors

        
        /// <summary>
        /// Cria uma nova instância do serviço 
        /// </summary>
        /// <param name="repositorio">Repositorio associado, injeção de dependência</param>
        /// <param name="repositorioClientes">Repositorio associado, injeção de dependência</param>
        /// <param name="repositorioListaDeContatos">Repositorio associado, injeção de dependência</param>
        public ContatosAppService(IContatoRepository repositorio, IClienteRepository repositorioClientes, IListaDeContatosRepository repositorioListaDeContatos)
        {
            if (repositorio == null)
                throw new ArgumentNullException("repositorio");

            _repositorioContatos = repositorio;

            if (repositorioClientes == null)
                throw new ArgumentNullException("repositorioClientes");

            _repositorioClientes = repositorioClientes;

            //_repositorioListaDeContatos

            if (repositorioListaDeContatos == null)
                throw new ArgumentNullException("repositorioListaDeContatos");

            _repositorioListaDeContatos = repositorioListaDeContatos;


        }

        #endregion

        #region Interface Members

        public ContatoDTO Add(ContatoDTO contatoDto)
        {
            //validar pré-condições
            if (contatoDto == null)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);
            if(_repositorioClientes.Get(contatoDto.ClienteId) == null)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);
            if (_repositorioContatos.GetFiltered(c => c.ClienteId == contatoDto.ClienteId
                && c.Numero == contatoDto.Numero && c.IsEnabled).Any())
                throw new Exception("Número já cadastrado");
            //Cria a partir da fábrica
            var contato = ContatoFactory.Create(contatoDto.Nome,  contatoDto.Numero, contatoDto.ClienteId);


            //persiste a entidade
            SalvarContato(contato);

            //retorna um DTO com ID preenchido
            return contato.ProjectedAs<ContatoDTO>();

        }

        public void Update(ContatoDTO contatoDto)
        {
            if (contatoDto == null || contatoDto.Id == Guid.Empty)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);

            //pegar item persistido
            var persisted = _repositorioContatos.Get(contatoDto.Id);
            if(_repositorioContatos.GetFiltered(c => c.ClienteId == persisted.ClienteId
                && c.Id != persisted.Id && c.Numero == persisted.Numero && c.IsEnabled).Any())
                throw new Exception("Número já cadastrado");
            if (persisted != null)
            {
                
                //Merge 
                persisted.Nome = contatoDto.Nome;
                persisted.Numero = contatoDto.Numero;

                //commit 
                _repositorioContatos.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotAddAdminWithEmptyInformation);
        }

        public void AddToList(Guid contatoId, Guid listaId)
        {
            if (listaId == Guid.Empty || contatoId == Guid.Empty)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);

            //pegar item persistido
            var lista = _repositorioListaDeContatos.Get(listaId);
            var contato = _repositorioContatos.Get(contatoId);
            if (lista != null && contato != null && lista.Contatos.All(c => c.Id != contatoId))
            {
                lista.Contatos.Add(contato);
                _repositorioListaDeContatos.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotAddAdminWithEmptyInformation);
        }

        public void RemoveFromList(Guid contatoId, Guid listaId)
        {
            if (listaId == Guid.Empty || contatoId == Guid.Empty)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);

            //pegar item persistido
            var lista = _repositorioListaDeContatos.Get(listaId);
            var contato = _repositorioContatos.Get(contatoId);
            if (lista != null && contato != null && lista.Contatos.Any(c => c.Id != contatoId))
            {
                lista.Contatos.Remove(contato);
                _repositorioListaDeContatos.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotAddAdminWithEmptyInformation);
        }

        public void Remove(Guid contatoId)
        {
            var contato = _repositorioContatos.Get(contatoId);

            if (contato != null)
            {
                //disable ( "logical delete" ) 
                contato.Disable();
                 
                //commit 
                _repositorioContatos.UnitOfWork.Commit();
                 
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingAdmin);
        }

        public List<ContatoDTO> Find(int pageIndex, int pageCount)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArguments);

            //get Contatos
            var contatos = _repositorioContatos.GetEnabled(pageIndex, pageCount);

            if (contatos != null
                &&
                contatos.Any())
            {
                return contatos.ProjectedAsCollection<ContatoDTO>();
            }
            else
                return null;
        }

        public List<ContatoDTO> ListAll()
        {

            //get Contatos
            var contatos = _repositorioContatos.GetAll();

            if (contatos != null
                &&
                contatos.Any())
            {
                return contatos.ProjectedAsCollection<ContatoDTO>();
            }
            else
                return null;
        }

        public List<ContatoDTO> ClientContracts(Guid clientId)
        {
           return _repositorioContatos.GetFiltered(c => c.ClienteId == clientId).ToList().ProjectedAsCollection<ContatoDTO>();
        }

        public ContatoDTO Find(Guid id)
        {

            var contato = _repositorioContatos.Get(id);

            if (contato != null)
            {
                return contato.ProjectedAs<ContatoDTO>();
            }
            else
                return null;
        }

        #endregion

        #region Private Methods

        void SalvarContato(Contato Contato)
        {
            //instanciar validator
            var validator = EntityValidatorFactory.CreateValidator();

            if (validator.IsValid(Contato)) //validar entidade
            {
                //adicionar ao repositorio
                _repositorioContatos.Add(Contato);

                //commit 
                _repositorioContatos.UnitOfWork.Commit();
            }
            else //entidade invalida, throw validation errors
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<Contato>(Contato));
        }

        /// <summary>
        /// Materializa uma entidade a partir de  um DTO 
        /// </summary>
        /// <param name="administradorDTO"></param>
        /// <returns></returns>
        Contato MaterializeContatoFromDto(ContatoDTO dto)
        {
            //create the current instance with changes from customerDTO

            var current = ContatoFactory.Create(dto.Nome, dto.Numero, dto.ClienteId);

            //set identity
            current.ChangeCurrentIdentity(dto.Id);
            current.SetTheCurrentClientReference(dto.ClienteId);

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
            _repositorioContatos.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
