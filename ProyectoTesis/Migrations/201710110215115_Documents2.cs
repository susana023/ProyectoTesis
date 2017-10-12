namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Documents2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Document", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Document", "ActiveFlag", c => c.Boolean(nullable: false));
            DropColumn("dbo.SaleDocument", "ActiveFlag");
            DropColumn("dbo.PurchaseOrder", "ActiveFlag");
            DropColumn("dbo.PurchaseOrder", "Date");
            DropColumn("dbo.ReferralGuide", "Date");
            DropColumn("dbo.Return", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Return", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.ReferralGuide", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseOrder", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseOrder", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.SaleDocument", "ActiveFlag", c => c.Boolean(nullable: false));
            DropColumn("dbo.Document", "ActiveFlag");
            DropColumn("dbo.Document", "Date");
        }
    }
}
