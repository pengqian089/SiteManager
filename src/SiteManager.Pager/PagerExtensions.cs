using Microsoft.AspNetCore.Mvc.Rendering;

namespace SiteManager.Pager
{
    /// <summary>
    /// 表示在应用程序中支持 HtmlPager 和 AjaxPager。 
    /// </summary>
    public static class PagerExtensions
    {
        #region Html Pager

        /// <summary>
        /// 使用要分页的总记录数、每页显示记录数、当前页索引和PagerOptions设置构建HtmlPager
        /// </summary>
        /// <param name="helper">HTMLHelper 实例</param>
        /// <param name="totalItemCount">要分页的总记录数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pagerOptions">选项</param>
        /// <returns>HtmlPager对象</returns>
        public static HtmlPager Pager(this IHtmlHelper helper, int totalItemCount, int pageSize, int pageIndex, PagerOptions pagerOptions)
        {
            return new HtmlPager
                (
                    helper,
                    totalItemCount,pageSize,
                    pageIndex,
                    pagerOptions
                );
        }

        /// <summary>
        /// 使用要分页的总记录数、每页显示记录数、当前页索引构建HtmlPager
        /// </summary>
        /// <param name="helper">HTMLHelper 实例</param>
        /// <param name="totalItemCount">要分页的总记录数</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <returns>HtmlPager对象</returns>
        public static HtmlPager Pager(this IHtmlHelper helper, int totalItemCount, int pageSize, int pageIndex)
        {
            return new HtmlPager
                (
                    helper,
                    totalItemCount, pageSize,
                    pageIndex
                );
        }

        /// <summary>
        /// 使用包含分页数据的IPagedList对象构建HtmlPager
        /// </summary>
        /// <param name="helper">HTMLHelper 实例</param>
        /// <param name="pagedList">包含要分页数据的IPagedList对象</param>
        /// <returns>HtmlPager对象</returns>
        public static HtmlPager Pager(this IHtmlHelper helper, IPagedList pagedList)
        {
            if (pagedList == null)
            {
                throw new ArgumentNullException("pagedList");
            }
            return new HtmlPager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, null);
        }

        /// <summary>
        /// 使用包含分页数据的IPagedList对象和PagerOptions设置构建HtmlPager
        /// </summary>
        /// <param name="helper">HTMLHelper 实例</param>
        /// <param name="pagedList">包含要分页数据的IPagedList对象</param>
        /// <param name="pagerOptions">选项</param>
        /// <returns>HtmlPager对象</returns>
        public static HtmlPager Pager(this IHtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions)
        {
            if (pagedList == null)
            {
                throw new ArgumentNullException(nameof(pagedList));
            }
            return Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, pagerOptions);
        }


        #endregion

        #region Ajax Pager

        /// <summary>
        /// 根据总记录数、每页显示记录数、当前页索引、PagerOptions设置项和MvcAjaxOptions设置项构建AjaxPager。
        /// </summary>
        /// <param name="helper">AjaxHelper 实例</param>
        /// <param name="totalItemCount">总记录数</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pagerOptions">选项</param>
        /// <param name="ajaxOptions">异步请求选项</param>
        /// <returns>AjaxPager</returns>
        public static AjaxPager AjaxPager(this IHtmlHelper helper, int totalItemCount, int pageSize, int pageIndex, PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions)
        {
            return new AjaxPager(helper, totalItemCount, pageSize, pageIndex, pagerOptions, ajaxOptions);
        }

        /// <summary>
        /// 根据包含分页数据的IPagedList对象构建AjaxPager。
        /// </summary>
        /// <param name="helper">AjaxHelper 实例</param>
        /// <param name="pagedList">包含分页数据的IPagedList对象</param>
        /// <returns>AjaxPager</returns>
        public static AjaxPager AjaxPager(this IHtmlHelper helper, IPagedList pagedList)
        {
            return new AjaxPager(helper, pagedList, null,null);
        }

        /// <summary>
        /// 根据包含分页数据的IPagedList对象和PagerOptions属性构建AjaxPager。
        /// </summary>
        /// <param name="helper">AjaxHelper 实例</param>
        /// <param name="pagedList">包含分页数据的IPagedList对象</param>
        /// <param name="pagerOptions">选</param>
        /// <returns>AjaxPager</returns>
        public static AjaxPager AjaxPager(this IHtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions)
        {
            return AjaxPager(helper, pagedList, pagerOptions, null);
        }

        /// <summary>
        /// 根据包含分页数据的IPagedList对象、PagerOptions和MvcAjaxOptions构建AjaxPager。
        /// </summary>
        /// <param name="helper">AjaxHelper 实例</param>
        /// <param name="pagedList">包含分页数据的IPagedList对象</param>
        /// <param name="pagerOptions">选项</param>
        /// <param name="ajaxOptions">异步请求选项</param>
        /// <returns>AjaxPager</returns>
        public static AjaxPager AjaxPager(this IHtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions)
        {
            if (pagedList == null)
            {
                throw new ArgumentNullException(nameof(pagedList));
            }
            return AjaxPager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex,pagerOptions, ajaxOptions);
        }

        #endregion
    }
}