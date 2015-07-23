using Bsc.Dmtds.Core.Module.Account;
using Bsc.Dmtds.Core.Persistence.Relational;

namespace Bsc.Dmtds.Respository.IRepository
{
    public interface ISettingPepository:IRepository<Setting>
    {
        Setting Get();

    }
}