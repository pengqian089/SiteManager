using System.Linq.Expressions;
using MongoDB.Driver.Linq;
using SiteManager.Infrastructure;

namespace SiteManager.MongodbAccess
{
    public interface IUnitOfWork:IDisposable
    {
        /// <summary>
        /// 获取指定的仓储库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> GetRepository<T>() where T:IBaseEntity,new();

        /// <summary>
        /// 保存对数据库的所有更改
        /// </summary>
        void Commit();

        /// <summary>
        /// 异步。保存对数据库的所有更改
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();

        /// <summary>
        /// 异步。回滚事务
        /// </summary>
        /// <returns></returns>
        Task RollbackAsync();

        /// <summary>
        /// 获取指定的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IMongoQueryable<T> SearchFor<T>(Expression<Func<T, bool>> predicate) where T : IBaseEntity, new();

        /// <summary>
        /// 数据库配置
        /// </summary>
        DbOption DbOption { get; }
    }
}