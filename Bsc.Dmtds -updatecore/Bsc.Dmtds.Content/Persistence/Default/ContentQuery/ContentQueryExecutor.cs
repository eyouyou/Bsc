using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Content.Query;

namespace Bsc.Dmtds.Content.Persistence.Default.ContentQuery
{
    internal static class ContentQueryExecutor
    {
        public static object Execute(IContentQuery<TextContent> contentQuery)
        {
            QueryExecutorBase queryExecutor = null;
            if (contentQuery is CategoriesQuery)
            {
                queryExecutor = new CategoriesQueryExecutor((CategoriesQuery)contentQuery);
            }
            else if (contentQuery is ParentQuery)
            {
                queryExecutor = new ParentQueryExecutor((ParentQuery)contentQuery);
            }
            else if (contentQuery is ChildrenQuery)
            {
                queryExecutor = new ChildrenQueryExecutor((ChildrenQuery)contentQuery);
            }
            else if (contentQuery is TextContentQuery)
            {
                queryExecutor = new TextContentQueryExecutor((TextContentQuery)contentQuery);
            }
            return queryExecutor.Execute();
        }
    }
}