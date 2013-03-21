namespace SmsGateway.Infrastructure.Data.CoreContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PacoteListaDeContato : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListasDeContatos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false),
                        ClienteId = c.Guid(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Pacotes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        Nome = c.String(nullable: false),
                        QuantidadeDeMensagens = c.Int(nullable: false),
                        DataDeVencimento = c.DateTime(nullable: false),
                        ValorCobradoPorMensagem = c.Double(nullable: false),
                        GratuitoAoNovoCliente = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContatosDaLista",
                c => new
                    {
                        ListaId = c.Guid(nullable: false),
                        ContatoId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ListaId, t.ContatoId })
                .ForeignKey("dbo.ListasDeContatos", t => t.ListaId, cascadeDelete: true)
                .ForeignKey("dbo.Contatos", t => t.ContatoId, cascadeDelete: true)
                .Index(t => t.ListaId)
                .Index(t => t.ContatoId);
            
            AddColumn("dbo.Clientes", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropIndex("dbo.ContatosDaLista", new[] { "ContatoId" });
            DropIndex("dbo.ContatosDaLista", new[] { "ListaId" });
            DropIndex("dbo.ListasDeContatos", new[] { "ClienteId" });
            DropForeignKey("dbo.ContatosDaLista", "ContatoId", "dbo.Contatos");
            DropForeignKey("dbo.ContatosDaLista", "ListaId", "dbo.ListasDeContatos");
            DropForeignKey("dbo.ListasDeContatos", "ClienteId", "dbo.Clientes");
            DropColumn("dbo.Clientes", "Email");
            DropTable("dbo.ContatosDaLista");
            DropTable("dbo.Pacotes");
            DropTable("dbo.ListasDeContatos");
        }
    }
}
