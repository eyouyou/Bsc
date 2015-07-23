namespace Bsc.Dmtds.Content.Query.Translator.String
{
    public interface ITranslator<T>
        where T : ContentBase
    {
        TranslatedQuery Translate(IContentQuery<T> contentQuery);

    }
}