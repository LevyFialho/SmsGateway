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

namespace Domain.Seedwork.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SmsGateway.Domain.Seedwork;
    using Domain.Seedwork.Tests.Classes;

    [TestClass()]
    public class EntityTests
    {
        [TestMethod()]
        public void TesteEqualsDuasEntidadesMesmoGuid()
        {
            //Arrange
            Guid id = Guid.NewGuid();

            var entityLeft = new SampleEntity();
            var entityRight = new SampleEntity();

            entityLeft.ChangeCurrentIdentity(id);
            entityRight.ChangeCurrentIdentity(id);
            
            //Act
            bool resultOnEquals = entityLeft.Equals(entityRight);
            bool resultOnOperator = entityLeft == entityRight;

            //Assert
            Assert.IsTrue(resultOnEquals);
            Assert.IsTrue(resultOnOperator);

        }
        [TestMethod()]
        public void TesteEqualsDuasEntidadesGuidDiferente()
        {
            //Arrange
            
            var entityLeft = new SampleEntity();
            var entityRight = new SampleEntity();

            entityLeft.GenerateNewIdentity();
            entityRight.GenerateNewIdentity();
           

            //Act
            bool resultOnEquals = entityLeft.Equals(entityRight);
            bool resultOnOperator = entityLeft == entityRight;

            //Assert
            Assert.IsFalse(resultOnEquals);
            Assert.IsFalse(resultOnOperator);

        }
        [TestMethod()]
        public void TesteEqualsComOperandoNull()
        {
            //Arrange

            SampleEntity entityLeft = null;
            SampleEntity entityRight = new SampleEntity();

            entityRight.GenerateNewIdentity();

            //Act
            if (!(entityLeft == (Entity)null))// ==(left,right)
                Assert.Fail();

            if (!(entityRight != (Entity)null))// !=(left,right)
                Assert.Fail();

            entityRight = null;

            //Act
            if (!(entityLeft == entityRight))// ==(left,right)
                Assert.Fail();

            if (entityLeft != entityRight)// !=(left,right)
                Assert.Fail();

          
        }
        [TestMethod()]
        public void TesteCompararMesmaReferenciaRetornaTrue()
        {
            //Arrange
            var entityLeft = new SampleEntity();
            SampleEntity entityRight = entityLeft;


            //Act
            if (! entityLeft.Equals(entityRight))
                Assert.Fail();

            if (!(entityLeft == entityRight))
                Assert.Fail();

        }
        [TestMethod()]
        public void CompareWhenLeftIsNullAndRightIsNullReturnFalseTest()
        {
            //Arrange

            SampleEntity entityLeft = null;
            var entityRight = new SampleEntity();

            entityRight.GenerateNewIdentity();

            //Act
            if (!(entityLeft == (Entity)null))// ==(left,right)
                Assert.Fail();

            if (!(entityRight != (Entity)null))// !=(left,right)
                Assert.Fail();
        }

        [TestMethod()]
        public void TesteDeEntidadeComIdentidadeGeradaNaoSerTransiente()
        {
            //Arrange
            var entity = new SampleEntity();

            //Act
            entity.GenerateNewIdentity();

            //Assert
            Assert.IsFalse(entity.IsTransient());
        }

        [TestMethod()]
        public void TesteDeMudancaDeId()
        {
            //Arrange
            var entity = new SampleEntity();
            entity.GenerateNewIdentity();

            //act
            Guid expected = entity.Id;
            entity.ChangeCurrentIdentity(Guid.NewGuid());

            //assert
            Assert.AreNotEqual(expected, entity.Id);
        }
    }
}
