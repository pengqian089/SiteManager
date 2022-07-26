using SiteManager.Entity;
using SiteManager.Infrastructure;

namespace SiteManager.ViewModel;

public class VmHonorary:IMapFrom<Honorary>
{
    public string Id { get; set; }

    public string Image { get; set; }

    public string Title { get; set; }
}