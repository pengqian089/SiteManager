namespace SiteManager.ViewModel;

public class VmCreateArticle
{
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
}