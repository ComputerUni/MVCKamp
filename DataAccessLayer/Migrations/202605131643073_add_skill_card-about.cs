namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_skill_cardabout : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CardAbouts",
                c => new
                    {
                        CardAboutID = c.Int(nullable: false, identity: true),
                        CardAboutName = c.String(),
                        CardAboutTitle = c.String(),
                        CardImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.CardAboutID);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillID = c.Int(nullable: false, identity: true),
                        SkillName = c.String(),
                        Percantage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SkillID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Skills");
            DropTable("dbo.CardAbouts");
        }
    }
}
