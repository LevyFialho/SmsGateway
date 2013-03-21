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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContatoAgg
{
    /// <summary>
    /// Aplicação do padrão Factory.Seu propósito é encapsular o processo de criação de novos objetos
    /// A fábrica cria objetos transientes, e não está relacionada a persistência de objetos
    /// </summary>
    public static class ContatoFactory
    {
      
        /// <summary>
        /// Cria um Contato transiente
        /// </summary>
        /// <param name="nome">Nome do Contato</param>
        /// <param name="senha">Senha do Contato</param>
        /// <returns>Um Contato válido</returns>
        public static Contato Create(string nome, long numero, Guid clienteId)
        {
            //criar uma nova instância e setar a identidade
            var contato = new Contato {Nome =  nome,  Numero = numero};
            contato.SetTheCurrentClientReference(clienteId);
            contato.Enable(); 
            contato.GenerateNewIdentity();
         
            return contato;
        }
    }
}
