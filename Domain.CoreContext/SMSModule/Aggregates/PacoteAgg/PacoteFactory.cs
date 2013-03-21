using System;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.PacoteAgg
{
    /// <summary>
    /// Aplicação do padrão Factory.Seu propósito é encapsular o processo de criação de novos objetos
    /// A fábrica cria objetos transientes, e não está relacionada a persistência de objetos
    /// </summary>
    public static class PacoteFactory
    {

        public static Pacote NovoPacote(string nome, int quantidadeDeMensagens,
            DateTime dataExpiracao, double valorCobradoPorMensagem, bool gratuitoAoNovoCliente = false)
        {
            //criar uma nova instância e setar a identidade
            var pacote = new Pacote
            {
                Nome = nome,
                QuantidadeDeMensagens = quantidadeDeMensagens,
                DataDeVencimento = dataExpiracao,
                ValorCobradoPorMensagem = valorCobradoPorMensagem,
                GratuitoAoNovoCliente = gratuitoAoNovoCliente
            };
            pacote.Enable();
            pacote.GenerateNewIdentity();

            return pacote;
        }

    }
}
