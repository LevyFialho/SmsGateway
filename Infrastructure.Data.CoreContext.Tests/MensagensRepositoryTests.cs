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
using System.Linq;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg.StatusAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;
using SmsGateway.Domain.Seedwork;
using SmsGateway.Infrastructure.Data.CoreContext.SMSModule.Repositories;
using SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Infrastructure.Data.CoreContext.Tests
{
    
    /// <summary>
    /// Classe para testes no repositório
    /// </summary>
    [TestClass()]
    public class MensagensRepositoryTests
    {
        [TestMethod()]
        public void RepositoryGetMethodReturnMaterializedEntityById()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new MensagensRepository(unitOfWork);
            var id = new Guid("32BC895F-41A4-4C38-AA96-B7945C8C385C");
            var specs = ContratoSpecifications.ContratosAtivosDeClientes();
            var contratosRepository = new ContratosRepository(unitOfWork);
            var contrato = contratosRepository.AllMatching(specs).FirstOrDefault();
            var repositoryStatus = new StatusRepository(unitOfWork);
            var status = repositoryStatus.GetFiltered(s => s.OperadoraApi == OperadoraApi.Null).FirstOrDefault();
           
            var msg = MensagemFactory.Create(contrato, "TESTE", "2188740956", "2188334455", status);
            msg.ChangeCurrentIdentity(id);
            repository.Add(msg); unitOfWork.Commit();
            //Act
            var entity = repository.Get(id);
            
            //Assert
            Assert.IsNotNull(entity);
            Assert.IsTrue(entity.Id == id);
        }

        [TestMethod()]
        public void RepositoryGetMethodReturnNullWhenIdIsEmpty()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new MensagensRepository(unitOfWork);

            //Act
            var entity = repository.Get(Guid.Empty);

            //Assert
            Assert.IsNull(entity);
        }

        [TestMethod()]
        public void RepositoryAddNewItemSaveItem()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new MensagensRepository(unitOfWork);
            var repositoryStatus = new StatusRepository(unitOfWork);
            var status = repositoryStatus.GetFiltered(s => s.OperadoraApi == OperadoraApi.Null).FirstOrDefault();
            var specs = ContratoSpecifications.ContratosAtivosDeClientes();
            var contratosRepository = new ContratosRepository(unitOfWork);
            var contrato = contratosRepository.AllMatching(specs).FirstOrDefault();
            var entity = MensagemFactory.Create(contrato, "TESTE", "2188740956", "2188334455", status);
            
            

            //Act
            repository.Add(entity);
            unitOfWork.Commit();
        }

        [TestMethod()]
        public void RepositoryGetAllReturnMaterializedAllItems()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new MensagensRepository(unitOfWork);

            //Act
            var allItems = repository.GetAll();

            //Assert
            Assert.IsNotNull(allItems);
            Assert.IsTrue(allItems.Any());
        }

        [TestMethod()]
        public void RepositoryFilterMethodReturnEntitisWithSatisfiedFilter()
        {
             //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new MensagensRepository(unitOfWork);

            //Act
            var result = repository.GetFiltered(c => c.IsEnabled);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.All(c => c.IsEnabled));
        }

        [TestMethod()]
        public void RepositoryRemoveItemDeleteIt()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new MensagensRepository(unitOfWork);

            var specs = ContratoSpecifications.ContratosAtivosDeClientes();
            var contratosRepository = new ContratosRepository(unitOfWork);
            var contrato = contratosRepository.AllMatching(specs).FirstOrDefault();
            var repositoryStatus = new StatusRepository(unitOfWork);
            var status = repositoryStatus.GetFiltered(s => s.OperadoraApi == OperadoraApi.Null).FirstOrDefault();
           
            var entity = MensagemFactory.Create(contrato, "TESTE", "2188740956", "2188334455", status);
           
            repository.Add(entity);
            repository.UnitOfWork.Commit();

            //Act
            repository.Remove(entity);
            unitOfWork.Commit();
        }
    }
}
