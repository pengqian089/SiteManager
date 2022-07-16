using System.Linq.Expressions;
using MongoDB.Driver;
using SiteManager.Infrastructure;

namespace SiteManager.MongodbAccess
{
    public class MongodbAccessUnitOfWork<T> : MongodbAccess<T> where T : IBaseEntity, new()
    {
        private readonly List<WriteModel<T>> _writes = new List<WriteModel<T>>();

        public override void Update(Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            _writes.Add(new UpdateManyModel<T>(filter, update));
        }

        public override void Insert(params T[] source)
        {
            foreach (var item in source)
            {
                _writes.Add(new InsertOneModel<T>(item));
            }
        }

        public override void Delete(Expression<Func<T, bool>> filter)
        {
            _writes.Add(new DeleteOneModel<T>(filter));
        }

        public override void Commit()
        {
            if (_writes.Any())
                Collection.BulkWrite(_writes);
        }

        public MongodbAccessUnitOfWork(DbOption option) : base(option)
        {
        }

        public MongodbAccessUnitOfWork(MongoClient client) : base(client) { }
    }
}