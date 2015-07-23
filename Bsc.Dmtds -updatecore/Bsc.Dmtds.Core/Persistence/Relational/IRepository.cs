using System;
using System.Collections.Generic;

namespace Bsc.Dmtds.Core.Persistence.Relational
{
    public interface IRepository<T>:IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        IEnumerable<T> All();

        T Get(T dummy);
        void Add(T item);
        void Update(T @new, T old);
        void Remove(T item);
//        /// <summary>
//        /// 添加实体
//        /// </summary>
//        /// <param name="item">Item to add to repository</param>
//        void Add(T item);
//
//        /// <summary>
//        /// 删除实体
//        /// </summary>
//        /// <param name="item">Item to delete</param>
//        void Remove(T item);
//
//        /// <summary>
//        /// 设置实体
//        /// </summary>
//        /// <param name="item">Item to modify</param>
//        void Modify(T item);
//
//        /// <summary>
//        ///attach实体 
//        ///In EF this can be done with Attach and with Update in NH
//        /// </summary>
//        /// <param name="item">Item to attach</param>
//        void TrackItem(T item);
//
//        /// <summary>
//        /// 修改
//        /// When calling Commit() method in UnitOfWork 
//        /// these changes will be saved into the storage
//        /// </summary>
//
//        /// <param name="new"></param>
//        /// <param name="old"></param>
//        void Update(T @new, T old);
//
//        /// <summary>
//        /// 通过不完整实体
//        /// </summary>
//        /// <returns></returns>
//        T Get(T dummy);
//
//        /// <summary>
//        /// 得到所有的
//        /// </summary>
//        /// <returns>List of selected elements</returns>
//        IEnumerable<T> GetAll();
//
//
//        /// <summary>
//        /// 得到分页
//        /// </summary>
//        /// <param name="pageIndex">Page index</param>
//        /// <param name="pageCount">Number of elements in each page</param>
//        /// <param name="orderByExpression">Order by expression for this query</param>
//        /// <param name="ascending">Specify if order is ascending</param>
//        /// <returns>List of selected elements</returns>
//        IEnumerable<T> GetPaged<TProperty>(int pageIndex, int pageCount, Expression<Func<T, TProperty>> orderByExpression, bool ascending);
//
//        /// <summary>
//        /// 得到过滤后的
//        /// 
//        /// </summary>
//        /// <param name="filter">Filter that each element do match</param>
//        /// <returns>List of selected elements</returns>
//        IEnumerable<T> GetFiltered(Expression<Func<T, bool>> filter);
    }
}
