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
using HumanAPIClient.Model;
using HumanAPIClient.Service;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;
namespace SmsGateway.Infrastructure.Operadoras
{
    /// <summary>
    /// Implementação concreta para utilização dos serviços da Operadora  
    /// </summary>
    public class OperadoraHuman: IOperadora
    {
        private static string usuario = "wa.nobrega";
        private static string senha = "R06Ud7MeiP";
        /// <summary>
        /// Envia uma mensagem e atualiza o seu status
        /// Envia a mensagem a partir da api HumanAPIClient.dll
        /// </summary>
        /// <param name="mensagem">Mensagem a ser enviada</param>
        /// <returns>Código do status da operação</returns>
        public String EnviarMensagem(Mensagem mensagem)
        {
            try
            {
                //Autenticacao 
                var sms = new SimpleSending(usuario, senha);
                var message = new SimpleMessage();

                message.To = mensagem.NumeroDoDestinatario;
                message.Message = mensagem.TextoDescriptografado;
                message.Id = mensagem.Id.ToString().Substring(0, 19);
                if (!string.IsNullOrEmpty(mensagem.NumeroDoRemetente))
                {
                    message.From = mensagem.NumeroDoRemetente;
                }
                List<string> response = sms.send(message);//Envia a msg e recebe resposta da Human
                return response[0]; 
            }
            catch (Exception)
            {

                return null;
            }

             
           
        }

        /// <summary>
        /// Atualiza o status de uma mensagem junto a operadora, sem reenviá-la.
        /// Só deve ser usado para mensagens cujo status requer atualização
        /// </summary>
        /// <param name="mensagem">Mensagem a ser atualizada</param>
        /// <returns>Código do status</returns>
        public string AtualizarStatusDaMensagem(Mensagem mensagem)
        {
            try
            {
                if (mensagem.Status.PrecisaAtualizar)
                {
                    var sms = new SimpleSending(usuario, senha);
                    var response = sms.query(mensagem.Id.ToString());
                    return response[0];
                    int i = 0;

                }
            }
            catch (Exception)
            {
                return null;
                
            }
            return null;
            
        }
    }
}
