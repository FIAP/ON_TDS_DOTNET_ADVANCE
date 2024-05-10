using Fiap.Web.Alunos.Logging;
using Fiap.Web.Alunos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fiap.Web.Alunos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomLogger _logger;

        public HomeController(ICustomLogger logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.Log("Acessando a página inicial.");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
