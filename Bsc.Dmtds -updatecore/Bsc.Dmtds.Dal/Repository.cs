using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Persistence.Relational;


namespace Bsc.Dmtds.Dal
{
    public abstract class Repository<T> : IRepository<T>
        where T:class
    {
        #region 成员

        readonly IQueryableUnitOfWork _unitOfWork;

        #endregion

        #region 构造函数
        /// <summary>
        /// new一个仓库的实例
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == (IUnitOfWork)null)
                throw new ArgumentNullException("unitOfWork");
            _unitOfWork = unitOfWork;
        }
        #region All
        public virtual IEnumerable<T> All()
        {
            return GetSet();
        }
        #endregion

        #region Add
        public virtual void Add(T item)
        {
            Remove(item);

            GetSet().Add(item);
            _unitOfWork.Commit();
        }
        #endregion

        #region Update
        public abstract void Update(T @new, T old);
        #endregion

        #region Get
        public abstract T Get(T dummy);
        #endregion

        #region Remove
        public void Remove(T item)
        {
            var entity = Get(item);
            if (entity != null)
            {
                GetSet().Remove(entity);
                _unitOfWork.Commit();
            }
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            if (id != 0)
                return GetSet().Find(id);
            else
                return null;
        }





        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="KProperty"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetPaged<KProperty>(int pageIndex, int pageCount, Expression<Func<T, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            else
            {
                return set.OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetFiltered(Expression<Func<T, bool>> filter)
        {
            return GetSet().Where(filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="persisted"></param>
        /// <param name="current"></param>
        public virtual void Merge(T persisted, T current)
        {
            _unitOfWork.ApplyCurrentValues(persisted, current);
        }

        #endregion

        #region IRepository接口成员

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }


        #endregion

        #region IDisposable 接口成员

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>释放事务提交
        /// </summary>
        public void Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }

        #endregion

        #region 私有成员

        IDbSet<T> GetSet()
        {
            return _unitOfWork.CreateSet<T>();
        }
        #endregion
    }
}
