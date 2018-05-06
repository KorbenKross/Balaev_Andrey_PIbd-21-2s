namespace VirtualStorePlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buyers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuyerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerSelections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuyerId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        KitchenerId = c.Int(),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buyers", t => t.BuyerId, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Kitcheners", t => t.KitchenerId)
                .Index(t => t.BuyerId)
                .Index(t => t.IngredientId)
                .Index(t => t.KitchenerId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngredientName = c.String(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IngredientElements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngredientId = c.Int(nullable: false),
                        ElementId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Elements", t => t.ElementId, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .Index(t => t.IngredientId)
                .Index(t => t.ElementId);
            
            CreateTable(
                "dbo.Elements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ElementName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductStorageElements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductStorageId = c.Int(nullable: false),
                        ElementId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Elements", t => t.ElementId, cascadeDelete: true)
                .ForeignKey("dbo.ProductStorages", t => t.ProductStorageId, cascadeDelete: true)
                .Index(t => t.ProductStorageId)
                .Index(t => t.ElementId);
            
            CreateTable(
                "dbo.ProductStorages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductStorageName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kitcheners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KitchenerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerSelections", "KitchenerId", "dbo.Kitcheners");
            DropForeignKey("dbo.IngredientElements", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.ProductStorageElements", "ProductStorageId", "dbo.ProductStorages");
            DropForeignKey("dbo.ProductStorageElements", "ElementId", "dbo.Elements");
            DropForeignKey("dbo.IngredientElements", "ElementId", "dbo.Elements");
            DropForeignKey("dbo.CustomerSelections", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.CustomerSelections", "BuyerId", "dbo.Buyers");
            DropIndex("dbo.ProductStorageElements", new[] { "ElementId" });
            DropIndex("dbo.ProductStorageElements", new[] { "ProductStorageId" });
            DropIndex("dbo.IngredientElements", new[] { "ElementId" });
            DropIndex("dbo.IngredientElements", new[] { "IngredientId" });
            DropIndex("dbo.CustomerSelections", new[] { "KitchenerId" });
            DropIndex("dbo.CustomerSelections", new[] { "IngredientId" });
            DropIndex("dbo.CustomerSelections", new[] { "BuyerId" });
            DropTable("dbo.Kitcheners");
            DropTable("dbo.ProductStorages");
            DropTable("dbo.ProductStorageElements");
            DropTable("dbo.Elements");
            DropTable("dbo.IngredientElements");
            DropTable("dbo.Ingredients");
            DropTable("dbo.CustomerSelections");
            DropTable("dbo.Buyers");
        }
    }
}
