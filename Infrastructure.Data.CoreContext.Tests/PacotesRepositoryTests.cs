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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContatoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.PacoteAgg;
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
    public class PacotesRepositoryTests
    {
        [TestMethod()]
        public void RepositoryGetMethodReturnMaterializedEntityById()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new PacotesRepository(unitOfWork);
            var id = new Guid("32BC805F-40A4-4C37-AA96-B7945C8C385C");

            
            var pacote = PacoteFactory.NovoPacote("TESTE", 10, DateTime.Now.AddYears(1), 1.5, true);
            pacote.ChangeCurrentIdentity(id);
            unitOfWork.Pacotes.Add(pacote); unitOfWork.Commit();
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
            var repository = new PacotesRepository(unitOfWork);

            //Act
            var entity = repository.Get(Guid.Empty);

            //Assert
            Assert.IsNull(entity);
        }

        [TestMethod()]
        public void RepositoryAddNewItemSaveItem()
        {
            //Arrange
            try
            {
                var unitOfWork = new CoreContextUnitOfWork();
                var repository = new PacotesRepository(unitOfWork);
                
                var entity = PacoteFactory.NovoPacote("TESTE", 10, DateTime.Now.AddYears(1), 1.5, true);


                //Act
                repository.Add(entity);
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                    
                throw;
            }
        }

        [TestMethod()]
        public void RepositoryGetAllReturnMaterializedAllItems()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new PacotesRepository(unitOfWork);

            //Act
            var allItems = repository.GetAll();

            //Assert
            Assert.IsNotNull(allItems);
            Assert.IsTrue(allItems.Any());
        }

       
 

        [TestMethod()]
        public void RepositoryRemoveItemDeleteIt()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new PacotesRepository(unitOfWork);
            
            var entity = PacoteFactory.NovoPacote("TESTETESTE", 10, DateTime.Now.AddYears(1), 1.5, true);
           
            repository.Add(entity);
            repository.UnitOfWork.Commit();

            //Act
            repository.Remove(entity);
            unitOfWork.Commit();
        }
    }
}
