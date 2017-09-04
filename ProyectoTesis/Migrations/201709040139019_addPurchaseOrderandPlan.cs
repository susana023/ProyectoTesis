namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPurchaseOrderandPlan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalesMargin",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        MarketMargin = c.Double(nullable: false),
                        StoreMargin = c.Double(nullable: false),
                        DistributionMargin = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.PurchaseOrderDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PurchaseOrderID = c.Int(nullable: false),
                        productID = c.Int(nullable: false),
                        BoxUnits = c.Int(nullable: false),
                        FractionUnits = c.Int(nullable: false),
                        Subtotal = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.productID, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseOrder", t => t.PurchaseOrderID, cascadeDelete: true)
                .Index(t => t.PurchaseOrderID)
                .Index(t => t.productID);
            
            CreateTable(
                "dbo.PurchaseOrder",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SupplierID = c.Int(nullable: false),
                        BillCorrelative = c.Int(nullable: false),
                        BillSerialNumber = c.String(),
                        ActiveFlag = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Igv = c.Double(nullable: false),
                        Subtotal = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Supplier", t => t.SupplierID, cascadeDelete: true)
                .Index(t => t.SupplierID);
            
            CreateTable(
                "dbo.PurchasePlanDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PurchasePlanID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        benefit = c.Double(nullable: false),
                        BoxUnits = c.Int(nullable: false),
                        FractionUnits = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PurchasePlan", t => t.PurchasePlanID, cascadeDelete: true)
                .Index(t => t.PurchasePlanID);
            
            CreateTable(
                "dbo.PurchasePlan",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Investment = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchasePlanDetail", "PurchasePlanID", "dbo.PurchasePlan");
            DropForeignKey("dbo.PurchaseOrder", "SupplierID", "dbo.Supplier");
            DropForeignKey("dbo.PurchaseOrderDetail", "PurchaseOrderID", "dbo.PurchaseOrder");
            DropForeignKey("dbo.PurchaseOrderDetail", "productID", "dbo.Product");
            DropForeignKey("dbo.SalesMargin", "ID", "dbo.Product");
            DropIndex("dbo.PurchasePlanDetail", new[] { "PurchasePlanID" });
            DropIndex("dbo.PurchaseOrder", new[] { "SupplierID" });
            DropIndex("dbo.PurchaseOrderDetail", new[] { "productID" });
            DropIndex("dbo.PurchaseOrderDetail", new[] { "PurchaseOrderID" });
            DropIndex("dbo.SalesMargin", new[] { "ID" });
            DropTable("dbo.PurchasePlan");
            DropTable("dbo.PurchasePlanDetail");
            DropTable("dbo.PurchaseOrder");
            DropTable("dbo.PurchaseOrderDetail");
            DropTable("dbo.SalesMargin");
        }
    }
}
