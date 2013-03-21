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


namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.AdministradorAgg
{

    using System.Collections.Generic;
    using Seedwork;

    /// <summary>
    /// Contrato do repositório de Administradores 
    /// <see cref="SmsGateway.Domain.Seedwork.IRepository{Cliente}"/>
    /// </summary>
    public interface IAdministradorRepository
        :IRepository<Administrador>
    {
        IEnumerable<Administrador> GetEnabled(int pageIndex, int pageCount);

    }
}
