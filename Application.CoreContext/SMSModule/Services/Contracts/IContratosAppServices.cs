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

namespace SmsGateway.Application.CoreContext.SMSModule.Services.Contracts
{
    /// <summary>
    /// Contrato base para serviço da aplicação. 
    /// O serviço é responsável por adaptar entidades a DTOS e executar operações que acessam o domínio da aplicação.
    ///  </summary>
    public interface IContratosAppServices: IDisposable
    {
        /// <summary>
        /// Pega um contrato por ID
        /// </summary>
        /// <param name="id">Guid do contrato</param>
        /// <returns>Contrato encontrado</returns>
        ContratoDTO GetContrato(Guid id);


        /// <summary>
        /// Pega um contrato por ID
        /// </summary>
        /// <param name="id">Guid do cliente</param>
        /// <returns>Contrato encontrado</returns>
        ContratoDTO GetContratoDoCliente(Guid id);

       

        /// <summary>
        /// Cria  um novo contrato com um cliente
        /// </summary>
        /// <param name="contratoDTO">Contrato a persistir</param>
        /// <returns></returns>
        ContratoDTO NovoContratoComCliente(ContratoDTO contratoDTO);

        /// <summary>
        /// Cria  um novo contrato com operadora
        /// </summary>
        /// <param name="contratoDTO">Contrato a persistir</param>
        /// <returns></returns>
        ContratoDTO NovoContratoComOperadora(ContratoDTO contratoDTO);

        /// <summary>
        /// Atualiza  um contrato
        /// </summary>
        /// <param name="contratoDTO">Contrato a persistir</param>
        /// <returns></returns>
        void RenovarContrato(ContratoDTO contratoDTO);

          /// <summary>
        /// Pega todos os contratos do tipo Cliente ativos no sistema
        /// </summary>
        /// <returns>Lista de contratos ativos de clientes</returns>
        List<ContratoDTO> GetContratosAtivosDeClientes();

        /// <summary>
        /// Pega todos os contratos do tipo Operadora ativos no sistema
        /// </summary>
        /// <returns>Lista de contratos ativos de operadoras</returns>
        List<ContratoDTO> GetContratosAtivosDeOperadoras();
    }
}
