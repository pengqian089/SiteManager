using SiteManager.ViewModel;

namespace SiteManager.Service.ServiceComponents;

public interface IBannerService
{
    Task<IList<VmBanner>> GetBannersAsync();

    Task<VmBanner> GetAsync(string id);

    Task CreateBannerAsync(VmBanner banner);

    Task UpdateBannerAsync(VmBanner banner);

    Task DeleteAsync(string id);
}