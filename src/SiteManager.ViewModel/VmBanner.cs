using SiteManager.Entity;
using SiteManager.Infrastructure;

namespace SiteManager.ViewModel;

public class VmBanner:IMapFrom<Banner>
{
    public string Id { get; set; }
    
    /// <summary>
    /// 背景图片
    /// </summary>
    public string Image { get; set; }
    
    /// <summary>
    /// 了解更多 连接
    /// </summary>
    public string Path { get; set; }
    
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }
}