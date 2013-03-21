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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg
{
    /// <summary>
    /// Aplicação do padrão Factory.Seu propósito é encapsular o processo de criação de novos objetos
    /// A fábrica cria objetos transientes, e não está relacionada a persistência de objetos
    /// </summary>
    public static class ClienteFactory
    {
      
        /// <summary>
        /// Cria um cliente transiente
        /// </summary>
        /// <param name="nome">Nome do Cliente</param>
        /// <param name="senha">Senha do Cliente</param>
        /// <returns>Um cliente válido</returns>
        public static Cliente Create(string nome, string senha, string email)
        {
            //criar uma nova instância e setar a identidade
           
            var cliente = new Cliente {Nome =  nome, Senha =  senha, Email = email};
            cliente.Enable(); 
            cliente.GenerateNewIdentity();
         
            return cliente;
        }

        public static SolicitacaoDeCadastro Solicitacao(string nome,  string email, long telefone)
        {
            var solicitacao = new SolicitacaoDeCadastro()
                {
                    Data =  DateTime.Now,
                    Nome = nome,
                    Email = email,
                    Telefone = telefone
                };
            solicitacao.Enable(); solicitacao.GenerateNewIdentity();
            return solicitacao;
        }
    }
}
