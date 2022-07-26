using AngleSharp.Dom;
using MongoDB.Bson;
using SiteManager.Infrastructure;
using SiteManager.Infrastructure.ExpressQuery;
using SiteManager.MongodbAccess;
using SiteManager.Pager;

namespace SiteManager.Service
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// 根据当前页索引pageIndex及每页记录数pageSize获取要分页的ViewModel数据对象。
        /// </summary>
        /// <typeparam name="TSource">原类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">包含所有要分页数据的IQueryable对象。</param>
        /// <param name="pageIndex">当前页索引。</param>
        /// <param name="pageSize">每页显示的记录数。</param>
        /// <returns></returns>
        public static IPagedList<TDestination> ToPagedList<TSource, TDestination>(this IQueryable<TSource> source,
            int pageIndex, int pageSize) where TDestination : IMapFrom<TSource>, IHaveCustomMapping, new()
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var itemIndex = (pageIndex - 1) * pageSize;
            var totalItemCount = source.Count();
            while (totalItemCount <= itemIndex && pageIndex > 1)
            {
                itemIndex = (--pageIndex - 1) * pageSize;
            }

            var pageOfItems = source.Skip(itemIndex).Take(pageSize);
            var data = SingleMapper.GetInstance().Mapper.Map<List<TDestination>>(pageOfItems.ToList());
            return new PagedList<TDestination>(data, pageIndex, pageSize, totalItemCount);
        }

        /// <summary>
        /// 根据当前页索引pageIndex及每页记录数pageSize获取要分页的ViewModel数据对象。
        /// </summary>
        /// <typeparam name="TSource">原类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">包含所有要分页数据的IQueryable对象。</param>
        /// <param name="pageIndex">当前页索引。</param>
        /// <param name="pageSize">每页显示的记录数。</param>
        /// <returns></returns>
        public static async Task<IPagedList<TDestination>> ToPagedListAsync<TSource, TDestination>(
            this IQueryable<TSource> source, int pageIndex, int pageSize) where TDestination : IMapFrom<TSource>, new()
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var itemIndex = (pageIndex - 1) * pageSize;
            var totalItemCount = source.Count();
            while (totalItemCount <= itemIndex && pageIndex > 1)
            {
                itemIndex = (--pageIndex - 1) * pageSize;
            }

            var pageOfItems = source.Skip(itemIndex).Take(pageSize);
            var data = SingleMapper.GetInstance().Mapper.Map<List<TDestination>>(await pageOfItems.ToListAsync());
            return new PagedList<TDestination>(data, pageIndex, pageSize, totalItemCount);
        }

        public static async Task<IPagedList<TDestination>> ToPagedListAsync<TSource, TDestination>(
            this IQueryable<TSource> source, int pageIndex, int pageSize, Func<TSource, TDestination> func)
            where TSource : IBaseEntity, new() 
            where TDestination : new()
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var itemIndex = (pageIndex - 1) * pageSize;
            var totalItemCount = source.Count();
            while (totalItemCount <= itemIndex && pageIndex > 1)
            {
                itemIndex = (--pageIndex - 1) * pageSize;
            }
            var pageOfItems = source.Skip(itemIndex).Take(pageSize);
            var data = await pageOfItems.ToListAsync();
            return new PagedList<TDestination>(data.Select(func), pageIndex, pageSize, totalItemCount);
        }

        public static async Task<IPagedList<TViewModel>> ToPagedListByModelAsync<TEntity, TViewModel>(
            this IRepository<TEntity> repository,
            TViewModel viewModel, int pageIndex, int pageSize)
            where TEntity : IBaseEntity, new()
            where TViewModel : IMapFrom<TEntity>, new()
        {
            var expression = viewModel.GenerateExpressTree();
            var source = await repository.SearchFor(expression)
                .ToPagedListAsync<TEntity, TViewModel>(pageIndex, pageSize);
            return source;
        }

        public static async Task<IPagedList<TModel>> ToPagedListAsync<TModel>(this IQueryable<TModel> source,
            int pageIndex, int pageSize)
            where TModel : IBaseEntity, new()
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var itemIndex = (pageIndex - 1) * pageSize;
            var totalItemCount = source.Count();
            while (totalItemCount <= itemIndex && pageIndex > 1)
            {
                itemIndex = (--pageIndex - 1) * pageSize;
            }

            var pageOfItems = source.Skip(itemIndex).Take(pageSize);
            var data = await pageOfItems.ToListAsync();
            return new PagedList<TModel>(data, pageIndex, pageSize, totalItemCount);
        }

        public static async Task DeleteMoreAsync<T>(this IRepository<T> repository, params string[] id)
            where T : BaseEntity, new()
        {
            var ids = id
                .Select(x => ObjectId.TryParse(x, out var oid) ? oid : (ObjectId?) null)
                .Where(x => x.HasValue)
                .Select(x => x.Value);
            await repository.DeleteAsync(x => ids.Contains(x.Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalItem">总数</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize,
            int totalItem)
        {
            return new PagedList<T>(source, pageIndex, pageSize, totalItem);
        }


        /// <summary>
        /// 获取img标签图片地址
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static List<string> GetElementsImageUrls(this IHtmlCollection<IElement> elements)
        {
            return elements.Select(x => x.Attributes["src"])
                .Where(x => x != null)
                .Select(x => x.Value)
                .Where(x => x.Any())
                .ToList();
        }
    }
}