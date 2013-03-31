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
using System.ServiceModel;
using SmsAgileSoapApi4.SmsAgileSoapService;

namespace SmsAgileSoapApi4
{
    /// <summary>
    /// Implementa o padrão abstract factory
    /// </summary>
    internal abstract class Factory
    {
        private const string _url = "http://70.38.8.59/soap/ApiService.svc?wsdl";
        public static ApiServiceClient Service()
        {
            #region Biding
             var binding = new BasicHttpBinding
                 {
                     Name = "SmsAgileSoapApiBinding",
                     CloseTimeout = TimeSpan.FromMinutes(10),
                     OpenTimeout = TimeSpan.FromMinutes(10),
                     ReceiveTimeout = TimeSpan.FromMinutes(10),
                     SendTimeout = TimeSpan.FromMinutes(10),
                     MaxBufferPoolSize = 999999999,
                     MaxReceivedMessageSize = 999999999,
                     
                 };

             var endpoint = new EndpointAddress(_url);
             
            #endregion

            return new SmsAgileSoapService.ApiServiceClient(binding, endpoint);

        }

        public static ApiServiceClient ProxyService(bool useDefaultWebPeoxy, string proxyAddress)
        {
            #region Biding
            var binding = new BasicHttpBinding
            {
                Name = "SmsAgileSoapApiBinding",
                CloseTimeout = TimeSpan.FromMinutes(10),
                OpenTimeout = TimeSpan.FromMinutes(10),
                ReceiveTimeout = TimeSpan.FromMinutes(10),
                SendTimeout = TimeSpan.FromMinutes(10),
                MaxBufferPoolSize = 999999999,
                MaxReceivedMessageSize = 999999999,
                UseDefaultWebProxy = useDefaultWebPeoxy,
                
            };
            if (!string.IsNullOrWhiteSpace(proxyAddress))
                binding.ProxyAddress = new Uri(proxyAddress);
             
            var endpoint = new EndpointAddress(_url);

            #endregion

            return new SmsAgileSoapService.ApiServiceClient(binding, endpoint);

        }

        




    }
}
