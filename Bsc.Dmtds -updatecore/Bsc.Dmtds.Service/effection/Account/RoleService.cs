using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Core.Module.Account;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Dal;

using Bsc.Dmtds.Respository.IRepository;

namespace Bsc.Dmtds.Service.effection.Account
{
    /// <summary>
    /// 角色管理配置
    /// </summary>
    [Dependency(typeof(RoleService))]
    public class RoleService :IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;

        }
        static IList<Permission> permissions = new List<Permission>();


        #region Permissions
        public virtual IEnumerable<Permission> AllPermissions()
        {
            return permissions;
        }
        public virtual void AddPermission(Permission permission)
        {
            permissions.Add(permission);
        }
        #endregion

        #region Roles
        public virtual void Add(Role role)
        {
            _roleRepository.Add(role);
        }
        public virtual void Delete(string roleName)
        {
            _roleRepository.Remove(new Role() { Name = roleName });
        }
        public virtual Role Get(string roleName)
        {
            return _roleRepository.Get(new Role() { Name = roleName });
        }
        public virtual IEnumerable<Role> All()
        {
            return _roleRepository.All();
        }
        public virtual void Update(string roleName, Role newRole)
        {
            var old = Get(roleName);
            _roleRepository.Update(newRole, old);
        }
        #endregion


    }

}