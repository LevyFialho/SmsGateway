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
using SmsGateway.Infrastructure.Data.Seedwork;
using SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork;

namespace SmsGateway.Infrastructure.Data.CoreContext.SMSModule.Repositories
{


    /// <summary>
    /// Implementação do repositório de Administradores
    /// </summary>
    public class ClientesRepository
        : Repository<Cliente>, IClienteRepository
    {
        #region Constructor

        /// <summary>
        /// Cria uma nova instância
        /// </summary>
        /// <param name="unitOfWork">Unidade de trabalho associada</param>
        public ClientesRepository(CoreContextUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion

        #region IClienteRepository Members

        public IEnumerable<Cliente> GetEnabled(int pageIndex, int pageCount)
        {
            var currentUnitOfWork = this.UnitOfWork as CoreContextUnitOfWork;

            return currentUnitOfWork.Clientes
                .Where(c => c.IsEnabled == true)
                //.OrderBy(c => )
                .Skip(pageIndex*pageCount)
                .Take(pageCount);
        }


        #endregion

        #region Override Members

        public override Cliente Get(Guid id)
        {
            if (id != Guid.Empty)
            {
                var currentUnitOfWork = this.UnitOfWork as CoreContextUnitOfWork;

                DbSet<Cliente> set = currentUnitOfWork.Set<Cliente>();

                return set.Include(c => c.ContratoAtual).SingleOrDefault(c => c.Id == id);
            }
            else
                return null;


        }

        #endregion
    }

    
}
