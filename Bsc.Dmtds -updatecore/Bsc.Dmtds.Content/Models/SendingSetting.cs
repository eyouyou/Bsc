using System.ComponentModel;
using System.Runtime.Serialization;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Content.Models
{
    public enum ChildLevel
    {
        [Description("Only first child level subsites")]
        OnlyFirst,  //
        [Description("To all subsites in the same hierarchy")]
        All   // 
    }
    [DataContract]
    public partial class SendingSetting : IRepositoryElement
    {
        public SendingSetting()
        { }

        public SendingSetting(Repository repository, string folderName)
        {
            this.Repository = repository;
            this.FolderName = FolderName;
        }
        public Repository Repository { get; set; }

        public string Name
        {
            get
            {
                return this.FolderName;
            }
            set
            {
                this.FolderName = value;
            }
        }

        [DataMember(Order = 2)]
        public string FolderName { get; set; }

        [DataMember(Order = 1)]
        public bool? SendReceived { get; set; }


        [DataMember(Order = 3)]
        public bool? SendToChildSites { get; set; }
        [DataMember(Order = 4)]
        public ChildLevel ChildLevel { get; set; }

        [DataMember(Order = 4)]
        public bool KeepStatus { get; set; }


    }
    public partial class SendingSetting : IPersistable, IIdentifiable
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
            isDummy = false;
            this.Repository = ((SendingSetting)source).Repository;
            this.FolderName = ((SendingSetting)source).FolderName;
        }

        void IPersistable.OnSaved()
        {
            isDummy = false;
        }

        void IPersistable.OnSaving()
        {
        }
        #endregion
    }
}