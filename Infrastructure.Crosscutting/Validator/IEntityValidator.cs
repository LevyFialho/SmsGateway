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
			

namespace SmsGateway.Infrastructure.Crosscutting.Validator
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contrato base para  validaçao de entidades
    /// </summary>
    public interface IEntityValidator
    {
        /// <summary>
        /// Faz a validação e retorna true se o estado da entidade for válido
        /// </summary>
        /// <typeparam name="TEntity">Tipo da entidade a validar</typeparam>
        /// <param name="item">Entidade a validar</param>
        /// <returns>Verdadeiro se válido</returns>
        bool IsValid<TEntity>(TEntity item)
            where TEntity : class;

        /// <summary>
        /// Retorna uma coleçao de erros caso o estado da entidade seja inválido
        /// <typeparam name="TEntity">Tipo da entidade</typeparam>
        /// <param name="item">Entidade com erros de validação</param>
        /// <returns></returns>
        IEnumerable<String> GetInvalidMessages<TEntity>(TEntity item)
            where TEntity : class;
    }
}
