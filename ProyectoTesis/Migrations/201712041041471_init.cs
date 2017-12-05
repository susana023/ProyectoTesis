namespace ProyectoTesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Dni = c.Int(),
                        Ruc = c.Long(),
                        Name = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        ActiveFlag = c.Boolean(nullable: false),
                        ClientType = c.Int(nullable: false),
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
                        Subtotal = c.Double(nullable: false),
                        Igv = c.Double(nullable: false),
                        DeliveredFlag = c.Boolean(),
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
                        BoxUnits = c.Int(),
                        FractionUnits = c.Int(),
                        ActiveFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
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
                        FractionUnits = c.Int(nullable: false),
                        LogicalStock = c.Double(nullable: false),
                        PhysicalStock = c.Double(nullable: false),
                        ActiveFlag = c.Boolean(nullable: false),
                        MinStock = c.Double(nullable: false),
                        MaxStock = c.Double(nullable: false),
                        ProductType = c.Int(nullable: false),
                        Codarti = c.String(),
                        PreviousCode = c.String(),
                        Weight = c.Double(nullable: false),
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
                        FractionPrice = c.Double(),
                        ActiveFlag = c.Boolean(nullable: false),
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
                        Ruc = c.Long(nullable: false),
                        BusinessName = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Email = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
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
                        ActiveFlag = c.Boolean(nullable: false),
                        Weight = c.Double(nullable: false),
                        ReferralGuide_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Document", t => t.ReferralGuide_ID)
                .Index(t => t.ProductID)
                .Index(t => t.ReferralGuide_ID);
            
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ActiveFlag = c.Boolean(nullable: false),
                        Subtotal = c.Double(nullable: false),
                        Igv = c.Double(nullable: false),
                        DistributorID = c.Int(),
                        SaleDocumentID = c.Int(),
                        ClientID = c.Int(),
                        Reason = c.Int(),
                        SupplierID = c.Int(),
                        BillCorrelative = c.Int(),
                        BillSerialNumber = c.String(),
                        SaleDocumentID1 = c.Int(),
                        Reason1 = c.Int(),
                        Correlative = c.Int(),
                        SerialNumber = c.String(),
                        OrderID = c.Int(),
                        DocumentType = c.Int(),
                        DeliveredFlag = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Distributor", t => t.DistributorID, cascadeDelete: true)
                .ForeignKey("dbo.Supplier", t => t.SupplierID, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Document", t => t.SaleDocumentID)
                .ForeignKey("dbo.Document", t => t.SaleDocumentID1)
                .Index(t => t.DistributorID)
                .Index(t => t.SaleDocumentID)
                .Index(t => t.SupplierID)
                .Index(t => t.SaleDocumentID1)
                .Index(t => t.OrderID);
            
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
                "dbo.Movement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(),
                        MovementDate = c.DateTime(),
                        ZoneID = c.Int(nullable: false),
                        MovementType = c.Int(nullable: false),
                        BoxUnits = c.Int(nullable: false),
                        FractionUnits = c.Int(nullable: false),
                        DocumentID = c.Int(),
                        ActiveFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Document", t => t.DocumentID)
                .ForeignKey("dbo.Zone", t => t.ZoneID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.ZoneID)
                .Index(t => t.DocumentID);
            
            CreateTable(
                "dbo.PurchaseOrderDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PurchaseOrderID = c.Int(nullable: false),
                        productID = c.Int(nullable: false),
                        BoxUnits = c.Int(),
                        FractionUnits = c.Int(),
                        Subtotal = c.Double(nullable: false),
                        BatchExpirationDay = c.DateTime(),
                        ZoneID = c.Int(),
                        ActiveFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.productID, cascadeDelete: true)
                .ForeignKey("dbo.Document", t => t.PurchaseOrderID, cascadeDelete: true)
                .ForeignKey("dbo.Zone", t => t.ZoneID)
                .Index(t => t.PurchaseOrderID)
                .Index(t => t.productID)
                .Index(t => t.ZoneID);
            
            CreateTable(
                "dbo.Zone",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StockroomID = c.Int(nullable: false),
                        Description = c.String(),
                        ActiveFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stockroom", t => t.StockroomID, cascadeDelete: true)
                .Index(t => t.StockroomID);
            
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
            
            CreateTable(
                "dbo.Correlative",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StoreID = c.Int(nullable: false),
                        CorrelativeNumber = c.Int(nullable: false),
                        SerialNumber = c.String(),
                        DocumentType = c.Int(nullable: false),
                        ActiveFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Store", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.StoreID);
            
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
                        ActiveFlag = c.Boolean(),
                        StoreID = c.Int(),
                        Type = c.Int(nullable: false),
                        StockroomID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Store", t => t.StoreID)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.ReturnDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReturnID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        BoxUnits = c.Int(nullable: false),
                        FractionUnits = c.Int(nullable: false),
                        Subtotal = c.Double(nullable: false),
                        ActiveFlag = c.Boolean(nullable: false),
                        Reason = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Document", t => t.ReturnID, cascadeDelete: true)
                .Index(t => t.ReturnID)
                .Index(t => t.ProductID);
            
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
                        ActiveFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Document", t => t.SaleDocumentID, cascadeDelete: true)
                .Index(t => t.SaleDocumentID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.SalesMargin",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        MarketMargin = c.Double(nullable: false),
                        StoreMargin = c.Double(nullable: false),
                        DistributionMargin = c.Double(nullable: false),
                        ActiveFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.ExpirationTime",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductType = c.Int(nullable: false),
                        Months = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductInZone",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ZoneID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        BoxUnits = c.Int(nullable: false),
                        FractionUnits = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PurchasePlanDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PurchasePlanID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Benefit = c.Double(nullable: false),
                        BoxUnits = c.Int(),
                        FractionUnits = c.Int(),
                        ActiveFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.PurchasePlan", t => t.PurchasePlanID, cascadeDelete: true)
                .Index(t => t.PurchasePlanID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.PurchasePlan",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ActiveFlag = c.Boolean(nullable: false),
                        Investment = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchasePlanDetail", "PurchasePlanID", "dbo.PurchasePlan");
            DropForeignKey("dbo.PurchasePlanDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.SalesMargin", "ID", "dbo.Product");
            DropForeignKey("dbo.ReferralGuideDetail", "ReferralGuide_ID", "dbo.Document");
            DropForeignKey("dbo.Movement", "ProductID", "dbo.Product");
            DropForeignKey("dbo.SaleDocumentDetail", "SaleDocumentID", "dbo.Document");
            DropForeignKey("dbo.SaleDocumentDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Document", "SaleDocumentID1", "dbo.Document");
            DropForeignKey("dbo.Document", "SaleDocumentID", "dbo.Document");
            DropForeignKey("dbo.Document", "OrderID", "dbo.Order");
            DropForeignKey("dbo.ReturnDetail", "ReturnID", "dbo.Document");
            DropForeignKey("dbo.ReturnDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Document", "SupplierID", "dbo.Supplier");
            DropForeignKey("dbo.Movement", "ZoneID", "dbo.Zone");
            DropForeignKey("dbo.Zone", "StockroomID", "dbo.Stockroom");
            DropForeignKey("dbo.User", "StoreID", "dbo.Store");
            DropForeignKey("dbo.Order", "UserID", "dbo.User");
            DropForeignKey("dbo.Stockroom", "StoreID", "dbo.Store");
            DropForeignKey("dbo.Correlative", "StoreID", "dbo.Store");
            DropForeignKey("dbo.ProductSupplier", "Stockroom_ID", "dbo.Stockroom");
            DropForeignKey("dbo.PurchaseOrderDetail", "ZoneID", "dbo.Zone");
            DropForeignKey("dbo.PurchaseOrderDetail", "PurchaseOrderID", "dbo.Document");
            DropForeignKey("dbo.PurchaseOrderDetail", "productID", "dbo.Product");
            DropForeignKey("dbo.Movement", "DocumentID", "dbo.Document");
            DropForeignKey("dbo.Document", "DistributorID", "dbo.Distributor");
            DropForeignKey("dbo.ReferralGuideDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.ProductSupplier", "SupplierID", "dbo.Supplier");
            DropForeignKey("dbo.ProductSupplier", "ProductID", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Order", "ClientID", "dbo.Client");
            DropIndex("dbo.PurchasePlanDetail", new[] { "ProductID" });
            DropIndex("dbo.PurchasePlanDetail", new[] { "PurchasePlanID" });
            DropIndex("dbo.SalesMargin", new[] { "ID" });
            DropIndex("dbo.SaleDocumentDetail", new[] { "ProductID" });
            DropIndex("dbo.SaleDocumentDetail", new[] { "SaleDocumentID" });
            DropIndex("dbo.ReturnDetail", new[] { "ProductID" });
            DropIndex("dbo.ReturnDetail", new[] { "ReturnID" });
            DropIndex("dbo.User", new[] { "StoreID" });
            DropIndex("dbo.Correlative", new[] { "StoreID" });
            DropIndex("dbo.Stockroom", new[] { "StoreID" });
            DropIndex("dbo.Zone", new[] { "StockroomID" });
            DropIndex("dbo.PurchaseOrderDetail", new[] { "ZoneID" });
            DropIndex("dbo.PurchaseOrderDetail", new[] { "productID" });
            DropIndex("dbo.PurchaseOrderDetail", new[] { "PurchaseOrderID" });
            DropIndex("dbo.Movement", new[] { "DocumentID" });
            DropIndex("dbo.Movement", new[] { "ZoneID" });
            DropIndex("dbo.Movement", new[] { "ProductID" });
            DropIndex("dbo.Document", new[] { "OrderID" });
            DropIndex("dbo.Document", new[] { "SaleDocumentID1" });
            DropIndex("dbo.Document", new[] { "SupplierID" });
            DropIndex("dbo.Document", new[] { "SaleDocumentID" });
            DropIndex("dbo.Document", new[] { "DistributorID" });
            DropIndex("dbo.ReferralGuideDetail", new[] { "ReferralGuide_ID" });
            DropIndex("dbo.ReferralGuideDetail", new[] { "ProductID" });
            DropIndex("dbo.ProductSupplier", new[] { "Stockroom_ID" });
            DropIndex("dbo.ProductSupplier", new[] { "SupplierID" });
            DropIndex("dbo.ProductSupplier", new[] { "ProductID" });
            DropIndex("dbo.OrderDetail", new[] { "ProductID" });
            DropIndex("dbo.OrderDetail", new[] { "OrderID" });
            DropIndex("dbo.Order", new[] { "UserID" });
            DropIndex("dbo.Order", new[] { "ClientID" });
            DropTable("dbo.PurchasePlan");
            DropTable("dbo.PurchasePlanDetail");
            DropTable("dbo.ProductInZone");
            DropTable("dbo.ExpirationTime");
            DropTable("dbo.SalesMargin");
            DropTable("dbo.SaleDocumentDetail");
            DropTable("dbo.ReturnDetail");
            DropTable("dbo.User");
            DropTable("dbo.Correlative");
            DropTable("dbo.Store");
            DropTable("dbo.Stockroom");
            DropTable("dbo.Zone");
            DropTable("dbo.PurchaseOrderDetail");
            DropTable("dbo.Movement");
            DropTable("dbo.Distributor");
            DropTable("dbo.Document");
            DropTable("dbo.ReferralGuideDetail");
            DropTable("dbo.Supplier");
            DropTable("dbo.ProductSupplier");
            DropTable("dbo.Product");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Order");
            DropTable("dbo.Client");
        }
    }
}
