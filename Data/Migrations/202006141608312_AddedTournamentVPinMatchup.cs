namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTournamentVPinMatchup : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TournamentTeams", newName: "TeamTournaments");
            DropPrimaryKey("dbo.TeamTournaments");
            AddPrimaryKey("dbo.TeamTournaments", new[] { "Team_Id", "Tournament_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TeamTournaments");
            AddPrimaryKey("dbo.TeamTournaments", new[] { "Tournament_Id", "Team_Id" });
            RenameTable(name: "dbo.TeamTournaments", newName: "TournamentTeams");
        }
    }
}
