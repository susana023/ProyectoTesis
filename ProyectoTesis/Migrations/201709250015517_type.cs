namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "PhysicalStock", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "PhysicalStock", c => c.Int(nullable: false));
        }
    }
}
