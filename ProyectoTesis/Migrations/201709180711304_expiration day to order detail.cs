namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expirationdaytoorderdetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrderDetail", "BatchExpirationDay", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseOrderDetail", "BatchExpirationDay");
        }
    }
}
