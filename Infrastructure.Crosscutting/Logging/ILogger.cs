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


namespace SmsGateway.Infrastructure.Crosscutting.Logging
{
    using System;

        /// <summary>
        /// Contrato base para ferramenta de Log, pode implementado com diversos frameworks.
        /// .NET Diagnostics API, EntLib, Log4Net,NLog etc.
        /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logar mensagem
        /// </summary>
        /// <param name="message">A mensagem</param>
        /// <param name="args">the message argument values</param>
        void Debug(string message, params object[] args);

        /// <summary>
        /// Logar mensagem
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="exception">Exception a escrever in mensagem</param>
        void Debug(string message,Exception exception,params object[] args);

        /// <summary>
        /// Logar mensagem 
        /// </summary>
        /// <param name="item"></param>
        void Debug(object item);

        /// <summary>
        /// Logar erro fatal
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Fatal(string message, params object[] args);

        /// <summary>
        /// Logar erro fatal
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Fatal(string message, Exception exception,params object[] args);

        /// <summary>
        /// Logar mensagem informativa
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogInfo(string message, params object[] args);

        /// <summary>
        /// Logar mensagem de alerta
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogWarning(string message, params object[] args);

        /// <summary>
        /// Logar mensagem de erro
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogError(string message, params object[] args);

        /// <summary>
        /// Logar mensagem de erro
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        void LogError(string message, Exception exception, params object[] args);
    }
}
