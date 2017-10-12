namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockroomAdd : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.StoreID);
            
            AddColumn("dbo.ProductSupplier", "Stockroom_ID", c => c.Int());
            AddColumn("dbo.User", "StockroomID", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductSupplier", "Stockroom_ID");
            CreateIndex("dbo.Zone", "StockroomID");
            AddForeignKey("dbo.ProductSupplier", "Stockroom_ID", "dbo.Stockroom", "ID");
            AddForeignKey("dbo.Zone", "StockroomID", "dbo.Stockroom", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zone", "StockroomID", "dbo.Stockroom");
            DropForeignKey("dbo.Stockroom", "StoreID", "dbo.Store");
            DropForeignKey("dbo.ProductSupplier", "Stockroom_ID", "dbo.Stockroom");
            DropIndex("dbo.Stockroom", new[] { "StoreID" });
            DropIndex("dbo.Zone", new[] { "StockroomID" });
            DropIndex("dbo.ProductSupplier", new[] { "Stockroom_ID" });
            DropColumn("dbo.User", "StockroomID");
            DropColumn("dbo.ProductSupplier", "Stockroom_ID");
            DropTable("dbo.Stockroom");
        }
    }
}
