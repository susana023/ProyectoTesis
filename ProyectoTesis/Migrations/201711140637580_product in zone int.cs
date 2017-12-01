namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productinzoneint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductInZone", "BoxUnits", c => c.Int(nullable: true));
            AlterColumn("dbo.ProductInZone", "FractionUnits", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductInZone", "FractionUnits", c => c.Double(nullable: false));
            AlterColumn("dbo.ProductInZone", "BoxUnits", c => c.Double(nullable: false));
        }
    }
}
