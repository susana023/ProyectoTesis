namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zone : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "StoreID", "dbo.Store");
            DropIndex("dbo.User", new[] { "StoreID" });
            AddColumn("dbo.SaleDocument", "DocumentType", c => c.Int(nullable: false));
            AddColumn("dbo.PurchaseOrderDetail", "ZoneID", c => c.Int());
            AlterColumn("dbo.Client", "Dni", c => c.Int());
            AlterColumn("dbo.Client", "Ruc", c => c.Long());
            AlterColumn("dbo.Client", "ClientType", c => c.Int(nullable: false));
            AlterColumn("dbo.Order", "DeliveredFlag", c => c.Boolean());
            AlterColumn("dbo.OrderDetail", "BoxUnits", c => c.Int());
            AlterColumn("dbo.OrderDetail", "FractionUnits", c => c.Int());
            AlterColumn("dbo.ProductSupplier", "FractionPrice", c => c.Double());
            AlterColumn("dbo.User", "ActiveFlag", c => c.Boolean());
            AlterColumn("dbo.User", "StoreID", c => c.Int());
            AlterColumn("dbo.PurchasePlanDetail", "BoxUnits", c => c.Int());
            AlterColumn("dbo.PurchasePlanDetail", "FractionUnits", c => c.Int());
            CreateIndex("dbo.PurchaseOrderDetail", "ZoneID");
            CreateIndex("dbo.User", "StoreID");
            AddForeignKey("dbo.PurchaseOrderDetail", "ZoneID", "dbo.Zone", "ID");
            AddForeignKey("dbo.User", "StoreID", "dbo.Store", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "StoreID", "dbo.Store");
            DropForeignKey("dbo.PurchaseOrderDetail", "ZoneID", "dbo.Zone");
            DropIndex("dbo.User", new[] { "StoreID" });
            DropIndex("dbo.PurchaseOrderDetail", new[] { "ZoneID" });
            AlterColumn("dbo.PurchasePlanDetail", "FractionUnits", c => c.Int(nullable: false));
            AlterColumn("dbo.PurchasePlanDetail", "BoxUnits", c => c.Int(nullable: false));
            AlterColumn("dbo.User", "StoreID", c => c.Int(nullable: false));
            AlterColumn("dbo.User", "ActiveFlag", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ProductSupplier", "FractionPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.OrderDetail", "FractionUnits", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderDetail", "BoxUnits", c => c.Int(nullable: false));
            AlterColumn("dbo.Order", "DeliveredFlag", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Client", "ClientType", c => c.Int());
            AlterColumn("dbo.Client", "Ruc", c => c.Long(nullable: false));
            AlterColumn("dbo.Client", "Dni", c => c.Int(nullable: false));
            DropColumn("dbo.PurchaseOrderDetail", "ZoneID");
            DropColumn("dbo.SaleDocument", "DocumentType");
            CreateIndex("dbo.User", "StoreID");
            AddForeignKey("dbo.User", "StoreID", "dbo.Store", "ID", cascadeDelete: true);
        }
    }
}
