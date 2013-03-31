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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;

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
    /// Implementação do serviço de gerência de clientes do sistema
    /// </summary>
    public class ClientesAppService
        : IClientesAppService
    {
        #region Members

        private readonly IClienteRepository _repositorioClientes;
        private readonly IContratoRepository _repositorioContratos;
        private readonly IMensagemRepository _repositorioMensagens;
        #endregion

        #region Constructors

        /// <summary>
        /// Cria uma nova instância do serviço 
        /// </summary>
        /// <param name="repositorio">Repositorio associado, injeção de dependência</param>
        /// <param name="repositorioContratos">Repositorio associado, injeção de dependência</param>
        /// <param name="repositorioMensagens">Repositorio associado, injeção de dependência </param>
        public ClientesAppService(IClienteRepository repositorio, IContratoRepository repositorioContratos,
            IMensagemRepository repositorioMensagens)
        {
            if (repositorio == null)
                throw new ArgumentNullException("repositorio");

            _repositorioClientes = repositorio;

            if (repositorioContratos == null)
                throw new ArgumentNullException("repositorioContratos");

            _repositorioContratos = repositorioContratos;

            if (repositorioMensagens == null)
                throw new ArgumentNullException("repositorioMensagens");

            _repositorioMensagens = repositorioMensagens;

        }

        #endregion

        #region Interface Members

        public AutenticacaoDTO Autenticar(string email, string senha)
        {
            var cliente = _repositorioClientes.GetFiltered(c => c.Email == email && c.Senha == senha).FirstOrDefault();
            if (cliente == null) return null;
            return new AutenticacaoDTO()
                {
                    Id = cliente.Id.ToString(),
                    Senha = cliente.Senha
                };

        }

        public AutenticacaoDTO Autenticar(Guid id, string senha)
        {
            var cliente = _repositorioClientes.GetFiltered(c => c.Id == id && c.Senha == senha).FirstOrDefault();
            if (cliente == null) return null;
            return new AutenticacaoDTO()
            {
                Id = cliente.Id.ToString(),
                Senha = cliente.Senha
            };
        }

        public ClienteDTO Add(ClienteDTO clienteDto)
        {
            //validar pré-condições
            if (clienteDto == null)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);

            //Cria a partir da fábrica
            var cliente = ClienteFactory.Create(clienteDto.Nome, clienteDto.Senha, clienteDto.Email);

            if(_repositorioClientes.GetFiltered(c => c.Email == cliente.Email ).Any())
                throw  new Exception("Já existe um cliente com este email");

            //persiste a entidade
            SalvarCliente(cliente);

            //retorna um DTO com ID preenchido
            return cliente.ProjectedAs<ClienteDTO>();

        }

        public void Update(ClienteDTO clienteDto)
        {
            if (clienteDto == null || clienteDto.Id == Guid.Empty)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);

            //pegar item persistido
            var persisted = _repositorioClientes.Get(clienteDto.Id);

            if (persisted != null)
            {
                //transforma o DTO passado em uma entidade
                var current = MaterializeClienteFromDto(clienteDto);

                //Merge 
                _repositorioClientes.Merge(persisted, current);

                //commit 
                _repositorioClientes.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotAddAdminWithEmptyInformation);
        }

        public void Remove(Guid clienteId)
        {
            var client = _repositorioClientes.Get(clienteId);

            if (client != null)
            {
                //disable ( "logical delete" ) 
                client.Disable();
                var contrato = _repositorioContratos.GetFiltered(c => c.Id == client.ContratoAtualId).FirstOrDefault();
                if (contrato != null)
                    contrato.Disable();
                //commit 
                _repositorioClientes.UnitOfWork.Commit();
                _repositorioContratos.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingAdmin);
        }

        public List<ClienteDTO> Find(int pageIndex, int pageCount)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArguments);

            //get clientes
            var clientes = _repositorioClientes.GetEnabled(pageIndex, pageCount);

            if (clientes != null
                &&
                clientes.Any())
            {
                return clientes.ProjectedAsCollection<ClienteDTO>();
            }
            else
                return null;
        }

        public List<ClienteDTO> ListAll()
        {

            //get clientes
            var clientes = _repositorioClientes.GetAll();

            if (clientes != null
                &&
                clientes.Any())
            {
                return clientes.ProjectedAsCollection<ClienteDTO>();
            }
            else
                return null;
        }

        public ClienteDTO Find(Guid id)
        {

            var cliente = _repositorioClientes.Get(id);

            if (cliente != null)
            {
                return cliente.ProjectedAs<ClienteDTO>();
            }
            else
                return null;
        }

        public DadosDoClienteDTO DadosDoCliente(AutenticacaoDTO autenticacao)
        {
            var dados = new DadosDoClienteDTO();
            var cliente = _repositorioClientes.Get(new Guid(autenticacao.Id));
            if ((cliente != null) && (cliente.Senha == autenticacao.Senha))
            {
                dados.Id = new Guid(autenticacao.Id);
                dados.Nome = cliente.Nome;
                dados.SaldoRemanescente = cliente.ContratoAtual.SaldoDeMensagens;
                dados.Senha = cliente.Senha;
                dados.TotalDeMensagensEnviadas =
                    _repositorioMensagens.GetFiltered(m => m.ContratoDoCliente.ClienteId == cliente.Id).Count();
            }
            return dados;
        }

        #endregion

        #region Private Methods

        void SalvarCliente(Cliente cliente)
        {
            //instanciar validator
            var validator = EntityValidatorFactory.CreateValidator();

            if (validator.IsValid(cliente)) //validar entidade
            {
                //adicionar ao repositorio
                _repositorioClientes.Add(cliente);

                //commit 
                _repositorioClientes.UnitOfWork.Commit();
            }
            else //entidade invalida, throw validation errors
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<Cliente>(cliente));
        }

        /// <summary>
        /// Materializa uma entidade a partir de  um DTO 
        /// </summary>
        /// <param name="administradorDTO"></param>
        /// <returns></returns>
        Cliente MaterializeClienteFromDto(ClienteDTO dto)
        {
            //create the current instance with changes from customerDTO

            var current = ClienteFactory.Create(dto.Nome, dto.Senha, dto.Email);

            //set identity
            current.ChangeCurrentIdentity(dto.Id);
            current.SetTheCurrentContractReference(dto.ContratoAtualId);
            if(!dto.IsEnabled)
                current.Disable();
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
            _repositorioClientes.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
