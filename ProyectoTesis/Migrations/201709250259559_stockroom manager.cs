namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockroommanager : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Stockroom", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stockroom", "UserID", c => c.Int(nullable: false));
        }
    }
}
