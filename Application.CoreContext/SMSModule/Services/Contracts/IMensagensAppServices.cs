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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;

namespace SmsGateway.Application.CoreContext.SMSModule.Services.Contracts
{
    /// <summary>
    /// Contrato base para serviço da aplicação. 
    /// O serviço é responsável por adaptar entidades a DTOS e executar operações que acessam o domínio da aplicação.
    ///  </summary>
    public interface IMensagensAppServices: IDisposable
    {
       
        /// <summary>
        /// Cadastra uma nova mensagem no sistema
        /// </summary>
        /// <param name="mensagem">Todos os clientes precisam ter um novo  contrato na hora do cadastro</param>
        /// <returns>Mensagem cadastrada</returns>
        MensagemDTO NovaMensagem(MensagemDTO mensagem);

        /// <summary>
        /// Atualiza uma mensagem no sistema
        /// </summary>
        /// <param name="mensagem">Mensagem a persistir</param>
        /// <returns></returns>
        void PersistirMensagem(MensagemDTO mensagem);

        /// <summary>
        /// Atualiza uma mensagem no sistema
        /// </summary>
        /// <param name="mensagem">Mensagem a persistir</param>
        /// <returns></returns>
        void PersistirMensagem(Mensagem mensagem);
        /// <summary>
        /// Pega uma mensagem no sistema
        /// </summary>
        /// <param name="id">Identificador da mensagem</param>
        /// <returns></returns>
        MensagemDTO GetMensagem(Guid id);

        /// <summary>
        /// Pega todas as mensagens de um cliente
        /// </summary>
        /// <param name="idCliente">Identificador do cliente</param>
        /// <returns></returns>
        List<MensagemDTO> GetMensagensDoCliente(Guid idCliente);

        /// <summary>
        /// Pega todas as mensagens de um contrato
        /// </summary>
        /// <param name="idContrato">Identificador do contrato</param>
        /// <returns></returns>
        List<MensagemDTO> GetMensagensDoContrato(Guid idContrato);

       

    }
}
