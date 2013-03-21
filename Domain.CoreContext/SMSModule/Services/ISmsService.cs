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

using System.Collections.Generic;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg.StatusAgg;

namespace SmsGateway.Domain.CoreContext.SMSModule.Services
{
    /// <summary>
    /// Contrato basico para envio de uma mensagem SMS
    /// </summary>
    public interface ISmsService
    {
        /// <summary>
        /// Contrato do serviço usado pelos clientes. 
        /// </summary>
        /// <param name="contratoDoCliente">Contrato do cliente</param>
        /// <param name="mensagem">Mensagem a ser enviada</param>
        Mensagem EnviarMensagem(Contrato contratoDoCliente, Mensagem mensagem);

        /// <summary>
        /// Contrato do serviço usado pelos clientes. 
        /// </summary>
        /// <param name="contratoDoCliente">Contrato do cliente</param>
        /// <param name="contratosDasOperadoras">Lista de Contratos que podem ser usados para envio de mensagem</param> /// <param name="contratosDasOperadoras">Lista de Contratos que podem ser usados para envio de mensagem</param>
        /// <param name="statusList">Lista de Contratos que podem ser usados para envio de mensagem</param>
        /// <param name="mensagens">Mensagens a serem enviadas</param>
        List<Mensagem> EnviarMensagens(Contrato contratoDoCliente, List<Contrato> contratosDasOperadoras,
                                       List<Status> statusList, List<Mensagem> mensagens);

    }
}
