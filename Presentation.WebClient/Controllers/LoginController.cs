using System;
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
using System.Web.Mvc;
using SmsGateway.Presentation.Seedwork.Services;
using SmsGateway.Presentation.WebClient.Models;

namespace SmsGateway.Presentation.WebClient.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Admin()
        {
            return View();
        }
        //
        // POST: /LogIn/Create

        [HttpPost]
        public ActionResult Admin(LoginModel model)
        {
            try
            {
                using (var manager = new Administradores())
                {
                    string errorMessage = "";
                    var user = manager.Login(model.Email, model.Senha, ref errorMessage);
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        @ViewBag.LoginMsg = errorMessage;
                        return View();
                    }
                    else
                    {
                        Session["Usuario"] = user;
                        return RedirectToAction("Solicitacoes", "Administradores");

                    }


                }
            }
            catch (Exception ex)
            {

                return View();
            }
        }

        //
        // GET: /Login/

        public ActionResult Cliente()
        {
            return View();
        }
        //
        // POST: /LogIn/Create

        [HttpPost]
        public ActionResult Cliente(LoginModel model)
        {
            try
            {
                using (var manager = new Administradores())
                {
                    string errorMessage = "";
                    var user = manager.LoginCliente(model.Email, model.Senha, ref errorMessage);
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        @ViewBag.LoginMsg = errorMessage;
                        return View();
                    }
                    else
                    {
                        Session["ClientAuthenticated"] = true;
                        Session["ClienteId"] = user.Id;
                        return RedirectToAction("Index", "AreaDoCliente");

                    }


                }
            }
            catch (Exception ex)
            {

                return View();
            }
        }

    }
}
