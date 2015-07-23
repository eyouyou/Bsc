using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Bsc.Dmtds.Content;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Sites.Persistence;
using Bsc.Dmtds.Sites.Versioning;

namespace Bsc.Dmtds.Sites.Models
{
    public partial class HtmlBlock : DirectoryResource
    {
        public HtmlBlock() { }
        public HtmlBlock(Site site, string name)
            : base(site, name)
        { }
        public HtmlBlock(string physicalPath)
            : base(physicalPath) { }
        [DataMember]
        public string Body { get; set; }

        #region DirectoryResource
        static string PATH_NAME = "HtmlBlocks";
        public override IEnumerable<string> RelativePaths
        {
            get { return new[] { PATH_NAME }; }
        }

        public override IEnumerable<string> ParseObject(IEnumerable<string> relativePaths)
        {
            this.Name = relativePaths.Last();

            return relativePaths.Take(relativePaths.Count() - 2);
        }
        #endregion

        public override bool Exists()
        {
            return Providers.HtmlBlockProvider.Get(this) != null;
        }

    }
    public partial class HtmlBlock : ISiteObject, IFilePersistable, IPersistable, IIdentifiable, IInheritable<HtmlBlock>, IVersionable
    {
        #region IPersistable
        public string UUID
        {
            get
            {
                return this.Name;
            }
            set
            {
                this.Name = value;
            }
        }
        private bool isDummy = true;
        bool IPersistable.IsDummy
        {
            get { return isDummy; }
        }

        void IPersistable.Init(IPersistable source)
        {
            this.Name = ((HtmlBlock)source).Name;
            this.Site = ((HtmlBlock)source).Site;
            this.isDummy = false;
        }

        void IPersistable.OnSaved()
        {
            isDummy = false;
        }

        void IPersistable.OnSaving()
        {

        }
        public string DataFileName
        {
            get { return "Body.html"; }
        }
        public string DataFile
        {
            get { return Path.Combine(this.PhysicalPath, DataFileName); }
        }
        #endregion

        #region IInheritable
        public HtmlBlock LastVersion()
        {
            return LastVersion(this.Site);
        }
        public HtmlBlock LastVersion(Site site)
        {
            var lastVersion = new HtmlBlock(site, this.Name);
            while (!lastVersion.Exists())
            {
                if (lastVersion.Site.Parent == null)
                {
                    break;
                }
                lastVersion = new HtmlBlock(lastVersion.Site.Parent, this.Name);
            }
            return lastVersion;
        }

        public bool HasParentVersion()
        {
            var parentSite = this.Site.Parent;
            while (parentSite != null)
            {
                var htmlBlock = new HtmlBlock(parentSite, this.Name);
                if (htmlBlock.Exists())
                {
                    return true;
                }
                parentSite = parentSite.Parent;
            }
            return false;
        }
        public bool IsLocalized(Site site)
        {
            return (new HtmlBlock(site, this.Name)).Exists();
        }
        #endregion

        #region IVersionable

        public string UserName
        {
            get;
            set;
        }
        #endregion
    }
}