using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SiteManager.Service.ServiceComponents;
using SiteManager.ViewModel;
using SiteManager.Web.Models;

namespace SiteManager.Web.Controllers;

public class NewsController : Controller
{
    private readonly IArticleService _articleService;

    public NewsController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    // GET
    public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 20)
    {
        var list = await _articleService.GetPagedListAsync(pageIndex, pageSize);
        return View(list);
    }

    [HttpGet]
    public async Task<IActionResult> SaveNews(string id = "")
    {
        VmArticle model = null;
        if (!string.IsNullOrEmpty(id))
        {
            model = await _articleService.GetAsync(id);
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SaveNews(NewsModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Select(x => x.Value?.Errors)
                .SelectMany(x => x)
                .Select(x => x.ErrorMessage)
                .Where(x => !string.IsNullOrEmpty(x))
                .ToArray();
            TempData["Message"] = string.Join("<br />", errors);
            return string.IsNullOrEmpty(model.Id)
                ? RedirectToAction("SaveNews")
                : RedirectToAction("SaveNews", "News", new {id = model.Id});
        }

        var cfg = new MapperConfigurationExpression();
        cfg.CreateMap(typeof(NewsModel), typeof(VmCreateArticle));
        cfg.CreateMap(typeof(NewsModel), typeof(VmEditArticle));
        var config = new MapperConfiguration(cfg);
        var mapper = new Mapper(config);
        if (string.IsNullOrEmpty(model.Id))
        {
            var viewModel = mapper.Map<VmCreateArticle>(model);
            await _articleService.AddAsync(viewModel);
            return RedirectToAction("Index");
        }

        var vm = mapper.Map<VmEditArticle>(model);
        await _articleService.UpdateAsync(vm);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        await _articleService.DeleteAsync(id);
        return Json(new ResultInfo(true));
    }
}