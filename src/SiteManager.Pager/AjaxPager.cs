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
    public class AjaxPager : IHtmlContent
    {
        private readonly IHtmlHelper _htmlHelper;
        private readonly int _currentPageIndex;
        private readonly int _pageSize;
        private readonly int _totalItemCount;
        private PagerOptions _pagerOptions;
        private MvcAjaxOptions _ajaxOptions;

        /// <summary>
        /// 根据AjaxHelper、总记录数、每页显示记录数、当前页索引、PagerOptions和MvcAjaxOptions属性值生成创建AjaxPager对象。
        /// </summary>
        /// <param name="html">此方法扩展的 AjaxHelper 实例。</param>
        /// <param name="totalItemCount">要分页的总记录数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pagerOptions">包含分页控件设置的PagerOptions对象。</param>
        /// <param name="ajaxOptions">包含Ajax设置的MvcAjaxOptions对象。</param>
        public AjaxPager(IHtmlHelper html, int totalItemCount, int pageSize, int pageIndex,PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions)
        {
            _htmlHelper = html;
            _totalItemCount = totalItemCount;
            _pageSize = pageSize;
            _currentPageIndex = pageIndex;
            _pagerOptions = pagerOptions;
            _ajaxOptions = ajaxOptions;
        }

        /// <summary>
        /// 根据AjaxHelper、包含分页数据的IPagedList对象、PagerOptions和MvcAjaxOptions属性值生成创建AjaxPager对象。
        /// </summary>
        /// <param name="ajax">此方法扩展的 AjaxHelper 实例。</param>
        /// <param name="pagedList"></param>
        /// <param name="pagerOptions"></param>
        /// <param name="ajaxOptions"></param>
        public AjaxPager(IHtmlHelper ajax, IPagedList pagedList,PagerOptions pagerOptions, MvcAjaxOptions ajaxOptions)
            : this(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex,pagerOptions, ajaxOptions) { }

        /// <summary>
        /// 用于构建PagerOptions对象的方法。
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public AjaxPager Options(Action<PagerOptionsBuilder> builder)
        {
            if (_pagerOptions == null)
            {
                _pagerOptions = new PagerOptions();
            }
            builder(new PagerOptionsBuilder(_pagerOptions));
            return this;
        }

        /// <summary>
        /// 用于构建MvcAjaxOptions对象的方法。
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public AjaxPager AjaxOptions(Action<MvcAjaxOptionsBuilder> builder)
        {
            if (_ajaxOptions == null)
            {
                _ajaxOptions = new MvcAjaxOptions();
            }
            builder(new MvcAjaxOptionsBuilder(_ajaxOptions));
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
            writer.Write(new PagerBuilder(_htmlHelper.ViewContext, urlHelper,totalPageCount, _currentPageIndex, _pagerOptions,_ajaxOptions).GenerateHtml());
        }
    }
}
