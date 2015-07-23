using Bsc.Dmtds.Sites.Extension.ModuleArea;

namespace Bsc.Dmtds.Sites.Extension.Management.Events
{
    public interface IModuleSiteRelationEvents
    {
        /// <summary>
        /// The event when site includeded.
        /// </summary>
        /// <param name="moduleContext">The module context.</param>
        void OnIncluded(ModuleContext moduleContext);
        /// <summary>
        /// The event when site excluded.
        /// </summary>
        /// <param name="moduleContext">The module context.</param>
        void OnExcluded(ModuleContext moduleContext); 
    }
}