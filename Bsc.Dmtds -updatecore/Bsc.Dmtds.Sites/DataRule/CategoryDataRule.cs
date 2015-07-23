using System.Runtime.Serialization;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Content.Query;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Sites.View;

namespace Bsc.Dmtds.Sites.DataRule
{
    [DataContract(Name = "CategoryDataRule")]
    [KnownTypeAttribute(typeof(CategoryDataRule))]
    public class CategoryDataRule : FolderDataRule
    {
        public override IContentQuery<Content.Models.TextContent> GetContentQuery(DataRuleContext dataRuleContext)
        {
            var contentQuery = base.GetContentQuery(dataRuleContext);
            if (contentQuery is MediaContentQuery)
            {
                throw new BscException(string.Format("The binary folder '{0}' does not support '{1}'.", FolderName, "CategoryDataRule"));
            }
            var site = dataRuleContext.Site;
            var repository = Sites.Models.ModelExtensions.GetRepository(site);
            var categoryFolder = (TextFolder)(new TextFolder(repository, CategoryFolderName).AsActual());
            if (categoryFolder == null)
            {
                throw new BscException(string.Format("The folder does not exists.\"{0}\"", CategoryFolderName));
            }
            contentQuery = ((TextContentQuery)contentQuery).Categories(categoryFolder);
            if (CategoryClauses != null)
            {
                contentQuery = contentQuery.Where(CategoryClauses.Parse(categoryFolder.GetSchema(), dataRuleContext.ValueProvider));
            }

            if (Page_Context.Current.EnabledInlineEditing(EditingType.Content))
            {
                contentQuery = contentQuery.Where(
                    new Bsc.Dmtds.Content.Query.Expressions.OrElseExpression(
                        new Bsc.Dmtds.Content.Query.Expressions.WhereEqualsExpression(null, "Published", true),
                        new Bsc.Dmtds.Content.Query.Expressions.WhereEqualsExpression(null, "Published", null)));
            }
            else
                contentQuery = contentQuery.WhereEquals("Published", true); //default query published=true.

            return contentQuery;
        }

        public override DataRuleType DataRuleType
        {
            get
            {
                return DataRule.DataRuleType.Category;
            }
        }
    }
}