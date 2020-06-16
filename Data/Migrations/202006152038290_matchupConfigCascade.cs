namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class matchupConfigCascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MatchupEntries", "ParentMatchup_Id", "dbo.Matchups");
            DropIndex("dbo.MatchupEntries", new[] { "ParentMatchup_Id" });
            AlterColumn("dbo.MatchupEntries", "ParentMatchup_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.MatchupEntries", "ParentMatchup_Id");
            AddForeignKey("dbo.MatchupEntries", "ParentMatchup_Id", "dbo.Matchups", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MatchupEntries", "ParentMatchup_Id", "dbo.Matchups");
            DropIndex("dbo.MatchupEntries", new[] { "ParentMatchup_Id" });
            AlterColumn("dbo.MatchupEntries", "ParentMatchup_Id", c => c.Int());
            CreateIndex("dbo.MatchupEntries", "ParentMatchup_Id");
            AddForeignKey("dbo.MatchupEntries", "ParentMatchup_Id", "dbo.Matchups", "Id");
        }
    }
}
