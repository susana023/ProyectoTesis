namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchaseplan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchasePlan", "TotalBenefit", c => c.Double(nullable: false));
            CreateIndex("dbo.PurchasePlanDetail", "ProductID");
            AddForeignKey("dbo.PurchasePlanDetail", "ProductID", "dbo.Product", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchasePlanDetail", "ProductID", "dbo.Product");
            DropIndex("dbo.PurchasePlanDetail", new[] { "ProductID" });
            DropColumn("dbo.PurchasePlan", "TotalBenefit");
        }
    }
}
