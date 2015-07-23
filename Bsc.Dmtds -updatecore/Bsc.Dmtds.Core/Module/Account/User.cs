using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Bsc.Dmtds.Common.Collection;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Core.Module.Account
{
    [DataContract]
    public class User :IIdentifiable
    {
        public string UserName { get; set; }
        [DataMember(Order = 1)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [DataMember(Order = 3)]
        public string Password { get; set; }

        [DataMember(Order = 5)]
        public bool IsAdministrator { get; set; }


        [DataMember]
        public string PasswordSalt
        {
            get;
            set;
        }

        private bool? isApproved;
        [DataMember]
        public bool IsApproved
        {
            get
            {
                if (!isApproved.HasValue)
                {
                    isApproved = true;
                }
                return isApproved.Value;
            }
            set
            {
                isApproved = value;
            }
        }

        [DataMember]
        public bool IsLockedOut
        {
            get;
            set;
        }

        [DataMember]
        public int FailedPasswordAttemptCount
        {
            get;
            set;
        }

        [DataMember]
        public DateTime? LastLockoutDate
        {
            get;
            set;
        }

        [DataMember]
        public DateTime? LastLoginDate
        {
            get;
            set;
        }
        [DataMember]
        public virtual DateTime? LastPasswordChangedDate { get; set; }
        [DataMember]
        public virtual string ActivateCode { get; set; }

        private DynamicDictionary customFields = null;
        [DataMember(Order = 7)]
        public DynamicDictionary CustomFields
        {
            get
            {
                if (customFields == null)
                {
                    customFields = new DynamicDictionary();
                }
                return customFields;
            }
            set
            {
                customFields = value;
            }
        }
        [XmlIgnore]
        public string CustomFieldsXml
        {
            get
            {
                string xml = "";
                if (CustomFields != null)
                {
                    xml = DataContractSerializationHelper.SerializeAsXml(this.CustomFields);
                }
                return xml;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    CustomFields = DataContractSerializationHelper.DeserializeFromXml<DynamicDictionary>(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the global roles. Splited by comma： role1,role2
        /// </summary>
        /// <value>
        /// The global roles.
        /// </value>
        [DataMember]
        public string GlobalRoles { get; set; }

        [DataMember]
        public string DefaultPage { get; set; }

        #region IIdentifiable

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

        #endregion

    }


}
