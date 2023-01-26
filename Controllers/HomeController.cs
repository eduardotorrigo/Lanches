using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LanchesMac.Models;
using LanchesMac.Infra.Repositories.Interface;
using LanchesMac.Models.ViewModels;
using LanchesMac.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Controllers;

public class HomeController : Controller
{
    private readonly ILancheRepository _lanchesRepository;

    public HomeController(ILancheRepository LancheRepository)
    {
        _lanchesRepository = LancheRepository;
    }
    public IActionResult Index()
    {
        var homeViewModel = new HomeViewModel
        {
            LanchesPreferidos = _lanchesRepository.LanchesPreferidos
        };

        return View(homeViewModel);
    }
}
