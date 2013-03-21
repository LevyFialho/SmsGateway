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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;
using SmsGateway.Infrastructure.Data.Seedwork;    
using SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork;

namespace SmsGateway.Infrastructure.Data.CoreContext.SMSModule.Repositories
{
    
    /// <summary>
    /// Implementação do repositório de Administradores
    /// </summary>
    public class MensagensRepository
        :Repository<Mensagem>,IMensagemRepository
    {
        #region Constructor

        /// <summary>
        /// Cria uma nova instância
        /// </summary>
        /// <param name="unitOfWork">Unidade de trabalho associada</param>
        public MensagensRepository(CoreContextUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion

        #region IMensagemRepository Members

        public IEnumerable<Mensagem> GetEnabled(int pageIndex, int pageCount)
        {
            var currentUnitOfWork = this.UnitOfWork as CoreContextUnitOfWork;

            return currentUnitOfWork.Mensagens
                                     .Where(c => c.IsEnabled == true)
                                     //.OrderBy(c => )
                                     .Skip(pageIndex * pageCount)
                                     .Take(pageCount);
        }


        #endregion

        #region Override Members

        public override Mensagem Get(Guid id)
        {
            if (id != Guid.Empty)
            {
                var currentUnitOfWork = this.UnitOfWork as CoreContextUnitOfWork;

                DbSet<Mensagem> set = currentUnitOfWork.CreateSet<Mensagem>();

                return set.Include(c => c.Status).Include(c => c.ContratoDoCliente).Include(c => c.ContratoDaOperadora).SingleOrDefault(c => c.Id == id);
            }
            else
                return null;


        }

        public override IEnumerable<Mensagem> GetAll()
        {
            var currentUnitOfWork = this.UnitOfWork as CoreContextUnitOfWork;

            DbSet<Mensagem> set = currentUnitOfWork.CreateSet<Mensagem>();

            return set.Include(c => c.Status).Include(c => c.ContratoDoCliente).Include(c => c.ContratoDaOperadora);
           
        }

        public override IEnumerable<Mensagem> GetFiltered(System.Linq.Expressions.Expression<Func<Mensagem,bool>> filter)
        {
            var currentUnitOfWork = this.UnitOfWork as CoreContextUnitOfWork;

            DbSet<Mensagem> set = currentUnitOfWork.CreateSet<Mensagem>();

            return set.Include(c => c.Status).Include(c => c.ContratoDoCliente).Include(c => c.ContratoDaOperadora).Where(filter);

        }

        public override IEnumerable<Mensagem> AllMatching(Domain.Seedwork.Specification.ISpecification<Mensagem> specification)
        {
            var currentUnitOfWork = this.UnitOfWork as CoreContextUnitOfWork;

            DbSet<Mensagem> set = currentUnitOfWork.CreateSet<Mensagem>();

            return set.Include(c => c.Status).Include(c => c.ContratoDoCliente).Include(c => c.ContratoDaOperadora).Where(specification.SatisfiedBy());

        }
        #endregion
    }
}
