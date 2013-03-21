//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftSmsGateway.codeplex.com/license
//===================================================================================
			
namespace SmsGateway.Infrastructure.Data.Seedwork
{
    
    using System.Collections.Generic;

    /// <summary>
    /// Contrato base para suporte a 'dialect specific queries'.
    /// </summary>
    public interface ISql
    {
        /// <summary>
        /// Executa query especifica na base de persistencia subjacente
        ///  </summary>
        /// <typeparam name="TEntity">Tipo de entidade para mapear os resultados da query</typeparam>
        /// <param name="sqlQuery">
        /// Query n alinguagem especifica
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        /// </example>
        /// </param>
        /// <param name="parameters">Vetor de valores de parametros</param>
        /// <returns>
        /// Resultados: IEnumerable
        /// </returns>
        IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters);

        /// <summary>
        /// Executa um comando qualquer na base de persistência subjacente
        /// </summary>
        /// <param name="sqlCommand">
        /// Comando a executar
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        /// </example>
        ///</param>
        /// <param name="parameters">Vetor de valores de parametros</param>
        /// <returns>Número de registros afetados</returns>
        int ExecuteCommand(string sqlCommand, params object[] parameters);


    }
}
