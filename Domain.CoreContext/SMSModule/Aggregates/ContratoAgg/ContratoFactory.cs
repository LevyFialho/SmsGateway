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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg
{
    /// <summary>
    /// Aplicação do padrão Factory.Seu propósito é encapsular o processo de criação de novos objetos
    /// A fábrica cria objetos transientes, e não está relacionada a persistência de objetos
    /// </summary>
    public static class ContratoFactory
    {

        /// <summary>
        /// Cria um novo contrato para um cliente no sistema
        /// </summary>
        /// <param name="dataInicial">Data em que o contrato entra em vigor</param>
        /// <param name="dataFinal">Data de expiração do contrato</param>
        /// <param name="saldoMensagens">Quantidade total de mensagens que podem ser enviadas através do contrato</param>
        /// <param name="valorMensagem">Valor cobrado por mensagem enviada através do contrato</param>
        /// <param name="cliente">Cliente ao qual o contrato está associado</param>
        /// <returns></returns>
        public static Contrato CriarContratoComCliente(DateTime dataInicial, DateTime dataFinal, int saldoMensagens,
                                      double valorMensagem, Cliente cliente)
        {
            //create new instance and set identity
            var contrato = new Contrato { DataInicial = dataInicial, DataFinal = dataFinal, Tipo = TipoDeContrato.Cliente, OperadoraApi = OperadoraApi.Null };
            contrato.SetarSaldoDeMensagens(saldoMensagens);
            contrato.SetarValorCobradoPorMensagem(valorMensagem);
            contrato.SetarCliente(cliente);
            contrato.Enable();
            contrato.GenerateNewIdentity();
            cliente.SetarContratoAtual(contrato);
            return contrato;

        }

        /// <summary>
        /// Cria um novo contrato para uma operadora no sistema
        /// </summary>
        /// <param name="dataInicial">Data em que o contrato entra em vigor</param>
        /// <param name="dataFinal">Data de expiração do contrato</param>
        /// <param name="saldoMensagens">Quantidade total de mensagens que podem ser enviadas através do contrato</param>
        /// <param name="valorMensagem">Valor cobrado por mensagem enviada através do contrato</param>
        /// <param name="operadoraApi">Tipo de api que o contrato utiliza para envio de mensagens</param>
        /// <returns></returns>
        public static Contrato CriarContratoComOperadora(DateTime dataInicial, DateTime dataFinal, int saldoMensagens,
                                      double valorMensagem, OperadoraApi operadoraApi)
        {
            //create new instance and set identity
            var contrato = new Contrato { DataInicial = dataInicial, DataFinal = dataFinal, Tipo = TipoDeContrato.Operadora, OperadoraApi = operadoraApi };
            contrato.SetarSaldoDeMensagens(saldoMensagens);
            contrato.AlterarValorCobradoPorMensagem(valorMensagem);
            contrato.Enable();
            contrato.GenerateNewIdentity();
            return contrato;

        }
    }
}