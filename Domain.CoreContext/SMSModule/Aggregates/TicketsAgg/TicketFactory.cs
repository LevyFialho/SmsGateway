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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.TicketsAgg
{
    /// <summary>
    /// Aplicação do padrão Factory.Seu propósito é encapsular o processo de criação de novos objetos
    /// A fábrica cria objetos transientes, e não está relacionada a persistência de objetos
    /// </summary>
    public static class TicketFactory
    {
       
        public static Ticket Create(string assunto, Guid clienteId)
        {
            //criar uma nova instância e setar a identidade
           
            var ticket = new Ticket() {Assunto = assunto, Data =  DateTime.Now, Mensagens = new List<MensagemDoTicket>()};
            ticket.Enable(); 
            ticket.GenerateNewIdentity();
            ticket.SetTheCurrentCleinteReference(clienteId);
            ticket.Status = StatusTicket.Pendente;
            return ticket;
        }

        public static MensagemDoTicket CreateMensagem(string texto, Guid ticketId)
        {
            //criar uma nova instância e setar a identidade

            var msg = new MensagemDoTicket() { Texto = texto, Data =  DateTime.Now};
            msg.Enable();
            msg.GenerateNewIdentity();
            msg.SetTheCurrentTicketReference(ticketId);
            
            return msg;
        }
    }
}
