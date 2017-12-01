namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _return : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Document", name: "Igv1", newName: "Igv2");
            RenameColumn(table: "dbo.Document", name: "Subtotal1", newName: "Subtotal2");
            AddColumn("dbo.Product", "Weight", c => c.Double(nullable: true));
            AddColumn("dbo.ReferralGuideDetail", "Weight", c => c.Double(nullable: true));
            AddColumn("dbo.Document", "Reason", c => c.Int());
            AddColumn("dbo.Document", "Reason1", c => c.Int());
            AddColumn("dbo.ReturnDetail", "Subtotal", c => c.Double(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReturnDetail", "Subtotal");
            DropColumn("dbo.Document", "Reason1");
            DropColumn("dbo.Document", "Reason");
            DropColumn("dbo.ReferralGuideDetail", "Weight");
            DropColumn("dbo.Product", "Weight");
            RenameColumn(table: "dbo.Document", name: "Subtotal2", newName: "Subtotal1");
            RenameColumn(table: "dbo.Document", name: "Igv2", newName: "Igv1");
        }
    }
}
