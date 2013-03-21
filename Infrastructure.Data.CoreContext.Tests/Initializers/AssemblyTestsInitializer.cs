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

			
namespace Infrastructure.Data.CoreContext.Tests.Initializers
{
    using System.Data.Entity;
    using SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AssemblyTestsInitialize
    {
        /// <summary>
        /// CoreContextUnitOfWorkInitializer é respon sável por inicializar o Unity of Work
        /// </summary>
        /// <param name="context">O contexto para teste</param>
        [AssemblyInitialize()]
        public static void RebuildUnitOfWork(TestContext context)
        {
            //Set default initializer for CoreContextUnitOfWork
            Database.SetInitializer<CoreContextUnitOfWork>(new CoreContextUnitOfWorkInitializer());
        }
    }
}
