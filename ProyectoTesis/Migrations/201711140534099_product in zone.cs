namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productinzone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductInZone",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ZoneID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        BoxUnits = c.Double(nullable: true),
                        FractionUnits = c.Double(nullable: true),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductInZone");
        }
    }
}
