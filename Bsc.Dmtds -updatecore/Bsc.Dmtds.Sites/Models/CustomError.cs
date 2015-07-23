using System;
using System.Runtime.Serialization;
using Bsc.Dmtds.Content;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Sites.Models
{
    [DataContract]
    public enum HttpErrorStatusCode
    {
        [EnumMember]
        Unauthorized_401 = 401,
        [EnumMember]
        NotFound_404 = 404,
        [EnumMember]
        InternalServerError_500 = 500,
        [EnumMember]
        NotImplemented_501 = 501
    }
    [DataContract]
    public partial class CustomError
    {
        public Site Site { get; set; }

        [DataMember(Order = 1)]
        public HttpErrorStatusCode StatusCode { get; set; }
        [DataMember(Order = 2)]
        public string RedirectUrl { get; set; }
        [DataMember(Order = 12)]
        public RedirectType RedirectType { get; set; }

        private bool? _showErrorPath = true;
        [DataMember]
        public bool ShowErrorPath
        {
            get
            {
                if (_showErrorPath == null)
                {
                    _showErrorPath = true;
                }
                return _showErrorPath.Value;
            }
            set { _showErrorPath = value; }
        }
    }
    public partial class CustomError : ISiteObject, IFilePersistable, IPersistable, IIdentifiable
    {
        #region Override site
        public static bool operator ==(CustomError obj1, CustomError obj2)
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
        public static bool operator !=(CustomError obj1, CustomError obj2)
        {
            return !(obj1 == obj2);
        }
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                var o = (CustomError)obj;
                if (this.StatusCode == o.StatusCode)
                {
                    return true;
                }
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return this.StatusCode.ToString();
        }
        #endregion

        #region IPersistable,IIdentifiable

        public string UUID
        {
            get
            {
                return this.StatusCode.ToString();
            }
            set
            {
                this.StatusCode = (HttpErrorStatusCode)Enum.Parse(typeof(HttpErrorStatusCode), value);
            }
        }
        private bool isDummy = true;
        public bool IsDummy
        {
            get { return isDummy; }
            set { isDummy = value; }
        }

        public void Init(IPersistable source)
        {
            isDummy = false;
            this.Site = ((CustomError)source).Site;
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
            get { return new CustomErrorsFile(Site).PhysicalPath; }
        }
        #endregion
    }
}