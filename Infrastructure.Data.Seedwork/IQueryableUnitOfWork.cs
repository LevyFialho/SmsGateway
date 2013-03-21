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


namespace SmsGateway.Infrastructure.Data.Seedwork
{
    using System.Data.Entity;
    using SmsGateway.Domain.Seedwork;

    /// <summary>
    /// Contrato para implementação do padrão unity of Work a partir do Entity Framework
    /// <remarks>
    /// Essa interface extende IUnitOfWork  para uso com o Entity framework 
    /// Isto permite que o padrão 'Persistence ignorant domain' seja adotado
    /// </remarks>
    /// </summary>
    public interface IQueryableUnitOfWork
        :IUnitOfWork,ISql
    {
        
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

        
        void Attach<TEntity>(TEntity item) where TEntity : class;

      
        void SetModified<TEntity>(TEntity item) where TEntity : class;

        
        void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class;

    }
}
