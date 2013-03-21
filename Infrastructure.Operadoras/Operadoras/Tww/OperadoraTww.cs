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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;

namespace SmsGateway.Infrastructure.Operadoras
{
    /// <summary>
    /// Implementação concreta para utilização dos serviços da Operadora  
    /// </summary>
    public class OperadoraTww: IOperadora
    {
        private static string _usuario = "MeuLogin";
        private static string _senha = "123";
        /// <summary>
        /// Envia uma mensagem e atualiza o seu status
        /// Envia mensagem através do web service http://webservices.twwwireless.com.br/reluzcap/wsreluzcap.asmx
        /// </summary>
        /// <param name="mensagem">Mensagem a ser enviada</param>
        /// <returns>Retorna o código de status</returns>
        public string EnviarMensagem(Mensagem mensagem)
        {
            var service = new OperadoraTwwServiceReference.ReluzCapWebServiceSoapClient();
            var resposta = service.EnviaSMS(_usuario, _senha, mensagem.NumeroDoRemetente, mensagem.NumeroDoDestinatario, mensagem.TextoDescriptografado);
            return resposta;
        }

        /// <summary>
        /// Atualiza o status de uma mensagem junto a operadora, sem enviá-la.
        /// Só deve ser usado para mensagens cujo status requer atualização
        /// </summary>
        /// <param name="mensagem">Mensagem a ser enviada</param>
        /// <returns>Retorna o código de status</returns>
        public string AtualizarStatusDaMensagem(Mensagem mensagem)
        {
            if (mensagem.Status.PrecisaAtualizar)
            {
                throw new NotImplementedException();
            }
            else
                return mensagem.Status.Codigo;
        }
    }
}
