namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Supplier", "BusinessName", c => c.String(nullable: false));
            AlterColumn("dbo.Supplier", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Supplier", "Phone", c => c.String());
            AlterColumn("dbo.Supplier", "BusinessName", c => c.String());
        }
    }
}
