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
    public class ListaDeContatosAppService
        : IListaDeContatosAppService
    {
        #region Members

        private readonly IListaDeContatosRepository _repositorioListasDeContatos;
        private readonly IContatoRepository _repositorioContatos;
        private readonly IClienteRepository _repositorioClientes;
        #endregion

        #region Constructors

        /// <summary>
        /// Cria uma nova instância do serviço 
        /// </summary>
        /// <param name="repositorioListasDeContatos">Repositorio associado, injeção de dependência</param>
        /// <param name="repositorioClientes">Repositorio associado, injeção de dependência</param>
        /// <param name="repositorioContatos"> </param>
        public ListaDeContatosAppService(IListaDeContatosRepository repositorioListasDeContatos,
            IClienteRepository repositorioClientes, IContatoRepository repositorioContatos)
        {
            if (repositorioListasDeContatos == null)
                throw new ArgumentNullException("repositorioListasDeContatos");

            _repositorioListasDeContatos = repositorioListasDeContatos;

            if (repositorioClientes == null)
                throw new ArgumentNullException("repositorioClientes");

            if (repositorioContatos == null)
                throw new ArgumentNullException("repositorioContatos");

            _repositorioContatos = repositorioContatos;

            _repositorioClientes = repositorioClientes;

        }

        #endregion

        #region Interface Members

        public ListaDeContatosDTO Add(ListaDeContatosDTO dto)
        {
            //validar pré-condições
            if (dto == null)
                throw new ArgumentException("dto");
            if (_repositorioClientes.Get(dto.ClienteId) == null)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);
            //Cria a partir da fábrica
            var lista = ListaDeContatosFactory.ListaDeContatos(dto.Nome, dto.ClienteId);
            if(dto.Contatos == null) dto.Contatos = new List<ContatoDTO>();
            foreach (var contato in dto.Contatos)
            {
                var novo = ContatoFactory.Create(contato.Nome, contato.Numero, contato.ClienteId);
                var jaExistente =
                    _repositorioContatos.GetFiltered(c => c.ClienteId == novo.ClienteId && c.Numero == novo.Numero).
                        FirstOrDefault();
                if (jaExistente == null)
                {
                    _repositorioContatos.Add(novo);
                    lista.Contatos.Add(novo);
                }
                else
                {
                    lista.Contatos.Add(jaExistente);
                }
               
            }
            if (_repositorioListasDeContatos.GetFiltered(l => l.Nome == dto.Nome && l.ClienteId == dto.ClienteId).Any())
                throw new Exception("Duplicated list name");
            //persiste a entidade
            SalvarLista(lista);

            //retorna um DTO com ID preenchido
            return lista.ProjectedAs<ListaDeContatosDTO>();

        }

        public void Update(ListaDeContatosDTO dto)
        {
            if (dto == null || dto.Id == Guid.Empty)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);

            //pegar item persistido
            var persisted = _repositorioListasDeContatos.Get(dto.Id);

            if (persisted != null)
            {
                persisted.Nome = dto.Nome; if (dto.Contatos == null) dto.Contatos = new List<ContatoDTO>();

                foreach (var contato in dto.Contatos.Where(contato => persisted.Contatos.All(c => c.Numero != contato.Numero)))
                {
                    _repositorioContatos.Add(ContatoFactory.Create(contato.Nome, contato.Numero, dto.ClienteId));
                }
                for (int i = 0; i < persisted.Contatos.Count; i++)
                {
                    var contato = persisted.Contatos.ElementAt(i);
                    if (dto.Contatos.Any(c => c.Numero == contato.Numero)) continue;
                    persisted.Contatos.Remove(contato);
                    i--;
                } 
                //commit 
                _repositorioListasDeContatos.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotAddAdminWithEmptyInformation);
        }

        public void Remove(Guid id)
        {
            var lista = _repositorioListasDeContatos.Get(id);

            if (lista != null)
            {
                //disable ( "logical delete" ) 
                lista.Disable();

                //commit 
                _repositorioListasDeContatos.UnitOfWork.Commit();

            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingAdmin);
        }

        public List<ListaDeContatosDTO> ListAll()
        {

            //get Contatos
            var listas = _repositorioListasDeContatos.GetAll();

            if (listas != null
                &&
                listas.Any())
            {
                return listas.ProjectedAsCollection<ListaDeContatosDTO>();
            }
            else
                return null;
        }

        public List<ListaDeContatosDTO> ListAllClientLists(Guid clienteId)
        {
            var lista = _repositorioListasDeContatos.GetFiltered(l => l.ClienteId == clienteId);

            if (lista != null)
            {
                return lista.ProjectedAsCollection<ListaDeContatosDTO>();
            }
            else
                return null;
        }

        public ListaDeContatosDTO Find(Guid id)
        {

            var lista = _repositorioListasDeContatos.Get(id);

            if (lista != null)
            {
                return lista.ProjectedAs<ListaDeContatosDTO>();
            }
            else
                return null;
        }

        public ListaDeContatosDTO Find(string nome, Guid clienteId)
        {

            var lista = _repositorioListasDeContatos.GetFiltered(l => l.ClienteId == clienteId && l.Nome == nome).FirstOrDefault();

            if (lista != null)
            {
                return lista.ProjectedAs<ListaDeContatosDTO>();
            }
            else
                return null;
        }
        #endregion

        #region Private Methods

        void SalvarLista(ListaDeContatos lista)
        {
            //instanciar validator
            var validator = EntityValidatorFactory.CreateValidator();

            if (validator.IsValid(lista)) //validar entidade
            {
                //adicionar ao repositorioListasDe
                _repositorioListasDeContatos.Add(lista);

                //commit 
                _repositorioListasDeContatos.UnitOfWork.Commit();
            }
            else //entidade invalida, throw validation errors
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages(lista));
        }



        #endregion

        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            //dispose todos os recursos
            _repositorioListasDeContatos.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
