using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.EnityFrameworkWrapper.Migrations;
using KMA.APRZP2019.TextEditorProject.EnityFrameworkWrapper.ModelConfiguration;
using System.Data.Entity;

namespace KMA.APRZP2019.TextEditorProject.EnityFrameworkWrapper
{
    public class TextEditorDbContext : DbContext
    {
        public TextEditorDbContext() : base("name=DB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TextEditorDbContext,Configuration>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRequest> UserRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserRequestConfiguration());
        }
    }
}
