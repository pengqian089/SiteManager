using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SiteManager.Infrastructure;

namespace SiteManager.MongodbAccess
{
    public interface IRepository
    {
        IMongoDatabase Database { get; }
    }

    public interface IRepository<T> : IRepository where T : IBaseEntity
    {
        /// <summary>
        /// mongodb客户端
        /// </summary>
        IMongoClient Client { get; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        DbOption DbOption { get; }

        /// <summary>
        /// 是否为工作单元
        /// </summary>
        bool CanSubmit { get; }

        /// <summary>
        /// 获取 该实体Mongodb的集合
        /// </summary>
        IMongoCollection<T> Collection { get; }

        IMongoQueryable<T> MongodbQueryable { get; }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        void Insert(params T[] source);


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        void Delete(Expression<Func<T, bool>> filter);

        /// <summary>
        /// 根据实体删除数据
        /// </summary>
        /// <param name="t"></param>
        void Delete(T t);

        /// <summary>
        /// 根据查询条件更新数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        void Update(Expression<Func<T, bool>> predicate, UpdateDefinition<T> update);

        /// <summary>
        /// 根据实体修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentException">If the entity no exists property 'id',then will thow exception.</exception>
        void Update(T entity);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IMongoQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IFindFluent<T, T> SearchFor(FilterDefinition<T> filter);

        /// <summary>
        /// 异步 数据查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IAsyncEnumerable<T> SearchForAsync(FilterDefinition<T> filter);

        /// <summary>
        /// 根据ObjectId获取单条记录，不存在将会返回null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find(ObjectId id);

        void Commit();


        #region 异步方法

        /// <summary>
        /// (异步)根据ObjectId获取单条记录，不存在将会返回null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FindAsync(ObjectId id);


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        Task InsertAsync(params T[] source);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DeleteResult> DeleteAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DeleteResult> DeleteAsync(FilterDefinition<T> filter);

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DeleteResult> DeleteAsync(ObjectId id);

        /// <summary>
        /// 根据查询条件更新数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<UpdateResult> UpdateAsync(Expression<Func<T, bool>> predicate, UpdateDefinition<T> update);

        /// <summary>
        /// 根据实体修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentException">If the entity no exists property 'id',then will thow exception.</exception>
        Task<ReplaceOneResult> UpdateAsync(T entity);

        #endregion
    }
}