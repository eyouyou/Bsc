

namespace Bsc.Dmtds.Form.Html.Controls
{
    public interface IControl
    {
        string Name { get; }

        string Render(ISchema schema, IColumn column);

        bool IsFile { get; }

        bool HasDataSource { get; }

        string GetValue(object oldValue, string newValue);

        string DataType { get; }
    }
}
