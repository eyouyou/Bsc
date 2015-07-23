

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class SelectExpression : Expression
    {
        public SelectExpression(IExpression expression, string[] fields)
            : base(expression)
        {
            this.Fields = fields;
        }
        public string[] Fields { get; private set; }        
    }
}
