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

namespace SmsGateway.Application.CoreContext.SMSModule.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SmsGateway.Application.CoreContext.DTO;
    using SmsGateway.Application.CoreContext.Resources;
    using SmsGateway.Application.Seedwork;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.AdministradorAgg;
    using SmsGateway.Domain.Seedwork.Specification;
    using SmsGateway.Infrastructure.Crosscutting.Logging;
    using SmsGateway.Infrastructure.Crosscutting.Validator;

    /// <summary>
    /// Implementação do serviço de gerência de administradores do sistema
    /// </summary>
    public class AdministradoresAppService
        : IAdministradoresAppService
    {
        #region Members

        private readonly IAdministradorRepository _repositorioAdministradores;

        #endregion

        #region Constructors

        /// <summary>
        /// Cria uma nova instância do serviço 
        /// </summary>
        /// <param name="repositorioAdministradores">Repositório associado, injeção de dependência</param>
        public AdministradoresAppService(IAdministradorRepository repositorioAdministradores)
        {
            if (repositorioAdministradores == null)
                throw new ArgumentNullException("repositorioAdministradores");

            _repositorioAdministradores = repositorioAdministradores;

        }

        #endregion

        #region Interface Members

        public AdministradorDTO Add(AdministradorDTO administradorDto)
        {
            //validar pré-condições
            if (administradorDto == null)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);

            //Cria a partir da fábrica
            var admin = AdministradorFactory.Create(administradorDto.Nome, administradorDto.Senha,
                                                       administradorDto.Email);

            //persiste a entidade
            SalvarAdministrador(admin);

            //retorna um DTO com ID preenchido
            return admin.ProjectedAs<AdministradorDTO>();

        }

        public void Update(AdministradorDTO administradorDTO)
        {
            if (administradorDTO == null || administradorDTO.Id == Guid.Empty)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);

            //pegar item persistido
            var persisted = _repositorioAdministradores.Get(administradorDTO.Id);

            if (persisted != null)
            {
                //transforma o DTO passado em uma entidade
                var current = MaterializeAdministratorFromDto(administradorDTO);

                //Merge 
                _repositorioAdministradores.Merge(persisted, current);

                //commit 
                _repositorioAdministradores.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotAddAdminWithEmptyInformation);
        }

        public void Remove(Guid adminId)
        {
            var admin = _repositorioAdministradores.Get(adminId);

            if (admin != null)
            {
                //disable ( "logical delete" ) 
                admin.Disable();

                //commit 
                _repositorioAdministradores.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingAdmin);
        }

        public List<AdministradorDTO> Find(int pageIndex, int pageCount)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArguments);

            //get customers
            var administrators = _repositorioAdministradores.GetEnabled(pageIndex, pageCount);

            if (administrators != null
                &&
                administrators.Any())
            {
                return administrators.ProjectedAsCollection<AdministradorDTO>();
            }
            else
                return null;
        }

        public List<AdministradorDTO> ListAll()
        {

            //get administrators
            var administrators = _repositorioAdministradores.GetAll();

            if (administrators != null
                &&
                administrators.Any())
            {
                return administrators.ProjectedAsCollection<AdministradorDTO>();
            }
            else
                return null;
        }

        public AdministradorDTO Find(Guid id)
        {
            
            var admin = _repositorioAdministradores.Get(id);

            if (admin != null) 
            {
                return admin.ProjectedAs<AdministradorDTO>();
            }
            else
                return null;
        }
       
        #endregion

        #region Private Methods

        void SalvarAdministrador(Administrador admin)
        {
            //instanciar validador
            var validator = EntityValidatorFactory.CreateValidator();

            if (validator.IsValid(admin)) //validar
            {
                //adicionar ao repositorio
                _repositorioAdministradores.Add(admin);

                //commit 
                _repositorioAdministradores.UnitOfWork.Commit();
            }
            else //objeto inválido, throw validation errors
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<Administrador>(admin));
        }

        /// <summary>
        /// Materializa uma entidade a partir de  um DTO 
        /// </summary>
        /// <param name="administradorDTO"></param>
        /// <returns></returns>
        Administrador MaterializeAdministratorFromDto(AdministradorDTO administradorDTO)
        {
            
            //criar atraves da fabrica
            var current = AdministradorFactory.Create(administradorDTO.Nome,
                                                      administradorDTO.Senha,
                                                      administradorDTO.Email);

            //set identity
            current.ChangeCurrentIdentity(administradorDTO.Id);


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
            _repositorioAdministradores.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
