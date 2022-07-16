using Microsoft.AspNetCore.Routing;

namespace SiteManager.Pager
{
    /// <summary>
    /// 分页控件相关选项和设置的对象。
    /// </summary>
    public class PagerOptions
    {
        /// <summary>
        /// 用默认属性值初始化PagerOptions对象。
        /// </summary>
        public PagerOptions()
        {
            AutoHide = true;
            PageIndexParameterName = "pageindex";
            NumericPagerItemCount = 10;
            AlwaysShowFirstLastPageNumber = false;
            ShowPrevNext = true;
            PrevPageText = "&lt;"; 
            NextPageText = "&gt;";
            ShowNumericPagerItems = true;
            ShowFirstLast = true;
            FirstPageText = "&lt;&lt;";
            LastPageText = "&gt;&gt;";
            ShowMorePagerItems = true;
            MorePageText = "...";
            ShowDisabledPagerItems = true;
            MaximumPageIndexItems = 20;
            TagName = "div";
            InvalidPageIndexErrorMessage = "Invalid page index";// MvcCorePagerResources.InvalidPageIndexErrorMessage;
            PageIndexOutOfRangeErrorMessage = "Page index out of range"; //MvcCorePagerResources.PageIndexOutOfRangeErrorMessage;
            MaximumPageNumber = 0;
            FirstPageRoute = null;
        }

        /// <summary>
        /// 生成分页链接的控制器的Action名称
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 生成分页链接的控制器名称
        /// </summary>
        public string Controller { get; set; }


        /// <summary>
        /// 生成分页链接的区域（Area）名称
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 生成分页链接的路由名称
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// 生成分页链接的路由的值
        /// </summary>
        public RouteValueDictionary RouteValues { get; set; }

        /// <summary>
        /// 要应用到分页容器元素上的Html属性
        /// </summary>
        public IDictionary<string,object> HtmlAttributes { get; set; }

        /// <summary>
        /// 第一页使用的路由名称（路由定义中应不包含页索引参数，或页索引参数默认值为UrlParameter.Optional）
        /// </summary>
        public string FirstPageRoute { get; set; }

        /// <summary>
        /// 指定总页数只有一页时，MvcCorePager是否自动隐藏
        /// </summary>
        public bool AutoHide { get; set; }

        /// <summary>
        /// 页索引超出范围时显示的错误消息
        /// </summary>
        public string PageIndexOutOfRangeErrorMessage { get; set; }

        /// <summary>
        /// 页索引无效（不是有效的数值，如字符串或其它字符）时显示的错误消息
        /// </summary>
        public string InvalidPageIndexErrorMessage { get; set; }

        /// <summary>
        /// 路由Url中页索引参数的名称
        /// </summary>
        public string PageIndexParameterName { get; set; }

        /// <summary>
        /// 页索引输入或选择框的客户端ID
        /// </summary>
        public string PageIndexBoxId { get; set; }

        /// <summary>
        /// 页索引输入或下拉框跳转按钮的客户端ID，若不设置此属性，则在改变页索引输入或下拉框的值后立即自动跳转
        /// </summary>
        public string GoToButtonId { get; set; }

        /// <summary>
        /// 页索引下拉框中显示的最大页索引条数，该属性仅当页索引框为下拉框时有效，为文本输入框时被忽略
        /// </summary>
        public int MaximumPageIndexItems { get; set; }

        /// <summary>
        /// 数字页索引格式化字符串
        /// </summary>
        public string PageNumberFormatString { get; set; }

        /// <summary>
        /// 当前页索引格式字符串
        /// </summary>
        public string CurrentPageNumberFormatString { get; set; }

        private string _tagName;
        /// <summary>
        /// 分页控件html容器标签名，默认为div
        /// </summary>
        public string TagName
        {
            get
            {
                return _tagName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new System.ArgumentNullException(nameof(TagName));// MvcCorePagerResources.TagNameCannotBeNull);
                _tagName = value;
            }
        }


        /// <summary>
        /// 包含数字页、当前页及上、下、前、后分页元素的html模板
        /// </summary>
        public string PagerItemTemplate { get; set; }


        /// <summary>
        /// 数字页索引分页元素的html模板
        /// </summary>
        public string NumericPagerItemTemplate { get; set; }


        /// <summary>
        /// 当前页分页元素的html模板
        /// </summary>
        public string CurrentPagerItemTemplate { get; set; }


        /// <summary>
        /// 上页、下页、首页和尾首四个分页元素的html模板
        /// </summary>
        public string NavigationPagerItemTemplate { get; set; }


        /// <summary>
        /// 更多页分页元素的html模板
        /// </summary>
        public string MorePagerItemTemplate { get; set; }


        /// <summary>
        /// 已禁用的分页元素的html模板
        /// </summary>
        public string DisabledPagerItemTemplate { get; set; }


        /// <summary>
        /// 是否总是显示第一页和最后一页两个数字页索引按钮
        /// </summary>
        public bool AlwaysShowFirstLastPageNumber { get; set; }

        /// <summary>
        /// 数字页索引分页按钮数目
        /// </summary>
        public int NumericPagerItemCount { get; set; }


        /// <summary>
        /// 是否显示上一页和下一页分页导航按钮
        /// </summary>
        public bool ShowPrevNext { get; set; }


        /// <summary>
        /// 上一页按钮上显示的文本
        /// </summary>
        public string PrevPageText { get; set; }


        /// <summary>
        /// 下一页按钮上显示的文本
        /// </summary>
        public string NextPageText { get; set; }


        /// <summary>
        /// 是否显示数字页索引按钮及更多页按钮
        /// </summary>
        public bool ShowNumericPagerItems { get; set; } = true;

        /// <summary>
        /// 是否显示第一页和最后一页分页导航按钮
        /// </summary>
        public bool ShowFirstLast { get; set; } = true;


        /// <summary>
        /// 第一页按钮上显示的文本
        /// </summary>
        public string FirstPageText { get; set; }


        /// <summary>
        /// 最后一页按钮上显示的文本
        /// </summary>
        public string LastPageText { get; set; }


        /// <summary>
        /// 是否显示更多页按钮
        /// </summary>
        public bool ShowMorePagerItems { get; set; }


        /// <summary>
        /// 更多页按钮上显示的文本
        /// </summary>
        public string MorePageText { get; set; }


        /// <summary>
        /// 分页控件的父容器标签的ID
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// 分页控件水平对齐方式
        /// </summary>
        public string HorizontalAlign { get; set; }


        /// <summary>
        /// 应用于分页控件的CSS样式类
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// 是否显示已禁用的分页按钮
        /// </summary>
        public bool ShowDisabledPagerItems { get; set; }


        /// <summary>
        /// 限制显示的最大页数，默认值为0，即根据总记录数算出的总页数
        /// </summary>
        public int MaximumPageNumber { get; set; }


        /// <summary>
        /// 首页、下页、下页和尾页四个导航按钮的位置
        /// </summary>
        public PagerItemsPosition NavigationPagerItemsPosition { get; set; } = PagerItemsPosition.BothSide;

        /// <summary>
        /// 页索引出错时要调用的Javascript函数
        /// </summary>
        public string OnPageIndexError { get; set; }

        /// <summary>
        /// 获取或设置分页元素（a标签）的css样式类名。
        /// </summary>
        public string PagerItemCssClass { get; set; }
    }
}