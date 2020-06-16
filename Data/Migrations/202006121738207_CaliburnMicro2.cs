namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CaliburnMicro2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TournamentTeams", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.TournamentTeams", "Team_Id", "dbo.Teams");
            DropIndex("dbo.TournamentTeams", new[] { "Tournament_Id" });
            DropIndex("dbo.TournamentTeams", new[] { "Team_Id" });
            AddColumn("dbo.Teams", "Tournament_Id", c => c.Int());
            CreateIndex("dbo.Teams", "Tournament_Id");
            AddForeignKey("dbo.Teams", "Tournament_Id", "dbo.Tournaments", "Id");
            DropTable("dbo.TournamentTeams");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TournamentTeams",
                c => new
                    {
                        Tournament_Id = c.Int(nullable: false),
                        Team_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tournament_Id, t.Team_Id });
            
            DropForeignKey("dbo.Teams", "Tournament_Id", "dbo.Tournaments");
            DropIndex("dbo.Teams", new[] { "Tournament_Id" });
            DropColumn("dbo.Teams", "Tournament_Id");
            CreateIndex("dbo.TournamentTeams", "Team_Id");
            CreateIndex("dbo.TournamentTeams", "Tournament_Id");
            AddForeignKey("dbo.TournamentTeams", "Team_Id", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TournamentTeams", "Tournament_Id", "dbo.Tournaments", "Id", cascadeDelete: true);
        }
    }
}
