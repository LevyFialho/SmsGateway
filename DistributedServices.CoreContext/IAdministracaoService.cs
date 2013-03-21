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
    /// WCF SERVICE FACADE for Banking Module
    /// </summary>
    [ServiceContract]
    public interface IAdministracaoService : IDisposable
    {
        #region Administradores
        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        AdministradorDTO GetAdministrador(Guid id);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<AdministradorDTO> ListAdministrador();

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        AdministradorDTO CreateAdministrador(AdministradorDTO dtoInstance);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        void UpdateAdministrador(AdministradorDTO dtoInstance);
        #endregion

        #region Clientes
        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        ClienteDTO GetCliente(Guid id);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<ClienteDTO> ListCliente();

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        ClienteDTO CreateCliente(ClienteDTO dtoInstance);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        void UpdateCliente(ClienteDTO dtoInstance);
        #endregion

        #region Contrato
        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        ContratoDTO GetContrato(Guid id);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<ContratoDTO> ListContratosDeClientes();

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<ContratoDTO> ListContratosDeOperadoras();

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        ContratoDTO CreateContrato(ContratoDTO dtoInstance);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        void UpdateContrato(ContratoDTO dtoInstance);
        #endregion

        #region Status
        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        StatusDTO GetStatus(Guid id);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<StatusDTO> ListStatus();

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        StatusDTO CreateStatus(StatusDTO dtoInstance);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        void UpdateStatus(StatusDTO dtoInstance);
        #endregion

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        bool StartAppDatabase();
    }
}
