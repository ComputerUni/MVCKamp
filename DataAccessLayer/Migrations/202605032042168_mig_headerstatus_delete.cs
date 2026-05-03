namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_headerstatus_delete : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Headings", "HeadingStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Headings", "HeadingStatus", c => c.Boolean(nullable: false));
        }
    }
}
