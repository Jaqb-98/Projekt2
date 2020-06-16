namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueTeamName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teams", "Name", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Teams", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Teams", new[] { "Name" });
            AlterColumn("dbo.Teams", "Name", c => c.String());
        }
    }
}
