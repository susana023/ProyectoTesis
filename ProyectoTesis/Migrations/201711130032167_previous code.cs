namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class previouscode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "PreviousCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "PreviousCode");
        }
    }
}
