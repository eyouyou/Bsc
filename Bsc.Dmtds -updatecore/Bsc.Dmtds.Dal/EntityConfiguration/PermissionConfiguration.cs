using System.Data.Entity.ModelConfiguration;
using Bsc.Dmtds.Core.Module.Account;

namespace Bsc.Dmtds.Dal.EntityConfiguration
{
    public class PermissionConfiguration : EntityTypeConfiguration<Permission>
    {
        public PermissionConfiguration()
        {
            this.HasKey(it => new { it.Id, it.RoleName });

            this.Property(it => it.Name).HasColumnType("nvarchar").HasMaxLength(256);
            this.Property(it => it.AreaName).HasColumnType("nvarchar").HasMaxLength(256);
            //this.Property(it => it.Group).HasColumnType("nvarchar").HasMaxLength(256);
            this.Property(it => it.DisplayName).HasColumnType("nvarchar").HasMaxLength(256);

            this.ToTable("bc_Permission");
        } 
    }
}