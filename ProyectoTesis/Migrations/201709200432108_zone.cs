namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrderDetail", "ZoneID", c => c.Int(nullable: false));
            CreateIndex("dbo.PurchaseOrderDetail", "ZoneID");
            AddForeignKey("dbo.PurchaseOrderDetail", "ZoneID", "dbo.Zone", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseOrderDetail", "ZoneID", "dbo.Zone");
            DropIndex("dbo.PurchaseOrderDetail", new[] { "ZoneID" });
            DropColumn("dbo.PurchaseOrderDetail", "ZoneID");
        }
    }
}
