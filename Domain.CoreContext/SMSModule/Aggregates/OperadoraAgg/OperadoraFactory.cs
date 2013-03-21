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


namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg
{
    /// <summary>
    /// Aplicação do padrão abstract factory, seu propósito é encapsular o processo de criação de novos objetos
    /// A fábrica cria objetos transientes, e não está relacionada a persistência do sistema diretamente
    /// Através da injeção de dependência desta factory, conseguimos isolar o dominio do sistema da 
    /// implementação concreta de comunicação com as Operadoras
    /// </summary>
    public abstract class OperadoraFactory
    {

        /// <summary>
        /// Cria um objeto do tipo Operadora, a partir do tipo de API que o contrato utiliza
        /// </summary>
        /// <param name="operadoraApi">Tipo de API que o contrato utiliza</param>
        /// <returns></returns>
        public abstract IOperadora Create(OperadoraApi operadoraApi);
    }
}
