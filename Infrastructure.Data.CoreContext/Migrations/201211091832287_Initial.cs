namespace SmsGateway.Infrastructure.Data.CoreContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administradores",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Senha = c.String(maxLength: 8),
                        Email = c.String(nullable: false, maxLength: 100),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Senha = c.String(nullable: false, maxLength: 8),
                        ContratoAtualId = c.Guid(nullable: true),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contratos", t => t.ContratoAtualId)
                .Index(t => t.ContratoAtualId);
            
            CreateTable(
                "dbo.Contratos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DataInicial = c.DateTime(nullable: false),
                        DataFinal = c.DateTime(nullable: false),
                        SaldoDeMensagens = c.Int(nullable: false),
                        ValorMensagem = c.Double(nullable: false),
                        ContratoRenovadoId = c.Guid(),
                        ClienteId = c.Guid(),
                        OperadoraApi = c.Int(nullable: false),
                        Tipo = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contratos", t => t.ContratoRenovadoId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId)
                .Index(t => t.ContratoRenovadoId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Mensagens",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContratoDaOperadoraId = c.Guid(),
                        ContratoDoClienteId = c.Guid(nullable: false),
                        TextoDaMensagem = c.String(nullable: false),
                        NumeroDoDestinatario = c.String(nullable: false),
                        NumeroDoRemetente = c.String(nullable: false),
                        DataDeRegistro = c.DateTime(nullable: false),
                        DataDoUltimoUpdate = c.DateTime(nullable: false),
                        DataDeEnvio = c.DateTime(),
                        StatusId = c.Guid(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contratos", t => t.ContratoDoClienteId)
                .ForeignKey("dbo.Contratos", t => t.ContratoDaOperadoraId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .Index(t => t.ContratoDoClienteId)
                .Index(t => t.ContratoDaOperadoraId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.String(),
                        Descricao = c.String(),
                        MensagemAoCliente = c.String(),
                        QuantoDebitarDoContratoDoCliente = c.Int(nullable: false),
                        QuantoDebitarDoContratoDaOperadora = c.Int(nullable: false),
                        ValorDaOperacao = c.Double(nullable: false),
                        OperadoraApi = c.Int(nullable: false),
                        PrecisaReenviar = c.Boolean(nullable: false),
                        PrecisaReenviarPorOutraOperadora = c.Boolean(nullable: false),
                        PrecisaAtualizar = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Mensagens", new[] { "StatusId" });
            DropIndex("dbo.Mensagens", new[] { "ContratoDaOperadoraId" });
            DropIndex("dbo.Mensagens", new[] { "ContratoDoClienteId" });
            DropIndex("dbo.Contratos", new[] { "ClienteId" });
            DropIndex("dbo.Contratos", new[] { "ContratoRenovadoId" });
            DropIndex("dbo.Clientes", new[] { "ContratoAtualId" });
            DropForeignKey("dbo.Mensagens", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Mensagens", "ContratoDaOperadoraId", "dbo.Contratos");
            DropForeignKey("dbo.Mensagens", "ContratoDoClienteId", "dbo.Contratos");
            DropForeignKey("dbo.Contratos", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.Contratos", "ContratoRenovadoId", "dbo.Contratos");
            DropForeignKey("dbo.Clientes", "ContratoAtualId", "dbo.Contratos");
            DropTable("dbo.Status");
            DropTable("dbo.Mensagens");
            DropTable("dbo.Contratos");
            DropTable("dbo.Clientes");
            DropTable("dbo.Administradores");
        }
    }
}
