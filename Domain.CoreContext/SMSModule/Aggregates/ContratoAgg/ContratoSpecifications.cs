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
using SmsGateway.Domain.Seedwork.Specification;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg
{
    /// <summary>
    /// Lista de Specifications para o aggregate
    /// Descrição do Pattern specifications:  http://en.wikipedia.org/wiki/Specification_pattern
    /// Basicamente, é usado para o encapsulamento de querys complexas
    /// </summary>
    public static class ContratoSpecifications
    {
        /// <summary>
        /// Contratos Ativos
        /// </summary>
        /// <returns>Specification associada a este critério</returns>
        public static Specification<Contrato> ContratosAtivos()
        {
            return new DirectSpecification<Contrato>(c => c.IsEnabled);
        }

        /// <summary>
        /// Contratos ativos de clientes
        /// </summary>
        /// <returns>Specification associada a este critério</returns>
        public static Specification<Contrato> ContratosAtivosDeClientes()
        {
            Specification<Contrato> specification = new DirectSpecification<Contrato>(c => c.IsEnabled);
            Specification<Contrato> specificationTipo = new DirectSpecification<Contrato>(c => c.Tipo == TipoDeContrato.Cliente);
             
            specification &= specificationTipo;
             

            return specification;
        }

        /// <summary>
        /// Contratos ativos de operadoras
        /// </summary>
        /// <returns>Specification associada a este critério</returns>
        public static Specification<Contrato> ContratosAtivosDeOperadoras()
        {
            Specification<Contrato> specification = new DirectSpecification<Contrato>(c => c.IsEnabled);
            Specification<Contrato> specificationTipo = new DirectSpecification<Contrato>(c => c.Tipo == TipoDeContrato.Operadora);

            specification &= specificationTipo;


            return specification;
        }

        
    }
}
