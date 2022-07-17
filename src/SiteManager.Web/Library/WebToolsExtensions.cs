using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using SiteManager.ViewModel;

namespace SiteManager.Web.Library;

public static class WebToolsExtensions
{
    /// <summary>
    /// 获取当前用户信息
    /// 未授权用户返回 null 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static async Task<VmUserInfo> GetUserInfo(this HttpContext context)
    {
        var auth = await context.AuthenticateAsync(Program.AuthorizeCookieName);
        if (auth.Succeeded && auth.Principal?.IsLogin() == true)
        {
            return auth.Principal.GetIdentity();
        }

        return null;
    }
    
    /// <summary>
    /// 根据Identity反射获取当前用户信息
    /// 未授权用户返回 null
    /// </summary>
    /// <param name="principal"></param>
    /// <returns></returns>
    public static VmUserInfo GetIdentity(this ClaimsPrincipal principal)
    {
        if (principal.IsLogin())
        {
            var userInfo = new VmUserInfo();
            foreach (var claims in principal.Claims)
            {
                var property = typeof(VmUserInfo).GetProperty(claims.Type);
                if (property == null) continue;
                if (property.PropertyType == typeof(DateTime?))
                {
                    property.SetValue(userInfo, DateTime.Parse(claims.Value));
                }
                else if (property.PropertyType == typeof(bool?))
                {
                    bool.TryParse(claims.Value, out var result);
                    property.SetValue(userInfo, result);
                }
                else
                {
                    typeof(VmUserInfo).GetProperty(claims.Type)?.SetValue(userInfo, claims.Value);
                }
            }

            return userInfo;
        }

        return null;
    }
    
    public static bool IsLogin(this ClaimsPrincipal principal)
    {
        return principal?.Identity?.IsAuthenticated == true;
    }
}