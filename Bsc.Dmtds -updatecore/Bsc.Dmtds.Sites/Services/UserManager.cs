using System;
using System.Collections.Generic;
using System.Linq;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Module.Account;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Service;
using Bsc.Dmtds.Sites.Models;
using Bsc.Dmtds.Sites.Persistence;
using User = Bsc.Dmtds.Sites.Models.User;

namespace Bsc.Dmtds.Sites.Services
{
    public class UserManager : ManagerBase<User, IUserProvider>
    {
        public IUserService UserService { get;private set; }
        public IRoleService RoleService { get;private set; }
        public UserManager(IUserProvider provider,IUserService accountUserManager, IRoleService roleManager)
            : base(provider)
        {
            this.UserService = accountUserManager;
            this.RoleService = roleManager;
        }


        #region All/Get/Update
        public override IEnumerable<User> All(Site site, string filterName)
        {
            var result = Provider.All(site).Select(it => it.AsActual());
            if (!string.IsNullOrEmpty(filterName))
            {
                result = result.Where(it => it.UserName.Contains(filterName, StringComparison.CurrentCultureIgnoreCase));
            }
            return result;
        }

        public override User Get(Site site, string name)
        {
            return Provider.Get(new User() { Site = site, UserName = name });
        }

        public override void Update(Site site, User @new, User old)
        {
            @new.Site = site;
            @old.Site = site;
            Provider.Update(@new, @old);
        }
        #endregion

        #region IsInRole
        public virtual bool IsInRole(Site site, string userName, string role)
        {
            if (IsAdministrator(userName))
            {
                return true;
            }
            var siteUser = this.Get(site, userName);

            if (siteUser != null && siteUser.Roles != null)
            {
                return siteUser.Roles.Any(it => it.EqualsOrNullEmpty(role, StringComparison.OrdinalIgnoreCase));
            }
            return false;
        }
        #endregion

        #region Authorize
        public virtual bool Authorize(Site site, string userName, Permission permission)
        {
            string contextKey = "Permission:" + permission.ToString();
            var allow = CallContext.Current.GetObject<bool?>(contextKey);
            if (!allow.HasValue)
            {
                allow = false;

                if (IsAdministrator(userName))
                {
                    allow = true;
                }
                else
                {
                    var roles = GetRoles(site, userName);
                    allow = roles.Any(it => it.HasPermission(permission));
                }                
                CallContext.Current.RegisterObject(contextKey, allow);
            }
            return allow.Value;
        }

        public virtual bool IsAdministrator(string userName)
        {
            var accountUser = UserService.Get(userName);
            if (accountUser == null)
            {
                return false;
            }
            return accountUser.IsAdministrator;
        }

        public virtual bool AllowCreatingSite(string userName)
        {
            return IsAdministrator(userName);
        }

        protected virtual IEnumerable<Role> GetRoles(Site site, string userName)
        {
            if (site != null)
            {
                var siteUser = this.Get(site, userName);

                if (siteUser != null && siteUser.Roles != null)
                {
                    return siteUser.Roles.Select(it => RoleService.Get(it)).Where(it => it != null);
                }
            }

            var accountUser = UserService.Get(userName);
            if (accountUser != null && !string.IsNullOrEmpty(accountUser.GlobalRoles))
            {
                return accountUser.GlobalRoles.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(it => RoleService.Get(it)).Where(it => it != null);
            }

            return new Role[0];
        }
        public virtual bool Authorize(Site site, string userName)
        {
            if (IsAdministrator(userName))
            {
                return true;
            }
            return GetRoles(site, userName).Count() > 0;
        }
        #endregion


    }
}