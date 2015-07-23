using System.Collections.Specialized;

namespace Bsc.Dmtds.Content.Models.Binder
{
    public interface ITextContentBinder
    {
        TextContent Bind(Schema schema, TextContent textContent, NameValueCollection values);
        TextContent Bind(Schema schema, TextContent textContent, System.Collections.Specialized.NameValueCollection values, bool thorwViolationException);
        TextContent Bind(Schema schema, TextContent textContent, System.Collections.Specialized.NameValueCollection values, bool update, bool thorwViolationException);

        TextContent Update(Schema schema, TextContent textContent, NameValueCollection values);
        TextContent Default(Schema schema);
        object ConvertToColumnType(Schema schema, string field, string value);
 
    }
}