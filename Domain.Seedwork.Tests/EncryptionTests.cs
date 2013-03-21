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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmsGateway.Domain.Seedwork;

namespace Domain.Seedwork.Tests
{
    
    
    

    /// <summary>
    /// Teste do framework Domain.Seedwork.Encryption
    /// </summary>
    [TestClass]
    public class EncryptionTests
    {

        [TestMethod]
        public void TesteBase64()
        {
            //Arrange
            const string texto = "TESTE";

            //Act
            var encrypted = Encryption.ToBase64(texto);

            //Assert
            Assert.AreEqual(texto, Encryption.FromBase64(encrypted));
        }
        [TestMethod]
        public void TesteMd5()
        {
            //Arrange
            const string texto = "TESTE";

            //Act
            var encrypted = Encryption.ToMd5(texto);

            //Assert
            Assert.AreEqual(texto, Encryption.FromMd5(encrypted));
        }
    }
}
