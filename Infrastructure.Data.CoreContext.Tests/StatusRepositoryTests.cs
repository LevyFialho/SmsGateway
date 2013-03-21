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
    public class StatusRepositoryTests
    {
        [TestMethod()]
        public void RepositoryGetMethodReturnMaterializedEntityById()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new StatusRepository(unitOfWork);
            var id = new Guid("32BC895F-40A4-4C38-AA96-B7945C8C385C");
            var status = StatusFactory.Create("TEST0", "Message registered.", "Not sent, waiting service.", 0, 0, 0,
                                              OperadoraApi.Null);
            status.ChangeCurrentIdentity(id);
            unitOfWork.Status.Add(status); unitOfWork.Commit();
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
            var repository = new StatusRepository(unitOfWork);

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
            var repository = new StatusRepository(unitOfWork);

            var entity = StatusFactory.Create("TEST01", "Repository tests status.", "Not sent, waiting service.", 0, 0, 0,
                                              OperadoraApi.Null);
            

            //Act
            repository.Add(entity);
            unitOfWork.Commit();
        }

        [TestMethod()]
        public void RepositoryGetAllReturnMaterializedAllItems()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new StatusRepository(unitOfWork);

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
            var repository = new StatusRepository(unitOfWork);

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
            var repository = new StatusRepository(unitOfWork);

            var entity = StatusFactory.Create("TEST03", "Repository tests status.", "Not sent, waiting service.", 0, 0, 0,
                                              OperadoraApi.Null);
           
            repository.Add(entity);
            repository.UnitOfWork.Commit();

            //Act
            repository.Remove(entity);
            unitOfWork.Commit();
        }
    }
}
