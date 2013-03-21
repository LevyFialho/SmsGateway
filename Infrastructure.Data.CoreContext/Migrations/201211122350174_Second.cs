namespace SmsGateway.Infrastructure.Data.CoreContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clientes", "Senha", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Clientes", "ContratoAtualId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clientes", "ContratoAtualId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Clientes", "Senha", c => c.String(nullable: false, maxLength: 8));
        }
    }
}
