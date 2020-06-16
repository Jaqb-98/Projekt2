namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CaliburnMicro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matchups", "Tournament_Id", c => c.Int());
            CreateIndex("dbo.Matchups", "Tournament_Id");
            AddForeignKey("dbo.Matchups", "Tournament_Id", "dbo.Tournaments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matchups", "Tournament_Id", "dbo.Tournaments");
            DropIndex("dbo.Matchups", new[] { "Tournament_Id" });
            DropColumn("dbo.Matchups", "Tournament_Id");
        }
    }
}
