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

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContatoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg.StatusAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.TicketsAgg;
using SmsGateway.Infrastructure.Data.Seedwork;    
using SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork;

namespace SmsGateway.Infrastructure.Data.CoreContext.SMSModule.Repositories
{
    
    /// <summary>
    /// Implementação do repositório  
    /// </summary>
    public class TicketsRepository
        :Repository<Ticket>,ITicketRepository
    {
        #region Constructor

        /// <summary>
        /// Cria uma nova instância
        /// </summary>
        /// <param name="unitOfWork">Unidade de trabalho associada</param>
        public TicketsRepository(CoreContextUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion

        #region IRepository Members

        public override Ticket Get(Guid id)
        {
            var currentUnitOfWork = this.UnitOfWork as CoreContextUnitOfWork;
            DbSet<Ticket> set = currentUnitOfWork.Set<Ticket>();
            return currentUnitOfWork.Tickets.Include(c => c.Mensagens).Include(c => c.Cliente).FirstOrDefault(l => l.Id == id);
        }

        public override IEnumerable<Ticket> GetAll()
        {
            var currentUnitOfWork = this.UnitOfWork as CoreContextUnitOfWork;
            DbSet<Ticket> set = currentUnitOfWork.Set<Ticket>();
            return currentUnitOfWork.Tickets.Include(c => c.Mensagens).Include(c => c.Cliente);
        }

        public override IEnumerable<Ticket> GetFiltered(System.Linq.Expressions.Expression<System.Func<Ticket, bool>> filter)
        {
            var currentUnitOfWork = this.UnitOfWork as CoreContextUnitOfWork;
            DbSet<Ticket> set = currentUnitOfWork.Set<Ticket>();
            return currentUnitOfWork.Tickets.Include(c => c.Mensagens).Include(c => c.Cliente).Where(filter);
        }
         



        #endregion

        
    }


    /// <summary>
    /// Implementação do repositório  
    /// </summary>
    public class MensagemDoTicketRepository
        : Repository<MensagemDoTicket>, IMensagemDoTicketRepository
    {
        #region Constructor

        /// <summary>
        /// Cria uma nova instância
        /// </summary>
        /// <param name="unitOfWork">Unidade de trabalho associada</param>
        public MensagemDoTicketRepository(CoreContextUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion

        #region IStatusRepository Members




        #endregion


    }
}
