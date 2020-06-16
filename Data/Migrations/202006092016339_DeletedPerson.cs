namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedPerson : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "Team_Id", "dbo.Teams");
            DropIndex("dbo.People", new[] { "Team_Id" });
            DropTable("dbo.People");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.People", "Team_Id");
            AddForeignKey("dbo.People", "Team_Id", "dbo.Teams", "Id");
        }
    }
}
