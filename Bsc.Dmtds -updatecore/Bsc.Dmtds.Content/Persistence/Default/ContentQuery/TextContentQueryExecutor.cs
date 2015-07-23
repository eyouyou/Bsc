using System;
using System.Collections.Generic;
using System.Linq;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Content.Query;

namespace Bsc.Dmtds.Content.Persistence.Default.ContentQuery
{
    internal class TextContentQueryExecutor : QueryExecutorBase
    {
        #region .ctor
        public TextContentQuery TextContentQuery { get; private set; }
        public TextContentQueryExecutor(TextContentQuery textContentQuery)
        {
            TextContentQuery = textContentQuery;
        }
        #endregion

        #region Execute
        public override object Execute()
        {
            IEnumerable<TextContent> contents = new TextContent[0];

            contents = TextContentQuery.Schema.GetContents();

            if (TextContentQuery.Folder != null)
            {
                contents = contents.Where(it => it.FolderName.EqualsOrNullEmpty(TextContentQuery.Folder.FullName, StringComparison.CurrentCultureIgnoreCase));
            }

            QueryExpressionTranslator translator = new QueryExpressionTranslator();

            var contentQueryable = translator.Translate(TextContentQuery.Expression, contents.AsQueryable());

            foreach (var categoryQuery in translator.CategoryQueries)
            {
                var categories = (IEnumerable<TextContent>)ContentQueryExecutor.Execute(categoryQuery);


                var categoryData = TextContentQuery.Repository.GetCategoryData()
                    .Where(it => categories.Any(c => it.CategoryUUID.EqualsOrNullEmpty(c.UUID, StringComparison.CurrentCultureIgnoreCase)));

                contentQueryable = contentQueryable.Where(it => categoryData.Any(c => it.UUID.EqualsOrNullEmpty(c.ContentUUID, StringComparison.CurrentCultureIgnoreCase)));

            }

            return Execute(contentQueryable, translator.OrderExpressions, translator.CallType, translator.Skip, translator.Take);
        }
        #endregion
    }
}