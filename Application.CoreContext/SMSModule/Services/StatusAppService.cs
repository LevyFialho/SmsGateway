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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg.StatusAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;

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
    public class StatusAppService
        : IStatusAppService
    {
        #region Members

        private readonly IStatusRepository _repositorio;

        #endregion

        #region Constructors

        /// <summary>
        /// Cria uma nova instância do serviço 
        /// </summary>
        /// <param name="repositorioAdministradores">Repositório associado, injeção de dependência</param>
        public StatusAppService(IStatusRepository repositorio)
        {
            if (repositorio == null)
                throw new ArgumentNullException("repositorio");

            _repositorio = repositorio;

        }

        #endregion

        #region Interface Members

        public StatusDTO Add(StatusDTO dto)
        {
            //validar pré-condições
            if (dto == null)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);

            //Cria a partir da fábrica
            
            var status = StatusFactory.Create(dto.Codigo, dto.Descricao, dto.MensagemAoCliente,
                                               dto.QuantoDebitarDoContratoDoCliente,
                                               dto.QuantoDebitarDoContratoDaOperadora, dto.ValorDaOperacao,
                                               Enumerations.Convert(dto.OperadoraApi));
                                                      

            //persiste a entidade
            SalvarStatus(status);

            //retorna um DTO com ID preenchido
            return status.ProjectedAs<StatusDTO>();

        }

        public void Update(StatusDTO status)
        {
            if (status == null || status.Id == Guid.Empty)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);

            //pegar item persistido
            var persisted = _repositorio.Get(status.Id);

            if (persisted != null)
            {
                //transforma o DTO passado em uma entidade
                var current = MaterializeFromDto(status);

                //Merge 
                _repositorio.Merge(persisted, current);

                //commit 
                _repositorio.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotAddAdminWithEmptyInformation);
        }

        public void Remove(Guid statusId)
        {
            var status = _repositorio.Get(statusId);

            if (status != null)
            {
                //disable ( "logical delete" ) 
                status.Disable();

                //commit 
                _repositorio.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingAdmin);
        }

        public List<StatusDTO> Find(int pageIndex, int pageCount)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArguments);

            //get 
            var status = _repositorio.GetEnabled(pageIndex, pageCount);

            if (status != null
                &&
                status.Any())
            {
                return status.ProjectedAsCollection<StatusDTO>();
            }
            else
                return null;
        }

        public List<StatusDTO> ListAll()
        {
            
            //get  
            var status = _repositorio.GetAll();

            if (status != null
                &&
                status.Any())
            {
                return status.ProjectedAsCollection<StatusDTO>();
            }
            else
                return null;
        }

        public StatusDTO Find(Guid id)
        {

            var status = _repositorio.Get(id);

            if (status != null) 
            {
                return status.ProjectedAs<StatusDTO>();
            }
            else
                return null;
        }
       
        #endregion

        #region Private Methods

        void SalvarStatus(Status status)
        {
            //recover validator
            var validator = EntityValidatorFactory.CreateValidator();

            if (validator.IsValid(status)) //if customer is valid
            {
                //add the customer into the repository
                _repositorio.Add(status);

                //commit the unit of work
                _repositorio.UnitOfWork.Commit();
            }
            else //customer is not valid, throw validation errors
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<Status>(status));
        }

        /// <summary>
        /// Materializa uma entidade a partir de  um DTO 
        /// </summary>
        /// <param name="administradorDTO"></param>
        /// <returns></returns>
        Status MaterializeFromDto(StatusDTO dto)
        {
            //create the current instance with changes from customerDTO
            var current = StatusFactory.Create(dto.Codigo, dto.Descricao, dto.MensagemAoCliente,
                                               dto.QuantoDebitarDoContratoDoCliente,
                                               dto.QuantoDebitarDoContratoDaOperadora, dto.ValorDaOperacao,
                                               Enumerations.Convert(dto.OperadoraApi));

            //set identity
            current.ChangeCurrentIdentity(dto.Id);


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
            _repositorio.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
