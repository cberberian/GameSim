namespace SimGame.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuildingUpgrades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Completed = c.Boolean(nullable: false),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartManufactureDateTime = c.DateTime(),
                        Quantity = c.Int(),
                        ProductTypeId = c.Int(nullable: false),
                        RequiredByTypeId = c.Int(),
                        ManufacturerId = c.Int(),
                        TimeToFulfill = c.Int(),
                        OrderId = c.Int(),
                        IsCityStorage = c.Boolean(nullable: false),
                        BuildingUpgradeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BuildingUpgrades", t => t.BuildingUpgradeId)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ProductTypes", t => t.RequiredByTypeId)
                .Index(t => t.ProductTypeId)
                .Index(t => t.RequiredByTypeId)
                .Index(t => t.ManufacturerId)
                .Index(t => t.OrderId)
                .Index(t => t.BuildingUpgradeId);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturerTypeId = c.Int(nullable: false),
                        QueueSize = c.Int(),
                        Description = c.String(),
                        IsCityStorage = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ManufacturerTypes", t => t.ManufacturerTypeId, cascadeDelete: true)
                .Index(t => t.ManufacturerTypeId);
            
            CreateTable(
                "dbo.ManufacturerTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        QueueSize = c.Int(),
                        HasFixedQueueSize = c.Boolean(nullable: false),
                        SupportsParallelManufacturing = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TimeToManufacture = c.Int(),
                        SalePriceInDollars = c.Int(nullable: false),
                        QuantityInStorage = c.Int(nullable: false),
                        Name = c.String(),
                        ManufacturerTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ManufacturerTypes", t => t.ManufacturerTypeId, cascadeDelete: true)
                .Index(t => t.ManufacturerTypeId);
            
            CreateTable(
                "dbo.ManufacturingQueueSlots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Active = c.Boolean(nullable: false),
                        SlotNumber = c.Int(),
                        ProductId = c.Int(),
                        DateTimeQueued = c.DateTime(),
                        DurationItMuniutes = c.Int(),
                        ManufacturerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "RequiredByTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.Products", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.ManufacturingQueueSlots", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ManufacturingQueueSlots", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.Manufacturers", "ManufacturerTypeId", "dbo.ManufacturerTypes");
            DropForeignKey("dbo.ProductTypes", "ManufacturerTypeId", "dbo.ManufacturerTypes");
            DropForeignKey("dbo.Products", "BuildingUpgradeId", "dbo.BuildingUpgrades");
            DropIndex("dbo.ManufacturingQueueSlots", new[] { "ManufacturerId" });
            DropIndex("dbo.ManufacturingQueueSlots", new[] { "ProductId" });
            DropIndex("dbo.ProductTypes", new[] { "ManufacturerTypeId" });
            DropIndex("dbo.Manufacturers", new[] { "ManufacturerTypeId" });
            DropIndex("dbo.Products", new[] { "BuildingUpgradeId" });
            DropIndex("dbo.Products", new[] { "OrderId" });
            DropIndex("dbo.Products", new[] { "ManufacturerId" });
            DropIndex("dbo.Products", new[] { "RequiredByTypeId" });
            DropIndex("dbo.Products", new[] { "ProductTypeId" });
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.ManufacturingQueueSlots");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.ManufacturerTypes");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.Products");
            DropTable("dbo.BuildingUpgrades");
        }
    }
}
