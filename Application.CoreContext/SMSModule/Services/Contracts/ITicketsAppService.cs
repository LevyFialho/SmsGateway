﻿//=================================================================================== 
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
    public interface ITicketsAppService: IDisposable
    {
        TicketDTO Add(TicketDTO dto);
        TicketDTO Update(TicketDTO dto);  
        List<TicketDTO> ListAll();
        TicketDTO Find(Guid id);
        List<TicketDTO> TicketsDoCliente(Guid idCliente);

    }

    public interface ISolicitacaoDeCadastroAppService : IDisposable
    {
        SolicitacaoDeCadastroDTO Add(SolicitacaoDeCadastroDTO dto, IEmailAppService service);
        void Update(SolicitacaoDeCadastroDTO dto);
        void Remove(Guid id);
        List<SolicitacaoDeCadastroDTO> ListAll(); 

    }
}
