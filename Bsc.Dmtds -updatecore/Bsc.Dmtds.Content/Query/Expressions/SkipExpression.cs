

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class SkipExpression : Expression
    {
        public SkipExpression(IExpression expression, int count)
            : base(expression)
        {
            this.Count = count;
        }
        public int Count { get; private set; }
    }
}
