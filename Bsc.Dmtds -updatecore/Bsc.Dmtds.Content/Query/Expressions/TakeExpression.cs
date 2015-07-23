

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class TakeExpression : Expression
    {
        public TakeExpression(IExpression expression, int count)
            : base(expression)
        {
            this.Count = count;
        }
        public int Count { get; set; }
    }
}
