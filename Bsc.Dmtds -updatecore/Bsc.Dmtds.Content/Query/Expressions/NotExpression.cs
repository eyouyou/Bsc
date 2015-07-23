

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class NotExpression : IWhereExpression
    {
        public NotExpression(IWhereExpression expression)
        {
            InnerExpression = expression;
        }
        public IWhereExpression InnerExpression { get; private set; }
    }
}
