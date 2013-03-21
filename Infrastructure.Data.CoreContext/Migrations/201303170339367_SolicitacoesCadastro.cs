namespace SmsGateway.Infrastructure.Data.CoreContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SolicitacoesCadastro : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Solicitacoes", "Senha");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Solicitacoes", "Senha", c => c.String(nullable: false));
        }
    }
}
