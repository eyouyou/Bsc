

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class WhereClauseExpression : Expression, IWhereExpression
    {
        public WhereClauseExpression(string whereClause)
            : this(null, whereClause)
        {

        }
        public WhereClauseExpression(IExpression expression, string whereClause)
            : base(expression)
        {
            this.WhereClause = whereClause;
        }
        public string WhereClause { get; private set; }
    }
}
