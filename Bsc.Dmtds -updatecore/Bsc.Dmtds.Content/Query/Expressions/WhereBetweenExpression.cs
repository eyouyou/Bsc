

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class WhereBetweenExpression : WhereFieldExpression
    {
        public WhereBetweenExpression(string fieldName, object start, object end)
            : this(null, fieldName, start, end)
        { }
        public WhereBetweenExpression(IExpression expression, string fieldName, object start, object end)
            : base(expression, fieldName)
        {
            this.Start = ExpressionValueHelper.Escape(start);
            this.End = ExpressionValueHelper.Escape(end);
        }
        public object Start { get; set; }
        public object End { get; private set; }
    }
}
