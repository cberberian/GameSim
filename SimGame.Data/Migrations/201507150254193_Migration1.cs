namespace SimGame.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturerTypeId = c.Int(nullable: false),
                        QueueSize = c.Int(nullable: false),
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
                        QueueSize = c.Int(nullable: false),
                        HasFixedQueueSize = c.Boolean(nullable: false),
                        SupportsParallelManufacturing = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        TimeToManufacture = c.Int(),
                        ManufacturerTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ManufacturerTypes", t => t.ManufacturerTypeId, cascadeDelete: true)
                .Index(t => t.ManufacturerTypeId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        ProductTypeId = c.Int(nullable: false),
                        ManufacturerId = c.Int(),
                        TimeToFulfill = c.Int(),
                        OrderId = c.Int(),
                        ProductType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ProductTypes", t => t.ProductType_Id)
                .Index(t => t.ProductTypeId)
                .Index(t => t.ManufacturerId)
                .Index(t => t.OrderId)
                .Index(t => t.ProductType_Id);
            
            CreateTable(
                "dbo.ManufacturingQueueSlots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Active = c.Boolean(nullable: false),
                        SlotNumber = c.Int(nullable: false),
                        ProductId = c.Int(),
                        DateTimeQueued = c.DateTime(),
                        DurationItMuniutes = c.Int(),
                        ManufacturerId = c.Int(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Manufacturers", "ManufacturerTypeId", "dbo.ManufacturerTypes");
            DropForeignKey("dbo.Products", "ProductType_Id", "dbo.ProductTypes");
            DropForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.Products", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.ManufacturingQueueSlots", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ManufacturingQueueSlots", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.ProductTypes", "ManufacturerTypeId", "dbo.ManufacturerTypes");
            DropIndex("dbo.ManufacturingQueueSlots", new[] { "ManufacturerId" });
            DropIndex("dbo.ManufacturingQueueSlots", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "ProductType_Id" });
            DropIndex("dbo.Products", new[] { "OrderId" });
            DropIndex("dbo.Products", new[] { "ManufacturerId" });
            DropIndex("dbo.Products", new[] { "ProductTypeId" });
            DropIndex("dbo.ProductTypes", new[] { "ManufacturerTypeId" });
            DropIndex("dbo.Manufacturers", new[] { "ManufacturerTypeId" });
            DropTable("dbo.Orders");
            DropTable("dbo.ManufacturingQueueSlots");
            DropTable("dbo.Products");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.ManufacturerTypes");
            DropTable("dbo.Manufacturers");
        }
    }
}
