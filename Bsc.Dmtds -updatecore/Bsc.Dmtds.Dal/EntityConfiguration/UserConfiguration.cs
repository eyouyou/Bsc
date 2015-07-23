using System.Data.Entity.ModelConfiguration;
using Bsc.Dmtds.Core.Module.Account;


namespace Bsc.Dmtds.Dal.EntityConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.HasKey(it => it.Email);
            this.Property(it => it.UserName).HasColumnType("nvarchar").HasMaxLength(256);
            this.Property(it => it.IsAdministrator);
            this.Property(it => it.CustomFieldsXml).HasColumnType("text");

            this.Property(it => it.Password).HasColumnType("nvarchar").HasMaxLength(256);
            this.Property(it => it.PasswordSalt).HasColumnType("nvarchar").HasMaxLength(256);
            this.Property(it => it.FailedPasswordAttemptCount);
            this.Property(it => it.IsLockedOut);
            this.Property(it => it.LastLockoutDate);
            this.Property(it => it.ActivateCode).HasColumnType("nvarchar").HasMaxLength(256);
            this.Property(it => it.GlobalRoles).HasColumnType("nvarchar").HasMaxLength(256);

            //this.Ignore(it => it.IsDummy);
            this.Ignore(it => it.UUID);
            //this.HasKey(it => new { it.UserName});
            this.ToTable("bc_Users");
        }
    }
}