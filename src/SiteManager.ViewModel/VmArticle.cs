using SiteManager.Entity;
using SiteManager.Infrastructure;

namespace SiteManager.ViewModel;

public class VmArticle:IMapFrom<Article>
{
    public string Id { get; set; }
    
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string Introduction { get; set; }
    
    /// <summary>
    /// 内容 严格 Markdown 标准
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// 文章相关图片地址
    /// </summary>
    public List<string> ImagesAddress { get; set; }

    /// <summary>
    /// 发表时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 最后修改时间
    /// </summary>
    public DateTime LastUpdateTime { get; set; }
}