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

namespace SmsGateway.Presentation.WebClient.Controllers
{
    public class AdministradoresController : AdminController
    {


        public ActionResult Index()
        {


            using (var manager = new Administradores())
            {
                var lista = manager.GetAll();

                return View(lista);
            }



        }



        public ActionResult Details(string id)
        {

            using (var manager = new Administradores())
            {
                var entity = manager.GetById(id);

                return View(entity);
            }
        }


        public ActionResult Create()
        {

            return View();
        }



        [HttpPost]
        public ActionResult Create(AdministradorDTO entity)
        {

            if (ModelState.IsValid)
            {
                using (var manager = new Administradores())
                {
                    var errorMessage = "";
                    manager.Create(entity, ref errorMessage);
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        @ViewBag.ErrorMsg = errorMessage;
                        return View(entity);
                    }

                    return RedirectToAction("Index");



                }


            }

            return View(entity);
        }


        public ActionResult Edit(string id)
        {

            using (var manager = new Administradores())
            {
                var entity = manager.GetById(id);
                return View(entity);
            }

        }



        [HttpPost]
        public ActionResult Edit(AdministradorDTO entity)
        {
            if (ModelState.IsValid)
            {
                using (var manager = new Administradores())
                {
                    manager.Update(entity);

                    return RedirectToAction("Index");
                }

            }
            return View(entity);
        }



        public ActionResult Inativar(string id)
        {
            using (var manager = new Administradores())
            {
                var usuarios = manager.Inativar(id);
                return RedirectToAction("Details", new { id = id });

            }


        }


        public ActionResult Ativar(string id)
        {

            using (var manager = new Administradores())
            {

                manager.Ativar(id);

                return RedirectToAction("Details", new { id = id });

            }

        }


        public ActionResult Solicitacoes()
        {


            ViewData["slider"] = null; ViewData["adminMenu"] = true;
            using (var manager = new Administradores())
            {
                var model = manager.ListarSolicitacoes();
                return View(model);
            }

        }


        [HttpPost]
        public ActionResult About(Models.SmsTestModel model)
        {

            if (ModelState.IsValid)
            {
                using (var manager = new Clientes())
                {


                    return RedirectToAction("Index");



                }


            }

            return View(model);
        }


        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }


    }
}
