namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correlative : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Correlative",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StoreID = c.Int(nullable: false),
                        CorrelativeNumber = c.Int(nullable: false),
                        SerialNumber = c.String(),
                        DocumentType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.StoreID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Correlative", "StoreID", "dbo.Store");
            DropIndex("dbo.Correlative", new[] { "StoreID" });
            DropTable("dbo.Correlative");
        }
    }
}
