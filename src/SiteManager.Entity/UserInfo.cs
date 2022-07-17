using SiteManager.Infrastructure;

namespace SiteManager.Entity;

public class User:IBaseEntity
{
    /// <summary>
    /// 账号
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// key
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 最后修改时间
    /// </summary>
    public DateTime? LastUpdateTime { get; set; }

    public UserInfo GetUserInfo()
    {
        return new UserInfo
        {
            Id = this.Id,
            Key = this.Key
        };
    }
}

public class UserInfo
{
    public UserInfo()
    {
        LastAccessTime = DateTime.Now;
    }

    /// <summary>
    /// 账号
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 最后访问时间
    /// </summary>
    public DateTime? LastAccessTime { get; set; }
        
    public string Key { get; set; }
}