using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiteManager.Web.Library;

namespace SiteManager.Web.Controllers;

public class AccountController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Login(string fromUrl = "")
    {
        if (await HttpContext.GetUserInfo() != null)
        {
            return Redirect("/");
        }

        return View(model: fromUrl);
    }
}