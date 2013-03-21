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
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;

namespace SmsGateway.DistributedServices.Restful.CoreContext
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {

            RouteTable.Routes.Add(new ServiceRoute("Administracao", new WebServiceHostFactory(),
                typeof(AdministracaoService)));

            RouteTable.Routes.Add(new ServiceRoute("Clientes", new WebServiceHostFactory(), 
                typeof(ClientService)));

            RouteTable.Routes.Add(new ServiceRoute("Api", new WebServiceHostFactory(),
                typeof(ApiService)));  
        }
    }
}
