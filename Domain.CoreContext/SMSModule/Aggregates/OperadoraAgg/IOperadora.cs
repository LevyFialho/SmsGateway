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

using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg
{
    /// <summary>
    /// Define o contrato base para operadoras do sistema.
    /// </summary>
    public interface IOperadora
    {

        /// <summary>
        /// Toda operadora deve ser capaz de enviar uma mensagem
        /// </summary>
        /// <param name="mensagem">Mensagem a ser enviada</param>
        /// <returns>Retorna o código de status da operação</returns>
        string EnviarMensagem(Mensagem mensagem);

        /// <summary>
        /// Toda operadora deve ser capaz de atualizar o status de envio para uma mensagem.
        /// </summary>
        /// <param name="mensagem">Mensagem a ser consultada</param>
        /// <returns>Retorna o código do status atualizado</returns>
        string AtualizarStatusDaMensagem(Mensagem mensagem);
    }
    /// <summary>
    /// Define os tipos de APIs que o sistema disponibiliza
    /// Cada API deve ter sua própia implementação concreta de IOperadora
    /// </summary>
    public enum OperadoraApi
    {
        Null,
        HumanSms,
        Comtele,
        Tww
    }
}
