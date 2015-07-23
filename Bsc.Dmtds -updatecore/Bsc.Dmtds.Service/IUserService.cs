using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Bsc.Dmtds.Core.Module.Account;


namespace Bsc.Dmtds.Service
{
    /// <summary>
    /// 用户操作
    /// </summary>
    public interface IUserService
    {


        #region 个人用户操作

        void Add(User user);
        void Delete(string userName);
        User Get(string userName);
        IEnumerable<User> All();
        void Update(string userName, User newUser);
        bool ChangePassword(string userName, string oldPassword, string newPassword);

        #endregion

        bool IsAdministrator(string userName);

        #region 登陆方式的操作


        #endregion

        #region 通用

        #endregion
    }

    public interface IUserStoreService
    {

    }



}
