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
    public class SolicitacaoDeCadastroRepository
        : Repository<SolicitacaoDeCadastro>, ISolicitacaoDeCadastroRepository
    {
        #region Constructor

        /// <summary>
        /// Cria uma nova instância
        /// </summary>
        /// <param name="unitOfWork">Unidade de trabalho associada</param>
        public SolicitacaoDeCadastroRepository(CoreContextUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion

     
       
    }

    
}
