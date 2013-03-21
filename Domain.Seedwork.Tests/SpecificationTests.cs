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
    using System.Linq.Expressions;
    using System.Linq;

    using Domain.Seedwork.Tests.Classes;

    using SmsGateway.Domain.Seedwork.Specification;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SmsGateway.Domain.Seedwork;
    using System.Collections.Generic;

    /// <summary>
    /// Teste do framework Domain.Seedwork.Specifications
    /// </summary>
    [TestClass]
    public class SpecificationTests
    {

        [TestMethod]
        public void TesteCriarNovaDirectSpecification()
        {
            //Arrange
            DirectSpecification<SampleEntity> adHocSpecification;
            Expression<Func<SampleEntity, bool>> spec = s => s.Id == Guid.NewGuid();

            //Act
            adHocSpecification = new DirectSpecification<SampleEntity>(spec);

            //Assert
            Assert.ReferenceEquals(new PrivateObject(adHocSpecification).GetField("_MatchingCriteria"), spec);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TesteDeExceptionArgumentNullNaConstrucaoDeDirectSpecification()
        {
            //Arrange
            DirectSpecification<SampleEntity> adHocSpecification;
            Expression<Func<SampleEntity, bool>> spec = null;

            //Act
            adHocSpecification = new DirectSpecification<SampleEntity>(spec);
        }
        [TestMethod()]
        public void TesteSpecificationAnd()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            var identifier = Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> leftSpec = s => s.Id == identifier;
            Expression<Func<SampleEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            //Act
            AndSpecification<SampleEntity> composite = new AndSpecification<SampleEntity>(leftAdHocSpecification, rightAdHocSpecification);

            //Assert
            Assert.IsNotNull(composite.SatisfiedBy());
            Assert.ReferenceEquals(leftAdHocSpecification, composite.LeftSideSpecification);
            Assert.ReferenceEquals(rightAdHocSpecification, composite.RightSideSpecification);

            var list = new List<SampleEntity>();
            var sampleA = new SampleEntity() {  SampleProperty = "1" };
            sampleA.ChangeCurrentIdentity(identifier);

            var sampleB = new SampleEntity() { SampleProperty = "the sample property" };
            sampleB.ChangeCurrentIdentity(identifier);

            list.AddRange(new SampleEntity[] { sampleA, sampleB });
            

            var result = list.AsQueryable().Where(composite.SatisfiedBy()).ToList();

            Assert.IsTrue(result.Count == 1);
        }
        [TestMethod()]
        public void TestSpecificationOr()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            var identifier = Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> leftSpec = s => s.Id == identifier;
            Expression<Func<SampleEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            //Act
            OrSpecification<SampleEntity> composite = new OrSpecification<SampleEntity>(leftAdHocSpecification, rightAdHocSpecification);

            //Assert
            Assert.IsNotNull(composite.SatisfiedBy());
            Assert.ReferenceEquals(leftAdHocSpecification, composite.LeftSideSpecification);
            Assert.ReferenceEquals(rightAdHocSpecification, composite.RightSideSpecification);

            var list = new List<SampleEntity>();

            var sampleA = new SampleEntity() { SampleProperty = "1" };
            sampleA.ChangeCurrentIdentity(identifier);

            var sampleB = new SampleEntity() { SampleProperty = "the sample property" };
            sampleB.GenerateNewIdentity();

            list.AddRange(new SampleEntity[] { sampleA, sampleB });


            var result = list.AsQueryable().Where(composite.SatisfiedBy()).ToList();

            Assert.IsTrue(result.Count() == 2);

            

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TesteSpecificationAndComArgummentNull()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            Expression<Func<SampleEntity, bool>> leftSpec = s => s.Id == Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            //Act
            AndSpecification<SampleEntity> composite = new AndSpecification<SampleEntity>(null, rightAdHocSpecification);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TesteSpecificationAndComArgummentNullRight()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            Expression<Func<SampleEntity, bool>> rightSpec = s => s.Id == Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> leftSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            //Act
            AndSpecification<SampleEntity> composite = new AndSpecification<SampleEntity>(leftAdHocSpecification, null);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TesteSpecificationOrComArgummentNullLeft()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            Expression<Func<SampleEntity, bool>> leftSpec = s => s.Id == Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            //Act
            OrSpecification<SampleEntity> composite = new OrSpecification<SampleEntity>(null, rightAdHocSpecification);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TesteSpecificationOrComArgummentNullRight()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            Expression<Func<SampleEntity, bool>> rightSpec = s => s.Id == Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> leftSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            //Act
            OrSpecification<SampleEntity> composite = new OrSpecification<SampleEntity>(leftAdHocSpecification, null);

        }
        [TestMethod]
        public void TesteUsoSpecificationAnd()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            var identifier = Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> leftSpec = s => s.Id == identifier;
            Expression<Func<SampleEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;


            //Act
            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            ISpecification<SampleEntity> andSpec = leftAdHocSpecification && rightAdHocSpecification;

            var list = new List<SampleEntity>();

            var sampleA = new SampleEntity() { SampleProperty = "1" };
            sampleA.ChangeCurrentIdentity(identifier);

            var sampleB = new SampleEntity() { SampleProperty = "the sample property" };
            sampleB.GenerateNewIdentity();

            var sampleC = new SampleEntity() { SampleProperty = "the sample property" };
            sampleC.ChangeCurrentIdentity(identifier);

            list.AddRange(new SampleEntity[] { sampleA, sampleB, sampleC });


            var result = list.AsQueryable().Where(andSpec.SatisfiedBy()).ToList();

            Assert.IsTrue(result.Count == 1);

        }
        
        [TestMethod]
        public void TesteUsoSpecificationOr()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            var identifier = Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> leftSpec = s => s.Id == identifier;
            Expression<Func<SampleEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;

            
            //Act
            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            var orSpec = leftAdHocSpecification || rightAdHocSpecification;
            

            //Assert
            var list = new List<SampleEntity>();
            var sampleA = new SampleEntity() { SampleProperty = "1" };
            sampleA.ChangeCurrentIdentity(identifier);

            var sampleB = new SampleEntity() { SampleProperty = "the sample property" };
            sampleB.GenerateNewIdentity();

            list.AddRange(new SampleEntity[] { sampleA, sampleB });

            var result = list.AsQueryable().Where(orSpec.SatisfiedBy()).ToList();

            Assert.IsTrue(result.Count() == 2);
        }
        
        [TestMethod()]
        public void TesteChecarOperadoresNot()
        {
            //Arrange
            Expression<Func<SampleEntity, bool>> specificationCriteria = t => t.Id == Guid.NewGuid();


            //Act
            Specification<SampleEntity> spec = new DirectSpecification<SampleEntity>(specificationCriteria);
            Specification<SampleEntity> notSpec = !spec;
            ISpecification<SampleEntity> resultAnd = notSpec && spec;
            ISpecification<SampleEntity> resultOr = notSpec || spec;

            //Assert
            Assert.IsNotNull(notSpec);
            Assert.IsNotNull(resultAnd);
            Assert.IsNotNull(resultOr);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TesteSpecificationNotComArgummentNull()
        {
            //Arrange
            NotSpecification<SampleEntity> notSpec;

            //Act
            notSpec = new NotSpecification<SampleEntity>((ISpecification<SampleEntity>)null);
        }
        
        [TestMethod()]
        public void TesteSpecificationTrue()
        {
            //Arrange
            ISpecification<SampleEntity> trueSpec = new TrueSpecification<SampleEntity>();
            bool expected = true;
            bool actual = trueSpec.SatisfiedBy().Compile()(new SampleEntity());
            //Assert
            Assert.IsNotNull(trueSpec);
            Assert.AreEqual(expected, actual);
        }
    }
}
