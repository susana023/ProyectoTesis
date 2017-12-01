namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class documents1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Document", "Igv", c => c.Double(nullable: true));
            AlterColumn("dbo.Document", "Subtotal", c => c.Double(nullable: true));
            DropColumn("dbo.Document", "Igv2");
            DropColumn("dbo.Document", "Subtotal2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Document", "Subtotal2", c => c.Double());
            AddColumn("dbo.Document", "Igv2", c => c.Double());
            AlterColumn("dbo.Document", "Subtotal", c => c.Double());
            AlterColumn("dbo.Document", "Igv", c => c.Double());
        }
    }
}
