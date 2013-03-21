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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Implementa o padrão Factory
    /// Factory responsável por criar TypeAdapters
    /// TypeAdapters são usados para mapear um DTO para um aggregate ou vice-versa
    /// </summary>
    public static class TypeAdapterFactory
    {
        #region Members

        static ITypeAdapterFactory _currentTypeAdapterFactory = null;

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Seta a fábrica de adapters corrente
        /// </summary>
        /// <param name="adapterFactory"></param>
        public static void SetCurrent(ITypeAdapterFactory adapterFactory)
        {
            _currentTypeAdapterFactory = adapterFactory;
        }
        /// <summary>
        /// Cria um novo TypeAdapter através da factory
        /// </summary>
        /// <returns></returns>
        public static ITypeAdapter CreateAdapter()
        {
            return _currentTypeAdapterFactory.Create();
        }
        #endregion
    }
}
