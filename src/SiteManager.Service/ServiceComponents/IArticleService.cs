using SiteManager.Pager;
using SiteManager.ViewModel;

namespace SiteManager.Service.ServiceComponents;

public interface IArticleService
{
    Task<IPagedList<VmArticle>> GetPagedListAsync(int pageIndex = 1, int pageSize = 20);

    Task<VmArticle> GetAsync(string id);

    Task AddAsync(VmCreateArticle article);

    Task UpdateAsync(VmEditArticle article);

    Task DeleteAsync(string id);
}