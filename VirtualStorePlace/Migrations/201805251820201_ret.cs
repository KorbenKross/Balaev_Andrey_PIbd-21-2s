namespace VirtualStorePlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ret : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Buyers", "Mail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Buyers", "Mail", c => c.String());
        }
    }
}
