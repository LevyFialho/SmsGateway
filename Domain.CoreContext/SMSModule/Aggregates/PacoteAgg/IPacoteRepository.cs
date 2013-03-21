using System.Collections.Generic;
using SmsGateway.Domain.Seedwork;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.PacoteAgg
{
    /// <summary>
    /// Aplicação deo padrão Repository
    /// </summary>
    public interface IPacoteRepository
     : IRepository<Pacote>
    {
        IEnumerable<Pacote> GetEnabled(int pageIndex, int pageCount);

    }
}
