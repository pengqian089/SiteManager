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

    public async Task<VmBanner> ToViewModelAsync(string savePath)
    {
        var stream = Image.OpenReadStream();
        await using var fileStream = new FileStream(savePath, FileMode.CreateNew, FileAccess.Write);
        await stream.CopyToAsync(fileStream);
        await fileStream.FlushAsync();
        await fileStream.DisposeAsync();

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