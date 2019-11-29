using System.Data.Entity.ModelConfiguration;
using KMA.APRZP2019.TextEditorProject.DBModels;

namespace KMA.APRZP2019.TextEditorProject.EnityFrameworkWrapper.ModelConfiguration
{
    class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("User");
            HasKey(user => user.Guid);
            Property(user => user.Guid).HasColumnName("Guid").IsRequired();
            Property(user => user.Login).HasColumnName("Login").HasMaxLength(300).IsRequired();
            Property(user => user.FirstName).HasColumnName("FirstName").IsRequired();
            Property(user => user.LastName).HasColumnName("LastName").IsRequired();
            Property(user => user.Email).HasColumnName("Email").HasMaxLength(256).IsRequired();
            Property(user => user.Password).HasColumnName("Password").IsRequired();
            HasIndex(ind => ind.Email).IsUnique(true);
            HasIndex(ind => ind.Login).IsUnique(true);
        }
    }
}
