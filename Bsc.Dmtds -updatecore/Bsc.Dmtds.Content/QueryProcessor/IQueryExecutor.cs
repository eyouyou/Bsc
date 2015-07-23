using System.Collections.Generic;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Content.QueryProcessor
{
    public interface IQueryExecutor<T>
        where T:IIdentifiable
    {
        object Execute();
        string BuildQuerySQL(SQLServerVisitor<T> visitor, out IEnumerable<Parameter> parameters);

    }
}