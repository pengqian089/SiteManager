using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SiteManager.Infrastructure;

namespace SiteManager.MongodbAccess
{
    public class UnitOfWork:IUnitOfWork
    {
        private Dictionary<Type, object> _repositories;

        public static async Task<UnitOfWork> CreateScopeAsync()
        {
            var option = DbTools.DefaultOption;
            return await CreateScopeAsync(option);
        }

        public static async Task<UnitOfWork> CreateScopeAsync(DbOption option)
        {
            var result = MongodbClient.GetClient(option);
            IClientSessionHandle session = await result.Item1.StartSessionAsync();
            session.StartTransaction();
            return new UnitOfWork(option, session, result.Item1); 
        }

        public static UnitOfWork CreateScope()
        {
            var option = DbTools.DefaultOption;
            return CreateScope(option);
        }

        public static UnitOfWork CreateScope(DbOption option)
        {
            var result = MongodbClient.GetClient(option);
            IClientSessionHandle session = result.Item1.StartSession();
            session.StartTransaction();
            return new UnitOfWork(option, session, result.Item1);
        }


        private UnitOfWork(DbOption option, IClientSessionHandle session ,IMongoClient client)
        {
            DbOption = option;
            Session = session;
            Client = client;
        }

        public void Dispose()
        {
            _repositories?.Clear();
            Session.Dispose();
            GC.SuppressFinalize(this);
        }

        private readonly IClientSessionHandle Session;

        private readonly IMongoClient Client;

        public IRepository<T> GetRepository<T>() where T : IBaseEntity, new()
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                IRepository<T> repository = new RepositoryUnitOfWork<T>(Client,DbOption,Session);;
                _repositories.Add(type, repository);
            }
            return (IRepository<T>)_repositories[type];
        }

        public void Commit()
        {
            Session.CommitTransaction();
        }

        public async Task CommitAsync()
        {
            await Session.CommitTransactionAsync();
        }

        public void Rollback()
        {
            Session.AbortTransaction();
        }

        public async Task RollbackAsync()
        {
            await Session.AbortTransactionAsync();
        }

        public IMongoQueryable<T> SearchFor<T>(Expression<Func<T, bool>> predicate) where T : IBaseEntity, new()
        {
            return GetRepository<T>().SearchFor(predicate);
        }

        public DbOption DbOption { get; }

        
    }
}