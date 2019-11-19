namespace KMA.APRZP2019.TextEditorProject.EnityFrameworkWrapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRequest",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        Filepath = c.String(nullable: false),
                        IsFileChanged = c.Boolean(nullable: false),
                        ChangedAt = c.DateTime(nullable: false),
                        User_Guid = c.Guid(),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.User", t => t.User_Guid)
                .Index(t => t.User_Guid);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 256),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRequest", "User_Guid", "dbo.User");
            DropIndex("dbo.User", new[] { "Email" });
            DropIndex("dbo.UserRequest", new[] { "User_Guid" });
            DropTable("dbo.User");
            DropTable("dbo.UserRequest");
        }
    }
}
