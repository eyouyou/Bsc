

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class WhereNotInExpression : WhereFieldExpression
    {
        public WhereNotInExpression(string fieldName, object[] values)
            : this(null, fieldName, values)
        {

        }
        public WhereNotInExpression(IExpression expression, string fieldName, object[] values)
            : base(expression, fieldName)
        {
            this.Values = values;
        }
        public object[] Values { get; set; }
    }
}
