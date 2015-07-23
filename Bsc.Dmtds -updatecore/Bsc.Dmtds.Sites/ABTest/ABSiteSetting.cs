using System.Collections.Generic;
using System.Runtime.Serialization;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.ABTest
{
    public class ABSiteRuleItem
    {
        public string RuleItemName { get; set; }
        public string SiteName { get; set; }
    }
    [DataContract]
    public partial class ABSiteSetting
    {
        [DataMember]
        public string MainSite { get; set; }
        [DataMember]
        public string RuleName { get; set; }
        [DataMember]
        public List<ABSiteRuleItem> Items { get; set; }
        [DataMember]
        public RedirectType? RedirectType { get; set; }
    }

    public partial class ABSiteSetting : IPersistable, IIdentifiable
    {
        #region IPersistable Members

        private bool isDummy = true;

        bool IPersistable.IsDummy
        {
            get { return isDummy; }
        }

        void IPersistable.Init(IPersistable source)
        {

        }

        void IPersistable.OnSaved()
        {
            isDummy = false;
        }

        void IPersistable.OnSaving()
        {

        }

        #endregion

        #region IIdentifiable

        public string UUID
        {
            get { return this.MainSite; }
            set { this.MainSite = value; }
        }

        #endregion
    }
}