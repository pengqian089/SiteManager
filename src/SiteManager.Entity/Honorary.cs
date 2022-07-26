using SiteManager.Infrastructure;

namespace SiteManager.Entity;

public class Honorary:BaseEntity
{
    /// <summary>
    /// 荣誉图片
    /// </summary>
    public string Image { get; set; }

    public string Title { get; set; }
}