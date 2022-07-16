using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using SiteManager.Infrastructure;
using SiteManager.MongodbAccess;

namespace SiteManager.Service
{
    public abstract class BasicService<T> where T : IBaseEntity, new()
    {
        /// <summary>
        /// Db 配置
        /// </summary>
        protected DbOption DbOption { get; } = DbTools.DefaultOption;
        
        protected IMapper Mapper => SingleMapper.GetInstance().Mapper;

        /// <summary>
        /// Repository 实例
        /// </summary>
        protected IRepository<T> Repository { get; }

        /// <summary>
        /// 默认 Repository
        /// </summary>
        protected BasicService()
        {
            Repository = new Repository<T>(DbOption);
        }

        protected BasicService(string collectionName)
        {
            Repository = new Repository<T>(DbOption,collectionName);
        }

        /// <summary>
        /// 指定 Db 配置，创建 Repository 实例
        /// </summary>
        /// <param name="option"></param>
        protected BasicService(DbOption option)
        {
            DbOption = option;
            Repository = new Repository<T>(option);
        }
        
        protected BasicService(DbOption option,string collectionName)
        {
            DbOption = option;
            Repository = new Repository<T>(DbOption,collectionName);
        }


        protected Func<string, Task<T>> TryParseObjectId =>
            async s =>
            {
                if (ObjectId.TryParse(s, out var id))
                {
                    return await Repository.FindAsync(id);
                }

                return default;
            };


        /// <summary>
        /// 从<see cref="string"/>转换成<see cref="ObjectId"/>
        /// 如果成功，将异步执行 <see cref="Action"/>
        /// </summary>
        /// <param name="strId">ID</param>
        /// <param name="action"><see cref="Action"/></param>
        /// <returns></returns>
        protected async Task TryParseObjectIdAction(string strId, Func<ObjectId, IRepository<T>, Task> action)
        {
            if (ObjectId.TryParse(strId, out var id))
            {
                await action(id, Repository);
            }
        }

        #region Mongodb GridFS

        /// <summary>
        /// 获取储存桶
        /// </summary>
        /// <returns></returns>
        protected virtual IGridFSBucket GetBucket()
        {
            return new GridFSBucket(Repository.Database);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id"></param>
        protected virtual async Task DeleteFileAsync(ObjectId id)
        {
            var gridFs = GetBucket();
            try
            {
                await gridFs.DeleteAsync(id);
            }
            catch (GridFSFileNotFoundException)
            {
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual async Task<GridFSDownloadStream<ObjectId>> DownloadAsync(ObjectId id)
        {
            var gridFs = GetBucket();
            var result = await gridFs.OpenDownloadStreamAsync(id);
            return result;
        }

        protected virtual async Task DownloadAsync(ObjectId id,Stream stream)
        {
            var gridFs = GetBucket();
            await gridFs.DownloadToStreamAsync(id,stream);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        protected virtual async Task<ObjectId> UploadAsync(string fileName, Stream stream)
        {
            var gridFs = GetBucket();
            return await gridFs.UploadFromStreamAsync(fileName, stream);
        }

        protected virtual async Task<List<GridFSFileInfo>> GetBucketFileInfos(params ObjectId[] id)
        {
            var gridFs = GetBucket();
            var filter = Builders<GridFSFileInfo>.Filter.In("_id", id);
            var result = await gridFs.FindAsync(filter);
            return await result.ToListAsync();
        }
        
        /// <summary>
        /// 获取文件名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected async IAsyncEnumerable<string> BucketFilenameAsync(params ObjectId[] id)
        {
            var gridFs = GetBucket();
            var filter = Builders<GridFSFileInfo>.Filter.In("_id", id);
            var result = await gridFs.FindAsync(filter);
            while (await result.MoveNextAsync())
            {
                foreach (var current in result.Current)
                {
                    yield return current.Filename;
                }
            }
        }
        #endregion
    }

    
    public abstract class BasicService<T, T1>:BasicService<T> where T : IBaseEntity, new() where T1 : IBaseEntity, new()
    {
        protected IRepository<T1> RepositoryItem2 { get; }

        /// <summary>
        /// 默认 Repository
        /// </summary>
        protected BasicService():base()
        {
            RepositoryItem2 = new Repository<T1>(DbOption);
        }

        /// <summary>
        /// 指定 Db 配置，创建 Repository 实例
        /// </summary>
        /// <param name="option"></param>
        protected BasicService(DbOption option):base(option)
        {
            RepositoryItem2 = new Repository<T1>(option);
        }
    }
}