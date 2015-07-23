using System.Collections.Generic;
using System.Runtime.Serialization;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.ABTest
{
    public class ABPageRuleItem
    {
        public string RuleItemName { get; set; }
        public string PageName { get; set; }
    }
    [DataContract]
    public partial class ABPageSetting
    {
        [DataMember]
        public string MainPage { get; set; }
        [DataMember]
        public string RuleName { get; set; }
        [DataMember]
        public List<ABPageRuleItem> Items { get; set; }
        [DataMember]
        public string ABTestGoalPage { get; set; }
    }
    public partial class ABPageSetting : ISiteObject, IPersistable, IIdentifiable
    {
        #region IPersistable Members
        private bool isDummy = true;
        bool IPersistable.IsDummy
        {
            get { return isDummy; }
        }

        void IPersistable.Init(IPersistable source)
        {
            isDummy = false;
            this.Site = ((ABPageSetting)source).Site;
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
            get
            {
                return this.MainPage;
            }
            set
            {
                this.MainPage = value;
            }
        }
        #endregion

        #region ISiteObject Members
        public Site Site
        {
            get;
            set;
        }
        #endregion
    }
}