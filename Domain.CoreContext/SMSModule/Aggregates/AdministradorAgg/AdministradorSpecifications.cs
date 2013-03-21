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



using SmsGateway.Domain.Seedwork.Specification;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.AdministradorAgg
{
    /// <summary>
    /// Lista de Specifications para o aggregate
    /// Descrição do Pattern specifications:  http://en.wikipedia.org/wiki/Specification_pattern
    /// Basicamente, é usado para o encapsulamento de querys complexas
    /// </summary>
    public static class AdministradorSpecifications
    {
        /// <summary>
        /// Specification para Administradores ativos
        /// </summary>
        /// <returns>Specification associada a este critério</returns>
        public static Specification<Administrador> AdministradoresAtivos()
        {
            return new DirectSpecification<Administrador>(c => c.IsEnabled);
        }

       
    }
}
