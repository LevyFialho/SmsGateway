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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;

namespace SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork.Mapping
{
    

    /// <summary>
    /// Define a configuração de mapeamento de um determinado tipo de entidade
    /// </summary>
    class ContratosEntityConfiguration
        :EntityTypeConfiguration<Contrato>
    {
        /// <summary>
        /// Cria uma nova instância da entity configuration
        /// </summary>
        public ContratosEntityConfiguration()
        {
            //configurar chaves e propriedades
            this.HasKey(c => c.Id);

            this.Property(c => c.DataInicial)
                .IsRequired();

            this.Property(c => c.DataFinal)
                .IsRequired();

            this.Property(c => c.Tipo)
              .IsRequired();

            this.Property(c => c.OperadoraApi)
                .IsRequired();

            this.Property(c => c.SaldoDeMensagens)
                .IsRequired();

            this.Property(c => c.ValorMensagem)
                .IsRequired();

            this.Property(c => c.IsEnabled)
                .IsRequired();

            //configurar relacionamentos
            this.HasOptional<Cliente>(c => c.Cliente)
                .WithMany(c => c.Contratos)
                .HasForeignKey(c => c.ClienteId)
                //.WithOptionalDependent().Map(map => map.MapKey("ClienteId"))
                //.HasForeignKey(c=> c.ClienteId)
                .WillCascadeOnDelete(false);
           

            //configurar tabela
            this.ToTable("Contratos");
        }
    }
}
