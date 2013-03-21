//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================

using SmsGateway.Application.CoreContext.DTO.SMSModule;

namespace SmsGateway.DistributedServices.CoreContext
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using SmsGateway.Application.CoreContext.DTO;
    using SmsGateway.DistributedServices.Seedwork.ErrorHandlers;

    /// <summary>
    /// WCF SERVICE FACADE 
    /// </summary>
    [ServiceContract]
    public interface IClientService
        :IDisposable
    {
        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        ContratoDTO GetContratoAtual(Guid idCliente);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        int GetSaldoDeMensagens(Guid idCliente);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<MensagemDTO> GetMensagensEnviadas(Guid idCliente);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        MensagemDTO GetMensagem(Guid idMensagem);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        MensagemDTO EnviarMensagem(Guid idCliente, MensagemDTO mensagem);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        MensagemDTO EnviarMensagem(string idCliente, string destinatario, string remetente, string texto);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        MensagemDTO EnviarMensagem(string idCliente, string destinatario, string texto);
    }
}
