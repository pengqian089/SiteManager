using MongoDB.Driver;
using SiteManager.Entity;
using SiteManager.Service.ServiceComponents;
using SiteManager.ViewModel;

namespace SiteManager.Service.Implement;

public class BannerService:BasicService<Banner>,IBannerService
{
    public async Task<IList<VmBanner>> GetBannersAsync()
    {
        var list = await Repository.SearchFor(x => true).ToListAsync();
        return Mapper.Map<List<VmBanner>>(list);
    }

    public async Task<VmBanner> GetAsync(string id)
    {
        var entity =await TryParseObjectId(id);
        return Mapper.Map<VmBanner>(entity);
    }

    public async Task CreateBannerAsync(VmBanner banner)
    {
        var entity = Mapper.Map<Banner>(banner);
        await Repository.InsertAsync(entity);
    }

    public async Task UpdateBannerAsync(VmBanner banner)
    {
        var entity = await TryParseObjectId(banner.Id);
        if (entity != null)
        {
            await Repository.UpdateAsync(Mapper.Map<Banner>(banner));
        }
    }

    public async Task DeleteAsync(string id)
    {
        await Repository.DeleteAsync(id);
    }
}