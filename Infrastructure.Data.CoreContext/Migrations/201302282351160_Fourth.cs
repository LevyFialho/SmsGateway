namespace SmsGateway.Infrastructure.Data.CoreContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contatos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false),
                        Numero = c.Long(nullable: false),
                        Pais = c.Int(nullable: false),
                        ClienteId = c.Guid(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Contatos", new[] { "ClienteId" });
            DropForeignKey("dbo.Contatos", "ClienteId", "dbo.Clientes");
            DropTable("dbo.Contatos");
        }
    }
}
