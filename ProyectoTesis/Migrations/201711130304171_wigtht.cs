namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wigtht : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Weight", c => c.Double(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Weight");
        }
    }
}
