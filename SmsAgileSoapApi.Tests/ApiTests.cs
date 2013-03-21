using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmsAgileSoapApi.Tests
{
    [TestClass]
    public class ApiTests
    {

        private const string ClienteId = "82d87721-2ab4-c3e9-a928-08cff0f797d7";
        private const string Senha = "@infnet";

        private SmsAgileSoapApi.Service Service
        {
            get { return new Service(ClienteId, Senha); }
        }

        #region Contatos
        [TestMethod]
        public void TesteAdicionarContato()
        {
            var contato = Service.AdicionarContato(new SmsAgileSoapApi.Contato("CONTATOTESTE", 99));
            Assert.IsTrue(contato.Id != Guid.Empty);
        }

        
        [TestMethod]
        public void TesteEditarContato()
        {
            var contato = Service.AdicionarContato(new SmsAgileSoapApi.Contato("CONTATOTESTE2", 1));
            var contatos = Service.ListarContatos().Where(c => c.Numero == 1);
            foreach (var c in contatos)
            {
                c.Numero = 2;
                Service.AtualizarContato(c);
            }
            Assert.IsTrue(Service.ListarContatos().Any(c => c.Numero == 2));

        }

        [TestMethod]
        public void TesteRemoverContato()
        {
            var contatos = Service.ListarContatos();
            foreach (var contato in contatos)
            {
                Service.RemoverContato(contato.Id);
            }
            Assert.AreEqual(Service.ListarContatos().Count, 0);
        }
        #endregion

        #region Listas de Contatos
        [TestMethod]
        public void TesteAdicionarLista()
        {
            var lista = new ListaDeContatos("TESTELISTA1");
            lista.Contatos.Add(new Contato("TESTECONTATOLISTA1", 3));
            Service.AdicionarListaDeContatos(lista);
        }
        [TestMethod]
        public void TesteAdicionarListasComMesmoContato()
        {
            var lista = new ListaDeContatos("TESTELISTA2");
            lista.Contatos.Add(new Contato("TESTECONTATOLISTA2", 4));
            Service.AdicionarListaDeContatos(lista);
            var lista2 = new ListaDeContatos("TESTELISTA3");
            lista2.Contatos.Add(new Contato("TESTECONTATOLISTA2", 4));
            Service.AdicionarListaDeContatos(lista2);
        }
         
        [TestMethod]
        public void TesteEditarLista()
        {
            var lista = Service.ListarListasDeContatos().FirstOrDefault();
            if (lista == null) return;
            lista.Nome = "EDITADA";
            lista.Contatos = new List<Contato>();
            Service.UpdateListaDeContatos(lista);
        }

        [TestMethod]
        public void TesteRemoverLista()
        {
            var lista = Service.ListarListasDeContatos().FirstOrDefault();
            if (lista == null) return;
            Service.RemoverListaDeContatos(lista);
            
        }
        #endregion

        #region Mensagens

        [TestMethod]
        public void TesteEnviarMensagem()
        {
            var mensagem = new SmsAgileSoapApi.Mensagem("TESTE UNITARIO", 552195389956, "SIMPLES");
            Service.EnviarMensagem(mensagem);
        }

        [TestMethod]
        public void TesteEnviarMensagemParaContatos()
        {
            var result = Service.EnviarMensagemParaContatos("TESTE CONTATOS 2","R", new List<Contato>(){new Contato("TESTECONTATOS", 552195389956)});
        }

        [TestMethod]
        public void TesteEnviarMensagemParaLista()
        {
        }

        #endregion

        #region Saldo
        [TestMethod]
        public void TesteGetSaldo()
        {
            var saldo = Service.Saldo();
            Assert.IsTrue(saldo > 0);
        }
        #endregion


    }
}
