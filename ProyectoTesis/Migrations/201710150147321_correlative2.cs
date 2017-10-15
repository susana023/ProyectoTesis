namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correlative2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Correlative", "ActiveFlag", c => c.Boolean(nullable: true, defaultValue: true));
            AlterColumn("dbo.Document", "SerialNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Document", "SerialNumber", c => c.Int());
            DropColumn("dbo.Correlative", "ActiveFlag");
        }
    }
}
