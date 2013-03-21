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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SmsGateway.Application.CoreContext.DTO.SMSModule;
using SmsGateway.Presentation.Seedwork.Resources;

namespace SmsGateway.Presentation.Seedwork.Services
{
    public class Clientes : IDisposable
    {


        ///<summary>
        ///Acessa a camada de serviços rest e busca uma lista com todos os clientes do sistema 
        /// </summary>
        /// <returns></returns>
        public List<ClienteDTO> GetAll()
        {
            try
            {
                using (var service = Restful.Factory.RestServiceAdministradores())
                {
                    var clientes = service.ListCliente();

                    return clientes;
                }
            }
            catch (Exception e)
            {
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                return null;
            }

        }

        ///<summary>
        ///Acessa a camada de serviços rest e busca uma lista com todos os clientes do sistema 
        /// </summary>
        /// <returns></returns>
        public ContratoDTO GetContratoDoCliente(string id)
        {
            try
            {
                using (var service = Restful.Factory.RestServiceClientes())
                {
                    var contrato = service.GetContratoAtual(id);

                    return contrato;
                }
            }
            catch (Exception e)
            {
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                return null;
            }

        }

        public List<ListaDeContatosDTO> GetListaDeContatosDoCliente(string id)
        {
            try
            {
                using (var service = Restful.Factory.RestServiceClientes())
                {
                    var listas = service.ListListasDeContatos().Where(l => l.ClienteId.ToString() == id);

                    return listas.ToList();
                }
            }
            catch (Exception e)
            {
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                return new List<ListaDeContatosDTO>();
            }

        }

        public List<MensagemDTO> GetMensagensDoCliente(string id)
        {
            try
            {
                using (var service = Restful.Factory.RestServiceClientes())
                {
                    var listas = service.GetMensagensEnviadas(id);

                    return listas.ToList();
                }
            }
            catch (Exception e)
            {
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                return new List<MensagemDTO>();
            }

        }
        
        public List<ContatoDTO> GetContatosDoCliente(string id)
        {
            try
            {
                using (var service = Restful.Factory.RestServiceClientes())
                {
                    var contatos = service.ContatosDoCliente(new Guid(id));

                    return contatos.ToList();
                }
            }
            catch (Exception e)
            {
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                return new List<ContatoDTO>();
            }

        }

        public List<TicketDTO> TicketsDoCliente(string clienteId)
        {
            try
            {
                using (var service = Restful.Factory.RestServiceClientes())
                {
                    var tickets = service.TicketsDoCliente(clienteId);

                    return tickets.ToList();
                }
            }
            catch (Exception e)
            {
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                return new List<TicketDTO>();
            }
        }

        public TicketDTO SalvarTicket(TicketDTO ticket)
        {
            using (var service = Restful.Factory.RestServiceClientes())
            {
               return  service.SalvaTicket(ticket);
                
            }
        }
       
        /// <summary>
        /// Acessa a camada de serviços rest e busca um cliente do sistema por ID
        /// </summary>
        /// <returns></returns>
        public ClienteDTO GetById(string id)
        {
            try
            {
                using (var service = Restful.Factory.RestServiceAdministradores())
                {
                    var cliente = service.GetCliente(id);

                    return cliente;
                }
            }
            catch (Exception e)
            {
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                return null;
            }

        }

        /// <summary>
        /// Acessa a camada de serviços rest e cria um cliente novo
        /// </summary>
        /// <returns></returns>
        public ClienteDTO Create(ClienteDTO cliente, ContratoDTO contrato, ref string errorMessage)
        {
            try
            {
                using (var service = Restful.Factory.RestServiceAdministradores())
                {

                    cliente = service.CreateCliente(cliente);
                    if (cliente == null)
                    {
                        errorMessage = Resources.Messages.GeneralException;
                        return null;
                    }
                    contrato.ClienteId = cliente.Id;
                    contrato.TipoDeContrato = TipoDeContratoDTO.Cliente;
                    contrato = service.CreateContrato(contrato);
                    if (contrato == null)
                    {
                        errorMessage = Resources.Messages.GeneralException;
                        return null;
                    }
                    return cliente;
                }
            }
            catch (Exception e)
            {
                errorMessage = Resources.Messages.GeneralException;
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                return null;
            }

        }

        /// <summary>
        /// Acessa a camada de serviços rest e atualiza um cliente 
        /// </summary>
        /// <returns></returns>
        public ClienteDTO Update(ClienteDTO cliente)
        {
            try
            {
                using (var service = Restful.Factory.RestServiceAdministradores())
                {
                    service.UpdateCliente(cliente);

                    return cliente;
                }
            }
            catch (Exception e)
            {
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                return null;
            }

        }

        /// <summary>
        /// Acessa a camada de serviços rest e atualiza um cliente 
        /// </summary>
        /// <returns></returns>
        public ContratoDTO RenovarContrato(ContratoDTO contrato)
        {
            try
            {
                using (var service = Restful.Factory.RestServiceAdministradores())
                {
                    service.RenovarContrato(contrato);

                    return contrato;
                }
            }
            catch (Exception e)
            {
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                return null;
            }

        }

        /// <summary>
        /// Acessa a camada de serviços rest e inativa um cliente
        /// </summary>
        /// <returns></returns>
        public ClienteDTO Inativar(string id)
        {
            try
            {
                using (var service = Restful.Factory.RestServiceAdministradores())
                {
                    var cliente = service.GetCliente(id);
                    
                    service.DisableCliente(cliente);

                    return cliente;
                }
            }
            catch (Exception e)
            {
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Acessa a camada de serviços rest e  ativa um cliente 
        /// </summary>
        /// <returns></returns>
        public ClienteDTO Ativar(string id)
        {
            try
            {
                using (var service = Restful.Factory.RestServiceAdministradores())
                {
                    var cliente = service.GetCliente(id);
                     
                    service.UpdateCliente(cliente);

                    return cliente;
                }
            }
            catch (Exception e)
            {
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                return null;
            }

        }

        public void SalvarContato(ContatoDTO dto)
        { 
            using (var service = Restful.Factory.RestServiceAdministradores())
            {
                if(dto.Id != Guid.Empty)
                    service.UpdateContato(dto);
                else
                {
                    service.CreateContato(dto);
                }

            }
        }

        public void ExcluirContato(ContatoDTO dto)
        {
            using (var service = Restful.Factory.RestServiceAdministradores())
            {
                if (dto.Id != Guid.Empty)
                    service.DisableContato(dto);
                 

            }
        }

        public void SalvarLista(ListaDeContatosDTO dto)
        {
            using (var service = Restful.Factory.RestServiceAdministradores())
            {
                if (dto.Id != Guid.Empty)
                    service.UpdateListaDeContatos(dto);
                else
                {
                    service.CreateListaDeContatos(dto);
                }

            }
        }

        public void ExcluirLista(ListaDeContatosDTO dto)
        {
            using (var service = Restful.Factory.RestServiceAdministradores())
            {
                if (dto.Id != Guid.Empty)
                    service.DisableListaDeContatos(dto);


            }
        }

        public void RemoverContatoDaLista(string idContato, string idLista)
        {
            using (var service = Restful.Factory.RestServiceAdministradores())
            {
                if (!string.IsNullOrWhiteSpace(idContato) && !string.IsNullOrWhiteSpace(idLista))
                    service.RemoveContatoFromList(idContato, idLista);
            }
        }

        public void AdicionarContatoaLista(string idContato, string idLista)
        {
            using (var service = Restful.Factory.RestServiceAdministradores())
            {
                if (!string.IsNullOrWhiteSpace(idContato) && !string.IsNullOrWhiteSpace(idLista))
                    service.AddContatoToList(idContato, idLista);
            }
        }

        public void Dispose()
        {

            GC.SuppressFinalize(this);

        }

        public MensagemDTO EnviarMensagem(AutenticacaoDTO autenticacao, string destinatario, string texto)
        {
            try
            {
                using (var service = Restful.Factory.RestServiceClientes())
                {
                    var msg = service.EnviarMensagemString(autenticacao, destinatario, texto);
                        
                    return msg;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string EnviarMensagemParaContato(ClienteDTO cliente, ContatoDTO contato, string texto, string remetente)
        {
            try
            {
                using (var service = Restful.Factory.RestServiceApi())
                {
                    var lista = new List<ContatoDTO>() {contato};
                    var autenticacao = new AutenticacaoDTO()
                    {
                        Id = cliente.Id.ToString(),
                        Senha = cliente.Senha
                    };
                    service.EnviarMensagemParaContatos(autenticacao, texto, remetente, lista);


                }
                return Messages.MensagensEnvidasComSucesso;
            }
            catch (Exception e)
            {
                return Messages.MensagemEnviadaComErro;
            }
        }

        public string EnviarMensagemParaLista(ClienteDTO cliente, ListaDeContatosDTO lista, string texto, string remetente)
        {
            try
            {
                using (var service = Restful.Factory.RestServiceApi())
                {

                    var autenticacao = new AutenticacaoDTO()
                        {
                            Id = cliente.Id.ToString(),
                            Senha = cliente.Senha
                        };
                    service.EnviarMensagemParaContatos(autenticacao, texto, remetente, lista.Contatos);


                }
                return Messages.MensagensEnvidasComSucesso;
            }
            catch (Exception e)
            {
                return Messages.MensagemEnviadaComErro;
            }
        }

    }

}
