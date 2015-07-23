using System.Collections.Generic;
using System.IO;
using Bsc.Dmtds.Content.Models;

namespace Bsc.Dmtds.Content.Persistence
{
    public interface ISchemaProvider : IContentElementProvider<Schema>
    {
        void Initialize(Schema schema);

        Schema Create(Repository repository, string schemaName, Stream templateStream);

        Schema Copy(Repository repository, string sourceName, string destName);

        void Export(Repository repository, IEnumerable<Schema> models, System.IO.Stream outputStream);
        void Import(Repository repository, System.IO.Stream zipStream, bool @override);

    }
}