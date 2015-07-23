

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class WhereBetweenOrEqualExpression : WhereBetweenExpression
    {
        public WhereBetweenOrEqualExpression(string fieldName, object start, object end)
            : base(null, fieldName, start, end)
        {

        }
        public WhereBetweenOrEqualExpression(IExpression expression, string fieldName, object start, object end)
            : base(expression, fieldName, start, end)
        { }

    }
}
