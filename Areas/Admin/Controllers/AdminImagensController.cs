using System.Diagnostics;
using LanchesMac.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LanchesMac.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminImagensController : Controller
{

    private readonly ConfigurationImagens _myConfig;
    private readonly IWebHostEnvironment _hostEnvironment;

    public AdminImagensController(IWebHostEnvironment hostEnvironment, IOptions<ConfigurationImagens> myConfig)
    {
        _hostEnvironment = hostEnvironment;
        _myConfig = myConfig.Value;
    }
    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> UploadFiles(List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
        {
            ViewData["Error"] = "Error: Arquivo(s) não selecionado(s)";
            return View(ViewData);
        }
        if (files.Count > 10)
        {
            ViewData["Error"] = "Error: Quantidade de arquivos excedeu o limite";
            return View(ViewData);
        }
        long size = files.Sum(f => f.Length);

        var filePathsName = new List<string>();

        var filePath = Path.Combine(_hostEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);

        foreach(var formFile in files)
        {
            if (formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".gif") || formFile.FileName.Contains("png"))
            {
                var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);
                filePathsName.Add(fileNameWithPath);

                using(var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
        }

        ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidor, com tamanho de : {size} bytes";
        ViewBag.Arquivos = filePathsName;
        return View(ViewData);
    }
    public IActionResult GetImagens()
    {
        FileManagerModel model = new FileManagerModel();
        var userImagesPath = Path.Combine(_hostEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);

        DirectoryInfo dir = new DirectoryInfo(userImagesPath);

        FileInfo[] files = dir.GetFiles();

        model.PathImagesProduto = _myConfig.NomePastaImagensProdutos;

        if(files.Length == 0)
        {
            ViewData["Error"] = $"Nenum arquivo encontrado na pasta {userImagesPath}";
        }

        model.Files = files;
        return View(model);
    }
    public IActionResult Deletefile (string fname)
    {
        string _imagemDeleta = Path.Combine(_hostEnvironment.WebRootPath, 
            _myConfig.NomePastaImagensProdutos + "\\", fname);

        if((System.IO.File.Exists(_imagemDeleta)))
        {
            System.IO.File.Delete(_imagemDeleta);

            ViewData["Deletado"] = $"Arquivos(s) {_imagemDeleta} deletado com sucesso";
        }
        return View("Index");
    }
}