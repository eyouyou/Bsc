using Bsc.Dmtds.Ninject.InRequestScope;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: System.Web.PreApplicationStartMethod(typeof(Bsc.Dmtds.Ninject.NinjectWebCommonStartupTask), "Start")]
namespace Bsc.Dmtds.Ninject
{
    public class NinjectWebCommonStartupTask
    {
        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
        }
    }
}