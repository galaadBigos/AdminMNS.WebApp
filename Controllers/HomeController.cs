using AdminMNS.WebApp.App_Code.Helpers;
using AdminMNS.WebApp.Models;
using AdminMNS.WebApp.Options;
using AdminMNS.WebApp.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace AdminMNS.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
		}

        public IActionResult Index(string message)
        {
            return View(model: message);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}