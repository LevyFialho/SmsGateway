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
    public class ClientesController : AdminController
    {
        //
        // GET: /Clientes/

        public ActionResult Index()
        {


            using (var manager = new Clientes())
            {
                List<ClienteDTO> clientes = manager.GetAll();

                return View(clientes);
            }



        }

        //
        // GET: /Clientes/Details/5

        public ActionResult Details(string id)
        {

            using (var manager = new Clientes())
            {
                var cliente = manager.GetById(id);

                return View(cliente);
            }
        }

        //
        // GET: /Clientes/Create

        public ActionResult Create()
        {

            return View();
        }

        //
        // POST: /Clientes/Create

        [HttpPost]
        public ActionResult Create(CreateClientesModel model)
        {

            if (ModelState.IsValid)
            {
                using (var manager = new Clientes())
                {
                    var errorMessage = "";
                    manager.Create(model.Cliente, model.ContratoAtual, ref errorMessage);
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        @ViewBag.ErrorMsg = errorMessage;
                        return View(model);
                    }
                 
                    return RedirectToAction("Index");



                }


            }

            return View(model);
        }

        //
        // GET: /Clientes/Edit/5

        public ActionResult Edit(string id)
        {

            using (var manager = new Clientes())
            {
                var cliente = manager.GetById(id);
                return View(cliente);
            }

        }

        //
        // POST: /Clientes/Edit/5

        [HttpPost]
        public ActionResult Edit(ClienteDTO cliente)
        {
            //if (!Autorizar()) return RedirectToAction("Index", "LogIn");
            if (ModelState.IsValid)
            {
                using (var manager = new Clientes())
                {
                    manager.Update(cliente);

                    return RedirectToAction("Index");
                }

            }
            return View(cliente);
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
