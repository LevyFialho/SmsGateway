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
using System.ServiceModel.Web;
using SmsGateway.DistributedServices.Restful.CoreContext;
using SmsGateway.Presentation.Seedwork.Resources;

namespace SmsGateway.Presentation.Seedwork.Services.Restful
{
    /// <summary>
    /// Implementa o padrão abstract factory, buscando a URI dos serviços REST em  tempo de execução 
    /// Cada serviço deve ter sua URI disponível no arquivo de configuração na área appSettings.
    /// Dependências(Keys list): 
    /// RestServiceAdministracao
    /// RestServiceClient

    /// </summary>
    public abstract class Factory
    {

        private static string GetUri(string serviceKey)
        {
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings[serviceKey]))
                return System.Configuration.ConfigurationManager.AppSettings[serviceKey];
            throw new Exception(Messages.RestfulServiceURINotFound + " "+ serviceKey);
        }
        public static IAdministracaoService RestServiceAdministradores()
        {

            string uri = GetUri("IAdministracaoService");
            var service = new WebChannelFactory<IAdministracaoService>(new Uri(uri));

            return service.CreateChannel();

        }
        public static IClientService RestServiceClientes()
        {
            string uri = GetUri("IClientService");
            var service = new WebChannelFactory<IClientService>(new Uri(uri));

            return service.CreateChannel();
        }
        public static IApiService RestServiceApi()
        {
            string uri = GetUri("IApiService");
            var service = new WebChannelFactory<IApiService>(new Uri(uri));

            return service.CreateChannel();
        }



    }
}
