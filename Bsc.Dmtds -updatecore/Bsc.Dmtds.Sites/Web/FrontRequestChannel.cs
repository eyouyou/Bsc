namespace Bsc.Dmtds.Sites.Web
{
    public enum FrontRequestChannel
    {
        Unknown,
        /// <summary>
        /// s~site1
        /// </summary>
        Debug,
        /// <summary>
        /// www.site1.com
        /// </summary>
        Host,
        /// <summary>
        /// www.kooboo.com/site1
        /// </summary>
        HostNPath,
        /// <summary>
        /// 
        /// </summary>
        Design,
        Draft
    }
}