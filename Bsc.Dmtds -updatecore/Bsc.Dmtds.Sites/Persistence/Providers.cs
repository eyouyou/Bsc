using Bsc.Dmtds.Core.Runtime;

namespace Bsc.Dmtds.Sites.Persistence
{
    public class Providers
    {
        public static IProviderFactory ProviderFactory
        {
            get
            {
                return EngineContext.Current.Resolve<IProviderFactory>();
            }
            set
            {
                (EngineContext.Current).ContainerManager.AddComponentInstance<IProviderFactory>(value);
            }
        }

        public static ISiteProvider SiteProvider
        {
            get
            {
                return ProviderFactory.GetProvider<ISiteProvider>();
            }
        }

        public static IPageProvider PageProvider
        {
            get
            {
                return ProviderFactory.GetProvider<IPageProvider>();
            }
        }

        public static ILayoutProvider LayoutProvider
        {
            get
            {
                return ProviderFactory.GetProvider<ILayoutProvider>();
            }
        }

        public static IViewProvider ViewProvider
        {
            get
            {
                return ProviderFactory.GetProvider<IViewProvider>();
            }
        }

        public static ICustomErrorProvider CustomErrorProvider
        {
            get
            {
                return ProviderFactory.GetProvider<ICustomErrorProvider>();
            }
        }

        public static IUrlRedirectProvider UrlRedirectProvider
        {
            get
            {
                return ProviderFactory.GetProvider<IUrlRedirectProvider>();
            }
        }
        public static IUrlKeyMapProvider UrlKeyMapProvider
        {
            get
            {
                return ProviderFactory.GetProvider<IUrlKeyMapProvider>();
            }
        }
        public static IPagePublishingQueueProvider PagePublishingProvider
        {
            get
            {
                return ProviderFactory.GetProvider<IPagePublishingQueueProvider>();
            }
        }
        public static IHtmlBlockProvider HtmlBlockProvider
        {
            get
            {
                return ProviderFactory.GetProvider<IHtmlBlockProvider>();
            }
        } 
    }
}