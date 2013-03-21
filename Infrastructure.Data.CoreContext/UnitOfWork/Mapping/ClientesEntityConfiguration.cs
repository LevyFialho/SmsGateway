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

namespace SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork.Mapping
{
    

    /// <summary>
    /// Define a configuração de mapeamento de um determinado tipo de entidade
    /// </summary>
    class ClientesEntityConfiguration
        :EntityTypeConfiguration<Cliente>
    {
        /// <summary>
        /// Cria uma nova instância da entity configuration
        /// </summary>
        public ClientesEntityConfiguration()
        {
            //configurar chaves e propriedades
            this.HasKey(c => c.Id); 

            this.Property(c => c.ContratoAtualId)
                .IsOptional();

            this.Property(c => c.Nome)
               // .HasMaxLength(100)
                .IsRequired();

            this.Property(c => c.Senha)
                .IsRequired();

            this.Property(c => c.Email)
                .IsRequired();
               // .HasMaxLength(100);

            this.Property(c => c.IsEnabled)
                .IsRequired();
            
            this.HasOptional(c => c.ContratoAtual)
                .WithMany()
                .HasForeignKey(c => c.ContratoAtualId)
                //.WithRequiredPrincipal(c => c.Cliente).Map(c => c.MapKey("ClienteId"))
                //.WithMany(c => c.ContratoAtualId )
                //.HasForeignKey(c => c.ContratoAtualId)
                .WillCascadeOnDelete(false);

            //configurar relacionamentos
           
            //configurar tabela
            this.ToTable("Clientes");
        }
    }
}
