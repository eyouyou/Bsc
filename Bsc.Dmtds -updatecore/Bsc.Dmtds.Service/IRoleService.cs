using System.Collections.Generic;
using System.Threading.Tasks;
using Bsc.Dmtds.Core.Module.Account;
using Bsc.Dmtds.Core.Runtime.Dependency;


namespace Bsc.Dmtds.Service
{
    /// <summary>
    /// 角色操作
    /// </summary>
    public interface IRoleService
    {
        IEnumerable<Permission> AllPermissions();
        void AddPermission(Permission permission);
        void Add(Role role);
        void Delete(string roleName);
        Role Get(string roleName);
        IEnumerable<Role> All();
        void Update(string roleName, Role newRole);
    }


}