using System;
using System.Linq;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Module.Account;
using Bsc.Dmtds.Core.Persistence.Relational;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Dal;
using Bsc.Dmtds.Respository.IRepository;

namespace Bsc.Dmtds.Respository
{
    [Dependency(typeof(IUserRepository),Order = 2)]
    [Dependency(typeof(IRepository<User>),Order = 2)]
    public class UserRepository:Repository<User>,IUserRepository
    {
        private readonly UnitOfWork _unitOfWork;
        public UserRepository(UnitOfWork unitOfWork)
            :base(unitOfWork)
        {
        }

        public User FindUserByEmail(string email)
        {
            return _unitOfWork.Users.Where(o => o.Email == email).FirstOrDefault();

        }

        public override void Update(User @new, User old)
        {
            var update = Get(old);
            update.Password = @new.Password;
            update.IsAdministrator = @new.IsAdministrator;
            update.CustomFields = @new.CustomFields;
            update.Email = @new.Email;

            _unitOfWork.Commit();
        }

        public override User Get(User dummy)
        {
            return _unitOfWork.Users.Where(o => o.Email == dummy.Email).FirstOrDefault();
        }
    }
}
