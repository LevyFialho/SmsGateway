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

using System.Net.Mail;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.TicketsAgg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmsGateway.Domain.Seedwork;
    using SmsGateway.Domain.CoreContext.Resources;

    /// <summary>
    /// Raiz do Aggregate Tickets.
    /// </summary>
    public class Ticket
        : Entity, IValidatableObject
    {

        #region Members

        bool _ativo;

        #endregion

        #region Properties

        public StatusTicket Status { get; set; }

        public string Assunto { get; set; }

        public DateTime Data { get; set; }


        public virtual Guid? ClienteId { get; set; }


        public virtual Cliente Cliente { get; set; }

        public virtual ICollection<MensagemDoTicket> Mensagens { get; set; }

        /// <summary>
        /// True se o cliente está ativo
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return _ativo;
            }
            private set
            {
                _ativo = value;
            }
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Desativa o cliente
        /// </summary>
        public void Disable()
        {
            if (IsEnabled)
                this._ativo = false;
        }

        /// <summary>
        /// Ativa o cliente
        /// </summary>
        public void Enable()
        {
            if (!IsEnabled)
                this._ativo = true;
        }

        /// <summary>
        /// Associa um cliente  
        /// </summary>
        /// <param name="cliente"></param>
        public void SetarCliente(Cliente cliente)
        {
            if (cliente == null
                ||
                cliente.IsTransient() || !cliente.IsEnabled)
            {
                throw new ArgumentException(Messages.exception_SetarContratoAtual);
            }

            //fix relation
            this.ClienteId = cliente.Id;

            this.Cliente = cliente;
        }

        /// <summary>
        /// Setar a referência do  cliente
        /// </summary>
        /// <param name="contractId"></param>
        public void SetTheCurrentCleinteReference(Guid clienteId)
        {
            if (clienteId != Guid.Empty)
            {
                //fix relation
                this.ClienteId = clienteId;

                this.Cliente = null;
            }
        }

        #endregion

        #region IValidatableObject Members


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();


            if (String.IsNullOrWhiteSpace(this.Assunto))
            {
                validationResults.Add(new ValidationResult(Messages.validation_NomeCliente,
                                                           new string[] { "Assunto" }));
            }


            if (Data == DateTime.MinValue)
            {
                validationResults.Add(new ValidationResult(Messages.validation_SenhaCliente,
                                                           new string[] { "Data" }));
            }

            //-->Check Email
            if (ClienteId == Guid.Empty)
            {
                validationResults.Add(new ValidationResult(Messages.validation_EmailAdministrador,
                                                           new string[] { "ClienteId" }));
            }
            return validationResults;
        }

        #endregion
    }

    public class MensagemDoTicket
       : Entity, IValidatableObject
    {

        #region Members

        bool _ativo;

        #endregion

        #region Properties

        
        public string Texto { get; set; }

        public DateTime Data { get; set; }


        public virtual Guid? TicketId { get; set; }


        public virtual Ticket Ticket { get; set; }


        /// <summary>
        /// True se o cliente está ativo
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return _ativo;
            }
            private set
            {
                _ativo = value;
            }
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Desativa o cliente
        /// </summary>
        public void Disable()
        {
            if (IsEnabled)
                this._ativo = false;
        }

        /// <summary>
        /// Ativa o cliente
        /// </summary>
        public void Enable()
        {
            if (!IsEnabled)
                this._ativo = true;
        }

        /// <summary>
        /// Associa um ticket  
        /// </summary>
        /// <param name="ticket"></param>
        public void SetarTicket(Ticket ticket)
        {
            if (ticket == null
                ||
                ticket.IsTransient() || !ticket.IsEnabled)
            {
                throw new ArgumentException(Messages.exception_SetarContratoAtual);
            }

            //fix relation
            this.TicketId = ticket.Id;

            this.Ticket = ticket;
        }

        /// <summary>
        /// Setar a referência 
        /// </summary>
        /// <param name="ticketId"></param>
        public void SetTheCurrentTicketReference(Guid ticketId)
        {
            if (ticketId != Guid.Empty)
            {
                //fix relation
                this.TicketId = ticketId;

                this.Ticket = null;
            }
        }

        #endregion

        #region IValidatableObject Members


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();


            if (String.IsNullOrWhiteSpace(this.Texto))
            {
                validationResults.Add(new ValidationResult(Messages.validation_NomeCliente,
                                                           new string[] { "Texto" }));
            }


            if (Data == DateTime.MinValue)
            {
                validationResults.Add(new ValidationResult(Messages.validation_SenhaCliente,
                                                           new string[] { "Data" }));
            }

            
            if (TicketId == Guid.Empty)
            {
                validationResults.Add(new ValidationResult(Messages.validation_EmailAdministrador,
                                                           new string[] { "ClienteId" }));
            }
            return validationResults;
        }

        #endregion
    }


    public enum StatusTicket
    {
        Pendente,
        Resolvido
    }
}
