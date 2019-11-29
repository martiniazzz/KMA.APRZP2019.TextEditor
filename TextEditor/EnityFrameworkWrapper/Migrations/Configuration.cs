namespace KMA.APRZP2019.TextEditorProject.EnityFrameworkWrapper.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<KMA.APRZP2019.TextEditorProject.EnityFrameworkWrapper.TextEditorDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KMA.APRZP2019.TextEditorProject.EnityFrameworkWrapper.TextEditorDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
