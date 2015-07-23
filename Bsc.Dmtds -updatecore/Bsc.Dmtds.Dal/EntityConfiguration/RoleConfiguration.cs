using System.Data.Entity.ModelConfiguration;
using Bsc.Dmtds.Core.Module.Account;


namespace Bsc.Dmtds.Dal.EntityConfiguration
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            this.HasKey(it => it.Name);
            this.HasMany(it => it.Permissions);
            this.Ignore(it => it.UUID);
            this.ToTable("bc_Roles");
        } 
    }
}