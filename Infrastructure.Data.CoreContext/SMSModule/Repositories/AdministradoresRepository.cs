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

using System.Collections.Generic;
using System.Linq;

namespace SmsGateway.Infrastructure.Data.CoreContext.SMSModule.Repositories
{
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.AdministradorAgg;
    using SmsGateway.Infrastructure.Data.Seedwork;    
    using SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork;
    /// <summary>
    /// Implementação do repositório de Administradores
    /// </summary>
    public class AdministradoresRepository
        :Repository<Administrador>,IAdministradorRepository
    {
        #region Constructor

        /// <summary>
        /// Cria uma nova instância
        /// </summary>
        /// <param name="unitOfWork">Unidade de trabalho associada</param>
        public AdministradoresRepository(CoreContextUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion

        #region IAdministradorRepository Members

        public IEnumerable<Administrador> GetEnabled(int pageIndex, int pageCount)
        {
            var currentUnitOfWork = this.UnitOfWork as CoreContextUnitOfWork;

            return currentUnitOfWork.Administradores
                                     .Where(c => c.IsEnabled == true)
                                     //.OrderBy(c => )
                                     .Skip(pageIndex * pageCount)
                                     .Take(pageCount);
        }


        #endregion
    }
}
