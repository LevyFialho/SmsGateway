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
using SmsGateway.Presentation.WebClient.Controllers.Shared;

namespace SmsGateway.Presentation.WebClient.Controllers
{
    public class HomeController: Controller
    {
        public ActionResult Index()
        {
            ViewData["slider"] = true;
            ViewData["adminMenu"] = null;
            return View();
        }

      

       
    }
}
