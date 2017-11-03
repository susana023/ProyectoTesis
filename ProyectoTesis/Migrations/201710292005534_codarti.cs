namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class codarti : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Codarti", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Codarti");
        }
    }
}
