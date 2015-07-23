using System.Runtime.Serialization;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Core;

namespace Bsc.Dmtds.Sites.ABTest
{
    [DataContract]
    public abstract class CustomRuleBase
    {
        /// <summary>
        /// The DataMember on this field is only used for the JSON.NET serializer.
        /// </summary>
        [DataMember]
        public abstract string RuleType { get; set; }
        /// <summary>
        /// The DataMember on this field is only used for the JSON.NET serializer.
        /// </summary>
        [DataMember]
        public abstract string DisplayText { get; set; }
        /// <summary>
        /// The DataMember on this field is only used for the JSON.NET serializer.
        /// </summary>
        [DataMember]
        public virtual string TemplateVirtualPath
        {
            get
            {
                var baseDir = Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<IBaseDir>();
                return UrlUtility.Combine(baseDir.DataVirtualPath, "Views", "ABRuleTemplates", RuleType + ".cshtml");
            }
            set { }
        }
    }
}