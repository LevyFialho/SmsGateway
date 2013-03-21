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

using AutoMapper;
using SmsGateway.Application.CoreContext.DTO.SMSModule;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.AdministradorAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContatoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg.StatusAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.PacoteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.TicketsAgg;

namespace SmsGateway.Application.CoreContext.DTO.Profiles
{
    

    class SmsModuleProfile
        : Profile
    {
        protected override void Configure()
        {
           
            var mapAdministrador = Mapper.CreateMap<Administrador, AdministradorDTO>();
            var mapCliente = Mapper.CreateMap<Cliente, ClienteDTO>();
            var mapContato = Mapper.CreateMap<Contato, ContatoDTO>();
            var mapStatus = Mapper.CreateMap<Status, StatusDTO>();
            var mapContrato = Mapper.CreateMap<Contrato, ContratoDTO>();
            var mapMensagem = Mapper.CreateMap<Mensagem, MensagemDTO>();
            var mapListasDecontatos = Mapper.CreateMap<ListaDeContatos, ListaDeContatosDTO>();
            var mapPacote = Mapper.CreateMap<Pacote, PacoteDTO>();
            var mapTicket = Mapper.CreateMap<Ticket, TicketDTO>();
            var mapMensagemDoTicket = Mapper.CreateMap<MensagemDoTicket, MensagemDoTicketDTO>();
            var mapSolicitacoes = Mapper.CreateMap<SolicitacaoDeCadastro, SolicitacaoDeCadastroDTO>();
             
            //mapear valores
            mapMensagem.ForMember(dto => dto.StatusId, mc => mc.MapFrom(e=> e.Status.Id));
            mapMensagem.ForMember(dto => dto.StatusCodigo, mc => mc.MapFrom(e => e.Status.Codigo));
            mapMensagem.ForMember(dto => dto.StatusMensagemAoCliente, mc => mc.MapFrom(e => e.Status.MensagemAoCliente));
            mapMensagem.ForMember(dto => dto.StatusQuantoDebitarDoContratoDoCliente, mc => mc.MapFrom(e => e.Status.QuantoDebitarDoContratoDoCliente));

            mapContrato.ForMember(dto => dto.OperadoraApi, mc => mc.MapFrom(e => Enumerations.Convert(e.OperadoraApi)));
            mapContrato.ForMember(dto => dto.TipoDeContrato, mc => mc.MapFrom(e => Enumerations.Convert(e.Tipo)));
            mapStatus.ForMember(dto => dto.OperadoraApi, mc => mc.MapFrom(e => Enumerations.Convert(e.OperadoraApi)));
            mapListasDecontatos.ForMember(dto => dto.Contatos, mc => mc.MapFrom(e => e.Contatos));
            mapTicket.ForMember(dto => dto.Status, mc => mc.MapFrom(e => Enumerations.Convert(e.Status)));
            mapTicket.ForMember(dto => dto.ClienteNome, mc => mc.MapFrom(e => e.Cliente.Nome));
            mapTicket.ForMember(dto => dto.Mensagens, mc => mc.MapFrom(e => e.Mensagens));
           
        }
    }
}
