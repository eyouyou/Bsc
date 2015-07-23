namespace Bsc.Dmtds.Common.Collection.Generic
{
    public class CKeyValuePair<TKey, TValue>
    {
        public CKeyValuePair()
        {
        }
        public CKeyValuePair(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}