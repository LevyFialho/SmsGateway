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

namespace SmsGateway.Infrastructure.Crosscutting.Adapter
{
    /// <summary>
    /// Contrato base para de um Type Adapter, que mapeia um DTO para um aggregate ou vice-versa
    /// <remarks>
    /// Este contrato funciona com mappers "automaticos" ou adhoc ( automapper,emitmapper,valueinjecter...)
    /// </remarks>
    /// </summary>
    public interface ITypeAdapter
    {
        /// <summary>
        /// Adapta um objeto fonte a uma uma instancia do tipo  <paramref name="TTarget"/>
        /// </summary>
        /// <typeparam name="TSource">Tipo do objeto fonte</typeparam>
        /// <typeparam name="TTarget">Tipo do objeto alvo</typeparam>
        /// <param name="source">Instância a ser adaptada</param>
        /// <returns><paramref name="source"/> instancia adaptada para <typeparamref name="TTarget"/></returns>
        TTarget Adapt<TSource, TTarget>(TSource source)
            where TTarget : class,new()
            where TSource : class;


        /// <summary>
        /// Adapta um objeto fonte a uma uma instancia do tipo  <paramref name="TTarget"/>
        /// </summary>
        /// <typeparam name="TTarget">Tipo do objeto alvo</typeparam>
        /// <param name="source">Instância a ser adaptada</param>
        /// <returns><paramref name="source"/> instancia adaptada para <typeparamref name="TTarget"/></returns>
        TTarget Adapt<TTarget>(object source)
            where TTarget : class,new();
    }
}
