

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class BinaryExpression : WhereFieldExpression
    {
        public BinaryExpression(IExpression expression, string fieldName, object value)
            : base(expression, fieldName)
        {
            this.Value = ExpressionValueHelper.Escape(value); 

        }
        public object Value { get; private set; }
    }
}
