namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatchupEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Double(nullable: false),
                        ParentMatchup_Id = c.Int(),
                        TeamCompeting_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Matchups", t => t.ParentMatchup_Id)
                .ForeignKey("dbo.Teams", t => t.TeamCompeting_Id)
                .Index(t => t.ParentMatchup_Id)
                .Index(t => t.TeamCompeting_Id);
            
            CreateTable(
                "dbo.Matchups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MatchupRound = c.Int(nullable: false),
                        Winner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Winner_Id)
                .Index(t => t.Winner_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Tournament_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tournaments", t => t.Tournament_Id)
                .Index(t => t.Tournament_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .Index(t => t.Team_Id);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TournamentName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.MatchupEntries", "TeamCompeting_Id", "dbo.Teams");
            DropForeignKey("dbo.Matchups", "Winner_Id", "dbo.Teams");
            DropForeignKey("dbo.People", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.MatchupEntries", "ParentMatchup_Id", "dbo.Matchups");
            DropIndex("dbo.People", new[] { "Team_Id" });
            DropIndex("dbo.Teams", new[] { "Tournament_Id" });
            DropIndex("dbo.Matchups", new[] { "Winner_Id" });
            DropIndex("dbo.MatchupEntries", new[] { "TeamCompeting_Id" });
            DropIndex("dbo.MatchupEntries", new[] { "ParentMatchup_Id" });
            DropTable("dbo.Tournaments");
            DropTable("dbo.People");
            DropTable("dbo.Teams");
            DropTable("dbo.Matchups");
            DropTable("dbo.MatchupEntries");
        }
    }
}
