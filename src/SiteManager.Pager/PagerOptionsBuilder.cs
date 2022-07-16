using Microsoft.AspNetCore.Routing;

namespace SiteManager.Pager
{
    /// <summary>
    /// 表示用于构建PagerOptions对象的辅助对象。
    /// </summary>
    public class PagerOptionsBuilder
    {
        private readonly PagerOptions _pagerOptions;
        
        public PagerOptionsBuilder(PagerOptions pagerOptions)
        {
            _pagerOptions = pagerOptions;
        }

        /// <summary>
        /// 设置 PagerOptions 的 Action 属性
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetAction(string actionName)
        {
            _pagerOptions.Action = actionName;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 Controller 属性
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetController(string controllerName)
        {
            _pagerOptions.Controller = controllerName;
            return this;
        }


        /// <summary>
        /// 设置 PagerOptions 的 Area 属性
        /// </summary>
        /// <param name="areaName"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetArea(string areaName)
        {
            _pagerOptions.Area = areaName;
            return this;
        }

        /// <summary>
        /// 向 PagerOptions 的 HtmlAttributes 属性添加项。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public PagerOptionsBuilder AddHtmlAttribute(string key, object value)
        {
            if (_pagerOptions.HtmlAttributes == null)
            {
                _pagerOptions.HtmlAttributes=new Dictionary<string, object>();
            }
            _pagerOptions.HtmlAttributes[key] = value;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 OnPageIndexError 属性
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetOnPageIndexError(string handler)
        {
            _pagerOptions.OnPageIndexError = handler;
            return this;
        }

        /// <summary>
        /// 向 PagerOptions 的 RouteValues 属性添加项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public PagerOptionsBuilder AddRouteValue(string key, object value)
        {
            if (_pagerOptions.RouteValues == null)
            {
                _pagerOptions.RouteValues=new RouteValueDictionary();
            }
            _pagerOptions.RouteValues[key] = value;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 Route 属性
        /// </summary>
        /// <param name="routeName"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetRoute(string routeName)
        {
            _pagerOptions.Route = routeName;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 FirstPageRoute 属性
        /// </summary>
        /// <param name="routeName"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetFirstPageRoute(string routeName)
        {
            _pagerOptions.FirstPageRoute = routeName;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 AutoHide 属性值为false
        /// </summary>
        /// <returns></returns>
        public PagerOptionsBuilder DisableAutoHide()
        {
            _pagerOptions.AutoHide = false;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 PageIndexOutOfRangeErrorMessage 属性
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetPageIndexOutOfRangeErrorMessage(string msg)
        {
            _pagerOptions.PageIndexOutOfRangeErrorMessage = msg;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 HorizontalAlign 属性
        /// </summary>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetHorizontalAlign(string alignment)
        {
            _pagerOptions.HorizontalAlign = alignment;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 InvalidPageIndexErrorMessage 属性
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetInvalidPageIndexErrorMessage(string msg)
        {
            _pagerOptions.InvalidPageIndexErrorMessage = msg;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 PageIndexParameterName 属性
        /// </summary>
        /// <param name="prmName"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetPageIndexParameterName(string prmName)
        {
            _pagerOptions.PageIndexParameterName = prmName;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 PageIndexBoxId 属性
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetPageIndexBoxId(string id)
        {
            _pagerOptions.PageIndexBoxId = id;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 MaximumPageIndexItems 属性
        /// </summary>
        /// <param name="itmes"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetMaximumPageIndexItems(int itmes)
        {
            _pagerOptions.MaximumPageIndexItems = itmes;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 GoToButtonId 属性
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetGoToButtonId(string id)
        {
            _pagerOptions.GoToButtonId = id;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 PageNumberFormatString 属性
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetPageNumberFormatString(string format)
        {
            _pagerOptions.PageNumberFormatString = format;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 CurrentPageNumberFormatString 属性
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetCurrentPageNumberFormatString(string format)
        {
            _pagerOptions.CurrentPageNumberFormatString = format;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 TagName 属性
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetTagName(string tagName)
        {
            _pagerOptions.TagName = tagName;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 PagerItemTemplate 属性
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetPagerItemTemplate(string template)
        {
            _pagerOptions.PagerItemTemplate = template;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 NumericPagerItemTemplate 属性
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetNumericPagerItemTemplate(string template)
        {
            _pagerOptions.NumericPagerItemTemplate = template;
            return this;
        }

        /// <summary>
        /// SetCurrentPagerItemTemplate
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetCurrentPagerItemTemplate(string template)
        {
            _pagerOptions.CurrentPagerItemTemplate = template;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 NavigationPagerItemTemplate 属性
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetNavigationPagerItemTemplate(string template)
        {
            _pagerOptions.NavigationPagerItemTemplate = template;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 MorePagerItemTemplate 属性
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetMorePagerItemTemplate(string template)
        {
            _pagerOptions.MorePagerItemTemplate = template;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 DisabledPagerItemTemplate 属性
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetDisabledPagerItemTemplate(string template)
        {
            _pagerOptions.DisabledPagerItemTemplate = template;
            return this;
        }

        /// <summary>
        /// 是否总是显示第一页和最后一页两个数字页索引按钮
        /// </summary>
        /// <returns></returns>
        public PagerOptionsBuilder AlwaysShowFirstLastPageNumber()
        {
            _pagerOptions.AlwaysShowFirstLastPageNumber = true;
            return this;
        }

        /// <summary>
        /// 设置 <see cref="PagerOptions.NumericPagerItemCount"/> 属性
        /// </summary>
        /// <param name="itemCount"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetNumericPagerItemCount(int itemCount)
        {
            _pagerOptions.NumericPagerItemCount = itemCount;
            return this;
        }

        /// <summary>
        /// 设置 <see cref="PagerOptions.ShowPrevNext"/> 为 <value>false</value>
        /// </summary>
        /// <returns></returns>
        public PagerOptionsBuilder HidePrevNext()
        {
            _pagerOptions.ShowPrevNext = false;
            return this;
        }

        /// <summary>
        /// 设置 <see cref="PagerOptions.PrevPageText"/> 属性
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetPrevPageText(string text)
        {
            _pagerOptions.PrevPageText = text;
            return this;
        }

        /// <summary>
        /// 设置 <see cref="PagerOptions.NextPageText"/> 属性
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetNextPageText(string text)
        {
            _pagerOptions.NextPageText = text;
            return this;
        }

        /// <summary>
        /// 设置 <see cref="PagerOptions.ShowNumericPagerItems"/> 属性
        /// </summary>
        /// <returns></returns>
        public PagerOptionsBuilder HideNumericPagerItems()
        {
            _pagerOptions.ShowNumericPagerItems = false;
            return this;
        }

        /// <summary>
        /// 设置 <see cref="PagerOptions.ShowFirstLast"/> 为 <value>false</value>
        /// </summary>
        /// <returns></returns>
        public PagerOptionsBuilder HideFirstLast()
        {
            _pagerOptions.ShowFirstLast = false;
            return this;
        }

        /// <summary>
        /// 设置 <see cref="PagerOptions.FirstPageText"/> 属性
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetFirstPageText(string text)
        {
            _pagerOptions.FirstPageText = text;
            return this;
        }

        /// <summary>
        /// 设置 <see cref="PagerOptions.LastPageText"/> 属性
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetLastPageText(string text)
        {
            _pagerOptions.LastPageText = text;
            return this;
        }

        /// <summary>
        /// 设置 <see cref="PagerOptions.ShowMorePagerItems"/> 属性
        /// </summary>
        /// <returns></returns>
        public PagerOptionsBuilder HideMorePagerItems()
        {
            _pagerOptions.ShowMorePagerItems = false;
            return this;
        }

        /// <summary>
        /// 设置 <see cref="PagerOptions.MorePageText"/> 属性
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetMorePageText(string text)
        {
            _pagerOptions.MorePageText = text;
            return this;
        }

        /// <summary>
        /// 设置 <see cref="PagerOptions.Id"/> 属性
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetId(string id)
        {
            _pagerOptions.Id = id;
            return this;
        }

        /// <summary>
        /// 设置 <see cref="PagerOptions.CssClass"/> 属性
        /// </summary>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetCssClass(string cssClass)
        {
            _pagerOptions.CssClass = cssClass;
            return this;
        }

        /// <summary>
        /// 设置 <see cref="PagerOptions.ShowDisabledPagerItems"/> 为 <value>false</value>
        /// </summary>
        /// <returns></returns>
        public PagerOptionsBuilder HideDisabledPagerItems()
        {
            _pagerOptions.ShowDisabledPagerItems = false;
            return this;
        }

        /// <summary>
        /// 设置 <see cref="PagerOptions.MaximumPageNumber"/> 属性
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetMaximumPageNumber(int number)
        {
            _pagerOptions.MaximumPageNumber = number;
            return this;
        }

        /// <summary>
        /// 设置 <see cref="PagerOptions.NavigationPagerItemsPosition"/> 属性
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetNavigationPagerItemsPosition(PagerItemsPosition position)
        {
            _pagerOptions.NavigationPagerItemsPosition = position;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 PagerItemCssClass 属性
        /// </summary>
        /// <param name="cssClass">要应用的css类名</param>
        /// <returns></returns>
        public PagerOptionsBuilder SetPagerItemCssClass(string cssClass)
        {
            _pagerOptions.PagerItemCssClass = cssClass;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 RouteValues 属性
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetRouteValues(RouteValueDictionary values)
        {
            _pagerOptions.RouteValues = values;
            return this;
        }

        /// <summary>
        /// 设置 PagerOptions 的 HtmlAttributes 属性
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public PagerOptionsBuilder SetHtmlAttributes(IDictionary<string, object> attributes)
        {
            _pagerOptions.HtmlAttributes = attributes;
            return this;
        }
    }
}
