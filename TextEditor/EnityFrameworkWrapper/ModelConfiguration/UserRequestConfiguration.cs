using KMA.APRZP2019.TextEditorProject.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.APRZP2019.TextEditorProject.EnityFrameworkWrapper.ModelConfiguration
{
    class UserRequestConfiguration : EntityTypeConfiguration<UserRequest>
    {
        public UserRequestConfiguration()
        {
            ToTable("UserRequest");
            HasKey(req => req.Guid);
            Property(req => req.Guid).HasColumnName("Guid").IsRequired();
            Property(req => req.Filepath).HasColumnName("Filepath").IsRequired();
            Property(req => req.IsFileChanged).HasColumnName("IsFileChanged").IsRequired();
            Property(req => req.ChangedAt).HasColumnName("ChangedAt").IsRequired();
        }
    }
}
