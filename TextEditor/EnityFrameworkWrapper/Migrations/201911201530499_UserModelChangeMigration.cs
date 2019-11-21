namespace KMA.APRZP2019.TextEditorProject.EnityFrameworkWrapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserModelChangeMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Login", c => c.String(nullable: false, maxLength: 300));
            CreateIndex("dbo.User", "Login", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "Login" });
            DropColumn("dbo.User", "Login");
        }
    }
}
