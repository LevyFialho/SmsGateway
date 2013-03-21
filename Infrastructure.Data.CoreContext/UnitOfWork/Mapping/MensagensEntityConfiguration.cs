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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;

namespace SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork.Mapping
{
    

    /// <summary>
    /// Define a configuração de mapeamento de um determinado tipo de entidade
    /// </summary>
    class MensagensEntityConfiguration
        :EntityTypeConfiguration<Mensagem>
    {
        /// <summary>
        /// Cria uma nova instância da entity configuration
        /// </summary>
        public MensagensEntityConfiguration()
        {
            //configurar chaves e propriedades
            this.HasKey(c => c.Id);

            this.Property(c => c.DataDeRegistro).IsRequired();

            this.Property(c => c.DataDoUltimoUpdate).IsRequired();
            
            this.Property(c => c.IsEnabled).IsRequired();

            this.Property(c => c.NumeroDoDestinatario).IsRequired();

            this.Property(c => c.NumeroDoRemetente).IsRequired();

            this.Property(c => c.TextoDaMensagem).IsRequired();

            this.Property(c => c.DataDeEnvio).IsOptional();
            
            //configurar relacionamentos
            this.HasRequired(c => c.ContratoDoCliente)
                .WithMany(c => c.MensagensDoCliente)
                .HasForeignKey(c => c.ContratoDoClienteId)
                .WillCascadeOnDelete(false);

             this.HasOptional(c => c.ContratoDaOperadora)
                //.WithOptionalDependent().Map(map => map.MapKey("ContratoDaOperadoraId"))
               .WithMany(c => c.MensagensEnviadas)
               .HasForeignKey(c => c.ContratoDaOperadoraId)
               //.WithOptionalDependent()
               .WillCascadeOnDelete(false);

             //configurar relacionamentos
             this.HasRequired(c => c.Status)
                 .WithMany(c => c.Mensagens)
                 .HasForeignKey(c => c.StatusId)
                 .WillCascadeOnDelete(false);

           
            //configurar tabela
            this.ToTable("Mensagens");
        }
    }
}
