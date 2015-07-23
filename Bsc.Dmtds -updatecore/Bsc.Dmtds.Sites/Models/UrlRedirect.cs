﻿using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Content;
using Bsc.Dmtds.Core.Persistence;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Sites.Models
{
    public enum RedirectType
    {
        Moved_Permanently_301 = 301,
        Found_Redirect_302 = 302,
        [Description("Server transfer")]
        Transfer = 200
    }
    [DataContract]
    public partial class UrlRedirect : IChangeTimeline
    {
        public UrlRedirect()
        {

        }
        public UrlRedirect(Site site)
        {
            this.Site = site;
        }
        string uuid = UniqueIdGenerator.GetInstance().GetBase32UniqueId(8);
        [DataMember]
        public string UUID
        {
            get
            {
                if (string.IsNullOrEmpty(uuid))
                {
                    uuid = inputUrl;
                }
                return uuid;
            }
            set
            {
                uuid = value;
            }
        }


        string inputUrl = string.Empty;
        [DataMember(Order = 1)]
        public string InputUrl
        {
            get
            {
                return this.inputUrl;
            }
            set
            {
                inputUrl = value;
            }
        }
        [DataMember(Order = 4)]
        public string OutputUrl { get; set; }
        [DataMember(Order = 8)]
        public bool Regex { get; set; }
        [DataMember(Order = 12)]
        public RedirectType RedirectType { get; set; }

        [DataMember]
        public DateTime? UtcCreationDate { get; set; }

        [DataMember]
        public DateTime? UtcLastestModificationDate { get; set; }

        [DataMember]
        public string LastestEditor { get; set; }

    }

    public partial class UrlRedirect : ISiteObject, IFilePersistable, IPersistable, IIdentifiable
    {
        public Site Site { get; set; }

        #region override object
        public static bool operator ==(UrlRedirect obj1, UrlRedirect obj2)
        {
            if (object.Equals(obj1, obj2) == true)
            {
                return true;
            }
            if (object.Equals(obj1, null) == true || object.Equals(obj2, null) == true)
            {
                return false;
            }
            return obj1.Equals(obj2);
        }
        public static bool operator !=(UrlRedirect obj1, UrlRedirect obj2)
        {
            return !(obj1 == obj2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is UrlRedirect))
            {
                return false;
            }
            if (obj != null)
            {
                var urlMap = (UrlRedirect)obj;
                if (this.UUID.EqualsOrNullEmpty(urlMap.UUID, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return base.Equals(obj);
        }
        public override string ToString()
        {
            return InputUrl;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region IPersistable


        private bool isDummy = true;
        public bool IsDummy
        {
            get { return isDummy; }
            set { isDummy = value; }
        }

        public void Init(IPersistable source)
        {
            isDummy = false;
            this.Site = ((UrlRedirect)source).Site;
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
            get { return new UrlRedirectsFile(this.Site).PhysicalPath; }
        }
        #endregion
    }
}