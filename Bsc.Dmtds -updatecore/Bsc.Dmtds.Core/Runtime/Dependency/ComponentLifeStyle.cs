namespace Bsc.Dmtds.Core.Runtime.Dependency
{
    public enum ComponentLifeStyle
    {
        Singleton = 0,
        Transient = 1,
        InThreadScope = 2,
        InRequestScope = 3
    }
}