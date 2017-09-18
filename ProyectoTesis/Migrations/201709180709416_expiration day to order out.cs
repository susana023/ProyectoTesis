namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expirationdaytoorderout : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PurchaseOrderDetail", "LotExpirationDay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrderDetail", "LotExpirationDay", c => c.DateTime(nullable: true));
        }
    }
}
