using System.Collections.Generic;
using System.Runtime.Serialization;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.ABTest
{
    [DataContract]
    public partial class ABRuleSetting
    {
        #region .ctor
        public ABRuleSetting()
        {
            this.RuleItems = new List<IVisitRule>();
        }
        public ABRuleSetting(Site site, string name)
            : this()
        {
            this.Site = site;
            this.Name = name;
        }
        #endregion
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string RuleType { get; set; }
        [DataMember]
        public List<IVisitRule> RuleItems
        {
            get;
            set;
        }
    }
    public partial class ABRuleSetting : ISiteObject, IPersistable, IIdentifiable
    {
        #region ISitePersistable
        public Site Site
        {
            get;
            set;
        }

        private bool isDummy = true;
        bool IPersistable.IsDummy
        {
            get { return isDummy; }
        }

        void IPersistable.Init(IPersistable source)
        {
            isDummy = false;
            this.Site = ((ABRuleSetting)source).Site;
        }

        void IPersistable.OnSaved()
        {
            isDummy = false;
        }

        void IPersistable.OnSaving()
        {

        }

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
        #endregion
    }
}