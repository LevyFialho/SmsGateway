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
using SmsGateway.Domain.CoreContext.Resources;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;

namespace SmsGateway.Infrastructure.Operadoras
{
    /// <summary> 
    /// Esta é a fábrica para criação do Operadoras, seu propósito é encapsular o processo de criação de novos objetos
    /// A fábrica cria objetos transientes, e não está relacionada a persistência do sistema diretamente
    /// </summary>
    public class OperadoraFactory: SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg.OperadoraFactory
    {

        /// <summary>
        /// Cria um objeto do tipo Operadora, a partir do tipo de API que o contrato utiliza
        /// </summary>
        /// <param name="operadoraApi">Tipo de API que o contrato utiliza</param>
        /// <returns></returns>
        public override IOperadora Create(OperadoraApi operadoraApi)
        {

            switch (operadoraApi)
            {
                case OperadoraApi.Null:
                    throw new Exception(Messages.exception_ApiOperadoraInvalida);
                case OperadoraApi.HumanSms:
                    return new OperadoraHuman();
                default:
                    throw new Exception(Messages.exception_ApiOperadoraInvalida);

            }


        }
    }
}
