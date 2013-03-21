//===================================================================================
// SMS GATEWAY
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


namespace SmsGateway.Application.Seedwork
{
    using System.Collections.Generic;
    using SmsGateway.Domain.Seedwork;
    using SmsGateway.Infrastructure.Crosscutting.Adapter;

    /// <summary>
    /// Usada para criar projeções de entidades (transformar de Entity para DTO).
    /// </summary>
    public static class ProjectionsExtensionMethods
    {
        /// <summary>
        /// Projeta um tipo usando um DTO
        /// </summary>
        /// <typeparam name="TProjection">O tipo de projeção do DTO</typeparam>
        /// <param name="entity">Entidade a projetar</param>
        /// <returns>O tipo projetado</returns>
        public static TProjection ProjectedAs<TProjection>(this Entity item)
            where TProjection : class,new()
        {
            var adapter = TypeAdapterFactory.CreateAdapter();
            return adapter.Adapt<TProjection>(item);
        }

        /// <summary>
        /// Retorna uma coleção de projeções de itens a partir de um tipo de DTO
        /// </summary>
        /// <typeparam name="TProjection">O tipo de projeção do DTO</typeparam>
        /// <param name="items">A coleção de entidades a projetar</param>
        /// <returns>Projected collection</returns>
        public static List<TProjection> ProjectedAsCollection<TProjection>(this IEnumerable<Entity> items)
            where TProjection : class,new()
        {
            var adapter = TypeAdapterFactory.CreateAdapter();
            return adapter.Adapt<List<TProjection>>(items);
        }
    }
}
