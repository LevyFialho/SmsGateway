namespace SmsGateway.Infrastructure.Data.CoreContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Third : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Administradores", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.Administradores", "Senha", c => c.String(nullable: false));
            AlterColumn("dbo.Administradores", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Clientes", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.Clientes", "Senha", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clientes", "Senha", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Clientes", "Nome", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Administradores", "Email", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Administradores", "Senha", c => c.String(maxLength: 8));
            AlterColumn("dbo.Administradores", "Nome", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
