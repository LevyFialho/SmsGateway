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
using SmsGateway.Domain.Seedwork;

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
    public class ContratosAppService
        : IContratosAppServices
    {
        #region Members

        private readonly IMensagemRepository _repositorioMensagens;
        private readonly IContratoRepository _repositorioContratos;
        private readonly IClienteRepository _repositorioClientes;
        #endregion

        #region Constructors

        /// <summary>
        /// Cria uma nova instância do serviço 
        /// </summary>
        /// <param name="repositorioMensagens">Repositório associado, injeção de dependência</param>
        /// <param name="repositorioContratos">Repositório associado, injeção de dependência</param>
        ///<param name="repositorioClientes">Repositório associado, injeção de dependência</param>
        public ContratosAppService(IMensagemRepository repositorioMensagens,
            IContratoRepository repositorioContratos, IClienteRepository repositorioClientes)
        {
            if (repositorioMensagens == null)
                throw new ArgumentNullException("repositorioMensagens");
            if (repositorioContratos == null)
                throw new ArgumentNullException("repositorioContratos");
            if (repositorioClientes == null)
                throw new ArgumentNullException("repositorioClientes");


            _repositorioMensagens = repositorioMensagens;
            _repositorioContratos = repositorioContratos;
            _repositorioClientes = repositorioClientes;

        }

        #endregion

        #region IContratosAppServices Members
        /// <summary>
        /// Pega um contrato por ID
        /// </summary>
        /// <param name="id">Guid do contrato</param>
        /// <returns>Contrato encontrado</returns>
        public ContratoDTO GetContrato(Guid id)
        {
            var contrato = _repositorioContratos.Get(id);

            if (contrato != null)
            {
                return contrato.ProjectedAs<ContratoDTO>();
            }
            else
                return null;
        }

        /// <summary>
        /// Pega um contrato por ID
        /// </summary>
        /// <param name="id">Guid do contrato</param>
        /// <returns>Contrato encontrado</returns>
        public ContratoDTO GetContratoDoCliente(Guid id)
        {
            var cliente = _repositorioClientes.Get(id);
            if ((cliente != null))
            {
                if ((cliente.ContratoAtualId != null))
                {

                    var contrato = _repositorioContratos.Get((Guid)cliente.ContratoAtualId);

                    if (contrato != null)
                    {
                        return contrato.ProjectedAs<ContratoDTO>();
                    }
                }
            }
             
                return null;
        }
      

        /// <summary>
        /// Cadastra um novo cliente no sistema
        /// </summary>
        /// <param name="clienteDTO">Cliente a cadastrar</param>
        /// <param name="contrato">Todos os clientes precisam ter um novo  contrato na hora do cadastro</param>
        /// <returns>Cliente cadastrado</returns>
        public ClienteDTO NovoCliente(ClienteDTO clienteDTO, ContratoDTO contrato)
        {
            //validar pré-condições
            if (clienteDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddClientWithEmptyInformation);

            //Cria a partir da fábrica
            var cliente = ClienteFactory.Create(clienteDTO.Nome, clienteDTO.Senha, clienteDTO.Email);

            //persiste a entidade
            SalvarNovoCliente(cliente);

            //retorna um DTO com ID preenchido
            return cliente.ProjectedAs<ClienteDTO>();
        }
        /// <summary>
        /// Cria  um novo contrato com um cliente
        /// </summary>
        /// <param name="contratoDTO">Contrato a persistir</param>
        /// <returns></returns>
        public ContratoDTO NovoContratoComCliente(ContratoDTO contratoDTO)
        {
            //validar pré-condições
            if (contratoDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddClientWithEmptyInformation);
            var cliente = _repositorioClientes.Get(contratoDTO.ClienteId);
            if (cliente == null)
                throw new ArgumentNullException("Cliente do contrato");
            //Cria a partir da fábrica
            var contrato = ContratoFactory.CriarContratoComCliente(contratoDTO.DataInicial,
                                                                   contratoDTO.DataFinal, contratoDTO.SaldoDeMensagens,
                                                                   contratoDTO.ValorMensagem,
                                                                   cliente);

            //persiste a entidade
            SalvarNovoContrato(contrato);
            //Atualizar referencia do contrato atual do cliente
            cliente.SetarContratoAtual(contrato);
            AtualizarCliente(cliente.ProjectedAs<ClienteDTO>());

            //retorna um DTO com ID preenchido
            return contrato.ProjectedAs<ContratoDTO>();
        }

        /// <summary>
        /// Cria  um novo contrato com operadora
        /// </summary>
        /// <param name="contratoDTO">Contrato a persistir</param>
        /// <returns></returns>
        public ContratoDTO NovoContratoComOperadora(ContratoDTO contratoDTO)
        {
            //validar pré-condições
            if (contratoDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddClientWithEmptyInformation);
            
            //Cria a partir da fábrica
            var operadoraApi = OperadoraApi.Null;
            var enumValues = Enum.GetValues(typeof(OperadoraApi))
                .OfType<OperadoraApi>().ToArray();
            //foreach (var value in enumValues.Where(value => contratoDTO.OperadoraApi == value.ToString()))
            //{
            //    operadoraApi = value;
            //}
            var contrato = ContratoFactory.CriarContratoComOperadora(contratoDTO.DataInicial,
                                                                   contratoDTO.DataFinal, contratoDTO.SaldoDeMensagens,
                                                                   contratoDTO.ValorMensagem,
                                                                   operadoraApi);

            //persiste a entidade
            SalvarNovoContrato(contrato);

            //retorna um DTO com ID preenchido
            return contrato.ProjectedAs<ContratoDTO>();
        }
        /// <summary>
        /// Atualiza  um contrato
        /// </summary>
        /// <param name="contratoDTO">Contrato a persistir</param>
        /// <returns></returns>
        public void RenovarContrato(ContratoDTO contratoDTO)
        {
            if (contratoDTO == null || contratoDTO.Id == Guid.Empty)
                throw new ArgumentException(Messages.warning_CannotAddContractWithEmptyInformation);

            //pegar item persistido
            var persisted = _repositorioContratos.Get(contratoDTO.Id);

            if (persisted != null)
            {
                //transforma o DTO passado em uma entidade
                var current = CreateNewContratoFromDto(contratoDTO);
               
                persisted.Disable();
                persisted.RenovarContrato(current);
                current.Cliente.SetarContratoAtual(current);

                //Merge 
                _repositorioContratos.Add(current);

                //commit 
                _repositorioContratos.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotAddContractWithEmptyInformation);
        }

        ///// <summary>
        ///// Atualiza um cliente
        ///// </summary>
        ///// <param name="clienteDTO">Cliente a persistir</param>
        ///// <returns></returns>
        public void AtualizarCliente(ClienteDTO clienteDTO)
        {
            if (clienteDTO == null || clienteDTO.Id == Guid.Empty)
                throw new ArgumentException(Messages.warning_CannotAddClientWithEmptyInformation);

            //pegar item persistido
            var persisted = _repositorioClientes.Get(clienteDTO.Id);

            if (persisted != null)
            {
                //transforma o DTO passado em uma entidade
                var current = MaterializeClienteFromDto(clienteDTO);
              
                //Merge 
                _repositorioClientes.Merge(persisted, current);

                //commit 
                _repositorioClientes.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotAddClientWithEmptyInformation);
        }

        /// <summary>
        /// Pega todos os contratos do tipo Cliente ativos no sistema
        /// </summary>
        /// <returns>Lista de contratos ativos de clientes</returns>
        public List<ContratoDTO> GetContratosAtivosDeClientes()
        {
            var specs = ContratoSpecifications.ContratosAtivosDeClientes();
            var result = _repositorioContratos.AllMatching(specs);
            return result.Select(contrato => contrato.ProjectedAs<ContratoDTO>()).ToList();
        }

        /// <summary>
        /// Pega todos os contratos do tipo Operadora ativos no sistema
        /// </summary>
        /// <returns>Lista de contratos ativos de operadoras</returns>
        public List<ContratoDTO> GetContratosAtivosDeOperadoras()
        {
            var specs = ContratoSpecifications.ContratosAtivosDeOperadoras();
            var result = _repositorioContratos.AllMatching(specs);
            return result.Select(contrato => contrato.ProjectedAs<ContratoDTO>()).ToList();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Adiciona um contrato ao repositorio
        /// </summary>
        /// <param name="contrato">Contrato a adicionar</param>
        void SalvarNovoContrato(Contrato contrato)
        {
            //intancia o validador
            var validator = EntityValidatorFactory.CreateValidator();

            if (validator.IsValid(contrato)) //se for valido
            {
                //adiciona ao repositorio
                _repositorioContratos.Add(contrato);

                //commit  
                _repositorioContratos.UnitOfWork.Commit();
            }
            else //throw validation errors
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<Contrato>(contrato));
        }

        /// <summary>
        /// Cria uma entidade a partir de  um DTO 
        /// </summary>
        /// <param name="contratoDTO"></param>
        /// <returns></returns>
        Contrato CreateNewContratoFromDto(ContratoDTO contratoDTO)
        {


            switch (contratoDTO.TipoDeContrato)
            {
                case TipoDeContratoDTO.Cliente:
                    var cliente = _repositorioClientes.Get(contratoDTO.ClienteId);
                    if (cliente == null)
                        throw new ArgumentNullException("cliente");
                    var current = ContratoFactory.CriarContratoComCliente(contratoDTO.DataInicial,
                                                                          contratoDTO.DataFinal,
                                                                          contratoDTO.SaldoDeMensagens,
                                                                          contratoDTO.ValorMensagem,
                                                                          cliente);
                   
                    return current;


                case TipoDeContratoDTO.Operadora:

                    var current2 = ContratoFactory.CriarContratoComOperadora(contratoDTO.DataInicial,
                                                                          contratoDTO.DataFinal,
                                                                          contratoDTO.SaldoDeMensagens,
                                                                          contratoDTO.ValorMensagem,
                                                                          Enumerations.Convert(contratoDTO.OperadoraApi));
                    
                    return current2;
                    break;
                default: throw new NotImplementedException();
            }

        }

        /// <summary>
        /// Materializa uma entidade a partir de  um DTO 
        /// </summary>
        /// <param name="contratoDTO"></param>
        /// <returns></returns>
        Contrato MaterializeContratoFromDto(ContratoDTO contratoDTO)
        {
            

            switch (contratoDTO.TipoDeContrato)
            {
                case  TipoDeContratoDTO.Cliente:
                    var cliente = _repositorioClientes.Get(contratoDTO.ClienteId);
                    if (cliente == null)
                        throw new ArgumentNullException("cliente");
                    var current = ContratoFactory.CriarContratoComCliente(contratoDTO.DataInicial,
                                                                          contratoDTO.DataFinal,
                                                                          contratoDTO.SaldoDeMensagens,
                                                                          contratoDTO.ValorMensagem,
                                                                          cliente);
                    current.ChangeCurrentIdentity(contratoDTO.Id);
                    if(!contratoDTO.IsEnabled)
                        current.Disable();
                    return current;


                case TipoDeContratoDTO.Operadora:
                   
                    var current2 = ContratoFactory.CriarContratoComOperadora(contratoDTO.DataInicial,
                                                                          contratoDTO.DataFinal,
                                                                          contratoDTO.SaldoDeMensagens,
                                                                          contratoDTO.ValorMensagem,
                                                                          Enumerations.Convert(contratoDTO.OperadoraApi));
                    current2.ChangeCurrentIdentity(contratoDTO.Id);
                    return current2;
                    break;
                default: throw new NotImplementedException();
            }

        }

        /// <summary>
        /// Adiciona um cliente ao Repositorio
        /// </summary>
        /// <param name="cliente">Cliente a adicionar</param>
        void SalvarNovoCliente(Cliente cliente)
        {
            //recover validator
            var validator = EntityValidatorFactory.CreateValidator();

            if (validator.IsValid(cliente)) //if customer is valid
            {
                //add the customer into the repository
                _repositorioClientes.Add(cliente);

                //commit the unit of work
                _repositorioClientes.UnitOfWork.Commit();
            }
            else //customer is not valid, throw validation errors
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<Cliente>(cliente));
        }

        /// <summary>
        /// Materializa uma entidade a partir de  um DTO 
        /// </summary>
        /// <param name="clienteDTO"></param>
        /// <returns></returns>
        Cliente MaterializeClienteFromDto(ClienteDTO clienteDTO)
        {
            var current = ClienteFactory.Create(clienteDTO.Nome, clienteDTO.Senha, clienteDTO.Email);
            current.ChangeCurrentIdentity(clienteDTO.Id);
            current.SetTheCurrentContractReference(clienteDTO.ContratoAtualId);
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
