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
using SmsGateway.Application.CoreContext.DTO.SMSModule;
using RestfulServices = SmsGateway.Presentation.Seedwork.Services.Restful;
using SmsGateway.Presentation.Seedwork.Resources;
namespace SmsGateway.Presentation.Seedwork.Services
{
    public class Administradores : IDisposable
    {

        /// <summary>
        /// Login do sistema
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <param name="errorMessage">Será preenchida em caso de falha.</param>
        /// <returns> Retorna um objeto nulo e a mensagem de erro preenchida em caso de falha.
        /// Em caso de sucesso, retorna um objeto com as informações do administrador.</returns>
        public AdministradorDTO Login(string email, string senha, ref string errorMessage)
        {
            try
            {
                using (var service = RestfulServices.Factory.RestServiceAdministradores())
                {
                    var admin = service.ListAdministrador().FirstOrDefault(a => a.Email == email && a.Senha == senha);
                     
                    if (admin == null)
                        errorMessage = Messages.LoginInvalid;
                    
                    else if (admin.IsEnabled == false)
                        errorMessage = Messages.LoginNotEnabled;
                    return admin;
                }
            }
            catch (Exception e)
            {
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                errorMessage += e.Message;
                return null;
            }

        }

        /// <summary>
        /// Login do sistema
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <param name="errorMessage">Será preenchida em caso de falha.</param>
        /// <returns> Retorna um objeto nulo e a mensagem de erro preenchida em caso de falha.
        /// Em caso de sucesso, retorna um objeto com as informações do administrador.</returns>
        public ClienteDTO LoginCliente(string email, string senha, ref string errorMessage)
        {
            try
            {
                using (var service = RestfulServices.Factory.RestServiceAdministradores())
                {
                    var cliente = service.ListCliente().FirstOrDefault(c => c.Email == email & c.Senha == senha);
                    if(cliente == null)
                    {
                        errorMessage = Messages.LoginInvalid;
                        return null;
                    }
                      if (cliente.IsEnabled == false)
                        errorMessage = Messages.LoginNotEnabled;
                    
                    return cliente;
                }
            }
            catch (Exception e)
            {
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                errorMessage += e.Message;
                return null;
            }

        }
        ///<summary>
        ///Acessa a camada de serviços rest e busca uma lista com todos os administradores do sistema 
        /// </summary>
        /// <returns></returns>
        public List<AdministradorDTO> GetAll()
        {
            try
            {
                using (var service = RestfulServices.Factory.RestServiceAdministradores())
                {
                    var admin = service.ListAdministrador();

                    return admin;
                }
            }
            catch (Exception e)
            {
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                //errorMessage += e.Message;
                return null;
            }

        }
        /// <summary>
        /// Acessa a camada de serviços rest e busca um administrador do sistema por ID
        /// </summary>
        /// <returns></returns>
        public AdministradorDTO GetById(string id)
        {
            try
            {
                using (var service = RestfulServices.Factory.RestServiceAdministradores())
                {
                    var admin = service.GetAdministrador(id);

                    return admin;
                }
            }
            catch (Exception e)
            {
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                //errorMessage += e.Message;
                return null;
            }

        }

        /// <summary>
        /// Acessa a camada de serviços rest e cria um administrador novo
        /// </summary>
        /// <returns></returns>
        public AdministradorDTO Create(AdministradorDTO admin, ref string errorMessage)
        {
            try
            {
                using (var service = RestfulServices.Factory.RestServiceAdministradores())
                {
                    //Validar email único

                    admin = service.CreateAdministrador(admin);
                    if (admin == null)
                        errorMessage = Messages.GeneralException;

                    return admin;
                }
            }
            catch (Exception e)
            {
                //LoggerFactory.CreateLog().LogError("{0}", e.Message + e.StackTrace);
                //errorMessage += e.Message;
                return null;
            }

        }

        /// <summary>
        /// Acessa a camada de serviços rest e atualiza um administrador 
        /// </summary>
        /// <returns></returns>
        public AdministradorDTO Update(AdministradorDTO admin)
        {
            try
            {
                using (var service = RestfulServices.Factory.RestServiceAdministradores())
                {
                    service.UpdateAdministrador(admin);

                    return admin;
                }
            }
            catch (Exception e)
            {
                //var log = //LoggerFactory.CreateLog();
                //if(log !=null)
                //    log.LogError("{0}", e.Message + e.StackTrace);
                ////errorMessage += e.Message;
                return null;
            }

        }

        /// <summary>
        /// Acessa a camada de serviços rest e eclui do sistema um administrador 
        /// </summary>
        /// <returns></returns>
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Acessa a camada de serviços rest e inativa um administrador 
        /// </summary>
        /// <returns></returns>
        public AdministradorDTO Inativar(string id)
        {
            using (var service = Restful.Factory.RestServiceAdministradores())
            {
                var admin = service.GetAdministrador(id);
             
                service.DisableAdministrador(admin);
                return admin;
            }
        }

        /// <summary>
        /// Acessa a camada de serviços rest e  ativa um administrador 
        /// </summary>
        /// <returns></returns>
        public AdministradorDTO Ativar(string id)
        {
            using (var service = Restful.Factory.RestServiceAdministradores())
            {
                var admin = service.GetAdministrador(id);
                 
                service.UpdateAdministrador(admin);
                return admin;
            }
        }

        public TicketDTO SalvarTicket(TicketDTO ticket)
        {
            using (var service = Restful.Factory.RestServiceAdministradores())
            {
                return service.SalvaTicket(ticket);


            }
        }

        public List<TicketDTO> ListarTickets()
        {
            try
            {
                using (var service = Restful.Factory.RestServiceAdministradores())
                {
                    return service.Tickets();


                }
            }
            catch (Exception)
            {
                    return new List<TicketDTO>();
                
            }
        }

        public List<SolicitacaoDeCadastroDTO> ListarSolicitacoes()
        {
            try
            {
                using (var service = Restful.Factory.RestServiceAdministradores())
                {
                    return service.ListaSolicitacoesDeCadastro().Where(s => s.IsEnabled).ToList();


                }
            }
            catch (Exception)
            {
                return new List<SolicitacaoDeCadastroDTO>();

            }
        }
        public SolicitacaoDeCadastroDTO SolicitacaoDeCadastro(string id)
        {
            try
            {
                using (var service = Restful.Factory.RestServiceAdministradores())
                {
                    return service.ListaSolicitacoesDeCadastro().FirstOrDefault(s => s.Id == new Guid(id));


                }
            }
            catch (Exception)
            {
                return new  SolicitacaoDeCadastroDTO();

            }
        }

        public void SalvaSolicitacaoDeCadastro(SolicitacaoDeCadastroDTO dto)
        {
            
                using (var service = Restful.Factory.RestServiceAdministradores())
                {
                     service.AddSolicitacaoDeCadastro(dto);


                }
             
        }

        public bool RecuperarSenha(string email)
        {

            using (var service = Restful.Factory.RestServiceAdministradores())
            {
                var administrador = service.ListCliente().FirstOrDefault(c => c.Email == email);
                if(administrador == null) return false;
                service.RecuperarSenha(administrador.Id.ToString());
                return true;
            }

        }

        public void RemoveSolicitacaoDeCadastro(string id)
        {

            using (var service = Restful.Factory.RestServiceAdministradores())
            {
                service.RemoveSolicitacaoDeCadastro(id);

            }

        }
        public TicketDTO Ticket(string id)
        {
            try
            {
                using (var service = Restful.Factory.RestServiceAdministradores())
                {
                    return service.Ticket(id);


                }
            }
            catch (Exception)
            {
                return null;

            }
        }
        public bool StartDatabase()
        {
            using (var service = Restful.Factory.RestServiceAdministradores())
            {
                return service.StartAppDatabase();

            }
        }

        public void Dispose()
        {

            GC.SuppressFinalize(this);

        }
    }
}
