using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Bsc.Dmtds.Common.HealthMonitoring;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Module.Account;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Dal.EntityConfiguration;

namespace Bsc.Dmtds.Dal
{
        
    [Dependency(ComponentLifeStyle.InRequestScope)]
    [Dependency(typeof(IQueryableUnitOfWork),ComponentLifeStyle.InRequestScope)]
    public class UnitOfWork : DbContext, IQueryableUnitOfWork
    {
        public UnitOfWork()
            : base("name=DmtdsConnection")
        {
            base.Configuration.ProxyCreationEnabled = true;
            base.Configuration.LazyLoadingEnabled = true;

            Database.SetInitializer(new BscDbInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new PermissionConfiguration());
        }

        #region IQueryableUnitOfWork接口成员

        #region iquery接口成员

        public DbSet<T> CreateSet<T>()
            where T : class
        {
            return base.Set<T>();
        }

        public void Attach<T>(T item)
            where T : class
        {
            base.Entry<T>(item).State = EntityState.Unchanged;
        }

        public void SetModified<T>(T item)
            where T : class
        {
            base.Entry<T>(item).State = EntityState.Modified;
        }

        public void ApplyCurrentValues<T>(T original, T current)
            where T : class
        {
            base.Entry<T>(original).CurrentValues.SetValues(current);
        }


        #endregion

        #region isql成员

        public IEnumerable<T> ExecuteQuery<T>(string sqlQuery, params object[] parameters)
        {
            return base.Database.SqlQuery<T>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion

        #region iunitofwork成员

        public void Commit()
        {
            try
            {
                base.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
            }
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                        .ForEach(entry =>
                        {
                            entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                        });

                }
            } while (saveFailed);
        }

        public void RollbackChanges()
        {
            base.ChangeTracker.Entries()
                .ToList()
                .ForEach(entry => entry.State = EntityState.Unchanged);

        }

        #endregion

        #endregion

        #region 实体

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }
        #endregion

    }

    public class BscDbInitializer : DropCreateDatabaseIfModelChanges<UnitOfWork>
    {
        protected override void Seed(UnitOfWork context)
        {
            InitializeIdentityForEf(context);
            base.Seed(context);
        }

        private static void InitializeIdentityForEf(UnitOfWork db)
        {
//            const string name = "admin@example.com";
//            const string password = "Admin@123456";
//            const string roleName = "Admin";
//            DbSet<User> userDbSet = db.CreateSet<User>();
//            DbSet<Role> roleDbSet = db.CreateSet<Role>();
//            DbSet<UserRole> userroleDbSet = db.CreateSet<UserRole>();
//            Task<Role> roleFindbyname= roleDbSet.FirstOrDefaultAsync<Role>((Expression<Func<Role, bool>>)(u => u.UUID.ToUpper() == roleName.ToUpper()));
//            var role=AsyncHelper.RunSync<Role>((Func<Task<Role>>)(() => roleFindbyname));
//            if (role == null)
//            {
//                role = new Role();
//                role.Name = roleName;
//                var roleresult = roleDbSet.Add(role);
//                db.Commit();
//            }
//            Task<User> userFindbyname = userDbSet.FirstOrDefaultAsync<User>((Expression<Func<User, bool>>)(u => u.UserName.ToUpper() == name.ToUpper()));
//            var user = AsyncHelper.RunSync<User>((Func<Task<User>>)(() => userFindbyname));
//            if (user==null)
//            {
//                user = new User { UserName = name, EmailConfirmed = true ,Email = name, IsLockedOut = false, PasswordSalt = CryptoHelper.HashPassword(password)};
//                userDbSet.Add(user);
//                db.Commit();
//            }
//            userroleDbSet.Add(new UserRole {RoleId = role.UUID, UserId = user.UUID});
//            db.Commit();
        }

    }
}
