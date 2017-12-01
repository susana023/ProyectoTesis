namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class peso : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Product", "Weight");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Weight", c => c.Double(nullable: false));
        }
    }
}
