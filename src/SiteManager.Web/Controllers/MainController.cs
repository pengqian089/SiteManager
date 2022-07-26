using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteManager.Service.ServiceComponents;
using SiteManager.ViewModel;
using SiteManager.Web.Library;
using SiteManager.Web.Models;

namespace SiteManager.Web.Controllers;

public class MainController : Controller
{
    private readonly IBannerService _bannerService;
    private readonly IHonoraryService _honoraryService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public MainController(IBannerService bannerService,
        IHonoraryService honoraryService,
        IWebHostEnvironment webHostEnvironment)
    {
        _bannerService = bannerService;
        _honoraryService = honoraryService;
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

    [NonAction]
    async Task<string> GetSavePathAsync(IFormFile image, string name)
    {
        var date = DateTime.Now;
        var savePath = Path.Combine(_webHostEnvironment.WebRootPath, name, date.ToString("yyyyMM"));
        var fileName = Guid.NewGuid().ToString("N") + image.FileName[image.FileName.LastIndexOf('.')..];
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        savePath = Path.Combine(savePath, fileName);
        var stream = image.OpenReadStream();
        await using var fileStream = new FileStream(savePath, FileMode.CreateNew, FileAccess.Write);
        await stream.CopyToAsync(fileStream);
        await fileStream.FlushAsync();
        await fileStream.DisposeAsync();

        return savePath[_webHostEnvironment.WebRootPath.Length..].Replace(Path.DirectorySeparatorChar, '/');
    }


    [HttpPost]
    public async Task<IActionResult> SaveBanner(SaveBanner banner)
    {
        // add
        if (string.IsNullOrEmpty(banner.Id))
        {
            if (banner.Image is not {Length: > 0} || !banner.Image.ContentType.Contains("image"))
            {
                TempData["Message"] = "请选择图片";
                return RedirectToAction("SaveBanner", string.IsNullOrEmpty(banner.Id) ? null : new {id = banner.Id});
            }


            await _bannerService.CreateBannerAsync(banner.ToViewModel(await GetSavePathAsync(banner.Image, "banner")));
            return RedirectToAction("Index");
        }

        //edit
        var editBanner = await _bannerService.GetAsync(banner.Id);
        if (editBanner == null) return RedirectToAction("Index");
        var vm = banner.ToViewModel(banner.Image == null
            ? editBanner.Image
            : await GetSavePathAsync(banner.Image, "banner"));
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
        if (banner == null) return Json(new ResultInfo("not found"));

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

    public async Task<IActionResult> HonoraryList()
    {
        ViewBag.Menu = MenuItems.Honorary;
        var list = await _honoraryService.GetListAsync();
        return View(list);
    }

    [HttpGet]
    public async Task<IActionResult> AddHonorary(string id = "")
    {
        VmHonorary model = null;
        ViewBag.Menu = MenuItems.Honorary;
        if (!string.IsNullOrEmpty(id))
        {
            model = await _honoraryService.GetAsync(id);
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddHonorary(IFormFile image, string title, string id = null)
    {
        //add
        if (string.IsNullOrEmpty(id))
        {
            if (image is not {Length: > 0} || !image.ContentType.Contains("image"))
            {
                TempData["Message"] = "请选择图片";
                return RedirectToAction("AddHonorary");
            }

            var path = await GetSavePathAsync(image, "honorary");
            await _honoraryService.AddAsync(path, title);
            return RedirectToAction("HonoraryList");
        }

        //edit
        var editHonorary = await _honoraryService.GetAsync(id);
        if (editHonorary == null) return RedirectToAction("Index");

        if (image != null)
        {
            DeletePicture(editHonorary.Image);
            editHonorary.Image = await GetSavePathAsync(image, "honorary");
        }

        editHonorary.Title = title;
        await _honoraryService.UpdateAsync(editHonorary);
        return RedirectToAction("HonoraryList");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteHonorary(string id)
    {
        var vm = await _honoraryService.GetAsync(id);
        if (vm == null) return Json(new ResultInfo("not found"));

        DeletePicture(vm.Image);
        await _honoraryService.DeleteAsync(id);
        return Json(new ResultInfo(true));
    }
}