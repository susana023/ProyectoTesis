namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PurchaseOrderDetail", "BoxUnits", c => c.Int());
            AlterColumn("dbo.PurchaseOrderDetail", "FractionUnits", c => c.Int());
            AlterColumn("dbo.PurchaseOrderDetail", "BatchExpirationDay", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseOrderDetail", "BatchExpirationDay", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseOrderDetail", "FractionUnits", c => c.Int(nullable: false));
            AlterColumn("dbo.PurchaseOrderDetail", "BoxUnits", c => c.Int(nullable: false));
        }
    }
}
