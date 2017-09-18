namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expirationdaytoorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrderDetail", "BatchExpirationDay", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseOrderDetail", "BatchExpirationDay");
        }
    }
}
