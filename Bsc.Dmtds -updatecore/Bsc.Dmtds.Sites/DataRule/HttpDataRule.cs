using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using Bsc.Dmtds.Common.Collection.Generic;
using Bsc.Dmtds.Core.Formula;
using Bsc.Dmtds.Sites.DataRule.Http;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.DataRule
{
    [DataContract(Name = "HttpDataRule")]
    [KnownTypeAttribute(typeof(HttpDataRule))]
    public class HttpDataRule
    {
        /// <summary>
        /// josn?key={key}
        /// </summary>
        [DataMember]
        public string URL { get; set; }

        private string _httpMethod = "GET";
        [DataMember]
        public string HttpMethod { get { return _httpMethod; } set { _httpMethod = value; } }
        private List<CKeyValuePair<string, string>> _formData = new List<CKeyValuePair<string, string>>();
        [DataMember]
        public List<CKeyValuePair<string, string>> FormData
        {
            get
            {
                return _formData;
            }
            set
            {
                _formData = value;
            }
        }
        private List<CKeyValuePair<string, string>> _headers = new List<CKeyValuePair<string, string>>();
        [DataMember]
        public List<CKeyValuePair<string, string>> Headers
        {
            get
            {
                return _headers;
            }
            set
            {
                _headers = value;
            }
        }
        [DataMember]
        public ResponseType ResponseType { get; set; }
        [DataMember]
        public string ContentType { get; set; }


        [DataMember]
        public DataRuleType DataRuleType
        {
            get { return DataRuleType.Http; }
            set { }
        }

        public bool HasAnyParameters()
        {
            return false;
        }

        #region Execute
        public object Execute(DataRuleContext dataRuleContext, TakeOperation operation, int cacheDuration)
        {
            var url = EvaluateStringFormulas(this.URL, dataRuleContext);
            NameValueCollection form = KeyValuesToNameValueCollection(dataRuleContext, this.FormData);
            NameValueCollection headers = KeyValuesToNameValueCollection(dataRuleContext, this.Headers);

            var httpDataRequest = Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<IHttpDataRequest>();
            return httpDataRequest.GetData(url, this.HttpMethod, ContentType, form, headers);
        }
        #endregion

        #region KeyValuesToNameValueCollection
        private static NameValueCollection KeyValuesToNameValueCollection(DataRuleContext dataRuleContext, IEnumerable<CKeyValuePair<string, string>> keyValuePairs)
        {
            NameValueCollection values = new NameValueCollection();
            if (keyValuePairs != null && keyValuePairs.Count() > 0)
            {
                foreach (var item in keyValuePairs)
                {
                    if (!string.IsNullOrEmpty(item.Key))
                    {
                        var value = EvaluateStringFormulas(item.Value, dataRuleContext);
                        values[item.Key] = value;
                    }
                }
            }

            return values;
        }
        #endregion

        #region EvaluateStringFormulas
        private class ValueProviderBridge : Bsc.Dmtds.Core.Formula.IValueProvider
        {
            Bsc.Dmtds.Sites.DataRule.IValueProvider _valueProvider;
            public ValueProviderBridge(Bsc.Dmtds.Sites.DataRule.IValueProvider valueProvider)
            {
                _valueProvider = valueProvider;
            }
            public object GetValue(string name)
            {
                return _valueProvider.GetValue(name);
            }
        }
        private static string EvaluateStringFormulas(string formula, DataRuleContext dataRuleContext)
        {
            var formulaParser = new FormulaParser();
            return formulaParser.Populate(formula, new ValueProviderBridge(dataRuleContext.ValueProvider));
        }
        #endregion
    }
}