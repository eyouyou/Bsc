

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class WhereContainsExpression : BinaryExpression
    {
        public WhereContainsExpression(string fieldName, object value)
            : this(null, fieldName, value)
        {

        }
        public WhereContainsExpression(IExpression expression, string fieldName, object value)
            : base(expression, fieldName, value)
        {

        }

    }
}
