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

        async Task<string> GetSavePath()
        {
            var date = DateTime.Now;
            var savePath = Path.Combine(_webHostEnvironment.WebRootPath, "banner", date.ToString("yyyyMM"));
            var fileName = Guid.NewGuid().ToString("N") + banner.Image.FileName[banner.Image.FileName.LastIndexOf('.')..];
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            savePath = Path.Combine(savePath, fileName);
            var stream = banner.Image.OpenReadStream();
            await using var fileStream = new FileStream(savePath, FileMode.CreateNew, FileAccess.Write);
            await stream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            await fileStream.DisposeAsync();

            return savePath[_webHostEnvironment.WebRootPath.Length..].Replace(Path.DirectorySeparatorChar,'/');
        }
        
        // add
        if (string.IsNullOrEmpty(banner.Id))
        {
            if (banner.Image is not {Length: > 0} || !banner.Image.ContentType.Contains("image"))
            {
                TempData["Message"] = "请选择图片";
                return RedirectToAction("SaveBanner", string.IsNullOrEmpty(banner.Id) ? null : new {id = banner.Id});
            }
            
            
            await _bannerService.CreateBannerAsync(banner.ToViewModel(await GetSavePath()));
            return RedirectToAction("Index");
        }
        
        //edit
        var editBanner = await _bannerService.GetAsync(banner.Id);
        if(editBanner == null) return RedirectToAction("Index");
        var vm = banner.ToViewModel(banner.Image == null ? editBanner.Image : await GetSavePath());
        if (banner.Image != null)
        {
            DeletePicture(editBanner.Image);
        }
        await _bannerService.UpdateBannerAsync(vm);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var banner = await _bannerService.GetAsync(id);
        if(banner == null)return Json(new ResultInfo("not found"));

        DeletePicture(banner.Image);
        await _bannerService.DeleteAsync(id);
        return Json(new ResultInfo(true));
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
}