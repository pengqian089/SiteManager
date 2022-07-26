using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Markdig;
using Microsoft.AspNetCore.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;
using SiteManager.Entity;
using SiteManager.Pager;
using SiteManager.Service.ServiceComponents;
using SiteManager.ViewModel;

namespace SiteManager.Service.Implement;

public class ArticleService:BasicService<Article>,IArticleService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ArticleService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    
    public async Task<IPagedList<VmArticle>> GetPagedListAsync(int pageIndex = 1, int pageSize = 20)
    {
        return await Repository.SearchFor(x => true).ToPagedListAsync<Article, VmArticle>(pageIndex, pageSize);
    }

    public async Task<VmArticle> GetAsync(string id)
    {
        var entity = await TryParseObjectId(id);
        if (entity == null) return null;
        return Mapper.Map<VmArticle>(entity);
    }

    public async Task AddAsync(VmCreateArticle article)
    {
        var html = Markdown.ToHtml(article.Content);
        var htmlParse = new HtmlParser();
        var document = await htmlParse.ParseDocumentAsync(html);
        var imageElements = document.GetElementsByTagName("img");

        var entity = new Article
        {
            Content = article.Content,
            CreateTime = DateTime.Now,
            ImagesAddress = GetElementsImageUrls(imageElements),
            Introduction = article.Introduction,
            LastUpdateTime = DateTime.Now,
            Title = article.Title
        };

        await Repository.InsertAsync(entity);
    }

    private List<string> GetElementsImageUrls(IHtmlCollection<IElement> elements)
    {
        return elements.Select(x => x.Attributes["src"])
            .Where(x => x != null)
            .Select(x => x.Value)
            .Where(x => x.Any())
            .ToList();
    }

    public async Task UpdateAsync(VmEditArticle article)
    {
        if (ObjectId.TryParse(article.Id,out var id))
        {
            var entity = await Repository.FindAsync(id);
            if (entity != null)
            {
                var htmlParse = new HtmlParser();
                var document = await htmlParse.ParseDocumentAsync(article.Content);
                var imageElements = document.GetElementsByTagName("img");
                var images2 = imageElements.GetElementsImageUrls();
                
                // 文章中的图片被更新，不再使用的图片将被删除
                var imageUrls = entity.ImagesAddress == null
                    ? new List<string>()
                    : entity.ImagesAddress.Where(x => images2.All(y => y != x)).ToList();
                imageUrls.ForEach(DeletePicture);
                
                
                var update = Builders<Article>.Update
                    .Set(x => x.Title, article.Title)
                    .Set(x => x.Content, article.Content)
                    .Set(x => x.Introduction, article.Introduction)
                    .Set(x => x.ImagesAddress, images2)
                    .Set(x => x.LastUpdateTime, DateTime.Now);
                await Repository.UpdateAsync(x => x.Id == id, update);
            }
        }
    }

    private void DeletePicture(string image)
    {
        var url = image.Replace('/', Path.DirectorySeparatorChar);
        url = url.StartsWith(Path.DirectorySeparatorChar) ? url[1..] : url;
        var path = Path.Combine(_webHostEnvironment.WebRootPath, url);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await TryParseObjectId(id);
        if (entity == null) return;
        entity.ImagesAddress.ForEach(DeletePicture);
        
        await Repository.DeleteMoreAsync(id);
    }
}