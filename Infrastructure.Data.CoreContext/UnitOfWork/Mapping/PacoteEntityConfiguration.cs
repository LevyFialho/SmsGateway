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
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.PacoteAgg;

namespace SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork.Mapping
{
    

    /// <summary>
    /// Define a configuração de mapeamento de um determinado tipo de entidade
    /// </summary>
    class PacoteEntityConfiguration
        :EntityTypeConfiguration<Pacote>
    {
        /// <summary>
        /// Cria uma nova instância da entity configuration
        /// </summary>
        public PacoteEntityConfiguration()
        {
            //configurar chaves e propriedades
            this.HasKey(c => c.Id);
            
            this.Property(c => c.Nome) 
                .IsRequired();

            this.Property(c => c.DataDeVencimento)
                .IsRequired();

            this.Property(c => c.GratuitoAoNovoCliente)
                .IsRequired();

            this.Property(c => c.QuantidadeDeMensagens)
               .IsRequired();

            this.Property(c => c.ValorCobradoPorMensagem)
               .IsRequired();

            this.Property(c => c.IsEnabled)
                .IsRequired();
            
            

            //configurar relacionamentos
           
            //configurar tabela
            this.ToTable("Pacotes");
        }
    }
}
