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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContatoAgg
{
    /// <summary>
    /// Aplicação do padrão Factory.Seu propósito é encapsular o processo de criação de novos objetos
    /// A fábrica cria objetos transientes, e não está relacionada a persistência de objetos
    /// </summary>
    public static class ListaDeContatosFactory
    {
      
        /// <summary>
        /// Cria uma Lista de contatos transiente
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="clienteId">Cliente relacionado</param>
        /// <returns>Um Contato válido</returns>
        public static ListaDeContatos ListaDeContatos(string nome, Guid clienteId)
        {
            //criar uma nova instância e setar a identidade
            var lista = new ListaDeContatos {Nome =  nome, };
            lista.SetTheCurrentClientReference(clienteId);
            lista.Enable();
            lista.GenerateNewIdentity();

            return lista;
        }


        /// <summary>
        /// Cria uma Lista de contatos transiente
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="clienteId">Cliente relacionado</param>
        /// <returns>Um Contato válido</returns>
        public static ListaDeContatos ListaDeContatos(string nome, Guid clienteId, IEnumerable<Contato> contatos)
        {
            //criar uma nova instância e setar a identidade
            var lista = new ListaDeContatos { Nome = nome, };
            lista.SetTheCurrentClientReference(clienteId);
            lista.Enable();
            lista.GenerateNewIdentity();
            foreach (var contato in contatos)
            {
                lista.Contatos.Add(contato);
            }
            return lista;
        }

         
    }
}
