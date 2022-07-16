namespace SiteManager.Pager
{
    /// <summary>
    /// 表示用于 MvcCorePager 在 Ajax 分页模式下的选项设置，该类继承自 AjaxOptions。
    /// </summary>
    public class MvcAjaxOptions
    {
        /// <summary>
        /// 获取或设置一个值，该值指示是否在Ajax分页模式下启用局部加载功能。
        /// </summary>
        public bool EnablePartialLoading { get; set; }

        /// <summary>
        /// 获取或设置在Ajax分页模式下，分页时向服务器端通过Ajax提交的数据所在的Form ID，用于实现Ajax分页模式下的查询功能。
        /// </summary>
        public string DataFormId { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否在Ajax分页模式下启用缓存。
        /// </summary>
        public bool AllowCache { get; set; } = true;


        /// <summary>
        /// 获取或设置一个值，该值指示是否在Ajax分页模式下是否启用浏览器历史记录支持功能。
        /// </summary>
        public bool EnableHistorySupport { get; set; } = true;

        public string Confirm { get; set; }

        public string HttpMethod { get; set; }

        public int LoadingElementDuration { get; set; }

        public string LoadingElementId { get; set; }

        public string OnBegin { get; set; } = string.Empty;

        public string OnComplete { get; set; } = string.Empty;

        public string OnFailure { get; set; } = string.Empty;

        public string OnSuccess { get; set; } = string.Empty;

        public string UpdateTargetId { get; set; }

        public IDictionary<string, object> ToUnobtrusiveHtmlAttributes()
        {
            var result = new Dictionary<string, object>
            {
                { "data-ajax", "true" },
            };

            PagerBuilder.AddToDictionaryIfNotEmpty(result, "data-ajax-method", HttpMethod);
            PagerBuilder.AddToDictionaryIfNotEmpty(result, "data-ajax-confirm", Confirm);
            PagerBuilder.AddToDictionaryIfNotEmpty(result, "data-ajax-begin", OnBegin);
            PagerBuilder.AddToDictionaryIfNotEmpty(result, "data-ajax-complete", OnComplete);
            PagerBuilder.AddToDictionaryIfNotEmpty(result, "data-ajax-failure", OnFailure);
            PagerBuilder.AddToDictionaryIfNotEmpty(result, "data-ajax-success", OnSuccess);

            if (!string.IsNullOrWhiteSpace(DataFormId))
            {
                result.Add("data-ajax-search-form", PagerBuilder.EscapeIdSelector(DataFormId));
            }
            if (!String.IsNullOrWhiteSpace(LoadingElementId))
            {
                result.Add("data-ajax-loading", PagerBuilder.EscapeIdSelector(LoadingElementId.Trim('#')));

                if (LoadingElementDuration > 0)
                {
                    result.Add("data-ajax-loading-duration", LoadingElementDuration);
                }
            }
            if (!String.IsNullOrWhiteSpace(UpdateTargetId))
            {
                result.Add("data-ajax-update", PagerBuilder.EscapeIdSelector(UpdateTargetId.Trim('#')));
            }
            if (EnablePartialLoading)
            {
                result.Add("data-ajax-partial-loading", "true");
            }
            if (!AllowCache)
            {
                result.Add("data-ajax-cache", "false");
            }
            if (!EnableHistorySupport)
                result.Add("data-ajax-history", "false");

            return result;
        }
        

    }
}
