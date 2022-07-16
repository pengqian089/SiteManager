using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SiteManager.Infrastructure;

namespace SiteManager.MongodbAccess
{
    public abstract class MongodbAccess
    {
        private readonly IMongoClient _client;

        private readonly string _dataBaseName;
        
        protected MongodbAccess(DbOption option)
        {
            _dataBaseName = option.Db;
            //DbOption = option;
            MongoIdentity identity = new MongoExternalIdentity(option.Db, option.ConnUser);
            MongoIdentityEvidence identityEvidence = new PasswordEvidence(option.Password);
            var setting = new MongoClientSettings
            {
                Server = new MongoServerAddress(option.Host, option.Port),
                Credential = new MongoCredential("SCRAM-SHA-1", identity, identityEvidence),
#if DEBUG
                //ClusterConfigurator = builder =>
                //{
                //    var logger = LoggerFactoryDb.CreateLogger<MongoLogEvents>();
                //    var logEvents = new MongoLogEvents(logger);

                //    builder.Subscribe<CommandStartedEvent>(x => logEvents.Handle(x));
                //    builder.Subscribe<CommandSucceededEvent>(x => logEvents.Handle(x));
                //}
#endif
                
            };
            _client = new MongoClient(setting);
        }

        protected MongodbAccess(IMongoClient client)
        {
            _dataBaseName = client.Settings.Credential.Identity.Source;
            _client = client;
        }

        /// <summary>
        /// 获取Mongodb客户端
        /// </summary>
        internal virtual IMongoClient Client => _client;

        /// <summary>
        /// 获取 MongoDB 数据库
        /// </summary>
        public virtual IMongoDatabase Database => _client.GetDatabase(_dataBaseName);

        protected virtual ILoggerFactory LoggerFactoryDb => LoggerFactory.Create(loggerBuilder =>
            loggerBuilder
                .AddConsole()
                .AddDebug()
        );
    }

    /// <summary>
    /// Mongodb 数据库访问抽象类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MongodbAccess<T> : MongodbAccess where T : IBaseEntity, new()
    {
        protected MongodbAccess(DbOption option,string collectionName):base(option)
        {
            _collectionName = collectionName;
        }
        
        protected MongodbAccess(IMongoClient client,string collectionName):base(client)
        {
            _collectionName = collectionName;
        }

        protected MongodbAccess(DbOption option) : base(option)
        {
        }

        protected MongodbAccess(IMongoClient client) : base(client)
        {
        }

        private readonly string _collectionName = typeof(T).Name;

        /// <summary>
        /// 获取 该实体中 MongoDB数据库的集合
        /// </summary>
        public virtual IMongoCollection<T> Collection => Database.GetCollection<T>(_collectionName);

        /// <summary>
        /// 获取 提供对MongoDB数据查询的Queryable
        /// </summary>
        /// <returns></returns>
        public IMongoQueryable<T> MongoQueryable => Database.GetCollection<T>(_collectionName).AsQueryable(
            //update 2020年5月6日 内存限制写入临时文件 https://docs.mongodb.com/manual/reference/command/aggregate/
            new AggregateOptions {AllowDiskUse = true});

        /// <summary>
        /// 根据筛选条件更新数据
        /// </summary>
        /// <param name="filter">筛选条件</param>
        /// <param name="update">要修改的数据</param>
        public abstract void Update(Expression<Func<T, bool>> filter, UpdateDefinition<T> update);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="source"></param>
        public abstract void Insert(params T[] source);

        /// <summary>
        /// 根据筛选条件删除数据
        /// </summary>
        /// <param name="filter"></param>
        public abstract void Delete(Expression<Func<T, bool>> filter);


        public virtual void StartSession()
        {
            using (var session = Client.StartSession())
            {
            }
        }

        public abstract void Commit();
    }
}