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

using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.PacoteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.TicketsAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Services;
using SmsGateway.Infrastructure.Data.CoreContext.SMSModule.Services;

namespace SmsGateway.DistributedServices.Restful.CoreContext.InstanceProviders
{
    using Microsoft.Practices.Unity;
    using SmsGateway.Application.CoreContext.SMSModule.Services;
    using SmsGateway.Application.CoreContext.SMSModule.Services.Contracts;

    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.AdministradorAgg;
   using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
     using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg.StatusAgg;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;
    using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContatoAgg;
    using SmsGateway.Infrastructure.Crosscutting.Adapter;
    using SmsGateway.Infrastructure.Crosscutting.Logging;
    using SmsGateway.Infrastructure.Crosscutting.NetFramework.Logging;
    using SmsGateway.Infrastructure.Crosscutting.NetFramework.Validator;
    using SmsGateway.Infrastructure.Crosscutting.Validator;

    using SmsGateway.Infrastructure.Data.CoreContext.SMSModule.Repositories;
    using SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork;
    using SmsGateway.Application.CoreContext.SMSModule;
    using SmsGateway.Infrastructure.Crosscutting.NetFramework.Adapter;
    

    /// <summary>
    /// Acessa o container usado para injeção de dependência
    /// Basicamente mapeia a implementação de cada interface
    /// </summary>
    public static class Container
    {
        #region Properties

        static  IUnityContainer _currentContainer;

        /// <summary>
        /// Get container atual
        /// </summary>
        /// <returns>Container</returns>
        public static IUnityContainer Current
        {
            get
            {
                return _currentContainer;
            }
        }

        #endregion

        #region Constructor
        
        static Container()
        {
            ConfigureContainer();

            ConfigureFactories();
        }

        #endregion

        #region Methods

        static void ConfigureContainer()
        {
            /*
             * Adicione aqui o código para configurar o container,
             * ou a chamada para configurar a partir do arquio de configuração
             */

            _currentContainer = new UnityContainer();
            
            
            //-> Unit of Work 
            _currentContainer.RegisterType(typeof(CoreContextUnitOfWork), new PerResolveLifetimeManager());
            //-> operadoras
            _currentContainer.RegisterType<OperadoraFactory, SmsGateway.Infrastructure.Operadoras.OperadoraFactory>();
           
            //-> repositories
            _currentContainer.RegisterType<IAdministradorRepository, AdministradoresRepository>();
            _currentContainer.RegisterType<IClienteRepository, ClientesRepository>();
            _currentContainer.RegisterType<IContratoRepository, ContratosRepository>();
            _currentContainer.RegisterType<IStatusRepository,StatusRepository>();
            _currentContainer.RegisterType<IMensagemRepository, MensagensRepository>();
            _currentContainer.RegisterType<IContatoRepository, ContatosRepository>();
            _currentContainer.RegisterType<IListaDeContatosRepository, ListasDeContatosRepository>();
            _currentContainer.RegisterType<IPacoteRepository, PacotesRepository>();
            _currentContainer.RegisterType<IMensagemDoTicketRepository, MensagemDoTicketRepository>();
            _currentContainer.RegisterType<ITicketRepository, TicketsRepository>();
            _currentContainer.RegisterType<ISolicitacaoDeCadastroRepository, SolicitacaoDeCadastroRepository>();
            //-> Adapters
            _currentContainer.RegisterType<ITypeAdapterFactory, AutomapperTypeAdapterFactory>(new ContainerControlledLifetimeManager());

            //-> Domain Services
            _currentContainer.RegisterType<ISmsService, SmsService>();
            //-> Infastructure Services
            _currentContainer.RegisterType<IDatabases, DatabaseService>();
            _currentContainer.RegisterType<IEmailService, EmailService>();
            //-> Application services
            _currentContainer.RegisterType<IAdministradoresAppService, AdministradoresAppService>();
            _currentContainer.RegisterType<IContratosAppServices, ContratosAppService>();
            _currentContainer.RegisterType<IMensagensAppServices, MensagensAppService>();
            _currentContainer.RegisterType<IClientesAppService, ClientesAppService>();
            _currentContainer.RegisterType<IStatusAppService, StatusAppService>();
            _currentContainer.RegisterType<ISmsAppServices, SmsAppService>();
            _currentContainer.RegisterType<IContatosAppService, ContatosAppService>();
            _currentContainer.RegisterType<IListaDeContatosAppService, ListaDeContatosAppService>();
            _currentContainer.RegisterType<IPacotesAppService, PacotesAppService>();
            _currentContainer.RegisterType<ISolicitacaoDeCadastroAppService, SolicitacaoDeCadastroAppService>();
            _currentContainer.RegisterType<ITicketsAppService, TicketsAppService>();
            _currentContainer.RegisterType<IEmailAppService, EmailsAppService>();
            //-> Distributed Services
            _currentContainer.RegisterType<IAdministracaoService, AdministracaoService>();
            _currentContainer.RegisterType<IClientService, ClientService>();
            _currentContainer.RegisterType<IApiService, ApiService>(); 
        }


        static void ConfigureFactories()
        {
            LoggerFactory.SetCurrent(new TraceSourceLogFactory());
            EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());

            var typeAdapterFactory = _currentContainer.Resolve<ITypeAdapterFactory>();
            TypeAdapterFactory.SetCurrent(typeAdapterFactory);
        }

        #endregion
    }
}