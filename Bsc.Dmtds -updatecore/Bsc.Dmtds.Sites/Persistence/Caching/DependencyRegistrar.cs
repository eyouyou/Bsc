using Bsc.Dmtds.Core.Runtime;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Sites.Persistence.FileSystem;

namespace Bsc.Dmtds.Sites.Persistence.Caching
{
    public class DependencyRegistrar: IDependencyRegistrar
    {
        private class ResolvingObserver : IResolvingObserver
        {
            public int Order
            {
                get { return 1; }
            }

            public object OnResolved(object resolvedObject)
            {
                if (resolvedObject is IHtmlBlockProvider)
                {
                    return new HtmlBlockProvider((IHtmlBlockProvider)resolvedObject);
                }
                if (resolvedObject is ILayoutProvider)
                {
                    return new LayoutProvider((ILayoutProvider)resolvedObject);
                }
                if (resolvedObject is IPageProvider)
                {
                    return new PageProvider((IPageProvider)resolvedObject);
                }
                if (resolvedObject is ISiteProvider)
                {
                    return new SiteProvider((ISiteProvider)resolvedObject);
                }
                if (resolvedObject is IUrlKeyMapProvider)
                {
                    return new UrlKeyMapProvider((IUrlKeyMapProvider)resolvedObject);
                }
                if (resolvedObject is IViewProvider)
                {
                    return new ViewProvider((IViewProvider)resolvedObject);
                }
                if (resolvedObject is IABRuleSettingProvider)
                {
                    return new ABRuleSettingProvider((IABRuleSettingProvider)resolvedObject);
                }
                if (resolvedObject is IABSiteSettingProvider)
                {
                    return new ABSiteSettingProvider((IABSiteSettingProvider)resolvedObject);
                }
                if (resolvedObject is IABPageSettingProvider)
                {
                    return new ABPageSettingProvider((IABPageSettingProvider)resolvedObject);
                }
                if (resolvedObject is ILabelProvider)
                {
                    return new LabelProvider((ILabelProvider)resolvedObject);
                }
                return resolvedObject;
            }
        }
        public void Register(IContainerManager containerManager, ITypeFinder typeFinder)
        {
            containerManager.AddResolvingObserver(new ResolvingObserver());
        }

        public int Order
        {
            get { return 1; }
        }
    }
}