using Bsc.Dmtds.Content.Models;

namespace Bsc.Dmtds.Content.Query.Translator.String
{
    public class MediaContentQueryTranslator:ITranslator<MediaContent>
    {
        public TranslatedQuery Translate(IContentQuery<MediaContent> contentQuery)
        {
            var mediaQuery = (MediaContentQuery)contentQuery;
            TranslatedMediaContentQuery query = new TranslatedMediaContentQuery(contentQuery.Repository, mediaQuery.MediaFolder);

            StringVisitor visitor = new StringVisitor(query);
            visitor.Visite(contentQuery.Expression);

            return query;
        }
    }
}