namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockroomDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductSupplier", "Stockroom_ID", "dbo.Stockroom");
            DropForeignKey("dbo.Stockroom", "StoreID", "dbo.Store");
            DropForeignKey("dbo.Zone", "StockroomID", "dbo.Stockroom");
            DropIndex("dbo.ProductSupplier", new[] { "Stockroom_ID" });
            DropIndex("dbo.Zone", new[] { "StockroomID" });
            DropIndex("dbo.Stockroom", new[] { "StoreID" });
            DropColumn("dbo.ProductSupplier", "Stockroom_ID");
            DropTable("dbo.Stockroom");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Stockroom",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Phone = c.String(),
                        Name = c.String(),
                        ActiveFlag = c.Boolean(nullable: false),
                        StoreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ProductSupplier", "Stockroom_ID", c => c.Int());
            CreateIndex("dbo.Stockroom", "StoreID");
            CreateIndex("dbo.Zone", "StockroomID");
            CreateIndex("dbo.ProductSupplier", "Stockroom_ID");
            AddForeignKey("dbo.Zone", "StockroomID", "dbo.Stockroom", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Stockroom", "StoreID", "dbo.Store", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ProductSupplier", "Stockroom_ID", "dbo.Stockroom", "ID");
        }
    }
}
