

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class WhereEndsWithExpression : BinaryExpression
    {
        public WhereEndsWithExpression(string fieldName, object value)
            : this(null, fieldName, value)
        {

        }
        public WhereEndsWithExpression(IExpression expression, string fieldName, object value)
            : base(expression, fieldName, value)
        {

        }
    }
}
