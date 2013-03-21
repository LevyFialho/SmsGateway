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

using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.AdministradorAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContatoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg.StatusAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.PacoteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.TicketsAgg;

namespace SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork.Mapping;
    using SmsGateway.Infrastructure.Data.Seedwork;

    public class CoreContextUnitOfWork
        :DbContext,IQueryableUnitOfWork
    {


        #region IDbSet Members

        IDbSet<Administrador> _administradores;
        public IDbSet<Administrador> Administradores
        {
            get { return _administradores ?? (_administradores = base.Set<Administrador>()); }
        }

        IDbSet<Cliente> _clientes;
        public IDbSet<Cliente> Clientes
        {
            get { return _clientes ?? (_clientes = base.Set<Cliente>()); }
        }

        IDbSet<Contato> _contatos;
        public IDbSet<Contato> Contatos
        {
            get { return _contatos ?? (_contatos = base.Set<Contato>()); }
        }

        IDbSet<ListaDeContatos> _listasDeContatos;
        public IDbSet<ListaDeContatos> ListasDeContatos
        {
            get { return _listasDeContatos ?? (_listasDeContatos = base.Set<ListaDeContatos>()); }
        }
 

        IDbSet<Contrato> _contratos;
        public IDbSet<Contrato> Contratos
        {
            get { return _contratos ?? (_contratos = base.Set<Contrato>()); }
        }

        IDbSet<Mensagem> _mensagens;
        public IDbSet<Mensagem> Mensagens
        {
            get { return _mensagens ?? (_mensagens = base.Set<Mensagem>()); }
        }

        IDbSet<Status> _status;
        public IDbSet<Status> Status
        {
            get { return _status ?? (_status = base.Set<Status>()); }
        }

        IDbSet<Pacote> _pacote;
        public IDbSet<Pacote> Pacotes
        {
            get { return _pacote ?? (_pacote = base.Set<Pacote>()); }
        }

        IDbSet<Ticket> _tickets;
        public IDbSet<Ticket> Tickets
        {
            get { return _tickets ?? (_tickets = base.Set<Ticket>()); }
        }

        IDbSet<MensagemDoTicket> _msgDosTickets;
        public IDbSet<MensagemDoTicket> MensagensDoTicket
        {
            get { return _msgDosTickets ?? (_msgDosTickets = base.Set<MensagemDoTicket>()); }
        }

        IDbSet<SolicitacaoDeCadastro> _solicitacoes;
        public IDbSet<SolicitacaoDeCadastro> Solicitacoes
        {
            get { return _solicitacoes ?? (_solicitacoes = base.Set<SolicitacaoDeCadastro>()); }
        }
        #endregion

        #region IQueryableUnitOfWork Members

        public DbSet<TEntity> CreateSet<TEntity>()
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void Attach<TEntity>(TEntity item) 
            where TEntity : class
        {
            
            base.Entry<TEntity>(item).State = System.Data.EntityState.Unchanged;
        }

        public void SetModified<TEntity>(TEntity item) 
            where TEntity : class
        {
           
            base.Entry<TEntity>(item).State = System.Data.EntityState.Modified;
        }
        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
            where TEntity : class
        {
            
            base.Entry<TEntity>(original).CurrentValues.SetValues(current);
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                               {
                                   entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                               });

                }
            } while (saveFailed);

        }

        public void RollbackChanges()
        {
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = System.Data.EntityState.Unchanged);
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return base.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion

        #region DbContext Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remover as conventions que não são usadas
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Adiciona a configuração das entidades de maneira estruturada usando as classes 'TypeConfiguration' em Mapping
            modelBuilder.Configurations.Add(new AdministradoresEntityConfiguration());
            modelBuilder.Configurations.Add(new ClientesEntityConfiguration());
            modelBuilder.Configurations.Add(new ContratosEntityConfiguration());
            modelBuilder.Configurations.Add(new MensagensEntityConfiguration());
            modelBuilder.Configurations.Add(new ContatosEntityConfiguration());
            modelBuilder.Configurations.Add(new ListaDeContatosEntityConfiguration());
            modelBuilder.Configurations.Add(new PacoteEntityConfiguration());
            modelBuilder.Configurations.Add(new TicketsEntityConfiguration());
            modelBuilder.Configurations.Add(new MensagemDoTicketEntityConfiguration());
            modelBuilder.Configurations.Add(new SolicitacaoDeCadastroEntityConfiguration());
        }
        #endregion

        #region Public methods
       
        #endregion
    }
}
