using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.ABTest
{
    public class ABPageTestHits
    {
        public string PageName { get; set; }
        public int ShowTimes { get; set; }
        public int HitTimes { get; set; }
    }
    [DataContract]
    public partial class ABPageTestResult
    {
        [DataMember]
        public string ABPageUUID { get; set; }

        [Obsolete("Use ABPageUUID")]
        [DataMember]
        public string PageVisitRuleUUID { get { return ABPageUUID; } set { this.ABPageUUID = value; } }
        [DataMember]
        public List<ABPageTestHits> PageHits { get; set; }
        [DataMember]
        public int TotalShowTimes { get; set; }
        [DataMember]
        public int TotalHitTimes { get; set; }
    }

    public partial class ABPageTestResult : ISiteObject, IPersistable, IIdentifiable
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
            this.Site = ((ABPageTestResult)source).Site;
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
                return this.ABPageUUID;
            }
            set
            {
                this.ABPageUUID = value;
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