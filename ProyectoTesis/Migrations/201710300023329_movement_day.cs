namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class movement_day : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movement", "MovementDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movement", "MovementDate");
        }
    }
}
