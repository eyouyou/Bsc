namespace Bsc.Dmtds.Core.Runtime
{
    public interface IStartupTask
    {
        void Execute();

        int Order { get; } 
    }
}