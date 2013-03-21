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
using SmsGateway.Domain.Seedwork.Specification;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg
{
    /// <summary>
    /// Lista de Specifications para o aggregate
    /// Descrição do Pattern specifications:  http://en.wikipedia.org/wiki/Specification_pattern
    /// Basicamente, é usado para o encapsulamento de querys complexas
    /// </summary>
    public static class MensagemSpecifications
    {

        /// <summary>
        /// Mensagens do cliente (em qualquer contrato)
        /// </summary>
        /// <returns>Specification associada a este critério</returns>
        public static Specification<Mensagem> MensagensDoCliente(Guid idCliente)
        {
            Specification<Mensagem> specification = new DirectSpecification<Mensagem>(c =>
                (c.ContratoDoCliente.ClienteId == idCliente));


            return specification;
        }

        /// <summary>
        /// Mensagens do contrato (tanto cliente quanto operadoras)
        /// </summary>
        /// <returns>Specification associada a este critério</returns>
        public static Specification<Mensagem> MensagensDoContrato(Guid idContrato)
        {
            Specification<Mensagem> specification = new DirectSpecification<Mensagem>(c => 
                (c.ContratoDoClienteId == idContrato) || (c.ContratoDaOperadoraId == idContrato));
            

            return specification;
        }
    }
}
