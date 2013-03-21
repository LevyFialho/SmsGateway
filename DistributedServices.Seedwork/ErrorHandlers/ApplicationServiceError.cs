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


namespace SmsGateway.DistributedServices.Seedwork.ErrorHandlers
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Default ServiceError
    /// </summary>
    [DataContract(Name = "ServiceError", Namespace = "SmsGateway.DistributedServices.Seedwork")]
    public class ApplicationServiceError
    {
        /// <summary>
        /// Mensagem de erro passada aos serviços do cliente
        /// </summary>
        [DataMember(Name = "ErrorMessage")]
        public string ErrorMessage { get; set; }
    }
}
