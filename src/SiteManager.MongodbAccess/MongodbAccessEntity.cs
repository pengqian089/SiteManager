using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using SiteManager.Infrastructure;
using UpdateResult = MongoDB.Driver.UpdateResult;

namespace SiteManager.MongodbAccess
{
    public class MongodbAccessEntity<T> : MongodbAccess<T> where T : IBaseEntity, new()
    {

        public override void Update(Expression<Func<T, bool>> filter, MongoDB.Driver.UpdateDefinition<T> update)
        {
            Collection.UpdateMany(filter, update);
        }

        public override void Insert(params T[] source)
        {
            Collection.InsertMany(source);
        }

        public override void Delete(Expression<Func<T, bool>> filter)
        {
            Collection.DeleteMany(filter);
        }

        public override void Commit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 修改符合条件的所有数据
        /// </summary>
        /// <param name="updateContent">要更改的内容，key为属性名，value为要更改的值</param>
        /// <param name="expr"></param>
        public async Task<UpdateResult> UpdateAsync(Dictionary<string, object> updateContent, Expression<Func<T, bool>> expr)
        {
            //var collection = Database.GetCollection<T>(_collectionName);
            //foreach (var item in updateContent)
            //{
            //    var param = Expression.Parameter(typeof(T));
            //    var body = Expression.MakeMemberAccess(param, typeof(T).GetProperty(item.Key));
            //    var funcType = typeof(Func<,>).MakeGenericType(typeof(T), typeof(T).GetProperty(item.Key).PropertyType);
            //    var lambda = Expression.Lambda(body, param);
            //    var lambda = Expression.Lambda(funcType, body, param);
            //    Expression<Func<T, ObjectId>> ex = x => x.Id;

            //    var func = lambda.Compile();
            //    var b = Expression.Lambda<Func<T, object>>(Expression.Convert(lambda, typeof(object)).Reduce());
            //    if (definition == null)
            //        definition = update.Set(lambda, item.Value);
            //    else
            //        definition.Set(lambda, item.Value);
            //}
            var update = new BsonDocument("$set", new BsonDocument(updateContent));
            var result = await Collection.UpdateManyAsync(expr, update);
            return result;
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task InsertManyAsync(IEnumerable<T> source)
        {
            await Collection.InsertManyAsync(source);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task InsertAsync(T entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="isMore">是否根据筛选结果删除多条数据 True:删除多条 False:删除1条</param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<DeleteResult> DeleteAsync(Expression<Func<T, bool>> filter, bool isMore)
        {
            DeleteResult result;
            if (isMore)
                result = await Collection.DeleteManyAsync(filter);
            else
                result = await Collection.DeleteOneAsync(filter);
            return result;
        }

        public MongodbAccessEntity(DbOption option) : base(option)
        {
        }

        public MongodbAccessEntity(IMongoClient client) : base(client) { }

        public MongodbAccessEntity(DbOption option,string collectionName):base(option,collectionName)
        {
            
        }
        
        public MongodbAccessEntity(IMongoClient client,string collectionName):base(client,collectionName)
        {
            
        }
    }
}
