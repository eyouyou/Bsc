using System;

namespace Bsc.Dmtds.Core.Runtime.Dependency
{
    /// <summary>
    /// 用于替代注入容器的InjectAttribute标签，这样可以避免对最终注入窗口的依赖
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = false, Inherited = true)]
    public class InjectAttribute : Attribute
    {
         
    }
}