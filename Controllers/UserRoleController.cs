using AdminMNS.WebApp.App_Code.Helpers;
using AdminMNS.WebApp.Data.Entities;
using AdminMNS.WebApp.Models.ViewModel.UserRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminMNS.WebApp.Controllers
{
	[Authorize(Roles = $"{Roles.Admin}, {Roles.Personnel}")]
	public class UserRoleController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserRoleController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
			_userManager = userManager;
			_roleManager = roleManager;
		}
        public async Task<IActionResult> Index()
		{
			List<IndexUserRoleViewModel> result = new List<IndexUserRoleViewModel>();
			List<User> users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
				//IndexUserRoleViewModel model = new IndexUserRoleViewModel(user);
            }

            return View();
		}
	}
}
