


namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class WhereLessThanExpression : BinaryExpression
    {
        public WhereLessThanExpression(string fieldName, object value)
            : this(null, fieldName, value)
        {

        }
        public WhereLessThanExpression(IExpression expression, string fieldName, object value)
            : base(expression, fieldName, value)
        {

        }
    }
}
