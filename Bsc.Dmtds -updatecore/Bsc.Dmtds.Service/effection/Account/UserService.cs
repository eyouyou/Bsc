using System;
using System.Collections.Generic;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Core.DataViolation;
using Bsc.Dmtds.Core.Module.Account;
using Bsc.Dmtds.Core.Runtime.Dependency;

using Bsc.Dmtds.Respository.IRepository;

namespace Bsc.Dmtds.Service.effection.Account
{
    [Dependency(typeof(IUserService))]
    public partial class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISettingPepository _settingPepository;
        public PasswordRepository _PasswordRepository;


        public UserService(IUserRepository userRepository, PasswordRepository passwordRepository, ISettingPepository settingPepository)
        {
            _userRepository = userRepository;
            _PasswordRepository = passwordRepository;
            _settingPepository = settingPepository;
        }

        #region 接口实现

        #region 个人用户操作

        #region Add
        public virtual void Add(User user)
        {
            #region Validate data
            List<DataViolationItem> violations = new List<DataViolationItem>();
            if (_userRepository.Get(user) != null)
            {
                violations.Add(new DataViolationItem("UserName", user.UserName, "DuplicateUserName"));
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                violations.Add(new DataViolationItem("Password", user.Password, "InvalidPassword"));
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                violations.Add(new DataViolationItem("Email", user.Email, "InvalidEmail"));
            }
            if (_userRepository.FindUserByEmail(user.Email) != null)
            {
                violations.Add(new DataViolationItem("Email", user.Email, "DuplicateEmail"));
            }
            if (violations.Count > 0)
            {
                throw new DataViolationException(violations);
            }
            #endregion

            var salt = CryptoHelper.GenerateSalt();
            var encodedPassword = CryptoHelper.HashPassword(user.Password, salt);
            user.Password = encodedPassword;
            user.PasswordSalt = salt;
            _userRepository.Add(user);
        }
        #endregion

        #region Delete
        public virtual void Delete(string userName)
        {
            _userRepository.Remove(new User() { UserName = userName });
        }
        #endregion

        #region Get
        public virtual User Get(string userName)
        {
            return _userRepository.Get(new User() { UserName = userName });
        }
        #endregion

        #region All
        public virtual IEnumerable<User> All()
        {
            return _userRepository.All();
        }
        #endregion

        #region Update
        public virtual void Update(string userName, User newUser)
        {
            var old = Get(userName);
            _userRepository.Update(newUser, old);
        }
        #endregion

        #region ChangePassword
        public virtual bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (!ValidateUser(userName, oldPassword))
            {
                return false;
            }
            return ChangePassword(userName, newPassword);
        }
        public virtual bool ChangePassword(string userName, string newPassword)
        {
            var user = _userRepository.Get(new User() { UserName = userName });

            if (user == null)
            {
                throw new ArgumentException("该用户名不存在.");
            }

            var salt = user.PasswordSalt;

            var encodedPassword = _PasswordRepository.EncryptPassword(newPassword, salt);
            user.Password = encodedPassword;
            user.LastPasswordChangedDate = DateTime.UtcNow;
            user.ActivateCode = null;

            Update(userName, user);

            return true;
        }
        #endregion

        public bool IsAdministrator(string userName)
        {
            var account=_userRepository.Get(new User{UserName = userName});
            if (account == null)
                return false;
            return account.IsAdministrator;
        }
        #endregion

        #region 登陆方式操作

        #endregion

        #region ValidateUser

        public virtual bool ValidateUser(string userName, string password)
        {
            bool isLockedOut;

            return ValidateUser(userName, password, out isLockedOut);
        }

        public virtual bool ValidateUser(string userName, string password, out bool isLockout)
        {
            var user = _userRepository.Get(new User() { UserName = userName });
            var setting = _settingPepository.Get();
            isLockout = false;
            #region DataViolation
            List<DataViolationItem> violations = new List<DataViolationItem>();
            if (user == null)
            {
                return false;
            }
            if (!user.IsApproved)
            {
                violations.Add(new DataViolationItem("UserName", userName, "UserNotApproved"));
            }
            if (user.IsLockedOut)
            {
                //锁定15分钟后解锁
                var time = (DateTime.UtcNow - user.LastLockoutDate.Value);
                if (time.Minutes > setting.MinutesToUnlock)
                {
                    user.IsLockedOut = false;
                    user.FailedPasswordAttemptCount = 0;
                    Update(userName, user);
                }
                else
                {
                    violations.Add(new DataViolationItem("UserName", userName, "UserLockedOut"));
                }
            }
            if (violations.Count > 0)
            {
                throw new DataViolationException(violations);
            }
            #endregion

            var encodedPassword = _PasswordRepository.EncryptPassword(password, user.PasswordSalt);
            var valid = encodedPassword == user.Password;
            if (valid == false && setting.FailedPasswordAttemptCount > 0)
            {
                user.FailedPasswordAttemptCount++;
                if (user.FailedPasswordAttemptCount >= setting.FailedPasswordAttemptCount)
                {
                    user.IsLockedOut = true;
                    user.LastLockoutDate = DateTime.Now;
                }
            }
            else
            {
                user.FailedPasswordAttemptCount = 0;
                user.LastLoginDate = DateTime.Now;
            }
            Update(user.UserName, user);
            return valid;
        }
        #endregion

        #endregion




    }
    /// <summary>
    /// 用户全局管理配置
    /// </summary>
    public partial class UserService 
    {

        

        #region Service 静态方法

        #endregion


    }




}
