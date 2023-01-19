using System.Diagnostics;
using LanchesMac.Infra.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LanchesMac.Controllers;

public class LanchesController : Controller
{
    private readonly ILancheRepository _lanchesRepository;
    
    public LanchesController(ILancheRepository LancheRepository)
    {
        _lanchesRepository = LancheRepository;
    }
    public IActionResult List()
    {
        var lanches = _lanchesRepository.Lanches;
        return View(lanches);
    }

}
