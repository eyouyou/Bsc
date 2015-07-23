

using Bsc.Dmtds.Content.Models;

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class WhereCategoryExpression : Expression, IWhereExpression
    {
        public WhereCategoryExpression(IContentQuery<TextContent> categoryQuery)
            : this(null, categoryQuery)
        {

        }
        public WhereCategoryExpression(IExpression expression, IContentQuery<TextContent> categoryQuery)
            : base(expression)
        {
            this.CategoryQuery = categoryQuery;
        }
        public IContentQuery<TextContent> CategoryQuery { get; private set; }
    }
}
