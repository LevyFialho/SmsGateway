using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmsGateway.Application.CoreContext.DTO.SMSModule;
using SmsGateway.Presentation.Seedwork.Resources;
using SmsGateway.Presentation.Seedwork.Services;
using SmsGateway.Presentation.WebClient.Controllers.Shared;
using SmsGateway.Presentation.WebClient.Models;

namespace SmsGateway.Presentation.WebClient.Controllers
{   
    public class AreaDoClienteController : ClientAuthController
    {
        private AreaDoClienteModel Model()
        {
            var model = new AreaDoClienteModel();
            using (var manager = new Clientes())
            {
                if (Session["ClientAuthenticated"] != null && Convert.ToBoolean(Session["ClientAuthenticated"]))
                {
                    var clienteId = Session["ClienteId"].ToString();
                    model.Cliente = manager.GetById(clienteId);
                    @ViewBag.NomeUsuario = model.Cliente.Nome;
                    model.Contrato = manager.GetContratoDoCliente(clienteId);
                    model.Mensagens = manager.GetMensagensDoCliente(clienteId);
                    model.ListasDeContatos = manager.GetListaDeContatosDoCliente(clienteId);
                    model.Contatos = manager.GetContatosDoCliente(clienteId);
                    model.Tickets = manager.TicketsDoCliente(clienteId);
                }
                else
                {
                    RedirectToAction("Cliente", "Login");
                }
            }
            
            return model;
        }

        private AdicionarContatoModel AddContatoModel(string ListaId)
        {
            var model = Model();
            var result = new AdicionarContatoModel()
                {
                    ListaId = ListaId,
                    ContatosForaDaLista = new List<ContatoDTO>()
                };
            var lista = model.ListasDeContatos.FirstOrDefault(l => l.Id == new Guid(ListaId));
            if (lista == null) return result;
            foreach (var contato in model.Contatos)
            {
                if(lista.Contatos.All(c => c.Id != contato.Id))
                {
                    result.ContatosForaDaLista.Add(contato);
                }
            }
            return result;
        }

        private string _resultadoEnvioDeMensagem;
        //SalvarContato  ExcluirContato

        public ActionResult Index()
        {
            return View(Model());
        }

        public ActionResult TrocarSenha()
        {
            

            return View();
        }
        [HttpPost]
        public ActionResult TrocarSenha(TrocarSenhaModel model)
        {
            var clientData = Model();
            if(model.SenhaAtual != clientData.Cliente.Senha)
            {
                ViewBag.ErrorMsg = Messages.SenhaAtualErrada;
                return View();
            }
            if(model.SenhaConfirmada != model.SenhaNova)
            {
                ViewBag.ErrorMsg = Messages.ConfirmacaoSenhaErrada;
                return View();
            }
            clientData.Cliente.Senha = model.SenhaConfirmada;
            using (var manager  = new Clientes())
            {
                manager.Update(clientData.Cliente);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Contatos()
        {
            return View(Model());
        }

        public ActionResult SalvarContato(string id)
        {
            if(!string.IsNullOrWhiteSpace(id))
                return View(Model().Contatos.FirstOrDefault(c => c.Id == new Guid(id)));

            return View(new ContatoDTO());

        }

        public ActionResult ExcluirContato(string id)
        {
            return View(Model().Contatos.FirstOrDefault(c => c.Id == new Guid(id)));
        }

        public ActionResult SalvarLista(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
                return View(Model().ListasDeContatos.FirstOrDefault(c => c.Id == new Guid(id)));

            return View(new ListaDeContatosDTO());

        }

        public ActionResult ExcluirLista(string id)
        {
            return View(Model().ListasDeContatos.FirstOrDefault(c => c.Id == new Guid(id)));
        }
        
        [HttpPost]
        public ActionResult RemoverContato(string idContato, string idLista)
        {
            using (var manager = new Clientes())
                manager.RemoverContatoDaLista(idContato, idLista);
            return RedirectToAction("SalvarLista", new {id = idLista});
        }

        public ActionResult AdicionarContato(string idLista)
        { 
            return View(AddContatoModel(idLista));
        }

      
        public ActionResult AddContato(string idContato, string idLista)
        {
            using (var manager = new Clientes())
                manager.AdicionarContatoaLista(idContato, idLista);
            return RedirectToAction("AdicionarContato", new {idLista = idLista});
        }
        [HttpPost]
        public ActionResult SalvarContato(ContatoDTO dto)
        {
            dto.ClienteId = new Guid(Session["ClienteId"].ToString());
            dto.IsEnabled = true;
            using (var manager = new Clientes())
            {
                manager.SalvarContato(dto);
            }
            return RedirectToAction("Contatos");

        }
        [HttpPost]
        public ActionResult ExcluirContato(ContatoDTO dto)
        {
            dto.ClienteId = new Guid(Session["ClienteId"].ToString());
            dto.IsEnabled = false;
            using (var manager = new Clientes())
            {
                manager.ExcluirContato(dto);
            }
            return RedirectToAction("Contatos");
        }
        [HttpPost]
        public ActionResult SalvarLista(ListaDeContatosDTO dto)
        {
            dto.ClienteId = new Guid(Session["ClienteId"].ToString());
            dto.IsEnabled = true;
            using (var manager = new Clientes())
            {
                manager.SalvarLista(dto);
            }
            return RedirectToAction("Contatos");

        }
        [HttpPost]
        public ActionResult ExcluirLista(ListaDeContatosDTO dto)
        {
            dto.ClienteId = new Guid(Session["ClienteId"].ToString()); 
            using (var manager = new Clientes())
            {
                manager.ExcluirLista(dto);
            }
            return RedirectToAction("Contatos");
        }
    
        public ActionResult Ticket(string id)
        {
            var clienteId = new Guid(Session["ClienteId"].ToString());
            var ticket = new TicketDTO()
                {Data = DateTime.Now, Assunto = string.Empty, ClienteId = clienteId, IsEnabled = true, Status =  StatusDoTicketDTO.Pendente, Mensagens =  new List<MensagemDoTicketDTO>()};
            using (var manager = new Clientes())
            {
              if(!string.IsNullOrWhiteSpace(id)) ticket=  manager.TicketsDoCliente(clienteId.ToString()).FirstOrDefault(t => t.Id == new Guid(id));
            }
            var model = new TicketModel() { Ticket = ticket, NovaMensagem = new MensagemDoTicketDTO() };
            return View(model);

        }

        [HttpPost]
        public ActionResult Ticket(TicketModel dto)
        {
            dto.Ticket.ClienteId = new Guid(Session["ClienteId"].ToString());
            dto.Ticket.IsEnabled = true;
            if(!string.IsNullOrEmpty(dto.NovaMensagem.Texto))
            {
                dto.NovaMensagem.Data = DateTime.Now;
                dto.NovaMensagem.IsEnabled = true;
                dto.Ticket.Mensagens.Add(dto.NovaMensagem);
            }
            using (var manager = new Clientes())
            {
              dto.Ticket =  manager.SalvarTicket(dto.Ticket);
                
            }
            return RedirectToAction("Ticket", new { id = dto.Ticket.Id });

        }
       

        public ActionResult Ajuda()
        {
            return View(Model());
        }

        public ActionResult Documentacao()
        {
            return View(Model());
        }
         

        public ActionResult MinhasMensagens()
        {
            var model = new MinhasMensagensModel()
                {
                    DadosDoCliente = Model(),
                    Mensagem = string.Empty,
                    ContatoId = string.Empty,
                    ListaId = string.Empty,
                    Resultado = _resultadoEnvioDeMensagem
                };
            return View(model);
        }
        [HttpPost]
        public ActionResult EnviarMensagemParaContato(MinhasMensagensModel model)
        {
            model.DadosDoCliente = Model();
            using (var manager = new Clientes())
            {
              _resultadoEnvioDeMensagem =  manager.EnviarMensagemParaContato(model.DadosDoCliente.Cliente,
                                                  model.DadosDoCliente.Contatos.FirstOrDefault(
                                                      c => c.Id == new Guid(model.ContatoId)), model.Mensagem,
                                                  model.DadosDoCliente.Cliente.Nome);
            }
            return RedirectToAction("MinhasMensagens");
        }
        [HttpPost]
        public ActionResult EnviarMensagemParaLista(MinhasMensagensModel model)
        {
            model.DadosDoCliente = Model();
            
            using (var manager = new Clientes())
            {
                _resultadoEnvioDeMensagem = manager.EnviarMensagemParaLista(model.DadosDoCliente.Cliente,
                                                  model.DadosDoCliente.ListasDeContatos.FirstOrDefault(
                                                      c => c.Id == new Guid(model.ListaId)), model.Mensagem,
                                                  model.DadosDoCliente.Cliente.Nome);
            }
            return RedirectToAction("MinhasMensagens");
        }
    }
}
