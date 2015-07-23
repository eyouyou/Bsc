using System.Linq;
using Bsc.Dmtds.Common.Globalization;
using Bsc.Dmtds.Sites.Globalization.Repository;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Globalization
{
    public class SiteLabelRepository : IElementRepository
    {
        public static string FileCulture = "Labels";
        //ConcurrentDictionary<ElementCacheKey, Element> entries = new ConcurrentDictionary<ElementCacheKey, Element>(new ElementCacheKeyEqualityComparer());

        public SiteLabelRepository(Site site)
        {
            StoreRepository = new XmlElementRepository(new LabelPath(site).PhysicalPath);
        }
        public IElementRepository StoreRepository { get; private set; }

        #region IElementRepository Members

        public IQueryable<System.Globalization.CultureInfo> EnabledLanguages()
        {
            return this.StoreRepository.EnabledLanguages();
        }

        public IQueryable<Element> Elements()
        {
            return this.StoreRepository.Elements();
        }

        public Element Get(string name, string category, string culture)
        {
            var label = this.StoreRepository.Get(name, category, FileCulture);
            if (label != null)
            {
                label.Culture = null;
            }
            return label;
        }

        public bool Add(Element element)
        {
            element.Culture = FileCulture;
            return this.StoreRepository.Add(element);
        }

        public bool Update(Element element)
        {
            element.Culture = FileCulture;
            return this.StoreRepository.Update(element);
        }

        public bool Remove(Element element)
        {
            element.Culture = FileCulture;
            return this.StoreRepository.Remove(element);
        }
        #endregion

        #region IElementRepository Members


        public IQueryable<ElementCategory> Categories()
        {
            return StoreRepository.Categories();
        }

        #endregion

        #region IElementRepository Members


        public bool RemoveCategory(string category, string culture)
        {
            return this.StoreRepository.RemoveCategory(category, FileCulture);
        }

        public void AddCategory(string category, string culture)
        {
            this.StoreRepository.AddCategory(category, FileCulture);
        }
        #endregion


        public void Clear()
        {
            StoreRepository.Clear();
        }

    }
}