using System.Diagnostics;
using LanchesMac.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LanchesMac.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminRelatorioVendasController : Controller
{
    private readonly RelatorioVendasService _relatorioVendas;

    public AdminRelatorioVendasController(RelatorioVendasService relatorioVendas)
    {
        _relatorioVendas = relatorioVendas;
    }

    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> RelatorioVendasSimples(DateTime? minDate, DateTime? maxDate)
    {
        if(!minDate.HasValue)
        {
            minDate = new DateTime(DateTime.Now.Year, 1, 1);
        }
        if(!maxDate.HasValue)
        {
            maxDate = DateTime.Now;
        }
        ViewData["minDate"] = minDate.Value.ToString("dd/MM/yyyy");
        ViewData["maxDate"] = maxDate.Value.ToString("dd/MM/yyyy");

        var result = await _relatorioVendas.FindByDateAsync(minDate, maxDate);

        return View(result);
    }
}
