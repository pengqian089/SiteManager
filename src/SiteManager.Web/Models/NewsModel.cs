using System.ComponentModel.DataAnnotations;

namespace SiteManager.Web.Models;

public class NewsModel
{
    public string Id { get; set; }
    
    /// <summary>
    /// 标题
    /// </summary>
    [Required]
    public string Title { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    [Required]
    public string Introduction { get; set; }
    
    /// <summary>
    /// 内容 严格 Markdown 标准
    /// </summary>
    [Required]
    public string Content { get; set; }
}