using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.EnityFrameworkWrapper.Migrations;
using KMA.APRZP2019.TextEditorProject.EnityFrameworkWrapper.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.APRZP2019.TextEditorProject.EnityFrameworkWrapper
{
    class TextEditorDbContext : DbContext
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
