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
// nos livros Domain driven Design (E. Evans) e 
//Patterns of Application Architechture (M. Fowler) na plataforma .Net
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
    public interface IClientesAppService: IDisposable
    {
         AutenticacaoDTO Autenticar(string email, string senha);
         AutenticacaoDTO Autenticar(Guid id, string senha);
         ClienteDTO Add(ClienteDTO dto);
         void Update(ClienteDTO dto);
         void Remove(Guid id);
         List<ClienteDTO> Find(int pageIndex, int pageCount);
         List<ClienteDTO> ListAll();
         ClienteDTO Find(Guid id);
         DadosDoClienteDTO DadosDoCliente(AutenticacaoDTO autenticacao);

    }

    public interface ISolicitacaoDeCadastro : IDisposable
    {
        SolicitacaoDeCadastroDTO Add(SolicitacaoDeCadastroDTO dto, IEmailAppService emailService);
        void Update(SolicitacaoDeCadastroDTO dto);
        void Remove(Guid id); 
        List<SolicitacaoDeCadastroDTO> ListAll();
        SolicitacaoDeCadastroDTO Find(Guid id);

    }
}
