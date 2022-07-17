using Microsoft.AspNetCore.Mvc;
using SiteManager.Web.Library;

namespace SiteManager.Web.Controllers;

public class MainController : Controller
{
    [Route("manage.html")]
    public IActionResult Index()
    {
        ViewBag.Menu = MenuItems.Banner;
        ViewData["title"] = "Banner管理"; 
        return View();
    }
}