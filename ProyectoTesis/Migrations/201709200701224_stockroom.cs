namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockroom : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stockroom", "Store_ID", "dbo.Store");
            DropIndex("dbo.Stockroom", new[] { "Store_ID" });
            RenameColumn(table: "dbo.Stockroom", name: "Store_ID", newName: "StoreID");
            AddColumn("dbo.Stockroom", "Name", c => c.String());
            AlterColumn("dbo.Stockroom", "StoreID", c => c.Int(nullable: false));
            CreateIndex("dbo.Stockroom", "StoreID");
            AddForeignKey("dbo.Stockroom", "StoreID", "dbo.Store", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stockroom", "StoreID", "dbo.Store");
            DropIndex("dbo.Stockroom", new[] { "StoreID" });
            AlterColumn("dbo.Stockroom", "StoreID", c => c.Int());
            DropColumn("dbo.Stockroom", "Name");
            RenameColumn(table: "dbo.Stockroom", name: "StoreID", newName: "Store_ID");
            CreateIndex("dbo.Stockroom", "Store_ID");
            AddForeignKey("dbo.Stockroom", "Store_ID", "dbo.Store", "ID");
        }
    }
}
