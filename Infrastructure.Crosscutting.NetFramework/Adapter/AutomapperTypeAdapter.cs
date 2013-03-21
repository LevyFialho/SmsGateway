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
			

namespace SmsGateway.Infrastructure.Crosscutting.NetFramework.Adapter
{
    using AutoMapper;
    using Crosscutting.Adapter;

    /// <summary>
    /// Implementação concreta de um type adapter usando o AutoMapper
    /// TypeAdapters são usados para mapear um DTO para um aggregate ou vice-versa
    /// </summary>
    public class AutomapperTypeAdapter
        :ITypeAdapter
    {
        #region ITypeAdapter Members

        /// <summary>
        /// <see cref="SmsGateway.Infrastructure.Crosscutting.Adapter.ITypeAdapter"/>
        /// </summary>
        /// <typeparam name="TSource"><see cref="SmsGateway.Infrastructure.Crosscutting.Adapter.ITypeAdapter"/></typeparam>
        /// <typeparam name="TTarget"><see cref="SmsGateway.Infrastructure.Crosscutting.Adapter.ITypeAdapter"/></typeparam>
        /// <param name="source"><see cref="SmsGateway.Infrastructure.Crosscutting.Adapter.ITypeAdapter"/></param>
        /// <returns><see cref="SmsGateway.Infrastructure.Crosscutting.Adapter.ITypeAdapter"/></returns>
        public TTarget Adapt<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class, new()
        {
            return Mapper.Map<TSource, TTarget>(source);
        }

        /// <summary>
        /// <see cref="SmsGateway.Infrastructure.Crosscutting.Adapter.ITypeAdapter"/>
        /// </summary>
        /// <typeparam name="TTarget"><see cref="SmsGateway.Infrastructure.Crosscutting.Adapter.ITypeAdapter"/></typeparam>
        /// <param name="source"><see cref="SmsGateway.Infrastructure.Crosscutting.Adapter.ITypeAdapter"/></param>
        /// <returns><see cref="SmsGateway.Infrastructure.Crosscutting.Adapter.ITypeAdapter"/></returns>
        public TTarget Adapt<TTarget>(object source) where TTarget : class, new()
        {
            return Mapper.Map<TTarget>(source);
        }

        #endregion
    }
}
