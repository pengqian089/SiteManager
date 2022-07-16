using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SiteManager.Infrastructure
{
    public static class DpzAppBuilderExtensions
    {
        public static IApplicationBuilder Init(this IApplicationBuilder app)
        {
            var env = app.GetHostingEnvironment();
            var appBuilder = app.HandlerOtherStatusCode(env);
            return appBuilder;
        }


        /// <summary>
        /// 处理非200状态码
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IApplicationBuilder HandlerOtherStatusCode(this IApplicationBuilder app, IHostingEnvironment env)
        {
            var appBuilder = app.UseStatusCodePages(async x =>
            {
                if (x.HttpContext.Response.StatusCode == 404)
                {
                    x.HttpContext.Response.ContentType = "text/html";
                    await x.HttpContext.Response.SendFileAsync(
                        Path.Combine(env.WebRootPath,"NotFound.html"));
                }
                else
                {
                    x.HttpContext.Response.ContentType = "text/plain";

                    await x.HttpContext.Response.WriteAsync(
                        "Status code page, status code: " +
                        x.HttpContext.Response.StatusCode);
                }
            });
            return appBuilder;
        }


        /// <summary>
        /// 获取有关运行应用程序的Web托管环境的信息。
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IHostingEnvironment GetHostingEnvironment(this IApplicationBuilder app)
        {
            if (app.ApplicationServices.GetService(typeof(IHostingEnvironment)) is IHostingEnvironment env)
            {
                EnvironmentInfo.ContentRootPath = env.ContentRootPath;
                EnvironmentInfo.EnvironmentName = env.EnvironmentName;
                EnvironmentInfo.WebRootPath = env.WebRootPath;
                return env;
            }

            return null;
        }

        /// <summary>
        /// 根据ObjectId快速查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T Find<T>(this IMongoCollection<T> collection, ObjectId id) where T : IBaseEntity
        {
            var filter = Builders<T>.Filter.Eq(new StringFieldDefinition<T, ObjectId>("Id"), id);
            return collection.Find(filter).SingleOrDefault();
        }

        /// <summary>
        /// 异步 根据ObjectId快速查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<T> FindAsync<T>(this IMongoCollection<T> collection, ObjectId id) where T : IBaseEntity
        {
            var filter = Builders<T>.Filter.Eq(new StringFieldDefinition<T, ObjectId>("Id"), id);
            return await collection.Find(filter).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 生成MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GenerateMd5(this string str)
        {
            var md5Provider = new MD5CryptoServiceProvider();
            var hash = md5Provider.ComputeHash(Encoding.Default.GetBytes(str));
            var md5 = BitConverter.ToString(hash).Replace("-", "");
            return md5;
        }

        /// <summary>
        /// 生成MD5 小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GenerateHashMd5(this string str)
        {
            using var md5 = MD5.Create();
            var result = string.Join("",
                from x in md5.ComputeHash(Encoding.Default.GetBytes(str))
                select x.ToString("x2")
            );
            return result;
        }

        /// <summary>
        /// 将当前时间转换为时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToTimeStamp(this DateTime dateTime)
        {
            var timeSpan = dateTime.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var timeStamp = (long) timeSpan.TotalSeconds;
            return timeStamp;
        }

        /// <summary>
        /// 异步 查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> source)
        {
            if (source is IAsyncCursorSource<T> mongoQueryable)
            {
                return await mongoQueryable.ToListAsync();
            }

            return new List<T>();
        }

        /// <summary>
        /// 遍历集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// 获取集合的下标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> source, T item)
        {
            var array = source.ToArray();
            return Array.IndexOf(array, item);
        }

        /// <summary>
        /// 将时间戳转换为DateTime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long timeStamp)
        {
            var utcTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).AddSeconds(timeStamp);
            return utcTime.LocalDateTime;
        }

        /// <summary>
        /// 只替换第一次匹配的指定的字符串，
        /// </summary>
        /// <param name="str"></param>
        /// <param name="oldStr">需要替换的字符串</param>
        /// <param name="newStr">新字符</param>
        /// <returns></returns>
        public static string ReplaceOne(this string str, string oldStr, string newStr)
        {
            var index = str.IndexOf(oldStr, StringComparison.Ordinal);
            if (index >= 0)
                return str.Substring(0, index) + newStr + str.Substring(index + oldStr.Length);
            return str;
        }

        public static bool WildCardMatch(this string wildCard, string value)
        {
            var reg = "^" + Regex.Escape(wildCard).Replace("\\?", ".").Replace("\\*", ".*") + "$";
            return Regex.IsMatch(value, reg);
        }
    }
}