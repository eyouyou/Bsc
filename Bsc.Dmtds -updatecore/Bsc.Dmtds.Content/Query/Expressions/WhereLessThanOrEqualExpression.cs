


namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class WhereLessThanOrEqualExpression : BinaryExpression
    {
        public WhereLessThanOrEqualExpression(string fieldName, object value)
            : this(null, fieldName, value)
        {

        }
        public WhereLessThanOrEqualExpression(IExpression expression, string fieldName, object value)
            : base(expression, fieldName, value)
        {

        }
    }
}
