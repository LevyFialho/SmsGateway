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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.TicketsAgg;

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
    public class TicketsAppService
        : ITicketsAppService
    {
        #region Members

        private readonly IClienteRepository _repositorioClientes;
        private readonly ITicketRepository _repositorioTickets;
        private readonly IMensagemDoTicketRepository _repositorioMensagensDoTicket;

        #endregion
        #region Constructors

        public TicketsAppService(IClienteRepository repositorioClientes, ITicketRepository repositorioTickets, IMensagemDoTicketRepository repositorioMensagensDoTicket)
        {
            if (repositorioClientes == null)
                throw new ArgumentNullException("repositorioClientes");

            _repositorioClientes = repositorioClientes;

            if (repositorioTickets == null)
                throw new ArgumentNullException("repositorioTickets");

            _repositorioTickets = repositorioTickets;

            if (repositorioMensagensDoTicket == null)
                throw new ArgumentNullException("repositorioMensagensDoTicket");

            _repositorioMensagensDoTicket = repositorioMensagensDoTicket;

        }

        #endregion

        #region Interface Members

        public TicketDTO Add(TicketDTO ticketDto)
        {
            return Update(ticketDto);

        }



        public TicketDTO Update(TicketDTO ticketDto)
        {
             //transforma o DTO passado em uma entidade
            var current = MaterializeTicketFromDto(ticketDto);
            current.Status = Enumerations.Convert(ticketDto.Status);
            var ticket = SalvarTicket(current);
            return ticket.ProjectedAs<TicketDTO>();
        }



        public List<TicketDTO> ListAll()
        {

            var tickets = _repositorioTickets.GetAll();

            if (tickets != null
                &&
                tickets.Any())
            {
                return tickets.ProjectedAsCollection<TicketDTO>();
            }
            else
                return null;
        }

        public TicketDTO Find(Guid id)
        {

            var ticket = _repositorioTickets.Get(id);

            if (ticket != null)
            {
                return ticket.ProjectedAs<TicketDTO>();
            }
            else
                return null;
        }

        public List<TicketDTO> TicketsDoCliente(Guid idCliente)
        {
            var ticket = _repositorioTickets.GetFiltered(t => t.ClienteId == idCliente);

            if (ticket != null)
            {
                return ticket.ProjectedAsCollection<TicketDTO>();
            }
            else
                return null;
        }

        #endregion

        #region Private Methods

        Ticket SalvarTicket(Ticket ticket)
        {
            //instanciar validator
            var validator = EntityValidatorFactory.CreateValidator();

            if (validator.IsValid(ticket)) //validar entidade
            {
                if (ticket.Mensagens == null) ticket.Mensagens = new List<MensagemDoTicket>();
                foreach (var msg in ticket.Mensagens)
                {
                    var persisted = _repositorioMensagensDoTicket.Get(msg.Id);
                    if (persisted == null)
                        _repositorioMensagensDoTicket.Add(msg);
                    else
                    {
                        persisted.Texto = msg.Texto;
                        if (msg.IsEnabled) persisted.Enable(); else persisted.Disable();
                        persisted.Data = msg.Data;
                    }
                }
                var ticketPersisted = _repositorioTickets.Get(ticket.Id);
                if (ticketPersisted == null)
                {
                    //adicionar ao repositorio 
                    _repositorioTickets.Add(ticket);
                }

                else
                {

                    ticketPersisted.Data = ticket.Data; 
                    ticketPersisted.Status = ticket.Status;
                    ticket.Assunto = ticket.Assunto;

                }

                //commit 
                _repositorioTickets.UnitOfWork.Commit();
                return _repositorioTickets.Get(ticket.Id);
            }
            else //entidade invalida, throw validation errors
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<Ticket>(ticket));
        }

        /// <summary>
        /// Materializa uma entidade a partir de  um DTO 
        /// </summary>
        /// <returns></returns>
        Ticket MaterializeTicketFromDto(TicketDTO dto)
        {
            //create the current instance with changes from customerDTO
            if (dto.Mensagens == null) dto.Mensagens = new List<MensagemDoTicketDTO>();
            var current = TicketFactory.Create(dto.Assunto, dto.ClienteId);

            //set identity
            current.ChangeCurrentIdentity(dto.Id);
            current.SetTheCurrentCleinteReference(dto.ClienteId);
            if (!dto.IsEnabled)
                current.Disable();
            foreach (var dtoMsg in dto.Mensagens)
            {
                var msg = TicketFactory.CreateMensagem(dtoMsg.Texto, current.Id);
                if (dtoMsg.Id != Guid.Empty)
                {
                    msg.ChangeCurrentIdentity(dtoMsg.Id);
                    msg.Data = dtoMsg.Data;
                }

                current.Mensagens.Add(msg);
            }
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
