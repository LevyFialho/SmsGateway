namespace SmsGateway.Infrastructure.Data.CoreContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCodigoPais : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Contatos", "Pais");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contatos", "Pais", c => c.Int(nullable: false));
        }
    }
}
