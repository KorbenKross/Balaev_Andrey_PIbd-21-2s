namespace VirtualStorePlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ret1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buyers", "Mail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Buyers", "Mail");
        }
    }
}
