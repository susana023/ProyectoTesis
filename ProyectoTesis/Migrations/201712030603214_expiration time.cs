namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expirationtime : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpirationTime",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductType = c.Int(nullable: false),
                        Months = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExpirationTime");
        }
    }
}
