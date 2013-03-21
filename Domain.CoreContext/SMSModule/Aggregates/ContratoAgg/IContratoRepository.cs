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
using SmsGateway.Domain.Seedwork;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg
{
    /// <summary>
    /// repository contract
    /// <see cref="SmsGateway.Domain.Seedwork.IRepository{Customer}"/>
    /// </summary>
    public interface IContratoRepository
        :IRepository<Contrato>
    {
        IEnumerable<Contrato> GetEnabled(int pageIndex, int pageCount);

        List<Contrato> GetContratosDeOperadorasAtivos();
      
    }
}
