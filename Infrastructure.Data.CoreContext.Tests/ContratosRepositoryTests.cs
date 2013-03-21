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
    public class ContratosRepositoryTests
    {
        [TestMethod()]
        public void RepositoryGetMethodReturnMaterializedEntityById()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new ContratosRepository(unitOfWork);
            var id = new Guid("32BC895F-40A4-4C37-AA96-B7945C8C385C");
            var contrato = ContratoFactory.CriarContratoComOperadora(System.DateTime.Now,
                                                                         System.DateTime.Now.AddYears(1), 1000, 1.50,
                                                                         OperadoraApi.Comtele);
            contrato.ChangeCurrentIdentity(id);
            unitOfWork.Contratos.Add(contrato); unitOfWork.Commit();
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
            var repository = new ContratosRepository(unitOfWork);

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
            var repository = new ContratosRepository(unitOfWork);

            var entity = ContratoFactory.CriarContratoComOperadora(System.DateTime.Now,
                                                                         System.DateTime.Now.AddYears(1), 1000, 1.50,
                                                                         OperadoraApi.Comtele);
            

            //Act
            repository.Add(entity);
            unitOfWork.Commit();
        }

        [TestMethod()]
        public void RepositoryGetAllReturnMaterializedAllItems()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new ContratosRepository(unitOfWork);

            //Act
            var allItems = repository.GetAll();

            //Assert
            Assert.IsNotNull(allItems);
            Assert.IsTrue(allItems.Any());
        }

        [TestMethod()]
        public void RepositoryAllMatchingMethodReturnEntitiesWithSatisfiedCriteria()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new ContratosRepository(unitOfWork);


            var spec = ContratoSpecifications.ContratosAtivosDeOperadoras();

            //Act
            var result = repository.AllMatching(spec);

            //Assert
            foreach (var entity in result)
            {
                Assert.IsTrue((entity.Tipo == TipoDeContrato.Operadora) & (entity.IsEnabled));
            }
            

        }

        [TestMethod()]
        public void RepositoryFilterMethodReturnEntitisWithSatisfiedFilter()
        {
             //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new ContratosRepository(unitOfWork);

            //Act
            var result = repository.GetFiltered(c => c.OperadoraApi == OperadoraApi.HumanSms);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.All(c => c.OperadoraApi == OperadoraApi.HumanSms));
        }

        [TestMethod()]
        public void RepositoryRemoveItemDeleteIt()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new ContratosRepository(unitOfWork);

            var entity =  ContratoFactory.CriarContratoComOperadora(System.DateTime.Now,
                                                                         System.DateTime.Now.AddYears(1), 1000, 1.50,
                                                                         OperadoraApi.Comtele);
           
            repository.Add(entity);
            repository.UnitOfWork.Commit();

            //Act
            repository.Remove(entity);
            unitOfWork.Commit();
        }
    }
}
