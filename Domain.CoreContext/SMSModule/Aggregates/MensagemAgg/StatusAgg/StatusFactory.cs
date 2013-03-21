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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg.StatusAgg
{
    /// <summary>
    /// Aplicação do padrão Factory.Seu propósito é encapsular o processo de criação de novos objetos
    /// A fábrica cria objetos transientes, e não está relacionada a persistência de objetos
    /// </summary>
    public static class StatusFactory
    {
      
        /// <summary>
        /// Cria um Status
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="descricao"></param>
        /// <param name="mensagemCliente"></param>
        /// <param name="debitoCliente"></param>
        /// <param name="debitoOperadora"></param>
        /// <param name="valorOperacao"></param>
        /// <param name="operadoraApi"></param>
        /// <returns></returns>
        public static Status Create(string codigo, string descricao, string mensagemCliente, 
            int debitoCliente,int debitoOperadora,double valorOperacao,OperadoraApi operadoraApi)
        {
            var status = new Status {
                Codigo = codigo,
                Descricao = descricao, 
                MensagemAoCliente = mensagemCliente, 
                OperadoraApi = operadoraApi};

            status.SetQuantoDebitarDoContratoDoCliente(debitoCliente);
            status.SetQuantoDebitarDoContratoDaOperadora(debitoOperadora);
            status.SetValordaOperacao(valorOperacao);
            status.Enable();
            status.GenerateNewIdentity();
            return status;
        }
    }
}
