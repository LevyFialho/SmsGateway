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
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Specification;

    /// <summary>
    /// Interface base para implementação do padrão "Repository", para mais informações
    /// sobre este pattern, acesse: 
    ///  http://blogs.msdn.com/adonet/archive/2009/06/16/using-repository-and-unit-of-work-patterns-with-entity-framework-4-0.aspx
    /// </summary>
    /// <remarks>
    /// Apesar do Entity Framework já conter uma classe base para repositórios genéricos (IDbSet),
    /// precisamos desta classe para manter o padrão "Persistence Ignorant" no domínio.
    /// </remarks> 
    /// <typeparam name="TEntity">Tipo de entidade para o repositório</typeparam>
    public interface IRepository<TEntity> : IDisposable
        where TEntity : Entity
    {
        /// <summary>
        /// Unity of Work do repositório
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Adicionar um item ao repositório
        /// </summary>
        /// <param name="item">Item a adicionar</param>
        void Add(TEntity item);

        /// <summary>
        /// Deleta um item do repositório
        /// </summary>
        /// <param name="item">Item a deletar</param>
        void Remove(TEntity item);

        /// <summary>
        /// Marca o item como modificado
        /// </summary>
        /// <param name="item">Item a modificar</param>
        void Modify(TEntity item);

        /// <summary>
        ///Atacha uma entidade ao repositório
        ///Similar ao Attach do EF e ao Update do NHibernate
        /// </summary>
        /// <param name="item">Item to attach</param>
        void TrackItem(TEntity item);

        /// <summary>
        /// Seta a entidade modificada no repositório
        /// Ao chamar o método Commit() no unity of work,
        /// estas mudanças serão efetuadas na base de dados.
        /// </summary>
        /// <param name="persisted">Item persistido</param>
        /// <param name="current">item corrente</param>
        void Merge(TEntity persisted, TEntity current);

        /// <summary>
        /// Pega um elemento através da chave
        /// </summary>
        /// <param name="id">Chave para busca</param>
        /// <returns></returns>
        TEntity Get(Guid id);

        /// <summary>
        /// Pega todos os elementos de um tipo no repositório
        /// </summary>
        /// <returns>Lista de elementos encontrados</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        ///Pega todos os elementos de um tipo de acordo 
        ///com uma query criteria specification
        /// <paramref name="specification"/>
        /// </summary>
        /// <param name="specification">Specification</param>
        /// <returns>Lista de elementos encontrados</returns>
        IEnumerable<TEntity> AllMatching(ISpecification<TEntity> specification);

        /// <summary>
        /// Pega todos os elementos de um tipo no repositório
        /// com opções de páginação e ordenação
        /// </summary>
        /// <param name="pageIndex">index da página</param>
        /// <param name="pageCount">Número de elementos por página</param>
        /// <param name="orderByExpression">Expressão de ordenação para query</param>
        /// <param name="ascending">Especifica se a ordenação é crescente</param>
        /// <returns>Lista de elementos encontrados</returns>
        IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending);

        /// <summary>
        /// Pega todos os elementos de um tipo no repositório
        /// de acordo com um filtro
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <returns>Lista de elementos encontrados</returns>
        IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter);
    }
}
