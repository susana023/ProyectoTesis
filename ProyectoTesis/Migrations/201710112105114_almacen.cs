namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class almacen : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stockroom", "ID", "dbo.User");
            DropForeignKey("dbo.ProductSupplier", "Stockroom_ID", "dbo.Stockroom");
            DropForeignKey("dbo.Zone", "StockroomID", "dbo.Stockroom");
            DropIndex("dbo.Stockroom", new[] { "ID" });
            DropPrimaryKey("dbo.Stockroom");
            AlterColumn("dbo.Stockroom", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Stockroom", "ID");
            AddForeignKey("dbo.ProductSupplier", "Stockroom_ID", "dbo.Stockroom", "ID");
            AddForeignKey("dbo.Zone", "StockroomID", "dbo.Stockroom", "ID", cascadeDelete: true);
        }

        public override void Down()
        {
            AlterColumn("dbo.Stockroom", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Stockroom", "ID");
            CreateIndex("dbo.Stockroom", "ID");
        }
    }
}
