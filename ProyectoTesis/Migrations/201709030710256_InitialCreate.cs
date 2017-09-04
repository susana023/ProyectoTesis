namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Dni = c.Int(nullable: false),
                        Ruc = c.Int(nullable: false),
                        Name = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        ActiveFlag = c.Boolean(nullable: false),
                        ClientType = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClientID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        DeliveredFlag = c.Boolean(nullable: false),
                        ActiveFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Client", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Subtotal = c.Double(nullable: false),
                        BoxUnits = c.Int(nullable: false),
                        FractionUnits = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.SaleDocument",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Correlative = c.Int(nullable: false),
                        SerialNumber = c.Int(nullable: false),
                        OrderID = c.Int(nullable: false),
                        Igv = c.Double(nullable: false),
                        Subtotal = c.Double(nullable: false),
                        ActiveFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.ReferralGuide",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DistributorID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        SaleDocumentID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Distributor", t => t.DistributorID, cascadeDelete: true)
                .ForeignKey("dbo.SaleDocument", t => t.SaleDocumentID, cascadeDelete: true)
                .Index(t => t.DistributorID)
                .Index(t => t.SaleDocumentID);
            
            CreateTable(
                "dbo.Distributor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Dni = c.Int(nullable: false),
                        Name = c.String(),
                        LastName = c.String(),
                        LicensePlate = c.String(),
                        ActiveFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ReferralGuideDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        BoxUnits = c.Int(nullable: false),
                        FractionUnits = c.Int(nullable: false),
                        DeliveredFlag = c.Boolean(nullable: false),
                        ReferralGuide_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.ReferralGuide", t => t.ReferralGuide_ID)
                .Index(t => t.ProductID)
                .Index(t => t.ReferralGuide_ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        BoxDescription = c.String(),
                        FractionDescription = c.String(),
                        BoxPrice = c.Double(nullable: false),
                        FractionPrice = c.Double(nullable: false),
                        ActiveFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductSupplier",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        SupplierID = c.Int(nullable: false),
                        BoxPrice = c.Double(nullable: false),
                        FractionPrice = c.Double(nullable: false),
                        Stockroom_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Supplier", t => t.SupplierID, cascadeDelete: true)
                .ForeignKey("dbo.Stockroom", t => t.Stockroom_ID)
                .Index(t => t.ProductID)
                .Index(t => t.SupplierID)
                .Index(t => t.Stockroom_ID);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ruc = c.Int(nullable: false),
                        BusinessName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ReturnDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReturnID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        BoxUnits = c.Int(nullable: false),
                        FractionUnits = c.Int(nullable: false),
                        Reason = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Return", t => t.ReturnID, cascadeDelete: true)
                .Index(t => t.ReturnID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Return",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SaleDocumentID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SaleDocument", t => t.SaleDocumentID, cascadeDelete: true)
                .Index(t => t.SaleDocumentID);
            
            CreateTable(
                "dbo.SaleDocumentDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SaleDocumentID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        BoxUnits = c.Int(nullable: false),
                        FractionUnits = c.Int(nullable: false),
                        Subtotal = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.SaleDocument", t => t.SaleDocumentID, cascadeDelete: true)
                .Index(t => t.SaleDocumentID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Movement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        ZoneID = c.Int(nullable: false),
                        MovementType = c.Int(nullable: false),
                        BoxUnits = c.Int(nullable: false),
                        FractionUnits = c.Int(nullable: false),
                        DocumentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Zone", t => t.ZoneID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.ZoneID);
            
            CreateTable(
                "dbo.Zone",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StockroomID = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stockroom", t => t.StockroomID, cascadeDelete: true)
                .Index(t => t.StockroomID);
            
            CreateTable(
                "dbo.Stockroom",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Phone = c.String(),
                        ActiveFlag = c.Boolean(nullable: false),
                        Store_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Store", t => t.Store_ID)
                .ForeignKey("dbo.User", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.Store_ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Dni = c.Int(nullable: false),
                        Name = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        Username = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        ActiveFlag = c.Boolean(nullable: false),
                        StoreID = c.Int(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.Store",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Description = c.String(),
                        ActiveFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReferralGuide", "SaleDocumentID", "dbo.SaleDocument");
            DropForeignKey("dbo.ReferralGuideDetail", "ReferralGuide_ID", "dbo.ReferralGuide");
            DropForeignKey("dbo.Movement", "ZoneID", "dbo.Zone");
            DropForeignKey("dbo.Zone", "StockroomID", "dbo.Stockroom");
            DropForeignKey("dbo.ProductSupplier", "Stockroom_ID", "dbo.Stockroom");
            DropForeignKey("dbo.Stockroom", "ID", "dbo.User");
            DropForeignKey("dbo.User", "StoreID", "dbo.Store");
            DropForeignKey("dbo.Stockroom", "Store_ID", "dbo.Store");
            DropForeignKey("dbo.Order", "UserID", "dbo.User");
            DropForeignKey("dbo.Movement", "ProductID", "dbo.Product");
            DropForeignKey("dbo.SaleDocumentDetail", "SaleDocumentID", "dbo.SaleDocument");
            DropForeignKey("dbo.SaleDocumentDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Return", "SaleDocumentID", "dbo.SaleDocument");
            DropForeignKey("dbo.ReturnDetail", "ReturnID", "dbo.Return");
            DropForeignKey("dbo.ReturnDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.ReferralGuideDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.ProductSupplier", "SupplierID", "dbo.Supplier");
            DropForeignKey("dbo.ProductSupplier", "ProductID", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.ReferralGuide", "DistributorID", "dbo.Distributor");
            DropForeignKey("dbo.SaleDocument", "OrderID", "dbo.Order");
            DropForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Order", "ClientID", "dbo.Client");
            DropIndex("dbo.User", new[] { "StoreID" });
            DropIndex("dbo.Stockroom", new[] { "Store_ID" });
            DropIndex("dbo.Stockroom", new[] { "ID" });
            DropIndex("dbo.Zone", new[] { "StockroomID" });
            DropIndex("dbo.Movement", new[] { "ZoneID" });
            DropIndex("dbo.Movement", new[] { "ProductID" });
            DropIndex("dbo.SaleDocumentDetail", new[] { "ProductID" });
            DropIndex("dbo.SaleDocumentDetail", new[] { "SaleDocumentID" });
            DropIndex("dbo.Return", new[] { "SaleDocumentID" });
            DropIndex("dbo.ReturnDetail", new[] { "ProductID" });
            DropIndex("dbo.ReturnDetail", new[] { "ReturnID" });
            DropIndex("dbo.ProductSupplier", new[] { "Stockroom_ID" });
            DropIndex("dbo.ProductSupplier", new[] { "SupplierID" });
            DropIndex("dbo.ProductSupplier", new[] { "ProductID" });
            DropIndex("dbo.ReferralGuideDetail", new[] { "ReferralGuide_ID" });
            DropIndex("dbo.ReferralGuideDetail", new[] { "ProductID" });
            DropIndex("dbo.ReferralGuide", new[] { "SaleDocumentID" });
            DropIndex("dbo.ReferralGuide", new[] { "DistributorID" });
            DropIndex("dbo.SaleDocument", new[] { "OrderID" });
            DropIndex("dbo.OrderDetail", new[] { "ProductID" });
            DropIndex("dbo.OrderDetail", new[] { "OrderID" });
            DropIndex("dbo.Order", new[] { "UserID" });
            DropIndex("dbo.Order", new[] { "ClientID" });
            DropTable("dbo.Store");
            DropTable("dbo.User");
            DropTable("dbo.Stockroom");
            DropTable("dbo.Zone");
            DropTable("dbo.Movement");
            DropTable("dbo.SaleDocumentDetail");
            DropTable("dbo.Return");
            DropTable("dbo.ReturnDetail");
            DropTable("dbo.Supplier");
            DropTable("dbo.ProductSupplier");
            DropTable("dbo.Product");
            DropTable("dbo.ReferralGuideDetail");
            DropTable("dbo.Distributor");
            DropTable("dbo.ReferralGuide");
            DropTable("dbo.SaleDocument");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Order");
            DropTable("dbo.Client");
        }
    }
}
