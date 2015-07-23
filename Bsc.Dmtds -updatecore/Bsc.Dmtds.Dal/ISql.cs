

using System.Collections.Generic;

namespace Bsc.Dmtds.Dal
{
    public interface ISql
    {
        IEnumerable<T> ExecuteQuery<T>(string sqlQuery, params object[] parameters);

        int ExecuteCommand(string sqlCommand, params object[] parameters);
    }
}