using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SiteManager.Web.Library;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInject(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        return services;
    }

    public static IServiceCollection AddDefaultInject(this IServiceCollection services,
        IConfiguration configuration)
    {
        var injectServices = configuration.GetSection("RegisterInject").Get<List<RegisterInject>>();
        if (injectServices == null || !injectServices.Any()) return services;
        foreach (var injectService in injectServices)
        {
            var injectTypes = Assembly.Load(injectService.InterfaceAssemblyName).GetTypes()
                .Where(x => x.Namespace == injectService.InterfaceNamespace && x.IsInterface)
                .ToList();
            if (injectService.Remove != null && injectService.Remove.Any())
            {
                injectTypes = injectTypes.Where(x => !injectService.Remove.Contains(x.FullName)).ToList();
            }

            var implementAssembly = Assembly.Load(injectService.ImplementAssemblyName).GetTypes()
                .Where(x => x.Namespace == injectService.ImplementNamespace && !x.IsAbstract && !x.IsInterface)
                .ToList();
            foreach (var injectType in injectTypes)
            {
                var defaultImplementType = implementAssembly.FirstOrDefault(x => injectType.IsAssignableFrom(x));
                if (defaultImplementType != null)
                {
                    services.AddScoped(injectType, defaultImplementType);
                }
            }

            if (injectService.Add != null && injectService.Add.Any())
            {
                foreach (var addService in injectService.Add)
                {
                    var injectType = injectTypes.FirstOrDefault(x => x.FullName == addService.ServiceFullName);
                    var implementType =
                        implementAssembly.FirstOrDefault(x => x.FullName == addService.ImplementFullName);
                    if (injectType == null || implementType == null || !injectType.IsAssignableFrom(implementType))
                        continue;
                    switch (addService.Type)
                    {
                        case "Transient":
                            services.AddTransient(injectType, implementType);
                            break;
                        case "Singleton":
                            services.AddSingleton(injectType, implementType);
                            break;
                        default:
                            services.AddScoped(injectType, implementType);
                            break;
                    }
                }
            }
        }

        return services;
    }


    private class RegisterInject
    {
        /// <summary>
        /// 接口程序集名称
        /// </summary>
        public string InterfaceAssemblyName { get; set; }

        /// <summary>
        /// 实现程序集名称
        /// </summary>
        public string ImplementAssemblyName { get; set; }

        /// <summary>
        /// 接口命名空间
        /// </summary>
        public string InterfaceNamespace { get; set; }

        /// <summary>
        /// 实现命名空间
        /// </summary>
        public string ImplementNamespace { get; set; }

        /// <summary>
        /// 要移除注册的接口完全限定名
        /// </summary>
        public List<string> Remove { get; set; }

        /// <summary>
        /// 要添加的自定义注入类型
        /// </summary>
        public List<AddService> Add { get; set; }
    }

    private class AddService
    {
        /// <summary>
        /// 注入类型:Transient Scoped(default) Singleton
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 依赖注入服务接口完全限定名
        /// </summary>
        public string ServiceFullName { get; set; }

        /// <summary>
        /// 实现完全限定名
        /// </summary>
        public string ImplementFullName { get; set; }
    }
}