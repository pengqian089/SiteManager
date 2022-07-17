using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SiteManager.Service.ServiceComponents;
using SiteManager.ViewModel;
using SiteManager.Web.Library;
using SiteManager.Web.Models;

namespace SiteManager.Web.Controllers;

public class MainController : Controller
{
    private readonly IBannerService _bannerService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public MainController(IBannerService bannerService,
        IWebHostEnvironment webHostEnvironment)
    {
        _bannerService = bannerService;
        _webHostEnvironment = webHostEnvironment;
    }

    [Route("manage.html")]
    public async Task<IActionResult> Index()
    {
        ViewBag.Menu = MenuItems.Banner;
        ViewData["title"] = "Banner管理";
        var list = await _bannerService.GetBannersAsync();
        return View(list);
    }

    [HttpGet]
    public async Task<IActionResult> SaveBanner(string id = "")
    {
        ViewBag.Menu = MenuItems.Banner;
        ViewData["title"] = "Banner管理";
        VmBanner banner = null;
        if (!string.IsNullOrEmpty(id))
        {
            banner = await _bannerService.GetAsync(id);
        }

        return View(banner);
    }

    [HttpPost]
    public async Task<IActionResult> SaveBanner(SaveBanner banner)
    {
        var date = DateTime.Now;
        var savePath = Path.Combine(_webHostEnvironment.WebRootPath, "banner", date.ToString("yyyyMM"));
        var fileName = Guid.NewGuid().ToString("N") + banner.Image.FileName[banner.Image.FileName.LastIndexOf('.')..];
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        savePath = Path.Combine(savePath, fileName);

        if (string.IsNullOrEmpty(banner.Id))
        {
            if (banner.Image is {Length: > 0} && banner.Image.ContentType.Contains("image"))
            {
                await _bannerService.CreateBannerAsync(await banner.ToViewModelAsync(savePath));
            }
            else
            {
                TempData["Message"] = "请选择图片";
                return RedirectToAction("SaveBanner",
                    string.IsNullOrEmpty(banner.Id) ? null : new {id = banner.Id});
            }
        }

        TempData["Message"] = "请选择图片";
        return RedirectToAction("SaveBanner", string.IsNullOrEmpty(banner.Id) ? null : new {id = banner.Id});
    }
}