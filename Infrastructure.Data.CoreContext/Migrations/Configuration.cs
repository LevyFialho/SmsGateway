using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.AdministradorAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg.StatusAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;
using SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork;

namespace SmsGateway.Infrastructure.Data.CoreContext.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork.CoreContextUnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CoreContextUnitOfWork context)
        {
            //Sempre que este método for alterado, todos os registros serão adicionados ao banco de dados



            //Preencher o banco com dados fixos

            #region Criar Administrador Geral
            if (!context.Administradores.Any())
            {
                var admin = AdministradorFactory.Create("Levy Fialho", "@infnet", "levy.fialho@gmail.com");
                var admin2 = AdministradorFactory.Create("Jair Martins", "@infnet", "jcam5110@gmail.com");
                var admin3 = AdministradorFactory.Create("Marrcelo Sá", "@infnet", "marcelo.sa@al.infnet.edu.br");

                context.Administradores.Add(admin);

                context.Administradores.Add(admin2);

                context.Administradores.Add(admin3);
            }
            #endregion

            #region Contrato com operadoras
            if (!context.Contratos.Any())
            {
                var contratoHuman = ContratoFactory.CriarContratoComOperadora(System.DateTime.Now,
                                                                              System.DateTime.Now.AddYears(1), 900, 1,
                                                                              OperadoraApi.HumanSms);
                context.Contratos.Add(contratoHuman); 
            }

            #endregion

            #region Cliente para testes
            if (!context.Clientes.Any())
            {
                var cliente = ClienteFactory.Create("Cliente", "@infnet", "levy.fialho@gmail.com");
                var cliente2 = ClienteFactory.Create("Jair Martins", "@infnet", "jcam5110@gmail.com");
                var cliente3 = ClienteFactory.Create("Marcelo Sá", "@infnet", "marcelo.sa@al.infnet.edu.br");

                context.Clientes.Add(cliente);
                context.Clientes.Add(cliente2);
                context.Clientes.Add(cliente3);
                #region Criar contrato do cliente
                context.Commit();

                //Contrato do cliente teste
                var contratoCliente = ContratoFactory.CriarContratoComCliente(System.DateTime.Now,
                                                                              System.DateTime.Now.AddYears(1), 20, 0.17,
                                                                              cliente);
                var contratoCliente2 = ContratoFactory.CriarContratoComCliente(System.DateTime.Now,
                                                                              System.DateTime.Now.AddYears(1), 20, 0.17,
                                                                              cliente2);
                var contratoCliente3 = ContratoFactory.CriarContratoComCliente(System.DateTime.Now,
                                                                              System.DateTime.Now.AddYears(1), 20, 0.17,
                                                                              cliente3);
                context.Contratos.Add(contratoCliente);
                context.Contratos.Add(contratoCliente2); context.Contratos.Add(contratoCliente3);

                #endregion
            }

            #endregion

            #region status Padrão
            if (!context.Status.Any())
            {
                var status = StatusFactory.Create("Registration", "Message was registered but never sent.",
                                                  "Não enviada.", 0, 0, 0,
                                                  OperadoraApi.Null);
                context.Status.Add(status);

                
                #region HumanSMS Status

                status = StatusFactory.Create("000 - Message Sent", "Message sent - Human SMS.",
                                                  "Enviada.", 1, 1, 0.17,
                                                  OperadoraApi.HumanSms);
                context.Status.Add(status);

                status = StatusFactory.Create("010 - Empty message content", "Empty message content - Human SMS.",
                                                 "Erro - Mensagem não enviada. Texto vazio.", 0, 0, 0,
                                                 OperadoraApi.HumanSms);
                context.Status.Add(status);

                status = StatusFactory.Create("012 - Message content overflow", "Message content overflow - Human SMS.",
                                                 "Erro - Mensagem não enviada. Tamanho limite excedido (150)", 0, 0, 0,
                                                 OperadoraApi.HumanSms);
                context.Status.Add(status);

                status = StatusFactory.Create("013 - Incorrect or incomplete 'to' mobile number", "Incorrect or incomplete ‘to’ mobile number - Human SMS.",
                                                 "Erro - Mensagem não enviada. Número do destinatário inválido.", 0, 0, 0,
                                                 OperadoraApi.HumanSms);
                context.Status.Add(status);

                status = StatusFactory.Create("014 - Empty 'to' mobile number", "Empty message content - Human SMS.",
                                                 "Erro - Mensagem não enviada.", 0, 0, 0,
                                                 OperadoraApi.HumanSms);
                context.Status.Add(status);

                status = StatusFactory.Create("015 - Scheduling date invalid or incorrect", "Empty message content - Human SMS.",
                                                 "Erro - Mensagem não enviada.", 0, 0, 0,
                                                 OperadoraApi.HumanSms);
                context.Status.Add(status);

                status = StatusFactory.Create("016 - ID overflow", "Empty message content - Human SMS.",
                                                 "Erro - Mensagem não enviada.", 0, 0, 0,
                                                 OperadoraApi.HumanSms);
                context.Status.Add(status);

                status = StatusFactory.Create("080 - Message with same ID already sent", "Message with same ID already sent - Human SMS.",
                                                 "Erro - Mensagem não enviada.", 0, 0, 0,
                                                 OperadoraApi.HumanSms);
                context.Status.Add(status);

                status = StatusFactory.Create("900 - Authentication error", "Authentication error - Human SMS.",
                                                 "Erro - Mensagem não enviada.", 0, 0, 0,
                                                 OperadoraApi.HumanSms);
                context.Status.Add(status);

                status = StatusFactory.Create("990 - account limit reached", "Account limit reached - Human SMS.",
                                                 "Erro - Mensagem não enviada.", 0, 0,0,
                                                 OperadoraApi.HumanSms);
                context.Status.Add(status);

                status = StatusFactory.Create("998 - Wrong operation requested", "Wrong operation requested - Human SMS.",
                                                 "Erro - Mensagem não enviada.", 0, 0, 0,
                                                 OperadoraApi.HumanSms);
                context.Status.Add(status);

                status = StatusFactory.Create("999 - Unknown error", "Unknown error - Human SMS.",
                                                 "Erro - Mensagem não enviada.", 0, 0, 0,
                                                 OperadoraApi.HumanSms);
                context.Status.Add(status);

                #endregion
            }

            #endregion

            
        }
    }
}
