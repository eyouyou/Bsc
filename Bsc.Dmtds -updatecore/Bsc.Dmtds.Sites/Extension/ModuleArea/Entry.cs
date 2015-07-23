using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.Routing;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea
{
    [DataContract]
    [KnownType(typeof(string[]))]
    public class Entry
    {
        public Entry()
        {
            this.Name = string.Empty;
            this.Controller = string.Empty;
            this.Action = string.Empty;
            this.LinkToEntryName = string.Empty;
        }
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string Controller { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string Action { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public RouteValueDictionary Values { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string LinkToEntryName { get; set; }
    }
    [DataContract]
    public class ModuleSettings
    {
        public ModuleSettings()
        {
            CustomSettings = new Dictionary<string, string>();
        }
        [DataMember(EmitDefaultValue = false)]
        public string ThemeName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Entry Entry { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Dictionary<string, string> CustomSettings { get; set; }
    }
}