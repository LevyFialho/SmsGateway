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
			

namespace SmsGateway.Infrastructure.Crosscutting.NetFramework.Logging
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Security;

    using Crosscutting.Logging;
    
    /// <summary>
    /// Implementação do contrato <see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/>
    /// usando System.Diagnostics API.
    /// </summary>
    public sealed class TraceSourceLog
        :ILogger
    {
        #region Members

        TraceSource source;

        #endregion

        #region  Constructor

       
        public TraceSourceLog()
        {
            source = new TraceSource("SmsGateway");
        }

        #endregion

        #region Private Methods
        
        void TraceInternal(TraceEventType eventType, string message)
        {

            if (source != null)
            {
                try
                {
                    source.TraceEvent(eventType, (int)eventType, message);
                }
                catch (SecurityException)
                {
                    //Cannot access to file listener or cannot have
                    //privileges to write in event log etc...
                }
            }
        }
        #endregion

        #region ILogger Members

        /// <summary>
        /// <see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        public void LogInfo(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                TraceInternal(TraceEventType.Information, messageToTrace);
            }
        }
        /// <summary>
        /// <see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        public void LogWarning(string message, params object[] args)
        {

            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                TraceInternal(TraceEventType.Warning, messageToTrace);
            }
        }

        /// <summary>
        /// <see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        public void LogError(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                TraceInternal(TraceEventType.Error, messageToTrace);
            }
        }

        /// <summary>
        /// <see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        /// <param name="exception"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        public void LogError(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message)
                &&
                exception != null)
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                var exceptionData = exception.ToString(); 
                TraceInternal(TraceEventType.Error, string.Format(CultureInfo.InvariantCulture, "{0} Exception:{1}", messageToTrace, exceptionData));
            }
        }

        /// <summary>
        /// <see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        public void Debug(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                TraceInternal(TraceEventType.Verbose, messageToTrace);
            }
        }

        /// <summary>
        /// <see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        /// <param name="exception"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        public void Debug(string message, Exception exception,params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message)
                &&
                exception != null)
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                var exceptionData = exception.ToString(); // The ToString() create a string representation of the current exception

                TraceInternal(TraceEventType.Error, string.Format(CultureInfo.InvariantCulture, "{0} Exception:{1}", messageToTrace, exceptionData));
            }
        }

        /// <summary>
        /// <see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="item"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        public void Debug(object item)
        {
            if (item != null)
            {
                TraceInternal(TraceEventType.Verbose, item.ToString());
            }
        }

        /// <summary>
        /// <see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        public void Fatal(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                TraceInternal(TraceEventType.Critical, messageToTrace);
            }
        }

        /// <summary>
        /// <see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        /// <param name="exception"><see cref="SmsGateway.Infrastructure.Crosscutting.Logging.ILogger"/></param>
        public void Fatal(string message, Exception exception,params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message)
                &&
                exception != null)
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                var exceptionData = exception.ToString(); // The ToString() create a string representation of the current exception

                TraceInternal(TraceEventType.Critical, string.Format(CultureInfo.InvariantCulture, "{0} Exception:{1}", messageToTrace, exceptionData));
            }
        }


        #endregion
    }
}
