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



namespace SmsGateway.Domain.Seedwork
{
    using System;

    /// <summary>
    /// Contrato base para implementação do padrão "Unity Of Work"
    /// Para mais detalhes, acesse:
    /// http://msdn.microsoft.com/en-us/magazine/dd882510.aspx
    /// <remarks>Apesar do Entity Framework já conter um Unity of Work genérico (DbContext),
    /// precisamos desta classe para manter o padrão "Persistence Ignorant" no domínio.
    /// </remarks> 
   /// </summary>
    public interface IUnitOfWork
        :IDisposable
    {
        /// <summary>
        /// Comita todas as alterações feitas no container
        /// </summary>
        ///<remarks>
        /// Se a entidade tem propriedades fixas e algum problema de concorrência aconteça,
        /// uma exception é lançada
        ///</remarks>
        void Commit();

        /// <summary>
        /// Commit all changes made in  a container.
        /// </summary>
        ///<remarks>
        ///Se a entidade tem propriedades fixas e algum problema de concorrência aconteça,
        /// as alterações do cliente são mantidas (refreshed - client wins)
        ///</remarks>
        void CommitAndRefreshChanges();


        /// <summary>
        /// Efetua Rollback das alterações.
        /// </summary>
        void RollbackChanges();

    }
}
