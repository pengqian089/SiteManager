﻿using Microsoft.AspNetCore.Mvc;

namespace SiteManager.Web.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}