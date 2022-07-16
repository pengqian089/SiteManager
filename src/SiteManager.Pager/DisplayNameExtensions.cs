using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SiteManager.Pager
{
    /// <summary>
    /// 提供一种从IPagedList&lt;T&gt; 和 PagedList&lt;T&gt;模型中获取显示名称的机制.
    /// </summary>
    public static class DisplayNameExtensions
    {
        /// <summary>
        /// 获取 PagedList&lt;T&gt; 泛型模型的显示名称;
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string DisplayNameFor<TModel, TValue>(this IHtmlHelper<PagedList<TModel>> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.DisplayNameForInnerType<TModel, TValue>(expression);
        }
    }
}
