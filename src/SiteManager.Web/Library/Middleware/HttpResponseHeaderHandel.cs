using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SiteManager.Web.Library.Middleware;

public class HttpResponseHeaderHandel
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="next"></param>
    public HttpResponseHeaderHandel(RequestDelegate next)
    {
        _next = next;
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task Invoke(HttpContext httpContext)
    {
        /*
         * from https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Access-Control-Expose-Headers
         * 列出哪些 http response headers 对外暴露
         * 这里设置为所有
         */
        httpContext.Response.Headers.Add("Access-Control-Expose-Headers","*");
        await _next.Invoke(httpContext);
    }
}