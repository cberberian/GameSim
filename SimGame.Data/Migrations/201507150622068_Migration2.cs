namespace SimGame.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Products", name: "ProductType_Id", newName: "RequiredByTypeId");
            RenameIndex(table: "dbo.Products", name: "IX_ProductType_Id", newName: "IX_RequiredByTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Products", name: "IX_RequiredByTypeId", newName: "IX_ProductType_Id");
            RenameColumn(table: "dbo.Products", name: "RequiredByTypeId", newName: "ProductType_Id");
        }
    }
}
