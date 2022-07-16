using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SiteManager.Infrastructure;

namespace SiteManager.MongodbAccess
{
    public class RepositoryUnitOfWork<T>:IRepository<T> where T : IBaseEntity,new()
    {
        private readonly MongodbAccess<T> _access;

        private readonly IClientSessionHandle _session;

        public RepositoryUnitOfWork(IMongoClient client, DbOption option, IClientSessionHandle session)
        {
            CanSubmit = true;
            DbOption = option;
            Client = client;
            _access = new MongodbAccessEntity<T>(Client);
            _session = session;
        }

        public IMongoClient Client { get; }
        public DbOption DbOption { get; }
        public bool CanSubmit { get; }
        public IMongoCollection<T> Collection => _access.Collection;
        public IMongoQueryable<T> MongodbQueryable => _access.MongoQueryable;
        
        public void Insert(params T[] source)
        {
           Collection.InsertMany(_session,source);
        }

        public void Delete(Expression<Func<T, bool>> filter)
        {
            Collection.DeleteMany(_session, filter);
        }

        public void Delete(T t)
        {
            Collection.DeleteOne(_session,new ObjectFilterDefinition<T>(t));
        }

        public void Update(Expression<Func<T, bool>> predicate, UpdateDefinition<T> update)
        {
            Collection.UpdateMany(_session, predicate, update);
        }

        public void Update(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null)
                throw new ArgumentException("In the entity no exists property 'id'.", nameof(entity));
            var id = idProperty.GetValue(entity);
            if (id == null)
                throw new ArgumentException("The entity property 'id' value is null.", nameof(entity));
            //var idTypeName = idProperty.PropertyType.Name;
            var param = Expression.Parameter(typeof(T), "__q");
            var left = Expression.Property(param, idProperty);
            var right = Expression.Constant(id);
            var equalExpression = (Expression)Expression.Equal(left, right);
            var lambda = Expression.Lambda<Func<T, bool>>(equalExpression, param);
            FilterDefinition<T> filter = lambda;
            Collection.ReplaceOne(_session,filter, entity);
        }

        public IMongoQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IFindFluent<T, T> SearchFor(FilterDefinition<T> filter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<T> SearchForAsync(FilterDefinition<T> filter)
        {
            throw new NotImplementedException();
        }

        public T Find(ObjectId id)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null && idProperty.PropertyType == typeof(ObjectId))
            {
                var filter = Builders<T>.Filter.Eq(new StringFieldDefinition<T, ObjectId>("Id"), id);
                return Collection.Find(_session, filter).SingleOrDefault();
            }
            return default(T);
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindAsync(ObjectId id)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null && idProperty.PropertyType == typeof(ObjectId))
            {
                var filter = Builders<T>.Filter.Eq(new StringFieldDefinition<T, ObjectId>("Id"), id);
                return await Collection.Find(_session, filter).SingleOrDefaultAsync();
            }
            return default(T);
        }

        public async Task InsertAsync(params T[] source)
        {
            await Collection.InsertManyAsync(_session, source);
        }

        public async Task<DeleteResult> DeleteAsync(Expression<Func<T, bool>> filter)
        {
            var result = await Collection.DeleteManyAsync(_session,filter);
            return result;
        }
        
        public async Task<DeleteResult> DeleteAsync(FilterDefinition<T> filter)
        {
            var result = await Collection.DeleteManyAsync(filter);
            return result;
        }

        public async Task<DeleteResult> DeleteAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateResult> UpdateAsync(Expression<Func<T, bool>> predicate, UpdateDefinition<T> update)
        {
            var result = await Collection.UpdateManyAsync(_session,predicate, update);
            return result;
        }

        public async Task<ReplaceOneResult> UpdateAsync(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null)
                throw new ArgumentException("In the entity no exists property 'id'.", nameof(entity));
            var id = idProperty.GetValue(entity);
            if (id == null)
                throw new ArgumentException("The entity property 'id' value is null.", nameof(entity));
            var idTypeName = idProperty.PropertyType.Name;
            FilterDefinition<T> filter;
            switch (idTypeName)
            {
                case "ObjectId":
                    var definitionObjectId = new StringFieldDefinition<T, ObjectId>("Id");
                    filter = Builders<T>.Filter.Eq(definitionObjectId, (ObjectId)id);
                    break;
                case "Int32":
                    var definitionInt32 = new StringFieldDefinition<T, int>("Id");
                    filter = Builders<T>.Filter.Eq(definitionInt32, (int)id);
                    break;
                case "String":
                    var definitionString = new StringFieldDefinition<T, string>("Id");
                    filter = Builders<T>.Filter.Eq(definitionString, (string)id);
                    break;
                default:
                    throw new Exception($"Do not support {idTypeName} type!");
            }
            var result = await Collection.ReplaceOneAsync(_session,filter, entity);
            return result;
        }

        public IMongoDatabase Database => _access.Database;
    }
}