namespace Bsc.Dmtds.Content.Query.Translator.String
{
    public static class TextTranslator
    {
        public static string Translate<T>(IContentQuery<T> contentQuery)
            where T : ContentBase
        {
            if (contentQuery is MediaContentQuery)
            {
                var translator = new MediaContentQueryTranslator();
                return translator.Translate((MediaContentQuery)contentQuery).ToString();
            }
            else
            {
                var translator = new TextContentQueryTranslator();
                return translator.Translate((TextContentQuery)contentQuery).ToString();
            }

        }
 
    }
}