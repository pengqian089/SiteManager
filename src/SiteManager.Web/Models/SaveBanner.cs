using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SiteManager.ViewModel;

namespace SiteManager.Web.Models;

public class SaveBanner
{
    public IFormFile Image { get; set; }
    
    public string Id { get; set; }
    
    public string Path { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }

    public VmBanner ToViewModel(string savePath)
    {
        return new VmBanner
        {
            Image = savePath,
            Content = Content,
            Id = Id,
            Path = Path,
            Title = Title
        };
    }
}