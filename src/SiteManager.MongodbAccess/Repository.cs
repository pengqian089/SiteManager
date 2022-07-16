using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SiteManager.Infrastructure;

namespace SiteManager.MongodbAccess
{
    public class Repository<T> : IRepository<T> where T : IBaseEntity, new()
    {
        private MongodbAccess<T> _access;

        public Repository()
        {
            DbOption = DbTools.DefaultOption;
            Init(DbOption);
        }

        public Repository(DbOption option)
        {
            DbOption = option;
            Init(option);
        }

        public Repository(string collectionName)
        {
            DbOption = DbTools.DefaultOption;
            Init(DbOption,collectionName);
        }

        public Repository(DbOption option,string collectionName)
        {
            DbOption = option;
            Init(option,collectionName);
        }


        //private DbOption option = null;

        void Init(DbOption option,string collectionName = null)
        {
            CanSubmit = false;
            _access = string.IsNullOrEmpty(collectionName)
                ? new MongodbAccessEntity<T>(option)
                : new MongodbAccessEntity<T>(option, collectionName);
            Client = _access.Client;
        }

        public IMongoClient Client { get; private set; }
        public DbOption DbOption { get; }
        
        

        public bool CanSubmit { get; private set; }

        public IMongoQueryable<T> MongodbQueryable => _access.MongoQueryable;

        public IMongoCollection<T> Collection => _access.Collection;

        public void Insert(params T[] source)
        {
            _access.Insert(source);
        }

        public void Delete(Expression<Func<T, bool>> filter)
        {
            _access.Delete(filter);
        }

        public void Delete(T t)
        {
            _access.Collection.DeleteOne(new ObjectFilterDefinition<T>(t));
        }

        public void Update(Expression<Func<T, bool>> predicate, UpdateDefinition<T> update)
        {
            _access.Update(predicate, update);
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
            Collection.ReplaceOne(filter, entity);
        }

        public IMongoQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return _access.MongoQueryable.Where(predicate);
        }

        public IFindFluent<T, T> SearchFor(FilterDefinition<T> filter)
        {
            return Collection.Find(filter);
        }

        public async IAsyncEnumerable<T> SearchForAsync(FilterDefinition<T> filter)
        {
            var result = await Collection.FindAsync(filter);
            while (await result.MoveNextAsync())
            {
                foreach (var item in result.Current)
                {
                    yield return item;
                }
            }
        }


        public T Find(ObjectId id)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null && idProperty.PropertyType == typeof(ObjectId))
                return Collection.Find(id);
            return default(T);
        }

        public async Task<T> FindAsync(ObjectId id)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null && idProperty.PropertyType == typeof(ObjectId))
                return await Collection.FindAsync(id);
            return default(T);
        }


        public async Task InsertAsync(params T[] source)
        {
            await Collection.InsertManyAsync(source);
        }


        public async Task<DeleteResult> DeleteAsync(Expression<Func<T, bool>> filter)
        {
            var result = await Collection.DeleteManyAsync(filter);
            return result;
        }

        public async Task<DeleteResult> DeleteAsync(FilterDefinition<T> filter)
        {
            var result = await Collection.DeleteManyAsync(filter);
            return result;
        }

        public async Task<DeleteResult> DeleteAsync(ObjectId id)
        {
            if (typeof(T).GetProperty("Id") == null)
                throw new Exception($"type {typeof(T)} no exists property 'id'.");
            var filter = Builders<T>.Filter.Eq(new StringFieldDefinition<T, ObjectId>("Id"), id);
            return await Collection.DeleteOneAsync(filter);
        }

        public async Task<UpdateResult> UpdateAsync(Expression<Func<T, bool>> predicate, UpdateDefinition<T> update)
        {
            var result = await Collection.UpdateManyAsync(predicate, update);
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
            var result = await Collection.ReplaceOneAsync(filter, entity);
            return result;
        }

        public void Commit()
        {
            _access.Commit();
        }

        public IMongoDatabase Database => _access.Database;
    }
}