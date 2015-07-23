


namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class WhereEqualsExpression : BinaryExpression
    {
        public WhereEqualsExpression(string fieldName, object value)
            : this(null, fieldName, value)
        { }
        public WhereEqualsExpression(IExpression expression, string fieldName, object value)
            : base(expression, fieldName, value)
        { }
    }
}
