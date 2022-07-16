namespace SiteManager.Pager
{
    /// <summary>
    /// 
    /// </summary>
    public class MvcAjaxOptionsBuilder
    {
        private readonly MvcAjaxOptions _ajaxOptions;

        /// <summary>
        /// 表示用于构建MvcAjaxOptions对象的辅助对象。
        /// </summary>
        /// <param name="ajaxOptions"></param>
        public MvcAjaxOptionsBuilder(MvcAjaxOptions ajaxOptions)
        {
            _ajaxOptions = ajaxOptions;
        }

        /// <summary>
        /// 设置 MvcAjaxOptions 的UpdateTargetId属性
        /// </summary>
        /// <param name="targetId"></param>
        /// <returns></returns>
        public MvcAjaxOptionsBuilder SetUpdateTargetId(string targetId)
        {
            _ajaxOptions.UpdateTargetId = targetId;
            return this;
        }

        /// <summary>
        /// 设置 MvcAjaxOptions 的HttpMethod属性
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public MvcAjaxOptionsBuilder SetHttpMethod(string method)
        {
            _ajaxOptions.HttpMethod = method;
            return this;
        }

        /// <summary>
        /// 设置 MvcAjaxOptions 的OnBegin属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MvcAjaxOptionsBuilder SetOnBegin(string name)
        {
            _ajaxOptions.OnBegin = name;
            return this;
        }

        /// <summary>
        /// 设置 MvcAjaxOptions 的OnSuccess属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MvcAjaxOptionsBuilder SetOnSuccess(string name)
        {
            _ajaxOptions.OnSuccess = name;
            return this;
        }

        /// <summary>
        /// 设置 MvcAjaxOptions 的OnComplete属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MvcAjaxOptionsBuilder SetOnComplete(string name)
        {
            _ajaxOptions.OnComplete = name;
            return this;
        }

        /// <summary>
        /// 设置 MvcAjaxOptions 的OnFailure属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MvcAjaxOptionsBuilder SetOnFailure(string name)
        {
            _ajaxOptions.OnFailure = name;
            return this;
        }

        /// <summary>
        /// 设置 MvcAjaxOptions 的LoadingElementId属性
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MvcAjaxOptionsBuilder SetLoadingElementId(string id)
        {
            _ajaxOptions.LoadingElementId = id;
            return this;
        }

        /// <summary>
        /// 设置 MvcAjaxOptions 的LoadingElementDuration属性
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public MvcAjaxOptionsBuilder SetLoadingElementDuration(int duration)
        {
            _ajaxOptions.LoadingElementDuration = duration;
            return this;
        }

        /// <summary>
        /// 设置 MvcAjaxOptions 的Confirm属性
        /// </summary>
        /// <param name="confirm"></param>
        /// <returns></returns>
        public MvcAjaxOptionsBuilder SetConfirm(string confirm)
        {
            _ajaxOptions.Confirm = confirm;
            return this;
        }

        /// <summary>
        /// 设置 MvcAjaxOptions 的EnablePartialLoading属性值为true
        /// </summary>
        /// <returns></returns>
        public MvcAjaxOptionsBuilder EnablePartialLoading()
        {
            _ajaxOptions.EnablePartialLoading = true;
            return this;
        }

        /// <summary>
        /// 设置 MvcAjaxOptions 的DataFormId属性
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MvcAjaxOptionsBuilder SetDataFormId(string id)
        {
            _ajaxOptions.DataFormId = id;
            return this;
        }

        /// <summary>
        /// 设置 MvcAjaxOptions 的AllowCache属性值为false
        /// </summary>
        /// <returns></returns>
        public MvcAjaxOptionsBuilder DisallowCache()
        {
            _ajaxOptions.AllowCache = false;
            return this;
        }

        /// <summary>
        /// 设置 MvcAjaxOptions 的EnableHistorySupport属性值为false
        /// </summary>
        /// <returns></returns>
        public MvcAjaxOptionsBuilder DisableHistorySupport()
        {
            _ajaxOptions.EnableHistorySupport = false;
            return this;
        }
    }
}
