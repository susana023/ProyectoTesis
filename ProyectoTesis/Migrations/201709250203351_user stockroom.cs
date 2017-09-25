namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userstockroom : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stockroom", "StoreID", "dbo.Store");
            DropIndex("dbo.Stockroom", new[] { "StoreID" });
            AlterColumn("dbo.Stockroom", "StoreID", c => c.Int(nullable: false));
            CreateIndex("dbo.Stockroom", "StoreID");
            AddForeignKey("dbo.Stockroom", "StoreID", "dbo.Store", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stockroom", "StoreID", "dbo.Store");
            DropIndex("dbo.Stockroom", new[] { "StoreID" });
            AlterColumn("dbo.Stockroom", "StoreID", c => c.Int());
            CreateIndex("dbo.Stockroom", "StoreID");
            AddForeignKey("dbo.Stockroom", "StoreID", "dbo.Store", "ID");
        }
    }
}
