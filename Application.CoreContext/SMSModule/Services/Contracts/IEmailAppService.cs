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
