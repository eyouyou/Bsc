using System.Collections.Generic;

namespace Bsc.Dmtds.Form
{
    public interface ISchema
    {
        string Name { get; set; }
        bool IsTreeStyle { get; set; }
        IEnumerable<IColumn> Columns { get; }
        IColumn this[string name] { get; }

        IColumn TitleColumn { get; } 
    }
}