using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Module.Account;
using Bsc.Dmtds.Core.Persistence.Relational;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Dal;
using Bsc.Dmtds.Respository.IRepository;

namespace Bsc.Dmtds.Respository
{
    [Dependency(typeof(IRepository<Role>),Order = 2)]
    [Dependency(typeof(IRoleRepository),Order = 2)]
    public class RoleRepository:Repository<Role>,IRoleRepository
    {
        public RoleRepository(UnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            
        }

        public override void Update(Role @new, Role old)
        {
            throw new System.NotImplementedException();
        }

        public override Role Get(Role dummy)
        {
            throw new System.NotImplementedException();
        }
    }
}
