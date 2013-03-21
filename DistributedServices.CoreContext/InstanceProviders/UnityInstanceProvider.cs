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

namespace SmsGateway.DistributedServices.CoreContext.InstanceProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Dispatcher;
    using System.ServiceModel;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// Unity instance provider. 
    /// Essa classe fornece um ponto de extensibilidade para criar instâncias de serviço WCF.
    /// <remarks>
    /// Aplica o conceito de injeção de dependência
    /// </remarks>
    /// </summary>
    public class UnityInstanceProvider : IInstanceProvider
    {
        #region Members

        Type _serviceType;
        IUnityContainer _container;

        #endregion

        #region Constructor

        /// <summary>
        /// Cria uma nova instância do unity provider
        /// </summary>
        /// <param name="serviceType">Serviço aonde a instancia será aplicada</param>
        public UnityInstanceProvider(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            _serviceType = serviceType;
            _container = Container.Current;
        }

        #endregion

        #region IInstance Provider Members

        /// <summary>
        /// <see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/>
        /// </summary>
        /// <param name="instanceContext"><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></param>
        /// <param name="message"><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></param>
        /// <returns><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></returns>
        public object GetInstance(InstanceContext instanceContext, System.ServiceModel.Channels.Message message)
        {
            //Esta é a única chamada ao container em toda a solução
            return _container.Resolve(_serviceType);
        }
        /// <summary>
        /// <see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/>
        /// </summary>
        /// <param name="instanceContext"><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></param>
        /// <returns><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></returns>
        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        /// <summary>
        /// <see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/>
        /// </summary>
        /// <param name="instanceContext"><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></param>
        /// <param name="instance"><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></param>
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            if (instance is IDisposable)
                ((IDisposable)instance).Dispose();
        }

        #endregion

    }
}