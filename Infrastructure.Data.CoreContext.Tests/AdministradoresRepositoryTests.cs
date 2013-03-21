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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.AdministradorAgg;
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
    public class AdministradoresRepositoryTests
    {
        [TestMethod()]
        public void AdministradoresRepositoryGetMethodReturnMaterializedEntityById()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var administradoresRepository = new AdministradoresRepository(unitOfWork);
            var id = new Guid("32BB805F-40A4-4C37-AA96-B7945C8C385C");

            //Act
            var admin = administradoresRepository.Get(id);
            
            //Assert
            Assert.IsNotNull(admin);
            Assert.IsTrue(admin.Id == id);
        }

        [TestMethod()]
        public void AdministradoresRepositoryGetMethodReturnNullWhenIdIsEmpty()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var administradoresRepository = new AdministradoresRepository(unitOfWork);

            //Act
            var admin = administradoresRepository.Get(Guid.Empty);

            //Assert
            Assert.IsNull(admin);
        }

        [TestMethod()]
        public void AdministradoresRepositoryAddNewItemSaveItem()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var administradoresRepository = new AdministradoresRepository(unitOfWork);

            var admin = AdministradorFactory.Create("teste", "teste", "teste@teste.com");
            admin.GenerateNewIdentity();

            //Act
            administradoresRepository.Add(admin);
            unitOfWork.Commit();
        }

        [TestMethod()]
        public void AdministradoresRepositoryGetAllReturnMaterializedAllItems()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var administradoresRepository = new AdministradoresRepository(unitOfWork);

            //Act
            var allItems = administradoresRepository.GetAll();

            //Assert
            Assert.IsNotNull(allItems);
            Assert.IsTrue(allItems.Any());
        }

        [TestMethod()]
        public void AdministradoresRepositoryAllMatchingMethodReturnEntitiesWithSatisfiedCriteria()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var administradoresRepository = new AdministradoresRepository(unitOfWork);


            var spec = AdministradorSpecifications.AdministradoresAtivos();

            //Act
            var result = administradoresRepository.AllMatching(spec);

            //Assert
            foreach(var admin in result)
            {
                Assert.IsTrue(admin.IsEnabled);
            }
            

        }

        [TestMethod()]
        public void AdministradoresRepositoryFilterMethodReturnEntitisWithSatisfiedFilter()
        {
             //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var administradoresRepository = new AdministradoresRepository(unitOfWork);

            //Act
            var result =administradoresRepository.GetFiltered(c=>c.Nome == "teste");

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.All(c => c.Nome == "teste"));
        }

        [TestMethod()]
        public void AdministradoresRepositoryRemoveItemDeleteIt()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var administradoresRepository = new AdministradoresRepository(unitOfWork);

            var admin = AdministradorFactory.Create("testeRemove", "delete", "testeRemove@email.com");
            
            administradoresRepository.Add(admin);
            administradoresRepository.UnitOfWork.Commit();

            //Act
            administradoresRepository.Remove(admin);
            unitOfWork.Commit();
        }
    }
}
