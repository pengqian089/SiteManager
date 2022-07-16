using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace SiteManager.Pager
{
    public partial class MvcCorePagerTagHelper
    {
        /// <summary>
        /// 第一页使用的路由名称（路由定义中应不包含页索引参数，或页索引参数默认值为UrlParameter.Optional）
        /// </summary>
        public string FirstPageRouteName
        {
            get => Options.FirstPageRoute;
            set => Options.FirstPageRoute = value;
        }
        /// <summary>
        /// 指定总页数只有一页时，MvcCorePager是否自动隐藏
        /// </summary>
        public bool AutoHide
        {
            get => Options.AutoHide;
            set => Options.AutoHide = value;
        }
        /// <summary>
        /// 页索引超出范围时显示的错误消息
        /// </summary>
        public string PageIndexOutOfRangeErrorMessage
        {
            get => Options.PageIndexOutOfRangeErrorMessage;
            set => Options.PageIndexOutOfRangeErrorMessage = value;
        }
        /// <summary>
        /// 页索引无效（不是有效的数值，如字符串或其它字符）时显示的错误消息
        /// </summary>
        public string InvalidPageIndexErrorMessage
        {
            get => Options.InvalidPageIndexErrorMessage;
            set => Options.InvalidPageIndexErrorMessage = value;
        }
        /// <summary>
        /// 路由Url中页索引参数的名称
        /// </summary>
        public string PageIndexParameterName
        {
            get => Options.PageIndexParameterName;
            set => Options.PageIndexParameterName = value;
        }
        /// <summary>
        /// 数字页索引格式化字符串
        /// </summary>
        public string PageNumberFormatString
        {
            get => Options.PageNumberFormatString;
            set => Options.PageNumberFormatString = value;
        }
        /// <summary>
        /// 当前页索引格式字符串
        /// </summary>
        public string CurrentPageNumberFormatString
        {
            get => Options.CurrentPageNumberFormatString;
            set => Options.CurrentPageNumberFormatString = value;
        }
        /// <summary>
        /// 分页控件html容器标签名，默认为div
        /// </summary>
        public string TagName
        {
            get => Options.TagName;
            set => Options.TagName = value;
        }
        /// <summary>
        /// 包含数字页、当前页及上、下、前、后分页元素的html模板
        /// </summary>
        public string PagerItemTemplate
        {
            get => Options.PagerItemTemplate;
            set => Options.PagerItemTemplate = value;
        }
        /// <summary>
        /// 数字页索引分页元素的html模板
        /// </summary>
        public string NumericPagerItemTemplate
        {
            get => Options.NumericPagerItemTemplate;
            set => Options.NumericPagerItemTemplate = value;
        }


        /// <summary>
        /// 当前页分页元素的html模板
        /// </summary>
        public string CurrentPagerItemTemplate
        {
            get => Options.CurrentPagerItemTemplate;
            set => Options.CurrentPagerItemTemplate = value;
        }


        /// <summary>
        /// 上页、下页、首页和尾首四个分页元素的html模板
        /// </summary>
        public string NavigationPagerItemTemplate
        {
            get => Options.NavigationPagerItemTemplate;
            set => Options.NavigationPagerItemTemplate = value;
        }


        /// <summary>
        /// 更多页分页元素的html模板
        /// </summary>
        public string MorePagerItemTemplate
        {
            get => Options.MorePagerItemTemplate;
            set => Options.MorePagerItemTemplate = value;
        }


        /// <summary>
        /// 已禁用的分页元素的html模板
        /// </summary>
        public string DisabledPagerItemTemplate
        {
            get => Options.DisabledPagerItemTemplate;
            set => Options.DisabledPagerItemTemplate = value;
        }

        /// <summary>
        /// 是否总是显示第一页和最后一页两个数字页索引按钮
        /// </summary>
        public bool AlwaysShowFirstLastPageNumber
        {
            get => Options.AlwaysShowFirstLastPageNumber;
            set => Options.AlwaysShowFirstLastPageNumber = value;
        }

        /// <summary>
        /// 数字页索引分页按钮数目
        /// </summary>
        public int NumericPagerItemCount
        {
            get => Options.NumericPagerItemCount;
            set => Options.NumericPagerItemCount = value;
        }


        /// <summary>
        /// 是否显示上一页和下一页分页导航按钮
        /// </summary>
        public bool ShowPrevNext
        {
            get => Options.ShowPrevNext;
            set => Options.ShowPrevNext = value;
        }


        /// <summary>
        /// 上一页按钮上显示的文本
        /// </summary>
        public string PrevPageText
        {
            get => Options.PrevPageText;
            set => Options.PrevPageText = value;
        }


        /// <summary>
        /// 下一页按钮上显示的文本
        /// </summary>
        public string NextPageText
        {
            get => Options.NextPageText;
            set => Options.NextPageText = value;
        }


        /// <summary>
        /// 是否显示数字页索引按钮及更多页按钮
        /// </summary>
        public bool ShowNumericPagerItems
        {
            get => Options.ShowNumericPagerItems;
            set => Options.ShowNumericPagerItems = value;
        }

        /// <summary>
        /// 是否显示第一页和最后一页分页导航按钮
        /// </summary>
        public bool ShowFirstLast
        {
            get => Options.ShowFirstLast;
            set => Options.ShowFirstLast = value;
        }


        /// <summary>
        /// 第一页按钮上显示的文本
        /// </summary>
        public string FirstPageText
        {
            get => Options.FirstPageText;
            set => Options.FirstPageText = value;
        }


        /// <summary>
        /// 最后一页按钮上显示的文本
        /// </summary>
        public string LastPageText
        {
            get => Options.LastPageText;
            set => Options.LastPageText = value;
        }


        /// <summary>
        /// 是否显示更多页按钮
        /// </summary>
        public bool ShowMorePagerItems
        {
            get => Options.ShowMorePagerItems;
            set => Options.ShowMorePagerItems = value;
        }


        /// <summary>
        /// 更多页按钮上显示的文本
        /// </summary>
        public string MorePageText
        {
            get => Options.MorePageText;
            set => Options.MorePageText = value;
        }


        /// <summary>
        /// 分页控件的父容器标签的ID
        /// </summary>
        public string Id
        {
            get => Options.Id;
            set => Options.Id = value;
        }


        /// <summary>
        /// 应用于分页控件的CSS样式类
        /// </summary>
        [HtmlAttributeName("class")]
        public string CssClass
        {
            get => Options.CssClass;
            set => Options.CssClass = value;
        }

        /// <summary>
        /// 是否显示已禁用的分页按钮
        /// </summary>
        public bool ShowDisabledPagerItems
        {
            get => Options.ShowDisabledPagerItems;
            set => Options.ShowDisabledPagerItems = value;
        }


        /// <summary>
        /// 限制显示的最大页数，默认值为0，即根据总记录数算出的总页数
        /// </summary>
        public int MaximumPageNumber
        {
            get => Options.MaximumPageNumber;
            set => Options.MaximumPageNumber = value;
        }

        /// <summary>
        /// 页索引输入或选择框的客户端ID
        /// </summary>
        public string PageIndexBoxId
        {
            get => Options.PageIndexBoxId;
            set => Options.PageIndexBoxId = value;
        }

        /// <summary>
        /// 页索引输入或下拉框跳转按钮的客户端ID，若不设置此属性，则在改变页索引输入或下拉框的值后立即自动跳转
        /// </summary>
        public string GoToButtonId
        {
            get => Options.GoToButtonId;
            set => Options.GoToButtonId = value;
        }

        /// <summary>
        /// 页索引下拉框中显示的最大页索引条数，该属性仅当页索引框为下拉框时有效，为文本输入框时被忽略
        /// </summary>
        public int MaximumPageIndexItems
        {
            get => Options.MaximumPageIndexItems;
            set => Options.MaximumPageIndexItems = value;
        }

        /// <summary>
        /// 首页、下页、下页和尾页四个导航按钮的位置
        /// </summary>
        public PagerItemsPosition NavigationPagerItemsPosition
        {
            get => Options.NavigationPagerItemsPosition;
            set => Options.NavigationPagerItemsPosition = value;
        }

        /// <summary>
        /// 页索引出错时要调用的Javascript函数
        /// </summary>
        public string OnPageIndexError
        {
            get => Options.OnPageIndexError;
            set => Options.OnPageIndexError = value;
        }


        /// <summary>
        /// 获取或设置分页元素（a标签）的css样式类名。
        /// </summary>
        public string PagerItemCssClass
        {
            get => Options.PagerItemCssClass;
            set => Options.PagerItemCssClass = value;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// 用于生成分页链接的控制器的Action名称
        /// </summary>
        [HtmlAttributeName(ActionAttributeName)]
        public string Action
        {
            get => Options.Action;
            set => Options.Action = value;
        }

        /// <summary>
        /// 用于生成分页链接的控制器名称
        /// </summary>
        [HtmlAttributeName(ControllerAttributeName)]
        public string Controller
        {
            get => Options.Controller;
            set => Options.Controller = value;
        }

        /// <summary>
        /// 用于生成分页链接的区域（Area）名称
        /// </summary>
        [HtmlAttributeName(AreaAttributeName)]
        public string Area
        {
            get => Options.Area;
            set => Options.Area = value;
        }

        /// <summary>
        /// 用于生成分页链接的路由名称
        /// </summary>
        [HtmlAttributeName(RouteAttributeName)]
        public string Route
        {
            get => Options.Route;
            set => Options.Route = value;
        }



        /// <summary>
        /// 用于生成分页链接的路由的值
        /// </summary>
        [HtmlAttributeName(RouteValuesDictionaryName, DictionaryAttributePrefix = RouteValuesPrefix)]
        public IDictionary<string, string> RouteValues
        {
            get
            {
                return Options.RouteValues.ToDictionary(r => r.Key, r => r.Value?.ToString());
            }
            set => Options.RouteValues = new RouteValueDictionary(value);
        }

        [HtmlAttributeName("asp-model")]
        public IPagedList Model { get; set; }

        #region Ajax settings

        [HtmlAttributeName("ajax-enabled")]
        public bool AjaxEnabled { get; set; }

        [HtmlAttributeName("ajax-update-target")]
        public string AjaxUpdateTarget
        {
            get => AjaxOptions.UpdateTargetId;
            set => AjaxOptions.UpdateTargetId = value;
        }

        [HtmlAttributeName("ajax-method")]
        public string AjaxMethod
        {
            get => AjaxOptions.HttpMethod;
            set => AjaxOptions.HttpMethod = value;
        }

        /// <summary>
        /// 获取或设置一个值，该值指示是否在Ajax分页模式下启用局部加载功能。
        /// </summary>
        [HtmlAttributeName("ajax-partial-loading")]
        public bool AjaxPartialLoading
        {
            get => AjaxOptions.EnablePartialLoading;
            set => AjaxOptions.EnablePartialLoading = value;
        }

        /// <summary>
        /// 获取或设置在Ajax分页模式下，分页时向服务器端通过Ajax提交的数据所在的Form ID，用于实现Ajax分页模式下的查询功能。
        /// </summary>
        [HtmlAttributeName("ajax-search-form")]
        public string AjaxDataFormId
        {
            get => AjaxOptions.DataFormId;
            set => AjaxOptions.DataFormId = value;
        }


        [HtmlAttributeName("ajax-allow-cache")]
        public bool AjaxAllowCache
        {
            get => AjaxOptions.AllowCache;
            set => AjaxOptions.AllowCache = value;
        }

        /// <summary>
        /// 获取或设置一个值，该值指示是否在Ajax分页模式下是否启用浏览器历史记录支持功能。
        /// </summary>
        [HtmlAttributeName("ajax-history-support")]
        public bool AjaxHistorySupport
        {
            get => AjaxOptions.EnableHistorySupport;
            set => AjaxOptions.EnableHistorySupport = value;
        }


        private PagerOptions _options;
        private MvcAjaxOptions _ajaxOptions;
        public PagerOptions Options
        {
            get
            {
                if (_options == null) { _options = new PagerOptions(); }
                return _options;
            }
            set { if (value != null) { _options = value; } }
        }

        public MvcAjaxOptions AjaxOptions
        {
            get => _ajaxOptions ?? (_ajaxOptions = new MvcAjaxOptions());
            set { if (value != null) { _ajaxOptions = value; } }
        }
        #endregion
    }
}
