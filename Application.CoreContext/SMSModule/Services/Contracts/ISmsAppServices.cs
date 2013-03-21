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
using System.Collections.Generic;
using SmsGateway.Application.CoreContext.DTO.SMSModule;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContatoAgg;

namespace SmsGateway.Application.CoreContext.SMSModule.Services.Contracts
{
    /// <summary>
    /// Contrato base para serviço da aplicação. 
    /// O serviço é responsável por adaptar entidades a DTOS e executar operações que acessam o domínio da aplicação.
    ///  </summary>
    public interface ISmsAppServices: IDisposable
    {
        /// <summary>
        /// Contrato do serviço usado pelos clientes. 
        /// </summary>
        /// <param name="autenticacao">Id do cliente</param>
        /// <param name="mensagem">Mensagem a ser enviada</param>
        MensagemDTO EnviarMensagem(AutenticacaoDTO autenticacao, MensagemDTO mensagem);

        ///// <summary>
        ///// Atualiza o status de uma mensagem junto a operadora
        ///// </summary>
        ///// <param name="mensagem">mensagem a pesquisar</param>
        ///// <returns>mensagem com status atualizado</returns>
        //MensagemDTO AtuaizarStatus(MensagemDTO mensagem);

        /// <summary>
        /// Envia uma mensagem para vários contatos
        /// </summary>
        /// <param name="autenticacao"></param>
        /// <param name="texto"></param>
        /// <param name="remetente"> </param>
        /// <param name="contatos"></param>
        /// <returns></returns>
        List<MensagemDTO> EnviarMensagemParaContatos(AutenticacaoDTO autenticacao, string texto,string remetente, IEnumerable<ContatoDTO> contatos);

        /// <summary>
        /// Envia uma mensagem para vários contatos
        /// </summary>
        /// <param name="autenticacao"></param>
        /// <param name="texto"></param>
        /// <param name="remetente"> </param>
        /// <param name="lista"></param>
        /// <returns></returns>
        List<MensagemDTO> EnviarMensagemParaListaDeContatos(AutenticacaoDTO autenticacao, string texto, string remetente, ListaDeContatosDTO lista);
    }
}
