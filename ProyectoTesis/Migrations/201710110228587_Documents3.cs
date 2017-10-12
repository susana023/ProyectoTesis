namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Documents3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movement", "DocumentID", "dbo.Document");
            DropForeignKey("dbo.ReferralGuideDetail", "ReferralGuide_ID", "dbo.ReferralGuide");
            DropForeignKey("dbo.ReturnDetail", "ReturnID", "dbo.Return");
            DropForeignKey("dbo.SaleDocumentDetail", "SaleDocumentID", "dbo.SaleDocument");
            DropForeignKey("dbo.PurchaseOrderDetail", "PurchaseOrderID", "dbo.PurchaseOrder");
            DropForeignKey("dbo.PurchaseOrder", "ID", "dbo.Document");
            DropForeignKey("dbo.PurchaseOrder", "SupplierID", "dbo.Supplier");
            DropForeignKey("dbo.ReferralGuide", "ID", "dbo.Document");
            DropForeignKey("dbo.ReferralGuide", "DistributorID", "dbo.Distributor");
            DropForeignKey("dbo.ReferralGuide", "SaleDocumentID", "dbo.SaleDocument");
            DropForeignKey("dbo.Return", "ID", "dbo.Document");
            DropForeignKey("dbo.Return", "SaleDocumentID", "dbo.SaleDocument");
            DropForeignKey("dbo.SaleDocument", "ID", "dbo.Document");
            DropForeignKey("dbo.SaleDocument", "OrderID", "dbo.Order");
            DropIndex("dbo.Movement", new[] { "DocumentID" });
            DropIndex("dbo.PurchaseOrderDetail", new[] { "PurchaseOrderID" });
            DropIndex("dbo.ReferralGuideDetail", new[] { "ReferralGuide_ID" });
            DropIndex("dbo.ReturnDetail", new[] { "ReturnID" });
            DropIndex("dbo.SaleDocumentDetail", new[] { "SaleDocumentID" });
            DropIndex("dbo.PurchaseOrder", new[] { "ID" });
            DropIndex("dbo.PurchaseOrder", new[] { "SupplierID" });
            DropIndex("dbo.ReferralGuide", new[] { "ID" });
            DropIndex("dbo.ReferralGuide", new[] { "DistributorID" });
            DropIndex("dbo.ReferralGuide", new[] { "SaleDocumentID" });
            DropIndex("dbo.Return", new[] { "ID" });
            DropIndex("dbo.Return", new[] { "SaleDocumentID" });
            DropIndex("dbo.SaleDocument", new[] { "ID" });
            DropIndex("dbo.SaleDocument", new[] { "OrderID" });
            DropColumn("dbo.ReferralGuideDetail", "ReferralGuide_ID");
            DropTable("dbo.Document");
            DropTable("dbo.PurchaseOrder");
            DropTable("dbo.ReferralGuide");
            DropTable("dbo.Return");
            DropTable("dbo.SaleDocument");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SaleDocument",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Correlative = c.Int(nullable: false),
                        SerialNumber = c.Int(nullable: false),
                        OrderID = c.Int(nullable: false),
                        Igv = c.Double(nullable: false),
                        Subtotal = c.Double(nullable: false),
                        DocumentType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Return",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        SaleDocumentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ReferralGuide",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        DistributorID = c.Int(nullable: false),
                        SaleDocumentID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PurchaseOrder",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        SupplierID = c.Int(nullable: false),
                        BillCorrelative = c.Int(nullable: false),
                        BillSerialNumber = c.String(),
                        Igv = c.Double(nullable: false),
                        Subtotal = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ActiveFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ReferralGuideDetail", "ReferralGuide_ID", c => c.Int());
            CreateIndex("dbo.SaleDocument", "OrderID");
            CreateIndex("dbo.SaleDocument", "ID");
            CreateIndex("dbo.Return", "SaleDocumentID");
            CreateIndex("dbo.Return", "ID");
            CreateIndex("dbo.ReferralGuide", "SaleDocumentID");
            CreateIndex("dbo.ReferralGuide", "DistributorID");
            CreateIndex("dbo.ReferralGuide", "ID");
            CreateIndex("dbo.PurchaseOrder", "SupplierID");
            CreateIndex("dbo.PurchaseOrder", "ID");
            CreateIndex("dbo.SaleDocumentDetail", "SaleDocumentID");
            CreateIndex("dbo.ReturnDetail", "ReturnID");
            CreateIndex("dbo.ReferralGuideDetail", "ReferralGuide_ID");
            CreateIndex("dbo.PurchaseOrderDetail", "PurchaseOrderID");
            CreateIndex("dbo.Movement", "DocumentID");
            AddForeignKey("dbo.SaleDocument", "OrderID", "dbo.Order", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SaleDocument", "ID", "dbo.Document", "ID");
            AddForeignKey("dbo.Return", "SaleDocumentID", "dbo.SaleDocument", "ID");
            AddForeignKey("dbo.Return", "ID", "dbo.Document", "ID");
            AddForeignKey("dbo.ReferralGuide", "SaleDocumentID", "dbo.SaleDocument", "ID");
            AddForeignKey("dbo.ReferralGuide", "DistributorID", "dbo.Distributor", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ReferralGuide", "ID", "dbo.Document", "ID");
            AddForeignKey("dbo.PurchaseOrder", "SupplierID", "dbo.Supplier", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PurchaseOrder", "ID", "dbo.Document", "ID");
            AddForeignKey("dbo.PurchaseOrderDetail", "PurchaseOrderID", "dbo.PurchaseOrder", "ID");
            AddForeignKey("dbo.SaleDocumentDetail", "SaleDocumentID", "dbo.SaleDocument", "ID");
            AddForeignKey("dbo.ReturnDetail", "ReturnID", "dbo.Return", "ID");
            AddForeignKey("dbo.ReferralGuideDetail", "ReferralGuide_ID", "dbo.ReferralGuide", "ID");
            AddForeignKey("dbo.Movement", "DocumentID", "dbo.Document", "ID");
        }
    }
}
