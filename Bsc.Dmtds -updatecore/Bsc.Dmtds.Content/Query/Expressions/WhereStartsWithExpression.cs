
namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class WhereStartsWithExpression : BinaryExpression
    {
        public WhereStartsWithExpression(string fieldName, object value)
            : this(null, fieldName, value)
        {

        }
        public WhereStartsWithExpression(IExpression expression, string fieldName, object value)
            : base(expression, fieldName, value)
        {

        }
    }
}
