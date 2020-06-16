namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueTournamentName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tournaments", "TournamentName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Tournaments", "TournamentName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tournaments", new[] { "TournamentName" });
            AlterColumn("dbo.Tournaments", "TournamentName", c => c.String());
        }
    }
}
