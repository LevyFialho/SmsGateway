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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContatoAgg;

namespace SmsGateway.Infrastructure.Data.CoreContext.SMSModule.Repositories
{
    
    using SmsGateway.Infrastructure.Data.Seedwork;
    using SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork;
    /// <summary>
    /// Implementação do repositório de Administradores
    /// </summary>
    public class ListasDeContatosRepository
        : Repository<ListaDeContatos>, IListaDeContatosRepository
    {
        #region Constructor

        /// <summary>
        /// Cria uma nova instância
        /// </summary>
        /// <param name="unitOfWork">Unidade de trabalho associada</param>
        public ListasDeContatosRepository(CoreContextUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion

        #region IAdministradorRepository Members

        public override ListaDeContatos Get(Guid id)
        {
            var currentUnitOfWork = this.UnitOfWork as CoreContextUnitOfWork;
            DbSet<ListaDeContatos> set = currentUnitOfWork.Set<ListaDeContatos>();
            return currentUnitOfWork.ListasDeContatos.Include(c => c.Contatos).FirstOrDefault(l => l.Id == id);
        }

        public override IEnumerable<ListaDeContatos> GetAll()
        {
            var currentUnitOfWork = this.UnitOfWork as CoreContextUnitOfWork;
            DbSet<ListaDeContatos> set = currentUnitOfWork.Set<ListaDeContatos>();
            return currentUnitOfWork.ListasDeContatos.Include(c => c.Contatos);
        }

        public override IEnumerable<ListaDeContatos> GetFiltered(System.Linq.Expressions.Expression<System.Func<ListaDeContatos, bool>> filter)
        {
            var currentUnitOfWork = this.UnitOfWork as CoreContextUnitOfWork;
            DbSet<ListaDeContatos> set = currentUnitOfWork.Set<ListaDeContatos>();
            return currentUnitOfWork.ListasDeContatos.Include(c => c.Contatos).Where(filter) ;
        }
        public IEnumerable<ListaDeContatos> GetEnabled(int pageIndex, int pageCount)
        {
            var currentUnitOfWork = this.UnitOfWork as CoreContextUnitOfWork;

            return currentUnitOfWork.ListasDeContatos
                                     .Where(c => c.IsEnabled == true)
                                     
                                     .Skip(pageIndex * pageCount)
                                     .Take(pageCount);
        }


        #endregion

        
    }
}
