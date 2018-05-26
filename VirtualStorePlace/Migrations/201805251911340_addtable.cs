namespace VirtualStorePlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.String(),
                        FromMailAddress = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        DateDelivery = c.DateTime(nullable: false),
                        BuyerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buyers", t => t.BuyerId)
                .Index(t => t.BuyerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageInfoes", "BuyerId", "dbo.Buyers");
            DropIndex("dbo.MessageInfoes", new[] { "BuyerId" });
            DropTable("dbo.MessageInfoes");
        }
    }
}
