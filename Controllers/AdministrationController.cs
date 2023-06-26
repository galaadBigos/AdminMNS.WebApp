using AdminMNS.WebApp.App_Code.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminMNS.WebApp.Controllers
{
	[Authorize(Roles = $"{Roles.Admin}, {Roles.Personnel}")]
	public class AdministrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
