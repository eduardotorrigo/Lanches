using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LanchesMac.Models;

namespace LanchesMac.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}
