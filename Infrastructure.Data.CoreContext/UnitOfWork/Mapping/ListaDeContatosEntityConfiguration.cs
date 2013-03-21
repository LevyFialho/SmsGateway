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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContatoAgg;

namespace SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork.Mapping
{
    

    /// <summary>
    /// Define a configuração de mapeamento de um determinado tipo de entidade
    /// </summary>
    class ListaDeContatosEntityConfiguration
        :EntityTypeConfiguration<ListaDeContatos>
    {
        /// <summary>
        /// Cria uma nova instância da entity configuration
        /// </summary>
        public ListaDeContatosEntityConfiguration()
        {
            //configurar chaves e propriedades
            this.HasKey(c => c.Id);

            this.Property(c => c.ClienteId)
                .IsRequired();

            this.Property(c => c.Nome)
               // .HasMaxLength(100)
                .IsRequired();

            //configurar relacionamentos
            this.HasRequired(c => c.Cliente)
                .WithMany().HasForeignKey(c => c.ClienteId)
                .WillCascadeOnDelete(false);


            this.HasMany(c => c.Contatos)
                .WithMany(c => c.Listas)
                .Map(mc =>
                    {
                        mc.MapLeftKey("ListaId");
                        mc.MapRightKey("ContatoId");
                        mc.ToTable("ContatosDaLista");
                    });
            
            //configurar tabela
            this.ToTable("ListasDeContatos");
        }
    }

    
}
