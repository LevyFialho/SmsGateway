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

using System;
using System.Web.Mvc;
using SmsGateway.Application.CoreContext.DTO.SMSModule;
using SmsGateway.Presentation.WebClient.Models;

namespace SmsGateway.Presentation.WebClient.Controllers.Shared
{
    public class AdminController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var user = (AdministradorDTO)Session["Usuario"];
                if ((user != null))
                {
                    @ViewBag.NomeUsuario = user.Nome;
                    ViewData["slider"] = null; ViewData["adminMenu"] = true;
                    base.OnActionExecuting(filterContext);
                    
                }
                else
                {
                   
                    filterContext.Result = RedirectToAction("Admin", "LogIn");
                }
            }
            catch
            {
                filterContext.Result = RedirectToAction("Admin", "LogIn");
            }

        }

        public ActionResult Logoff()
        {
            
                Session["Usuario"] = null;
                return RedirectToAction("Admin", "LogIn");

           
        }

    }

    public class ClientAuthController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {

                if (Session["ClientAuthenticated"] != null && Convert.ToBoolean(Session["ClientAuthenticated"]))
                {
                    
                    ViewData["slider"] = null; ViewData["clienteMenu"] = true;
                    base.OnActionExecuting(filterContext);

                }
                else
                {

                    filterContext.Result = RedirectToAction("Cliente", "LogIn");
                }
            }
            catch
            {
                filterContext.Result = RedirectToAction("Cliente", "LogIn");
            }

        }

        public ActionResult Logoff()
        {

            Session["ClientAuthenticated"] = null;
            Session["clienteId"] = null;
            return RedirectToAction("Cliente", "LogIn");


        }
 
    }
}
