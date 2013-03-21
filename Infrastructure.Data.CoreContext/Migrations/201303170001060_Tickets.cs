namespace SmsGateway.Infrastructure.Data.CoreContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tickets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        Assunto = c.String(nullable: false),
                        Data = c.DateTime(nullable: false),
                        ClienteId = c.Guid(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.MensagensDoTicket",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Texto = c.String(nullable: false),
                        Data = c.DateTime(nullable: false),
                        TicketId = c.Guid(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tickets", t => t.TicketId)
                .Index(t => t.TicketId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.MensagensDoTicket", new[] { "TicketId" });
            DropIndex("dbo.Tickets", new[] { "ClienteId" });
            DropForeignKey("dbo.MensagensDoTicket", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "ClienteId", "dbo.Clientes");
            DropTable("dbo.MensagensDoTicket");
            DropTable("dbo.Tickets");
        }
    }
}
