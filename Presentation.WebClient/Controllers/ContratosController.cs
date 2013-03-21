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
using System.Collections.Generic;
using System.Web.Mvc;
using SmsGateway.Application.CoreContext.DTO.SMSModule;
using SmsGateway.Presentation.Seedwork.Services;
using SmsGateway.Presentation.WebClient.Controllers.Shared;
using SmsGateway.Presentation.WebClient.Models;
namespace SmsGateway.Presentation.WebClient.Controllers
{
    public class ContratosController : AdminController
    {
       



        //
        // GET: /Clientes/Edit/5

        public ActionResult Edit(string clienteId)
        {

            using (var manager = new Clientes())
            {
                var contrato = manager.GetContratoDoCliente(clienteId);
                return View(contrato);
            }

        }

        //
        // POST: /Clientes/Edit/5

        [HttpPost]
        public ActionResult Edit(ContratoDTO contrato)
        {
            //if (!Autorizar()) return RedirectToAction("Index", "LogIn");
            if (ModelState.IsValid)
            {
                using (var manager = new Clientes())
                {
                    manager.RenovarContrato(contrato);

                    return RedirectToAction("Contrato", "Clientes", new {id = contrato.ClienteId});
                }

            }
            return View(contrato);
        }



        //
        // GET: /Clientes/Inativar/5

        public ActionResult Inativar(string id)
        {
            using (var manager = new Clientes())
            {
                var cliente = manager.Inativar(id);
                return RedirectToAction("Details", new { id = id });

            }


        }

        //
        // GET: /Clientes/Ativar/5

        public ActionResult Ativar(string id)
        {

            using (var manager = new Clientes())
            {

                manager.Ativar(id);

                return RedirectToAction("Details", new { id = id });

            }

        }


        // GET: /Clientes/Contrato

        public ActionResult Contrato(string id)
        {


            using (var manager = new Clientes())
            {
                var contrato = manager.GetContratoDoCliente(id);

                return View(contrato);
            }



        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }



    }
}
