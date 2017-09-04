namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeRUCType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Client", "Ruc", c => c.Long(nullable: false));
            AlterColumn("dbo.Supplier", "Ruc", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Supplier", "Ruc", c => c.Int(nullable: false));
            AlterColumn("dbo.Client", "Ruc", c => c.Int(nullable: false));
        }
    }
}
