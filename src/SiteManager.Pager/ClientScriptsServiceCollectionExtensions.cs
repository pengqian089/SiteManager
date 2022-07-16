using Microsoft.Extensions.DependencyInjection;

namespace SiteManager.Pager
{
    public static class ClientScriptsServiceCollectionExtensions
    {
        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="services"></param>
        public static void UseMvcCorePagerScripts(this IServiceCollection services)
        {
            services.ConfigureOptions(typeof(ClientScriptsConfigurationOptions));
        }
    }
}
