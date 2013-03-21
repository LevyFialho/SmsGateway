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


using System.Linq;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContatoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;

namespace Infrastructure.Data.CoreContext.Tests.Initializers
{
    using System;
    using System.Data.Entity;

    using SmsGateway.Domain.Seedwork;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.AdministradorAgg;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg.StatusAgg;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;
    using SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork;

    /// <summary>
    /// Inicializa a base de dados para testes da unidade de trabalho Corecontext
    /// Link sobre initializers:  http://msdn.microsoft.com/en-us/library/gg696323(v=VS.103).aspx
    /// </summary>
    public class CoreContextUnitOfWorkInitializer
        : DropCreateDatabaseAlways<CoreContextUnitOfWork>
    {
        protected override void Seed(CoreContextUnitOfWork unitOfWork)
        {
            //Administradores

            var admin = AdministradorFactory.Create("TestAdmin", "test", "test@test.com");
            admin.ChangeCurrentIdentity(new Guid("32BB805F-40A4-4C37-AA96-B7945C8C385C"));
            unitOfWork.Administradores.Add(admin);

            //Preencher o banco com dados fixos

            #region Criar Administrador Geral
            var admin2 = AdministradorFactory.Create("System Admin", "passw0rd", "admin@smsgateway.com");
            unitOfWork.Administradores.Add(admin2);
            #endregion

            #region Contrato com operadoras
            var contratoHuman = ContratoFactory.CriarContratoComOperadora(System.DateTime.Now,
                                                                          System.DateTime.Now.AddYears(1), 1000, 1,
                                                                          OperadoraApi.HumanSms);
            unitOfWork.Contratos.Add(contratoHuman);
            var contratoTww = ContratoFactory.CriarContratoComOperadora(System.DateTime.Now,
                                                                         System.DateTime.Now.AddYears(1), 1000, 1.50,
                                                                         OperadoraApi.Tww);
            unitOfWork.Contratos.Add(contratoTww);
            #endregion

            #region Cliente para testes
            var cliente = ClienteFactory.Create("Cliente", "teste", "teste@teste.com.br");
            unitOfWork.Clientes.Add(cliente);
            
            #endregion  

            #region Contatos para testes 
            var contato = ContatoFactory.Create("PEDRO",  2199887766, cliente.Id);
            unitOfWork.Contatos.Add(contato);
            #endregion

            #region status Padrão

            var status = StatusFactory.Create("DEFAULT", "Message registered.", "Not sent, waiting service.", 0, 0, 0,
                                              OperadoraApi.Null);
            unitOfWork.Status.Add(status);

            #endregion

            unitOfWork.Commit();

            #region Criar contrato do cliente
            //Contrato do cliente teste
            var contratoCliente = ContratoFactory.CriarContratoComCliente(System.DateTime.Now,
                                                                           System.DateTime.Now.AddYears(1), 1000, 1,
                                                                          cliente);
             unitOfWork.Contratos.Add(contratoCliente);
            #endregion



        }
    }
}
