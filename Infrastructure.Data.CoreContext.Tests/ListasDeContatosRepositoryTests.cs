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
using System.Collections.Generic;
using System.Linq;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContatoAgg;
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
    public class ListaDeContatosRepositoryTests
    {
        [TestMethod()]
        public void RepositoryGetMethodReturnMaterializedEntityById()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new ListasDeContatosRepository(unitOfWork);
            var cliente = unitOfWork.Clientes.FirstOrDefault();
            var id = new Guid("32BC805F-40A4-4C37-AA96-B7945C8C385C");
            var lista = ListaDeContatosFactory.ListaDeContatos("TEST1", cliente.Id,
                                                               new List<Contato>()
                                                                   {
                                                                       ContatoFactory.Create("TT",  
                                                                                             2134567564, cliente.Id)
                                                                   });
            lista.ChangeCurrentIdentity(id);
            unitOfWork.ListasDeContatos.Add(lista); unitOfWork.Commit();
            //Act
            var entity = repository.Get(id);
            
            //Assert
            Assert.IsNotNull(entity);
            Assert.IsTrue(entity.Id == id);
        }

 

        [TestMethod()]
        public void RepositoryAddNewItemSaveItem()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new ListasDeContatosRepository(unitOfWork);
            var cliente = unitOfWork.Clientes.FirstOrDefault();
            var contato = ContatoFactory.Create("TESTE2",   2176543210 , cliente.Id);
            var lista = ListaDeContatosFactory.ListaDeContatos("TESTE2", cliente.Id, new List<Contato> {contato});

            //Act
            repository.Add(lista);
            unitOfWork.Commit();
        }

         

        [TestMethod()]
        public void RepositoryRemoveItemFromList()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new ListasDeContatosRepository(unitOfWork);
            var cliente = unitOfWork.Clientes.FirstOrDefault();
            var contato = ContatoFactory.Create("TESTE3",  2176543210, cliente.Id);
            var lista = ListaDeContatosFactory.ListaDeContatos("TESTE3", cliente.Id, new List<Contato> { contato });

            //Act
            repository.Add(lista);
            repository.UnitOfWork.Commit();

            //Act
            lista.Contatos.Remove(contato);
            repository.UnitOfWork.Commit();
             
            Assert.IsFalse(lista.Contatos.Any()); 
        }

        [TestMethod()]
        public void RepositoryRemoveItem()
        {
            //Arrange
            var unitOfWork = new CoreContextUnitOfWork();
            var repository = new ListasDeContatosRepository(unitOfWork);
            var cliente = unitOfWork.Clientes.FirstOrDefault();
            var contato = ContatoFactory.Create("TESTE4",   2176543210, cliente.Id);
            var lista = ListaDeContatosFactory.ListaDeContatos("TESTE4", cliente.Id, new List<Contato> { contato });

            //Act
            repository.Add(lista);
            repository.UnitOfWork.Commit();

            //Act
            repository.Remove(lista);
            unitOfWork.Commit();
        }
    }
}
