namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stocks : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stockroom", "StoreID", "dbo.Store");
            DropIndex("dbo.Stockroom", new[] { "StoreID" });
            AddColumn("dbo.Product", "MinStock", c => c.Double(nullable: false));
            AddColumn("dbo.Product", "MaxStock", c => c.Double(nullable: false));
            AddColumn("dbo.Product", "ProductType", c => c.Int(nullable: false));
            AddColumn("dbo.Stockroom", "UserID", c => c.Int());
            AlterColumn("dbo.Stockroom", "StoreID", c => c.Int());
            CreateIndex("dbo.Stockroom", "StoreID");
            AddForeignKey("dbo.Stockroom", "StoreID", "dbo.Store", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stockroom", "StoreID", "dbo.Store");
            DropIndex("dbo.Stockroom", new[] { "StoreID" });
            AlterColumn("dbo.Stockroom", "StoreID", c => c.Int(nullable: false));
            DropColumn("dbo.Stockroom", "UserID");
            DropColumn("dbo.Product", "ProductType");
            DropColumn("dbo.Product", "MaxStock");
            DropColumn("dbo.Product", "MinStock");
            CreateIndex("dbo.Stockroom", "StoreID");
            AddForeignKey("dbo.Stockroom", "StoreID", "dbo.Store", "ID", cascadeDelete: true);
        }
    }
}
