using SiteManager.Entity;
using SiteManager.Infrastructure;

namespace SiteManager.ViewModel;

public class VmUserInfo:IMapFrom<UserInfo>
{
    public VmUserInfo()
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