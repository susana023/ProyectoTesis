namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class documents : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReferralGuide", "SaleDocumentID", "dbo.SaleDocument");
            DropForeignKey("dbo.Return", "SaleDocumentID", "dbo.SaleDocument");
            DropForeignKey("dbo.SaleDocumentDetail", "SaleDocumentID", "dbo.SaleDocument");
            DropForeignKey("dbo.ReturnDetail", "ReturnID", "dbo.Return");
            DropForeignKey("dbo.PurchaseOrderDetail", "PurchaseOrderID", "dbo.PurchaseOrder");
            DropForeignKey("dbo.ReferralGuideDetail", "ReferralGuide_ID", "dbo.ReferralGuide");
            DropPrimaryKey("dbo.SaleDocument");
            DropPrimaryKey("dbo.ReferralGuide");
            DropPrimaryKey("dbo.Return");
            DropPrimaryKey("dbo.PurchaseOrder");
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.SaleDocument", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.ReferralGuide", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Return", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Movement", "ExpirationDate", c => c.DateTime());
            AlterColumn("dbo.Movement", "DocumentID", c => c.Int());
            AlterColumn("dbo.PurchaseOrder", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.SaleDocument", "ID");
            AddPrimaryKey("dbo.ReferralGuide", "ID");
            AddPrimaryKey("dbo.Return", "ID");
            AddPrimaryKey("dbo.PurchaseOrder", "ID");
            CreateIndex("dbo.Movement", "DocumentID");
            CreateIndex("dbo.PurchaseOrder", "ID");
            CreateIndex("dbo.ReferralGuide", "ID");
            CreateIndex("dbo.Return", "ID");
            CreateIndex("dbo.SaleDocument", "ID");
            AddForeignKey("dbo.Movement", "DocumentID", "dbo.Document", "ID");
            AddForeignKey("dbo.PurchaseOrder", "ID", "dbo.Document", "ID");
            AddForeignKey("dbo.ReferralGuide", "ID", "dbo.Document", "ID");
            AddForeignKey("dbo.Return", "ID", "dbo.Document", "ID");
            AddForeignKey("dbo.SaleDocument", "ID", "dbo.Document", "ID");
            AddForeignKey("dbo.ReferralGuide", "SaleDocumentID", "dbo.SaleDocument", "ID");
            AddForeignKey("dbo.Return", "SaleDocumentID", "dbo.SaleDocument", "ID");
            AddForeignKey("dbo.SaleDocumentDetail", "SaleDocumentID", "dbo.SaleDocument", "ID");
            AddForeignKey("dbo.ReturnDetail", "ReturnID", "dbo.Return", "ID");
            AddForeignKey("dbo.PurchaseOrderDetail", "PurchaseOrderID", "dbo.PurchaseOrder", "ID");
            AddForeignKey("dbo.ReferralGuideDetail", "ReferralGuide_ID", "dbo.ReferralGuide", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReferralGuideDetail", "ReferralGuide_ID", "dbo.ReferralGuide");
            DropForeignKey("dbo.PurchaseOrderDetail", "PurchaseOrderID", "dbo.PurchaseOrder");
            DropForeignKey("dbo.ReturnDetail", "ReturnID", "dbo.Return");
            DropForeignKey("dbo.SaleDocumentDetail", "SaleDocumentID", "dbo.SaleDocument");
            DropForeignKey("dbo.Return", "SaleDocumentID", "dbo.SaleDocument");
            DropForeignKey("dbo.ReferralGuide", "SaleDocumentID", "dbo.SaleDocument");
            DropForeignKey("dbo.SaleDocument", "ID", "dbo.Document");
            DropForeignKey("dbo.Return", "ID", "dbo.Document");
            DropForeignKey("dbo.ReferralGuide", "ID", "dbo.Document");
            DropForeignKey("dbo.PurchaseOrder", "ID", "dbo.Document");
            DropForeignKey("dbo.Movement", "DocumentID", "dbo.Document");
            DropIndex("dbo.SaleDocument", new[] { "ID" });
            DropIndex("dbo.Return", new[] { "ID" });
            DropIndex("dbo.ReferralGuide", new[] { "ID" });
            DropIndex("dbo.PurchaseOrder", new[] { "ID" });
            DropIndex("dbo.Movement", new[] { "DocumentID" });
            DropPrimaryKey("dbo.PurchaseOrder");
            DropPrimaryKey("dbo.Return");
            DropPrimaryKey("dbo.ReferralGuide");
            DropPrimaryKey("dbo.SaleDocument");
            AlterColumn("dbo.PurchaseOrder", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Movement", "DocumentID", c => c.Int(nullable: false));
            AlterColumn("dbo.Movement", "ExpirationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Return", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ReferralGuide", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.SaleDocument", "ID", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.Document");
            AddPrimaryKey("dbo.PurchaseOrder", "ID");
            AddPrimaryKey("dbo.Return", "ID");
            AddPrimaryKey("dbo.ReferralGuide", "ID");
            AddPrimaryKey("dbo.SaleDocument", "ID");
            AddForeignKey("dbo.ReferralGuideDetail", "ReferralGuide_ID", "dbo.ReferralGuide", "ID");
            AddForeignKey("dbo.PurchaseOrderDetail", "PurchaseOrderID", "dbo.PurchaseOrder", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ReturnDetail", "ReturnID", "dbo.Return", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SaleDocumentDetail", "SaleDocumentID", "dbo.SaleDocument", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Return", "SaleDocumentID", "dbo.SaleDocument", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ReferralGuide", "SaleDocumentID", "dbo.SaleDocument", "ID", cascadeDelete: true);
        }
    }
}
