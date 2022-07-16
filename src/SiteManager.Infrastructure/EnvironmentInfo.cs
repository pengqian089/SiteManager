namespace SiteManager.Infrastructure
{
    public class EnvironmentInfo
    {
        /// <summary>
        /// 运行环境的内容路径
        /// </summary>
        public static string ContentRootPath { get; set; }
        
        /// <summary>
        /// Web根目录
        /// </summary>
        public static string WebRootPath { get; set; }

        /// <summary>
        /// 运行环境名
        /// </summary>
        public static string EnvironmentName { get; set; }

        /// <summary>
        /// 默认UserAgent
        /// </summary>
        public const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.5005.63 Safari/537.36 Edg/102.0.1245.30";
    }
}