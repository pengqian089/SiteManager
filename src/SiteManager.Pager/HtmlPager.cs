using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace SiteManager.Pager
{
    /// <summary>
    /// 表示已Html编码的Html分页控件。
    /// </summary>
    public class HtmlPager:IHtmlContent
    {
        private readonly IHtmlHelper _htmlHelper;
        private readonly int _currentPageIndex;
        private readonly int _pageSize;
        private readonly int _totalItemCount;
        private PagerOptions _pagerOptions;

        /// <summary>
        /// 根据HtmlHelper、总记录数、每页显示记录数、当前页索引和PagerOptions创建HtmlPager。
        /// </summary>
        /// <param name="html">此方法扩展的 HTMLHelper 实例</param>
        /// <param name="totalItemCount"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pagerOptions"></param>
        public HtmlPager(IHtmlHelper html, int totalItemCount, int pageSize, int pageIndex,PagerOptions pagerOptions)
        {
            _htmlHelper = html;
            _totalItemCount = totalItemCount;
            _pageSize = pageSize;
            _currentPageIndex = pageIndex;
            _pagerOptions = pagerOptions;
        }

        /// <summary>
        /// 根据HtmlHelper、总记录数、每页显示记录数和当前页索引创建HtmlPager。
        /// </summary>
        /// <param name="html">此方法扩展的 HTMLHelper 实例</param>
        /// <param name="totalItemCount"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        public HtmlPager(IHtmlHelper html, int totalItemCount, int pageSize, int pageIndex):this(html,totalItemCount,pageSize,pageIndex,null){}

        /// <summary>
        /// 根据HtmlHelper、包含分页数据的IPagedList对象和PagerOptions创建HtmlPager。
        /// </summary>
        /// <param name="html">此方法扩展的 HTMLHelper 实例</param>
        /// <param name="pagedList"></param>
        /// <param name="pagerOptions"></param>
        public HtmlPager(IHtmlHelper html, IPagedList pagedList,PagerOptions pagerOptions) : this(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex,pagerOptions) { }

        /// <summary>
        /// 根据HtmlHelper和包含分页数据的IPagedList对象创建HtmlPager。
        /// </summary>
        /// <param name="html">此方法扩展的 HTMLHelper 实例</param>
        /// <param name="pagedList"></param>
        public HtmlPager(IHtmlHelper html, IPagedList pagedList):this(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex){}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public HtmlPager Options(Action<PagerOptionsBuilder> builder)
        {
            if (_pagerOptions == null)
            {
                _pagerOptions = new PagerOptions();
            }
            builder(new PagerOptionsBuilder(_pagerOptions));
            return this;
        }

        
        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }
            var totalPageCount = (int)Math.Ceiling(_totalItemCount / (double)_pageSize);
            var urlHelperFactory = _htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
            var urlHelper = urlHelperFactory.GetUrlHelper(_htmlHelper.ViewContext);
            var pager = new PagerBuilder(_htmlHelper.ViewContext, urlHelper, totalPageCount, _currentPageIndex, _pagerOptions);
            writer.Write(pager.GenerateHtml());
        }
    }

}
