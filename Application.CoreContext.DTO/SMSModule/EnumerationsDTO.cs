using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.TicketsAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;

namespace SmsGateway.Application.CoreContext.DTO.SMSModule
{
    /// <summary>
    /// Data transfer object para o enum de Apis disponiveis
    /// </summary>
    public enum OperadoraApiDTO
    {
        Null,
        HumanSms,
        Comtele,
        Tww
    }
     

    /// <summary>
    /// Data transfer object para o enum de tipos de contrato disponiveis
    /// </summary>
    public enum TipoDeContratoDTO
    {
        Cliente,
        Operadora
    }

    public static class Enumerations
    {
        public static OperadoraApiDTO Convert(OperadoraApi value)
        {
            switch(value)
            {
                default:
                    throw new NotImplementedException();
                case OperadoraApi.Null:
                    return  OperadoraApiDTO.Null;
                case OperadoraApi.Comtele:
                    return OperadoraApiDTO.Comtele;
                case OperadoraApi.HumanSms:
                    return OperadoraApiDTO.HumanSms;
                case OperadoraApi.Tww:
                    return OperadoraApiDTO.Tww;
            }
        }

        public static OperadoraApi Convert(OperadoraApiDTO value)
        {
            switch (value)
            {
                default:
                    throw new NotImplementedException();
                case OperadoraApiDTO.Null:
                    return OperadoraApi.Null;
                case OperadoraApiDTO.Comtele:
                    return OperadoraApi.Comtele;
                case OperadoraApiDTO.HumanSms:
                    return OperadoraApi.HumanSms;
                case OperadoraApiDTO.Tww:
                    return OperadoraApi.Tww;
            }
        }

        public static TipoDeContratoDTO Convert(TipoDeContrato value)
        {
            switch (value)
            {
                default:
                    throw new NotImplementedException();
                case TipoDeContrato.Cliente:
                    return  TipoDeContratoDTO.Cliente;
                case TipoDeContrato.Operadora:
                    return TipoDeContratoDTO.Operadora;
            }
        }

        public static TipoDeContrato Convert(TipoDeContratoDTO value)
        {
            switch (value)
            {
                default:
                    throw new NotImplementedException();
                case TipoDeContratoDTO.Cliente:
                    return TipoDeContrato.Cliente;
                case TipoDeContratoDTO.Operadora:
                    return TipoDeContrato.Operadora;
            }
        }

        public static StatusTicket Convert(StatusDoTicketDTO value)
        {
            switch (value)
            {
                default:
                    throw new NotImplementedException();
                case StatusDoTicketDTO.Resolvido: 
                    return  StatusTicket.Resolvido;
                case StatusDoTicketDTO.Pendente:
                    return StatusTicket.Pendente;
            }
        }

        public static StatusDoTicketDTO Convert(StatusTicket value)
        {
            switch (value)
            {
                default:
                    throw new NotImplementedException();
                case StatusTicket.Resolvido:
                    return StatusDoTicketDTO.Resolvido;
                case StatusTicket.Pendente:
                    return StatusDoTicketDTO.Pendente;
            }
        }
    }
}
