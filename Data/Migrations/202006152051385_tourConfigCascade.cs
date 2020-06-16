namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tourConfigCascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Matchups", "Tournament_Id", "dbo.Tournaments");
            DropIndex("dbo.Matchups", new[] { "Tournament_Id" });
            AlterColumn("dbo.Matchups", "Tournament_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Matchups", "Tournament_Id");
            AddForeignKey("dbo.Matchups", "Tournament_Id", "dbo.Tournaments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matchups", "Tournament_Id", "dbo.Tournaments");
            DropIndex("dbo.Matchups", new[] { "Tournament_Id" });
            AlterColumn("dbo.Matchups", "Tournament_Id", c => c.Int());
            CreateIndex("dbo.Matchups", "Tournament_Id");
            AddForeignKey("dbo.Matchups", "Tournament_Id", "dbo.Tournaments", "Id");
        }
    }
}
