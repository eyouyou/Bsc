using System;
using System.Configuration;

namespace Bsc.Dmtds.Core.Mvc.WebResourceLoader.Configuration
{
    public sealed class ReferenceCollection : ConfigurationElementCollection
    {
        private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

        public ReferenceCollection() : base(StringComparer.OrdinalIgnoreCase) { }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ReferenceElement();
        }

        public ReferenceElement Get(int index)
        {
            return (ReferenceElement)base.BaseGet(index);
        }

        public ReferenceElement Get(string name)
        {
            return (ReferenceElement)base.BaseGet(name);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ReferenceElement)element).Name;
        }

        public string GetKey(int index)
        {
            return (string)base.BaseGetKey(index);
        }

        // Properties
        public string[] AllKeys
        {
            get { return ObjectArrayToStringArray(base.BaseGetAllKeys()); }
        }

        public ReferenceElement this[int index]
        {
            get { return (ReferenceElement)base.BaseGet(index); }
        }

        public new ReferenceElement this[string name]
        {
            get { return (ReferenceElement)base.BaseGet(name); }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return _properties; }
        }

        protected override string ElementName
        {
            get { return "reference"; }
        }

        internal static string[] ObjectArrayToStringArray(object[] objectArray)
        {
            string[] array = new string[objectArray.Length];
            objectArray.CopyTo(array, 0);
            return array;
        }
    }
}