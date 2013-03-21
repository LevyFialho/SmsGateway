namespace SmsGateway.Infrastructure.Data.CoreContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Solicitacoes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Solicitacoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false),
                        Senha = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Telefone = c.Long(nullable: false),
                        Data = c.DateTime(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Solicitacoes");
        }
    }
}
