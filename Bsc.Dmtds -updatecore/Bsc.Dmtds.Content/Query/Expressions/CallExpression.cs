

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public enum CallType
    {
        Unspecified,
        Count,
        First,
        Last,
        LastOrDefault,
        FirstOrDefault        
    }
    public class CallExpression : Expression
    {
        public CallExpression(IExpression expression, CallType type)
            : base(expression)
        {
            this.CallType = type;
        }
        public CallType CallType { get; private set; }
    }
}
