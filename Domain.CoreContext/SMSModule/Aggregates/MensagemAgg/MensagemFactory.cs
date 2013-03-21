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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg.StatusAgg;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg
{
    /// <summary>
    /// Aplicação do padrão Factory.Seu propósito é encapsular o processo de criação de novos objetos
    /// A fábrica cria objetos transientes, e não está relacionada a persistência de objetos
    /// </summary>
    public static class MensagemFactory
    {
      
        /// <summary>
        /// Cria uma nova mensagem associada ao contrato de um determinado cliente.
        /// </summary>
        /// <param name="contratoCliente"></param>
        /// <param name="textoMensagem"></param>
        /// <param name="numeroDestinatario"></param>
        /// <param name="numeroRemetente"></param>
        /// <returns></returns>
        public static Mensagem Create(Contrato contratoCliente, string textoMensagem, string numeroDestinatario,
            string numeroRemetente, Status status)
        {
            //criar uma nova instância e setar a identidade
            var msg = new Mensagem
                {
                    TextoDaMensagem = textoMensagem,
                    NumeroDoDestinatario = numeroDestinatario,
                    NumeroDoRemetente = numeroRemetente
                };
            msg.SetarContratoDoCliente(contratoCliente);
            msg.DataDeRegistro = DateTime.Now;
            msg.DataDoUltimoUpdate = DateTime.Now;
            msg.SetarStatus(status);
            msg.Enable();
            //Set status

            msg.GenerateNewIdentity();

            return msg;
        }
    }
}
