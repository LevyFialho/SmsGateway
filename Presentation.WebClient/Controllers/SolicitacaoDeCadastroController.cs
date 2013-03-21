using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmsGateway.Application.CoreContext.DTO.SMSModule;
using SmsGateway.Presentation.Seedwork.Resources;
using SmsGateway.Presentation.Seedwork.Services;
using SmsGateway.Presentation.WebClient.Models;

namespace SmsGateway.Presentation.WebClient.Controllers
{
    public class SolicitacoesDeCadastroController : Controller
    {

         

        public ActionResult Details(string id)
        {
            var model = new SolicitacaoDeCadastroDTO() { Data = DateTime.Now };
            using (var manager = new Administradores())
            {
                if (!string.IsNullOrWhiteSpace(id)) model = manager.SolicitacaoDeCadastro(id);
            }
             return View(model);
        }

        public ActionResult Remove(string id)
        {
            using (var manager = new Administradores())
            {
                manager.RemoveSolicitacaoDeCadastro(id);
            }
            return RedirectToAction("About", "Administradores");
        }

        public ActionResult Create()
        {

            var model = new SolicitacaoDeCadastroDTO()
                {Data = DateTime.Now, Email = string.Empty, IsEnabled = true, Nome = string.Empty};
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SolicitacaoDeCadastroDTO model)
        {
            
            using (var manager = new Administradores())
            {
              manager.SalvaSolicitacaoDeCadastro(model);
            }
            return RedirectToAction("Details", new { id = model.Id });
        }


        public ActionResult RecuperarSenha()
        {
           
            var model = new RecuperarSenhaModel(){Email = string.Empty, Mensagem = string.Empty};
            return View(model);
        }

        [HttpPost]
        public ActionResult RecuperarSenha(RecuperarSenhaModel model)
        {

            using (var manager = new Administradores())
            {
                manager.RecuperarSenha(model.Email);
                model.Mensagem = Messages.RecuperouSenha;
            }
            return View(model);
        }

    }
}
