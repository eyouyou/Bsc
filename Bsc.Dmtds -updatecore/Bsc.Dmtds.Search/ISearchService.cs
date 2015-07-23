using System.Collections.Generic;
using Bsc.Dmtds.Core.Mvc.Paging;

namespace Bsc.Dmtds.Search
{
    public interface ISearchService
    {
        void Add<T>(T o);
        void Update<T>(T o);
        void Delete<T>(T o);
        void BatchAdd<T>(IEnumerable<T> list);
        void BatchUpdate<T>(IEnumerable<T> list);
        void BatchDelete<T>(IEnumerable<T> list);
        void BatchDelete(string folderName);

        PagedList<Models.ResultObject> Search(string key, int pageIndex, int pageSize, params string[] folders);
 
    }
}