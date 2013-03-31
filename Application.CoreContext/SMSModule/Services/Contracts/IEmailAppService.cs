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
// nos livros Domain driven Design (E. Evans) e 
//Patterns of Application Architechture (M. Fowler) na plataforma .Net
//===================================================================================

using System.Net.Mail;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;

namespace SmsGateway.Application.CoreContext.SMSModule.Services.Contracts
{
    public interface IEmailAppService
    {
        void SendEmail(MailMessage message);
        void NovaSolicitacaoDeCadastro(SolicitacaoDeCadastro solicitacao);
        void RecuperarSenha(string nome, string email, string senha); 
  
    }
}
