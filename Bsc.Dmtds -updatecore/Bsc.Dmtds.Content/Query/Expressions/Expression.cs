

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public abstract class Expression : IExpression
    {
        public Expression(IExpression expression)
        {
            this.InnerExpression = expression;
        }
        public IExpression InnerExpression { get; private set; }
    }
}
