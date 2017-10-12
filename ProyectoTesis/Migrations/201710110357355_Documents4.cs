namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Documents4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ActiveFlag = c.Boolean(nullable: false),
                        Correlative = c.Int(),
                        SerialNumber = c.Int(),
                        OrderID = c.Int(),
                        Igv = c.Double(),
                        Subtotal = c.Double(),
                        DocumentType = c.Int(),
                        SupplierID = c.Int(),
                        BillCorrelative = c.Int(),
                        BillSerialNumber = c.String(),
                        Igv1 = c.Double(),
                        Subtotal1 = c.Double(),
                        DistributorID = c.Int(),
                        SaleDocumentID = c.Int(),
                        ClientID = c.Int(),
                        SaleDocumentID1 = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Distributor", t => t.DistributorID, cascadeDelete: true)
                .ForeignKey("dbo.Document", t => t.SaleDocumentID)
                .ForeignKey("dbo.Document", t => t.SaleDocumentID1)
                .ForeignKey("dbo.Supplier", t => t.SupplierID, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.SupplierID)
                .Index(t => t.DistributorID)
                .Index(t => t.SaleDocumentID)
                .Index(t => t.SaleDocumentID1);
            
            AddColumn("dbo.ReferralGuideDetail", "ReferralGuide_ID", c => c.Int());
            CreateIndex("dbo.Movement", "DocumentID");
            CreateIndex("dbo.PurchaseOrderDetail", "PurchaseOrderID");
            CreateIndex("dbo.ReferralGuideDetail", "ReferralGuide_ID");
            CreateIndex("dbo.ReturnDetail", "ReturnID");
            CreateIndex("dbo.SaleDocumentDetail", "SaleDocumentID");
            AddForeignKey("dbo.Movement", "DocumentID", "dbo.Document", "ID");
            AddForeignKey("dbo.ReferralGuideDetail", "ReferralGuide_ID", "dbo.Document", "ID");
            AddForeignKey("dbo.ReturnDetail", "ReturnID", "dbo.Document", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SaleDocumentDetail", "SaleDocumentID", "dbo.Document", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PurchaseOrderDetail", "PurchaseOrderID", "dbo.Document", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Document", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Document", "SupplierID", "dbo.Supplier");
            DropForeignKey("dbo.PurchaseOrderDetail", "PurchaseOrderID", "dbo.Document");
            DropForeignKey("dbo.SaleDocumentDetail", "SaleDocumentID", "dbo.Document");
            DropForeignKey("dbo.Document", "SaleDocumentID1", "dbo.Document");
            DropForeignKey("dbo.ReturnDetail", "ReturnID", "dbo.Document");
            DropForeignKey("dbo.Document", "SaleDocumentID", "dbo.Document");
            DropForeignKey("dbo.ReferralGuideDetail", "ReferralGuide_ID", "dbo.Document");
            DropForeignKey("dbo.Document", "DistributorID", "dbo.Distributor");
            DropForeignKey("dbo.Movement", "DocumentID", "dbo.Document");
            DropIndex("dbo.SaleDocumentDetail", new[] { "SaleDocumentID" });
            DropIndex("dbo.ReturnDetail", new[] { "ReturnID" });
            DropIndex("dbo.ReferralGuideDetail", new[] { "ReferralGuide_ID" });
            DropIndex("dbo.PurchaseOrderDetail", new[] { "PurchaseOrderID" });
            DropIndex("dbo.Movement", new[] { "DocumentID" });
            DropIndex("dbo.Document", new[] { "SaleDocumentID1" });
            DropIndex("dbo.Document", new[] { "SaleDocumentID" });
            DropIndex("dbo.Document", new[] { "DistributorID" });
            DropIndex("dbo.Document", new[] { "SupplierID" });
            DropIndex("dbo.Document", new[] { "OrderID" });
            DropColumn("dbo.ReferralGuideDetail", "ReferralGuide_ID");
            DropTable("dbo.Document");
        }
    }
}
