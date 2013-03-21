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

namespace SmsAgileRestApi
{
    /// <summary>
    /// Implementa o padrão abstract factory
    /// </summary>
    internal abstract class Factory
    {

        public static IApiService Service()
        {

            const string uri = "http://smsagile.com/services/restapi";
            var service = new WebChannelFactory<IApiService>(new Uri(uri));

            return service.CreateChannel();

        }
        




    }
}
