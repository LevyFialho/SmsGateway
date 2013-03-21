using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmsGateway.Application.CoreContext.SMSModule.Services.Contracts;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.AdministradorAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Services;

namespace SmsGateway.Application.CoreContext.SMSModule.Services
{
    public  class EmailsAppService: IEmailAppService
    {

         #region Members

        private readonly IAdministradorRepository _repositorioAdministradores;
        private readonly IEmailService _service;
        #endregion

        #region Constructors

        
        /// <summary>
        /// Cria uma nova instância do serviço 
        /// </summary>
        /// <param name="service">Serviço associado, injeção de dependência</param>
        public EmailsAppService(IEmailService service, IAdministradorRepository repositorioAdministradores)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            _service = service;

            if (repositorioAdministradores == null)
                throw new ArgumentNullException("repositorioAdministradores");

            _repositorioAdministradores = repositorioAdministradores;
        }

        #endregion
        public void SendEmail(MailMessage message)
        {
            _service.SendEmail(message);
        }

        public void NovaSolicitacaoDeCadastro(SolicitacaoDeCadastro solicitacao)
        {
            var message = new MailMessage();
            message.Body = "Nova solicitação de cadastro no SMSAGILE.COM </br> Nome: " + solicitacao.Nome +
                "<br />Telefone: " + solicitacao.Telefone + " </br>Email: " + solicitacao.Email + " </br>";
            foreach (var admin in _repositorioAdministradores.GetFiltered(a => a.IsEnabled))
            {
                message.To.Add(new MailAddress(admin.Email));
           
            }
            SendEmail(message);
        }

        public void RecuperarSenha(string nome, string email, string senha)
        {
            var message = new MailMessage();
            message.Body = " Olá "+ nome +" Você fez um pedido de Recuperação de Senha do portal SMSAGILE.COM  </br> Sua senha é: " + senha;
            message.To.Add(new MailAddress(email));
            SendEmail(message);
        }
  
    }
}
