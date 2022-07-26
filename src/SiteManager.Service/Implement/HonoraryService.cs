using MongoDB.Driver;
using SiteManager.Entity;
using SiteManager.Service.ServiceComponents;
using SiteManager.ViewModel;

namespace SiteManager.Service.Implement;

public class HonoraryService:BasicService<Honorary>,IHonoraryService
{
    public async Task AddAsync(string image,string title)
    {
        var entity = new Honorary
        {
            Image = image,
            Title = title
        };
        await Repository.InsertAsync(entity);
    }

    public async Task UpdateAsync(VmHonorary viewModel)
    {
        var entity = await TryParseObjectId(viewModel.Id);
        if (entity != null)
        {
            await Repository.UpdateAsync(Mapper.Map<Honorary>(viewModel));
        }
    }

    public async Task DeleteAsync(string id)
    {
        await Repository.DeleteMoreAsync(id);
    }

    public async Task<VmHonorary> GetAsync(string id)
    {
        var entity = await TryParseObjectId(id);
        if (entity == null) return null;
        return Mapper.Map<VmHonorary>(entity);
    }

    public async Task<IList<VmHonorary>> GetListAsync()
    {
        var list = await Repository.SearchFor(x => true).ToListAsync();
        return Mapper.Map<List<VmHonorary>>(list);
    }
}