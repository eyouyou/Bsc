

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class WhereNotEqualsExpression : BinaryExpression
    {
        public WhereNotEqualsExpression(string fieldName, object value)
            : this(null, fieldName, value)
        {

        }
        public WhereNotEqualsExpression(IExpression expression, string fieldName, object value)
            : base(expression, fieldName, value)
        {

        }
    }
}
