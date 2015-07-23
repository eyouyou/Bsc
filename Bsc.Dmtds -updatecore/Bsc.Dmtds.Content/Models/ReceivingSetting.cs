using System.Runtime.Serialization;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Content.Models
{
    [DataContract]
    public partial class ReceivingSetting : IRepositoryElement
    {
        public Repository Repository
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
        [DataMember(Order = 2)]
        public string SendingRepository { get; set; }

        [DataMember(Order = 3)]
        public string SendingFolder { get; set; }

        //[DataMember(Order = 7)]
        //public bool? Published { get; set; }

        //[DataMember(Order = 8)]
        //public ContentAction AcceptAction { get; set; }

        [DataMember(Order = 11)]
        public string ReceivingFolder { get; set; }

        //[DataMember(Order = 13)]
        //public bool? SetPublished { get; set; }
        [DataMember(Order = 13)]
        public bool KeepStatus { get; set; }


    }
    public partial class ReceivingSetting : IPersistable, IIdentifiable
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
            this.Name = ((ReceivingSetting)source).Name;
            this.Repository = ((ReceivingSetting)source).Repository;
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