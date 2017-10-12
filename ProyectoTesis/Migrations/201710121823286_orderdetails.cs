namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderdetails : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Document", name: "Igv", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Document", name: "Subtotal", newName: "__mig_tmp__1");
            RenameColumn(table: "dbo.Document", name: "Igv1", newName: "Igv");
            RenameColumn(table: "dbo.Document", name: "Subtotal1", newName: "Subtotal");
            RenameColumn(table: "dbo.Document", name: "__mig_tmp__0", newName: "Igv1");
            RenameColumn(table: "dbo.Document", name: "__mig_tmp__1", newName: "Subtotal1");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Document", name: "Subtotal1", newName: "__mig_tmp__1");
            RenameColumn(table: "dbo.Document", name: "Igv1", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Document", name: "Subtotal", newName: "Subtotal1");
            RenameColumn(table: "dbo.Document", name: "Igv", newName: "Igv1");
            RenameColumn(table: "dbo.Document", name: "__mig_tmp__1", newName: "Subtotal");
            RenameColumn(table: "dbo.Document", name: "__mig_tmp__0", newName: "Igv");
        }
    }
}
