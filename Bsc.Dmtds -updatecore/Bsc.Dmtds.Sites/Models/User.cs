using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Sites.Models
{

    [DataContract]
    public partial class User
    {
        public class DataFilePath
        {
            public DataFilePath(User user)
            {
                this.PhysicalPath = Path.Combine(GetBasePath(user.Site), user.UserName + ".config");
            }
            public static string GetBasePath(Site site)
            {
                return Path.Combine(site.PhysicalPath, "Users");
            }
            public string PhysicalPath { get; private set; }
        }
        public Site Site { get; set; }
        [DataMember(Order = 1)]
        public string UserName { get; set; }
        [DataMember(Order = 3)]
        public List<string> Roles { get; set; }
        [DataMember(Order = 5)]
        public Dictionary<string, string> Profile { get; set; }
    }

    public partial class User : ISiteObject, IFilePersistable, IPersistable, IIdentifiable
    {
        #region IPersistable
        public string UUID
        {
            get
            {
                return this.UserName;
            }
            set
            {
                this.UserName = value;
            }
        }

        private bool isDummy = true;
        public bool IsDummy
        {
            get
            {
                return isDummy;
            }
            set
            {
                isDummy = value;
            }
        }

        public void Init(IPersistable source)
        {
            isDummy = false;
            this.Site = ((User)source).Site;
        }

        public void OnSaved()
        {
            isDummy = false;
        }

        public void OnSaving()
        {

        }

        public string DataFile
        {
            get { return new DataFilePath(this).PhysicalPath; }
        }
        #endregion
    }

}