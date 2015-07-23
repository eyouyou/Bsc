

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class OrElseExpression : IWhereExpression
    {
        public OrElseExpression(IWhereExpression left, IWhereExpression right)
        {
            this.Left = left;
            this.Right = right;
        }
        public IWhereExpression Left { get; private set; }
        public IWhereExpression Right { get; private set; }
    }
}
