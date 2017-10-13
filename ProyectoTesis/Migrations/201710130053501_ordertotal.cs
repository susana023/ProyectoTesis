namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ordertotal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Subtotal", c => c.Double(nullable: false));
            AddColumn("dbo.Order", "Igv", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Igv");
            DropColumn("dbo.Order", "Subtotal");
        }
    }
}
