namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activeflag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetail", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductSupplier", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Supplier", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReferralGuideDetail", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Movement", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.PurchaseOrderDetail", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Zone", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReturnDetail", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.SaleDocumentDetail", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.SalesMargin", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.PurchasePlanDetail", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.PurchasePlan", "ActiveFlag", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchasePlan", "ActiveFlag");
            DropColumn("dbo.PurchasePlanDetail", "ActiveFlag");
            DropColumn("dbo.SalesMargin", "ActiveFlag");
            DropColumn("dbo.SaleDocumentDetail", "ActiveFlag");
            DropColumn("dbo.ReturnDetail", "ActiveFlag");
            DropColumn("dbo.Zone", "ActiveFlag");
            DropColumn("dbo.PurchaseOrderDetail", "ActiveFlag");
            DropColumn("dbo.Movement", "ActiveFlag");
            DropColumn("dbo.ReferralGuideDetail", "ActiveFlag");
            DropColumn("dbo.Supplier", "ActiveFlag");
            DropColumn("dbo.ProductSupplier", "ActiveFlag");
            DropColumn("dbo.OrderDetail", "ActiveFlag");
        }
    }
}
