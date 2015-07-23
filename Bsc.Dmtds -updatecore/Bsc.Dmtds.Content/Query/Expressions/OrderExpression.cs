

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class OrderExpression : WhereFieldExpression
    {
        public OrderExpression(IExpression expression, string fieldName, bool descending) :
            base(expression,fieldName)
        {
            this.Descending = descending;
        }
        
        public bool Descending { get; set; }
    }
}
