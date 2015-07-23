using System.Runtime.Serialization;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Content.Query;

namespace Bsc.Dmtds.Sites.DataRule
{
    [DataContract(Name = "SchemaDataRule")]
    [KnownTypeAttribute(typeof(SchemaDataRule))]
    public class SchemaDataRule : DataRuleBase
    {
        [DataMember(Order = 13)]
        public string SchemaName { get; set; }

        public override IContentQuery<Bsc.Dmtds.Content.Models.TextContent> GetContentQuery(DataRuleContext dataRuleContext)
        {
            var site = dataRuleContext.Site;
            var repositoryName = site.Repository;
            if (string.IsNullOrEmpty(repositoryName))
            {
                throw new BscException("The repository for site is null.");
            }
            var repository = new Repository(repositoryName);
            var schema = new Schema(repository, SchemaName);
            var contentQuery = (IContentQuery<Bsc.Dmtds.Content.Models.TextContent>)schema.CreateQuery();

            contentQuery.Where(WhereClauses.Parse(schema, dataRuleContext.ValueProvider));

            return contentQuery;
        }
        public override DataRuleType DataRuleType
        {
            get { return DataRuleType.Schema; }
        }

        public override Schema GetSchema(Repository repository)
        {
            return new Schema(repository, SchemaName);
        }
    }
}