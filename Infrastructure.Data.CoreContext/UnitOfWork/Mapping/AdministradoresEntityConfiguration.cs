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
using System.Data.Entity.ModelConfiguration;

namespace SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork.Mapping
{
    

    /// <summary>
    /// Define a configuração de mapeamento de um determinado tipo de entidade
    /// </summary>
    class AdministradoresEntityConfiguration
        :EntityTypeConfiguration<Administrador>
    {
        /// <summary>
        /// Cria uma nova instância da entity configuration
        /// </summary>
        public AdministradoresEntityConfiguration()
        {
            //configurar chaves e propriedades
            this.HasKey(c => c.Id);

            this.Property(c => c.Email)
               // .HasMaxLength(100)
                .IsRequired();

            this.Property(c => c.Nome)
                //.HasMaxLength(100)
                .IsRequired();

            this.Property(c => c.Senha).IsRequired();
                //.HasMaxLength(100);

            this.Property(c => c.IsEnabled)
                .IsRequired();
            //configurar relacionamentos
           
            //configurar tabela
            this.ToTable("Administradores");
        }
    }
}
