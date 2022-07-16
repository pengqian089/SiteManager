using Microsoft.Extensions.Configuration;

namespace SiteManager.Infrastructure
{
    public class DbOption
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Host { get; set; } = "127.0.0.1";

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 27017;

        /// <summary>
        /// 连接用户
        /// </summary>
        public string ConnUser { get; set; } = "";

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = "";

        //public string ConnectString { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Db { get; set; } = "dpz";
    }

    public static class DbTools
    {
        private static DbOption _option;

        /// <summary>
        /// 默认Db配置
        /// </summary>
        public static DbOption DefaultOption
        {
            get => _option;
            set
            {
                if (_option != null)
                {
                    throw new Exception("DefaultOption read only");
                    return;
                }
                _option = value;
            }
        }
        


        /// <summary>
        /// 从 appsettings.json 中读取配置信息
        /// </summary>
        /// <returns></returns>
        private static DbOption LoadDefaultOption()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(EnvironmentInfo.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .Build();
            var dbopt = config.GetSection("DbOption");
            return new DbOption
            {
                Host = dbopt.GetSection("Host").Value,
                ConnUser = dbopt.GetSection("ConnUser").Value,
                Db = dbopt.GetSection("Db").Value,
                Password = dbopt.GetSection("Password").Value,
                Port = Convert.ToInt32(dbopt.GetSection("Port").Value)
            };
        }
    }
}