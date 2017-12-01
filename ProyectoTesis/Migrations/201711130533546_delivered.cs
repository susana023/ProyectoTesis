namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delivered : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Document", "DeliveredFlag", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Document", "DeliveredFlag");
        }
    }
}
