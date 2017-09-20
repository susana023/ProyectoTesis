namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "FractionUnits", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "LogicalStock", c => c.Double(nullable: false));
            AddColumn("dbo.Product", "PhysicalStock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "PhysicalStock");
            DropColumn("dbo.Product", "LogicalStock");
            DropColumn("dbo.Product", "FractionUnits");
        }
    }
}
