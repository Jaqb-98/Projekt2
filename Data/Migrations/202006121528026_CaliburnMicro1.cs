namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CaliburnMicro1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teams", "Tournament_Id", "dbo.Tournaments");
            DropIndex("dbo.Teams", new[] { "Tournament_Id" });
            CreateTable(
                "dbo.TournamentTeams",
                c => new
                    {
                        Tournament_Id = c.Int(nullable: false),
                        Team_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tournament_Id, t.Team_Id })
                .ForeignKey("dbo.Tournaments", t => t.Tournament_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .Index(t => t.Tournament_Id)
                .Index(t => t.Team_Id);
            
            DropColumn("dbo.Teams", "Tournament_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "Tournament_Id", c => c.Int());
            DropForeignKey("dbo.TournamentTeams", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.TournamentTeams", "Tournament_Id", "dbo.Tournaments");
            DropIndex("dbo.TournamentTeams", new[] { "Team_Id" });
            DropIndex("dbo.TournamentTeams", new[] { "Tournament_Id" });
            DropTable("dbo.TournamentTeams");
            CreateIndex("dbo.Teams", "Tournament_Id");
            AddForeignKey("dbo.Teams", "Tournament_Id", "dbo.Tournaments", "Id");
        }
    }
}
