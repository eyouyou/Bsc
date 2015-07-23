using Bsc.Dmtds.Content.Query.Expressions;

namespace Bsc.Dmtds.Content.Query.Translator.String
{
    public abstract class TranslatedQuery
    {
        #region IQuery Members

        public string SelectText
        {
            get;
            internal set;
        }

        public string ClauseText
        {
            get;
            internal set;
        }

        public string SortText
        {
            get;
            internal set;
        }
        public CallType CallType { get; internal set; }
        public int Top { get; internal set; }
        public int Skip { get; internal set; }
        public int TakeCount { get; internal set; }
        #endregion
    }
}