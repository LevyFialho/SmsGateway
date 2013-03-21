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


using System.Data.Entity.ModelConfiguration;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.TicketsAgg;

namespace SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork.Mapping
{
    

    /// <summary>
    /// Define a configuração de mapeamento de um determinado tipo de entidade
    /// </summary>
    class TicketsEntityConfiguration
        : EntityTypeConfiguration<Ticket>
    {
        /// <summary>
        /// Cria uma nova instância da entity configuration
        /// </summary>
        public TicketsEntityConfiguration()
        {
            //configurar chaves e propriedades
            this.HasKey(c => c.Id); 

            this.Property(c => c.ClienteId)
                .IsRequired();

            this.Property(c => c.Assunto) 
                .IsRequired();

            this.Property(c => c.Data)
                .IsRequired();

            
            this.Property(c => c.IsEnabled)
                .IsRequired();

            //configurar relacionamentos
            this.HasRequired(c => c.Cliente)
                .WithMany(c => c.Tickets)
                .HasForeignKey(c => c.ClienteId) 
                .WillCascadeOnDelete(false);
             
           
            //configurar tabela
            this.ToTable("Tickets");
        }
    }



    class MensagemDoTicketEntityConfiguration
        : EntityTypeConfiguration<MensagemDoTicket>
    {
        /// <summary>
        /// Cria uma nova instância da entity configuration
        /// </summary>
        public MensagemDoTicketEntityConfiguration()
        {
            //configurar chaves e propriedades
            this.HasKey(c => c.Id);

            this.Property(c => c.TicketId)
                .IsRequired();

            this.Property(c => c.Texto)
                .IsRequired();

            this.Property(c => c.Data)
                .IsRequired();


            this.Property(c => c.IsEnabled)
                .IsRequired();

            //configurar relacionamentos
            this.HasRequired(c => c.Ticket)
                .WithMany(c => c.Mensagens)
                .HasForeignKey(c => c.TicketId)
                .WillCascadeOnDelete(false);


            //configurar tabela
            this.ToTable("MensagensDoTicket");
        }
    }
}
