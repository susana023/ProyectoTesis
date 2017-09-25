namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class typeouser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "Type", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Type", c => c.String());
        }
    }
}
