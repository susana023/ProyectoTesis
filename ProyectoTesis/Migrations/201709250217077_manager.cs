namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manager : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stockroom", "UserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stockroom", "UserID", c => c.Int());
        }
    }
}
