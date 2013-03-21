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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.PacoteAgg;

namespace SmsGateway.Application.CoreContext.SMSModule.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SmsGateway.Application.CoreContext.DTO;
    using SmsGateway.Application.CoreContext.Resources;
    using SmsGateway.Application.Seedwork;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.PacoteAgg;
    using SmsGateway.Domain.Seedwork.Specification;
    using SmsGateway.Infrastructure.Crosscutting.Logging;
    using SmsGateway.Infrastructure.Crosscutting.Validator;

    /// <summary>
    /// Implementação do serviço de gerência de pacotes do sistema
    /// </summary>
    public class PacotesAppService
        : IPacotesAppService
    {
        #region Members

        private readonly IPacoteRepository _repositorioPacotes; 
        #endregion

        #region Constructors

        
        /// <summary>
        /// Cria uma nova instância do serviço 
        /// </summary>
        /// <param name="repositorio">Repositorio associado, injeção de dependência</param>
        public PacotesAppService(IPacoteRepository repositorio)
        {
            if (repositorio == null)
                throw new ArgumentNullException("repositorio");

            _repositorioPacotes = repositorio;

          
        }

        #endregion

        #region Interface Members

        public PacoteDTO Add(PacoteDTO dto)
        {
            //validar pré-condições
            if (dto == null)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);


            var pacote = PacoteFactory.NovoPacote(dto.Nome, dto.QuantidadeDeMensagens, dto.DataDeVencimento, dto.ValorCobradoPorMensagem, dto.GratuitoAoNovoCliente);


            if(_repositorioPacotes.GetFiltered(p => p.Nome  == pacote.Nome ).Any())
                throw  new Exception("Já existe um pacote com este nome");

            //persiste a entidade
            SalvarPacote(pacote);

            //retorna um DTO com ID preenchido
            return pacote.ProjectedAs<PacoteDTO>();

        }

        public void Update(PacoteDTO pacoteDto)
        {
            if (pacoteDto == null || pacoteDto.Id == Guid.Empty)
                throw new ArgumentException(Messages.warning_CannotAddAdminWithEmptyInformation);

            //pegar item persistido
            var persisted = _repositorioPacotes.Get(pacoteDto.Id);

            if (persisted != null)
            {
                //transforma o DTO passado em uma entidade
                var current = MaterializePacoteFromDto(pacoteDto);

                //Merge 
                _repositorioPacotes.Merge(persisted, current);

                //commit 
                _repositorioPacotes.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotAddAdminWithEmptyInformation);
        }

        public void Remove(Guid pacoteId)
        {
            var pacote = _repositorioPacotes.Get(pacoteId);

            if (pacote != null)
            {
                //disable ( "logical delete" ) 
                pacote.Disable();
               
                //commit 
                _repositorioPacotes.UnitOfWork.Commit(); 
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingAdmin);
        }

        public List<PacoteDTO> Find(int pageIndex, int pageCount)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArguments);

            //get pacotes
            var pacotes = _repositorioPacotes.GetEnabled(pageIndex, pageCount);

            if (pacotes != null
                &&
                pacotes.Any())
            {
                return pacotes.ProjectedAsCollection<PacoteDTO>();
            }
            else
                return null;
        }

        public List<PacoteDTO> ListAll()
        {

            //get pacotes
            var pacotes = _repositorioPacotes.GetAll();

            if (pacotes != null
                &&
                pacotes.Any())
            {
                return pacotes.ProjectedAsCollection<PacoteDTO>();
            }
            else
                return null;
        }

        public PacoteDTO Find(Guid id)
        {

            var pacote = _repositorioPacotes.Get(id);

            if (pacote != null)
            {
                return pacote.ProjectedAs<PacoteDTO>();
            }
            else
                return null;
        }

        #endregion

        #region Private Methods

        void SalvarPacote(Pacote pacote)
        {
            //instanciar validator
            var validator = EntityValidatorFactory.CreateValidator();

            if (validator.IsValid(pacote)) //validar entidade
            {
                //adicionar ao repositorio
                _repositorioPacotes.Add(pacote);

                //commit 
                _repositorioPacotes.UnitOfWork.Commit();
            }
            else //entidade invalida, throw validation errors
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<Pacote>(pacote));
        }

        /// <summary>
        /// Materializa uma entidade a partir de  um DTO 
        /// </summary>
        /// <param name="administradorDTO"></param>
        /// <returns></returns>
        Pacote MaterializePacoteFromDto(PacoteDTO dto)
        {
            //create the current instance with changes from customerDTO

            var current = PacoteFactory.NovoPacote(dto.Nome, dto.QuantidadeDeMensagens, dto.DataDeVencimento, dto.ValorCobradoPorMensagem, dto.GratuitoAoNovoCliente);

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
            _repositorioPacotes.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
