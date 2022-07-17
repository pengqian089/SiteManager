using System.IO.Compression;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SiteManager.Infrastructure;
using SiteManager.Web.Library.Middleware;

const string corsScheme = "Open-SiteManager";



var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
DbTools.DefaultOption = configuration.GetSection("DbOption").Get<DbOption>();

#region services

var services = builder.Services;
services.AddMvc();

//压缩
services.AddResponseCompression(options =>
{
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
    {
        "application/font-woff2",
        "image/svg+xml",
        "text/plain",
        "application/lrc"
    });
    options.EnableForHttps = true;
});
services.Configure<BrotliCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; });
services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; });

//跨域
services.AddCors(options =>
{
    options.AddPolicy(corsScheme, cfg =>
    {
        cfg
            .WithOrigins(configuration.GetSection("Origins").Get<string[]>())
            .WithMethods("GET", "PUT", "POST", "DELETE", "PATCH","OPTION")
            .AllowAnyHeader();
    });
});
#endregion

#region configuration

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//启用压缩
app.UseResponseCompression();
//启用静态文件
app.UseStaticFiles();

//启用跨域配置
app.UseCors(corsScheme);

//开启路由 及自定义中间件
app.UseRouting().UseMiddleware<HttpResponseHeaderHandel>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
});

app.Init();

app.Run();
#endregion

public partial class Program
{
    /// <summary>
    /// 身份认证Cookie名称
    /// </summary>
    public const string AuthorizeCookieName = "SiteManager.Authoriza";
}