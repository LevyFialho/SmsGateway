using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmsGateway.Application.CoreContext.DTO.SMSModule;
using SmsGateway.Presentation.Seedwork.Services;
using SmsGateway.Presentation.WebClient.Controllers.Shared;
using SmsGateway.Presentation.WebClient.Models;

namespace SmsGateway.Presentation.WebClient.Controllers
{
    public class TicketsController : AdminController
    {
        //
        // GET: /Tickets/

        public ActionResult Index()
        {
            using (var manager = new Administradores())
            {
                var tickets = manager.ListarTickets();
                return View(tickets);
            }
        }

        public ActionResult Ticket(string id)
        {
            var ticket = new TicketDTO() { Data = DateTime.Now, Assunto = string.Empty,   IsEnabled = true, Status = StatusDoTicketDTO.Pendente, Mensagens = new List<MensagemDoTicketDTO>() };
            using (var manager = new Administradores())
            {
                if (!string.IsNullOrWhiteSpace(id)) ticket = manager.Ticket(id);
            }
            var model = new TicketModel() { Ticket = ticket, NovaMensagem = new MensagemDoTicketDTO() };
            return View(model);
        }

        [HttpPost]
        public ActionResult Ticket(TicketModel model)
        {
            if (!string.IsNullOrEmpty(model.NovaMensagem.Texto))
            {
                model.NovaMensagem.Data = DateTime.Now;
                model.NovaMensagem.IsEnabled = true;
                model.Ticket.Mensagens.Add(model.NovaMensagem);
            }
            using (var manager = new Administradores())
            {
                model.Ticket = manager.SalvarTicket(model.Ticket);
            }
            return RedirectToAction("Ticket", new {id = model.Ticket.Id});
        }
    }
}
