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
    /// Implementação do serviço de gerência de clientes do sistema
    /// </summary>
    public class SolicitacaoDeCadastroAppService
        : ISolicitacaoDeCadastroAppService
    {
        #region Members

        private readonly ISolicitacaoDeCadastroRepository _repositorioSolicitacao; 
        #endregion

        #region Constructors

        
        /// <summary>
        /// Cria uma nova instância do serviço 
        /// </summary>
        /// <param name="repositorio">Repositorio associado, injeção de dependência</param> 
        public SolicitacaoDeCadastroAppService(ISolicitacaoDeCadastroRepository repositorio)
        {
            if (repositorio == null)
                throw new ArgumentNullException("repositorio");
             
            _repositorioSolicitacao = repositorio;

        }

        #endregion

        #region Interface Members

        public SolicitacaoDeCadastroDTO Add(SolicitacaoDeCadastroDTO solicitacaoDTO, IEmailAppService emailService)
        {
            //validar pré-condições
            if (solicitacaoDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);

            //Cria a partir da fábrica
            var solicitacao = ClienteFactory.Solicitacao(solicitacaoDTO.Nome, solicitacaoDTO.Email, solicitacaoDTO.Telefone);

           
            //persiste a entidade
            SalvarSolicitacao(solicitacao);

            emailService.NovaSolicitacaoDeCadastro(solicitacao);

            //retorna um DTO com ID preenchido
            return solicitacao.ProjectedAs<SolicitacaoDeCadastroDTO>();

        }

        
        public void Update(SolicitacaoDeCadastroDTO solicitacaoDTO)
        {
            if (solicitacaoDTO == null || solicitacaoDTO.Id == Guid.Empty)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);

            //pegar item persistido
            var persisted = _repositorioSolicitacao.Get(solicitacaoDTO.Id);

            if (persisted != null)
            {
                //transforma o DTO passado em uma entidade
                var current = MaterializeFromDto(solicitacaoDTO);

                //Merge 
                _repositorioSolicitacao.Merge(persisted, current);

                //commit 
                _repositorioSolicitacao.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotAddAdminWithEmptyInformation);
        }

        public void Remove(Guid solicitacaoId)
        {
            var solicitacao = _repositorioSolicitacao.Get(solicitacaoId);

            if (solicitacao != null)
            {
                //disable ( "logical delete" ) 
                solicitacao.Disable();
                
                //commit 
                _repositorioSolicitacao.UnitOfWork.Commit(); 
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingAdmin);
        }

     
        public List<SolicitacaoDeCadastroDTO> ListAll()
        {

            //get clientes
            var clientes = _repositorioSolicitacao.GetAll();

            if (clientes != null
                &&
                clientes.Any())
            {
                return clientes.ProjectedAsCollection<SolicitacaoDeCadastroDTO>();
            }
            else
                return null;
        }

        public SolicitacaoDeCadastroDTO Find(Guid id)
        {

            var cliente = _repositorioSolicitacao.Get(id);

            if (cliente != null)
            {
                return cliente.ProjectedAs<SolicitacaoDeCadastroDTO>();
            }
            else
                return null;
        }

        #endregion

        #region Private Methods

        void SalvarSolicitacao(SolicitacaoDeCadastro solicitacao)
        {
            //instanciar validator
            var validator = EntityValidatorFactory.CreateValidator();

            if (validator.IsValid(solicitacao)) //validar entidade
            {
                //adicionar ao repositorio
                _repositorioSolicitacao.Add(solicitacao);

                //commit 
                _repositorioSolicitacao.UnitOfWork.Commit();
            }
            else //entidade invalida, throw validation errors
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<SolicitacaoDeCadastro>(solicitacao));
        }
         
        SolicitacaoDeCadastro MaterializeFromDto(SolicitacaoDeCadastroDTO dto)
        {
            //create the current instance with changes from customerDTO

            var current = ClienteFactory.Solicitacao(dto.Nome, dto.Email, dto.Telefone);

            //set identity
            current.ChangeCurrentIdentity(dto.Id); 
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
            _repositorioSolicitacao.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
