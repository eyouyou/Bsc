using System.Collections.Generic;
using System.Runtime.Serialization;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Sites.Models
{
    [DataContract]
    public partial class SubmissionSetting
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string PluginType { get; set; }
        [DataMember]
        public Dictionary<string, string> Settings { get; set; }
    }
    public partial class SubmissionSetting : ISiteObject, IPersistable, IIdentifiable
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
            this.Site = ((SubmissionSetting)source).Site;
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