using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Module.Account;
using Bsc.Dmtds.Core.Persistence.Relational;
using Bsc.Dmtds.Dal;

namespace Bsc.Dmtds.Respository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        User FindUserByEmail(string email);   

    }
}
