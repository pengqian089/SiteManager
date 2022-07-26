using SiteManager.ViewModel;

namespace SiteManager.Service.ServiceComponents;

public interface IHonoraryService
{
    Task AddAsync(string image,string title);

    Task UpdateAsync(VmHonorary viewModel);

    Task DeleteAsync(string id);

    Task<VmHonorary> GetAsync(string id);

    Task<IList<VmHonorary>> GetListAsync();
}