namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchasePlan", "BeginDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchasePlan", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.PurchasePlan", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchasePlan", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.PurchasePlan", "EndDate");
            DropColumn("dbo.PurchasePlan", "BeginDate");
        }
    }
}
